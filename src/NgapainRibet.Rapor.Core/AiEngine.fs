namespace NgapainRibet.Rapor.Core

open System
open System.Text
open System.Threading
open System.Threading.Tasks
open LLama
open LLama.Common


(* PENTING — baca sebelum compile:
Kode di modul ini ditulis berdasarkan pola yang didokumentasikan di
LLamaSharp Quick Start & contoh resmi (LLamaWeights.LoadFromFile(Async),
InteractiveExecutor, ChatSession.ChatAsync mengembalikan
IAsyncEnumerable&lt;string&gt;) untuk versi 0.27.0. Karena saya tidak bisa
compile di sandbox ini (tidak ada .NET SDK / akses NuGet), beberapa
detail kecil — tipe parameter persis pada `ModelParams`
(mis. apakah `ContextSize` itu `int` atau `uint32`) — mungkin perlu
disesuaikan begitu file ini pertama kali di-build di mesinmu. Compiler
error di titik itu seharusnya jelas menunjukkan tipe yang benar.
Kontrak publik di bawah ini (signature fungsi `loadModelAsync` &
`generateAsync`) didesain stabil terlepas dari detail itu, supaya kode
VB.NET yang memanggilnya tidak perlu berubah kalau pun ada penyesuaian
kecil di internal LLamaSharp.
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

    /// <summary>
    /// Muat model GGUF dari `modelPath` ke memori secara asynchronous.
    /// </summary>
    /// <param name="modelPath">Path lengkap ke file model GGUF di cache lokal.</param>
    /// <param name="contextSize">Ukuran context untuk inference (misal: 512-1024), sesuai rekomendasi spesifikasi untuk menghemat RAM.</param>
    /// <param name="gpuLayerCount">Jumlah layer yang dipetakan ke GPU. Set ke 0 untuk CPU-only sesuai spesifikasi.</param>
    /// <param name="onState">Callback untuk melaporkan status loading model (LoadingModel, Ready, Failed).</param>
    /// <returns>Task yang menghasilkan Result berisi Engine jika berhasil, atau pesan error jika gagal.</returns>
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

    /// <summary>
    /// Bersihkan resource native (model + context) yang dipegang LLamaSharp.
    /// </summary>
    /// <param name="engine">Instance Engine yang ingin dibersihkan.</param>
    /// <remarks>
    /// WAJIB dipanggil saat selesai (mis. saat Form UI ditutup), karena ini
    /// memegang memory native (bukan cuma .NET) yang tidak otomatis
    /// dibersihkan oleh garbage collector.
    /// </remarks>
    let disposeEngine (engine: Engine) : unit =
        engine.Context.Dispose()
        engine.Weights.Dispose()

    /// <summary>
    /// Jalankan satu kali inference: bangun ChatSession baru dari
    /// `systemPrompt` + `userPrompt`, lalu stream token hasil generate
    /// lewat `onToken` (supaya UI bisa menampilkan efek "sedang mengetik"),
    /// dan kembalikan keseluruhan teks setelah selesai.
    /// </summary>
    /// <param name="engine">Instance Engine yang sudah dimuat.</param>
    /// <param name="systemPrompt">Instruksi sistem yang mendetail untuk mengarahkan AI menghasilkan narasi rapor sesuai kebutuhan (misal: menyertakan nama mata pelajaran, menyebutkan kelebihan/kekurangan siswa, memakai nada tertentu, dst).</param>
    /// <param name="userPrompt">Prompt singkat yang berisi data spesifik siswa (misal: nama siswa, kelebihan/kekurangan utama, dst). Instruksi detail sebaiknya dimasukkan di `systemPrompt` supaya `userPrompt` tetap pendek dan to the point, sesuai prinsip "Prompt Simplicity" di spesifikasi.</param>
    /// <param name="maxTokens">Batas maksimal token yang dihasilkan untuk satu narasi rapor. Sesuaikan dengan estimasi panjang narasi yang diinginkan, dan pastikan cukup untuk menghindari truncation yang tidak diinginkan.</param>
    /// <param name="onToken">Callback untuk setiap token yang dihasilkan secara streaming, supaya UI bisa menampilkan narasi secara bertahap (efek "sedang mengetik"). Token yang diterima di sini mungkin belum membentuk kata lengkap, jadi pastikan UI menampilkannya dengan cara yang sesuai (misal: menambahkan ke buffer sementara sampai spasi muncul, dst).</param>
    /// <param name="onState">Callback untuk melaporkan status inference (Generating, Ready, Failed), supaya UI bisa menampilkan indikator yang sesuai (loading spinner, tombol disabled, dst).</param>
    /// <param name="ct">CancellationToken untuk membatalkan proses inference jika diperlukan (misal: user menekan tombol "Batal"). Pastikan untuk menangani pembatalan ini dengan benar di dalam fungsi, supaya tidak ada resource yang bocor atau state yang tidak konsisten.</param>
    /// <returns>Task yang menghasilkan Ok dengan narasi rapor lengkap jika berhasil, atau Error berisi pesan kegagalan.</returns>
    /// <remarks>
    /// Sengaja pakai <see cref="Action{T}"/> (bukan `IAsyncEnumerable`/event F#) karena ini jauh lebih mudah dikonsumsi dari VB.NET — lihat instruksi "Clean Interoperability" di spesifikasi proyek.
    /// Hasil dibungkus <c>Result</c> (bukan string polos) supaya pesan error tidak pernah bisa disalahartikan sebagai narasi yang sungguhan dihasilkan AI — konsisten dengan pola di <see cref="loadModelAsync"/>.
    /// Pastikan untuk menangani status `Failed` dengan benar di UI, karena ini bisa terjadi jika model gagal dimuat, terjadi error saat inference, atau jika proses dibatalkan lewat <paramref name="ct"/>.
    /// </remarks>
    let generateAsync
        (engine: Engine)
        (systemPrompt: string)
        (userPrompt: string)
        (maxTokens: int)
        (onToken: Action<string>)
        (onState: Action<DomainModels.AiState>)
        (ct: CancellationToken)
        : Task<Result<string, string>> =
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
                return Ok(sb.ToString())

            with ex ->
                onState.Invoke(DomainModels.AiState.Failed ex.Message)
                return Error ex.Message
        }
