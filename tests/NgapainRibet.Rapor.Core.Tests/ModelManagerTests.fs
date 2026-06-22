module NgapainRibet.Rapor.Core.Tests.ModelManagerTests

open Xunit
open NgapainRibet.Rapor.Core
open System.IO
open System.Threading
open System

/// CATATAN: test ini sengaja di-Skip secara default.
/// Ini bukan unit test biasa — ini download SUNGGUHAN ~1.1GB dari
/// HuggingFace setiap kali dijalankan. Kalau tidak di-Skip, setiap
/// `dotnet test` (termasuk yang dijalankan otomatis/CI) akan ikut
/// menarik file besar itu, lambat dan rentan gagal karena jaringan.
///
/// Cara pakai: hapus baris `Skip = "..."` di bawah sementara waktu kalau
/// memang mau memverifikasi ulang pipeline download+checksum end-to-end,
/// lalu pasang lagi Skip-nya setelah selesai.
[<Fact(Skip = "Test integrasi manual — download ~1.1GB sungguhan dari HuggingFace. Hapus Skip ini sementara untuk verifikasi ulang.")>]
let ``Should download and verify model from HuggingFace`` () =
    task {
        let reportDownloadState (state: DomainModels.DownloadState) =
            match state with
            | DomainModels.DownloadState.NotStarted -> Console.WriteLine "Download not started."
            | DomainModels.DownloadState.Downloading progress ->
                let progressTxt = (progress * 100.0).ToString("F2")
                Console.WriteLine $"Downloading... {progressTxt}%%"
            | DomainModels.DownloadState.Completed -> Console.WriteLine "Download completed successfully."
            | DomainModels.DownloadState.Error message -> Console.WriteLine $"Download failed with error: {message}"

        let! state =
            ModelManager.downloadModelAsync
                ModelManager.defaultModelUrl
                ModelManager.defaultModelFileName
                reportDownloadState
                CancellationToken.None
                ModelManager.defaultSha256

        // Assert state is Completed, file exists, checksum valid
        Assert.Equal(DomainModels.DownloadState.Completed, state)
        Assert.True(File.Exists(ModelManager.getModelPath ModelManager.defaultModelFileName))

        // SENGAJA TIDAK menghapus file cache setelah sukses — biarkan
        // tetap ada supaya percobaan berikutnya (termasuk smoke test
        // manual AiEngine) tidak perlu download ulang ~1.1GB dari nol.
        // Hapus manual dari folder cache (lihat ModelManager.getCacheDirectory)
        // kalau memang butuh ruang disk kembali.
        Console.WriteLine $"Model tersimpan di: {ModelManager.getModelPath ModelManager.defaultModelFileName}"
    }
