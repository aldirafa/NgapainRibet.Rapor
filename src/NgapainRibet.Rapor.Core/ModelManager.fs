namespace NgapainRibet.Rapor.Core

open System
open System.IO
open System.Net.Http
open System.Threading
open System.Threading.Tasks
open NgapainRibet.Rapor.Core.DomainModels

/// <summary>
/// Mengurus cache lokal & download model GGUF dari HuggingFace.
/// Sesuai spesifikasi bagian 2 & 3: app utama kecil, model di-download
/// terpisah saat pertama kali dijalankan, disimpan di cache lokal.
/// </summary>
/// <remarks>
/// CATATAN: hanya pakai System.Net.Http (tanpa SDK HuggingFace khusus)
/// supaya dependency tetap minim — sesuai prinsip "Resource Consciousness"
/// di spesifikasi.
/// </remarks>
module ModelManager =

    /// Model default — Qwen2.5-1.5B-Instruct, kuantisasi Q4_K_M (~1.1 GB),
    /// dari repo resmi Qwen di HuggingFace. Dicek tersedia per Juni 2026.
    let defaultModelFileName = "qwen2.5-1.5b-instruct-q4_k_m.gguf"

    let defaultModelUrl =
        "https://huggingface.co/Qwen/Qwen2.5-1.5B-Instruct-GGUF/resolve/main/qwen2.5-1.5b-instruct-q4_k_m.gguf"

    let defaultSha256 =
        "6a1a2eb6d15622bf3c96857206351ba97e1af16c30d7a74ee38970e434e9407e"

    /// Folder cache lokal tempat semua file .gguf disimpan.
    /// Mac/Linux: ~/.local/share/NgapainRibetRapor/models
    /// Windows:   %LOCALAPPDATA%\NgapainRibetRapor\models
    let getCacheDirectory () : string =
        let baseDir =
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)

        let dir = Path.Combine(baseDir, "NgapainRibetRapor", "models")
        Directory.CreateDirectory(dir) |> ignore
        dir

    /// Path lengkap ke file model tertentu di cache lokal (belum tentu ada).
    let getModelPath (fileName: string) : string =
        Path.Combine(getCacheDirectory (), fileName)

    /// Cek apakah file model sudah ada di cache lokal.
    let isModelCached (fileName: string) : bool = File.Exists(getModelPath fileName)

    /// Download model dari `url` ke cache lokal sebagai `fileName`, melapor
    /// progress lewat `onProgress`. Aman dipanggil berkali-kali — kalau file
    /// sudah ada, langsung melapor `Completed` tanpa download ulang.
    /// Didesain dengan callback (bukan IAsyncEnumerable/event F#) supaya
    /// gampang dikonsumsi dari VB.NET di sisi UI.
    /// note: sha256 without "-"
    let downloadModelAsync
        (url: string)
        (fileName: string)
        (onProgress: Action<DownloadState>)
        (ct: CancellationToken)
        (sha256: string)
        : Task<DownloadState> =
        task {
            let destPath = getModelPath fileName

            if File.Exists destPath then
                onProgress.Invoke Completed
                return Completed
            else
                onProgress.Invoke(Downloading 0.0)
                use client = new HttpClient()
                // Model GGUF gede bgt sampe ~1.5GB ish, jadi pakai timeout
                // yang lebih wajar
                client.Timeout <- TimeSpan.FromHours(2.0)

                try
                    use! response = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, ct)
                    response.EnsureSuccessStatusCode() |> ignore

                    let totalBytes =
                        match Option.ofNullable response.Content.Headers.ContentLength with
                        | None -> 0L
                        | Some v -> v

                    use! contentStream = response.Content.ReadAsStreamAsync ct

                    // Download ke .tmp dulu baru di-rename setelah beres
                    // utuh biar file setengah jadi ga disangka completed,
                    // andaikata proses diinterupsi di tengah jalan
                    let tempPath = $"{destPath}.tmp"

                    use fileStream =
                        new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None)

                    let buffer = Array.zeroCreate<byte> 81920
                    let mutable totalRead = 0L
                    let mutable bytesRead = 1

                    while bytesRead > 0 do
                        let! read = contentStream.ReadAsync(buffer, 0, buffer.Length, ct)
                        bytesRead <- read

                        if read > 0 then
                            do! fileStream.WriteAsync(buffer, 0, read, ct)
                            totalRead <- totalRead + int64 read

                            if totalBytes > 0L then
                                let pct = float totalRead / float totalBytes
                                onProgress.Invoke(Downloading pct)

                    fileStream.Close()
                    File.Move(tempPath, destPath)
                    onProgress.Invoke(Completed)

                    // verify SHA256 checksum
                    use sha256Alg = System.Security.Cryptography.SHA256.Create()
                    use fileStreamForHash = File.OpenRead destPath
                    let hashBytes = sha256Alg.ComputeHash fileStreamForHash

                    let hashString =
                        BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant()

                    if hashString <> sha256 then
                        onProgress.Invoke(Error "SHA256 checksum mismatch")
                        failwith "SHA256 checksum mismatch"

                    return Completed
                with
                | :? OperationCanceledException ->
                    // dicancel pengguna, bukan error sungguhan
                    return NotStarted
                | ex ->
                    let state = Error ex.Message
                    onProgress.Invoke state
                    return state

        }
