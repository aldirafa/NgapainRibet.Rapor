namespace NgapainRibet.Rapor.Core

open System
open System.Text
open System.Threading
open System.Threading.Tasks
open LLama
open LLama.Common


(* PENTING — baca sebelum compile:
/// Kode di modul ini ditulis berdasarkan pola yang didokumentasikan di
/// LLamaSharp Quick Start & contoh resmi (LLamaWeights.LoadFromFile(Async),
/// InteractiveExecutor, ChatSession.ChatAsync mengembalikan
/// IAsyncEnumerable&lt;string&gt;) untuk versi 0.27.0. Karena saya tidak bisa
/// compile di sandbox ini (tidak ada .NET SDK / akses NuGet), beberapa
/// detail kecil — tipe parameter persis pada `ModelParams`
/// (mis. apakah `ContextSize` itu `int` atau `uint32`) — mungkin perlu
/// disesuaikan begitu file ini pertama kali di-build di mesinmu. Compiler
/// error di titik itu seharusnya jelas menunjukkan tipe yang benar.
/// Kontrak publik di bawah ini (signature fungsi `loadModelAsync` &
/// `generateAsync`) didesain stabil terlepas dari detail itu, supaya kode
/// VB.NET yang memanggilnya tidak perlu berubah kalau pun ada penyesuaian
/// kecil di internal LLamaSharp.
*)

/// <summary>
/// Wrapper di sekitar LLamaSharp untuk load model GGUF dan menjalankan
/// inference, CPU-only sesuai spesifikasi bagian 2 (target laptop guru
/// dengan spek rendah).
/// </summary>
module AiEngine =

    /// Pegangan ke model & context yang sudah dimuat. Disposable —
    /// panggil `disposeEngine` saat aplikasi ditutup atau model diganti.
    type Engine =
        {
            /// Pegangan ke bobot model yang sudah dimuat.
            Weights: LLamaWeights
            /// Pegangan ke context model yang sudah dimuat.
            Context: LLamaContext
            /// Executor untuk menjalankan inference interaktif.
            Executor: InteractiveExecutor
        }

    /// Muat model GGUF dari `modelPath` ke memori.
    /// `contextSize` kecil (512-1024) sesuai rekomendasi spesifikasi untuk
    /// menghemat RAM. `gpuLayerCount = 0` artinya CPU-only — JANGAN ubah
    /// ini kecuali kita sudah eksplisit mendukung GPU di masa depan.
    let loadModelAsync
        (modelPath: string)
        (contextSize: int)
        (gpuLayerCount: int)
        (onState: Action<DomainModels.AiState>)
        : Task<Result<Engine, string>> =
        task {
            onState.Invoke DomainModels.LoadingModel

            try
                let parameters =
                    ModelParams(modelPath, ContextSize = uint32 contextSize, GpuLayerCount = gpuLayerCount)

                let! weights = LLamaWeights.LoadFromFileAsync(parameters)
                let context = weights.CreateContext parameters
                let executor = InteractiveExecutor context
                onState.Invoke DomainModels.Ready

                return
                    Ok
                        { Weights = weights
                          Context = context
                          Executor = executor }
            with ex ->
                onState.Invoke(DomainModels.Failed ex.Message)
                return Error ex.Message
        }

    /// Bersihkan resource native (model + context) yang dipegang LLamaSharp.
    /// WAJIB dipanggil saat selesai (mis. saat Form UI ditutup), karena ini
    /// memegang memory native (bukan cuma .NET) yang tidak otomatis
    /// dibersihkan oleh garbage collector.
    let disposeEngine (engine: Engine) : unit =
        engine.Context.Dispose()
        engine.Weights.Dispose()

    /// Jalankan satu kali inference: bangun ChatSession baru dari
    /// `systemPrompt` + `userPrompt`, lalu stream token hasil generate
    /// lewat `onToken` (supaya UI bisa menampilkan efek "sedang mengetik"),
    /// dan kembalikan keseluruhan teks setelah selesai.
    ///
    /// Sengaja pakai `Action<string>` (bukan IAsyncEnumerable/event F#)
    /// karena ini jauh lebih mudah dikonsumsi dari VB.NET — lihat instruksi
    /// "Clean Interoperability" di spesifikasi proyek.
    let generateAsync
        (engine: Engine)
        (systemPrompt: string)
        (userPrompt: string)
        (maxTokens: int)
        (onToken: Action<string>)
        (onState: Action<DomainModels.AiState>)
        (ct: CancellationToken)
        : Task<string> =
        task {
            onState.Invoke DomainModels.AiState.Generating

            try
                let chatHistory = ChatHistory()
                chatHistory.AddMessage(AuthorRole.System, systemPrompt)
                let session = ChatSession(engine.Executor, chatHistory)

                let inferenceParams =
                    InferenceParams(MaxTokens = maxTokens, AntiPrompts = ResizeArray<string> [ "User:" ])

                let message = ChatHistory.Message(AuthorRole.User, userPrompt)
                let stream = session.ChatAsync(message, inferenceParams)
                let enumerator = stream.GetAsyncEnumerator(ct)
                let sb = StringBuilder()

                let mutable keepGoing = true

                while keepGoing do
                    let! moved = enumerator.MoveNextAsync()

                    if moved then
                        let token: string = enumerator.Current
                        sb.Append token |> ignore
                        onToken.Invoke token
                    else
                        keepGoing <- false

                onState.Invoke DomainModels.AiState.Ready
                return sb.ToString()

            with ex ->
                onState.Invoke(DomainModels.AiState.Failed ex.Message)
                return sprintf "Error: %s" ex.Message
        }
