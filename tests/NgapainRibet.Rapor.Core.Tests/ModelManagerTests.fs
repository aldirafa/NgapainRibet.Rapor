module NgapainRibet.Rapor.Core.Tests.ModelManagerTests

open Xunit
open NgapainRibet.Rapor.Core
open System.IO
open System.Threading
open System

[<Fact>]
let ``Should download and verify model from HuggingFace`` () =
    task {
        let reportDownloadState (state: DomainModels.DownloadState) =
            // Download state: NotStarted, Downloading 0.5, Completed, or Error "message"
            // if Downloading, show percentage with 2 decimal place
            match state with
            | DomainModels.DownloadState.NotStarted -> Console.WriteLine "Download not started."
            | DomainModels.DownloadState.Downloading progress ->
                Console.SetCursorPosition(0, Console.CursorTop)

                if progress < 1.0 then
                    Console.Write("\rDownloading... {0}%           ", (progress * 100.0).ToString("F2"))
                else
                    Console.WriteLine "\rDownloading... 100%"
            | DomainModels.DownloadState.Completed -> Console.WriteLine "Download completed successfully."
            | DomainModels.DownloadState.Error message -> Console.WriteLine $"Download failed with error: {message}"

        let! state =
            ModelManager.downloadModelAsync
                ModelManager.defaultModelUrl
                ModelManager.defaultModelFileName
                reportDownloadState
                CancellationToken.None
                ModelManager.defaultSha256

        // Assert state is Completed, file exists
        Assert.Equal(DomainModels.DownloadState.Completed, state)
        Assert.Equal(true, File.Exists(ModelManager.getModelPath ModelManager.defaultModelFileName))

        // Delete the file after test to keep cache clean
        let modelPath = ModelManager.getModelPath ModelManager.defaultModelFileName

        if File.Exists modelPath then
            File.Delete modelPath
            Console.WriteLine $"Deleted cached model file at {modelPath}"
    }
