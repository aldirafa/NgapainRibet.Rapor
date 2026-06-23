Imports Microsoft.FSharp.Collections
Imports Microsoft.FSharp.Core
Imports NgapainRibet.Rapor.Core


''' <summary>
''' Form utama. Memegang lifecycle Engine LLamaSharp (load sekali, lazy,
''' dipakai ulang selama aplikasi jalan) dan jadi titik masuk alur
''' "isi data siswa → generate narasi".
''' </summary>
Public Class MainWindow

    Dim dummyStrengths = New FSharpList(Of String)("Kreatif", New FSharpList(Of String)("Inovatif", FSharpList(Of String).Empty))
    Dim dummyWeaknesses = New FSharpList(Of String)("Kurang disiplin", New FSharpList(Of String)("Terlalu santai", FSharpList(Of String).Empty))
    Private _currentStudent As New DomainModels.Student("Budi Santoso", dummyStrengths, dummyWeaknesses, "Wali kelas yang santai dan ramah, gaya teman sebaya", "Perlu perhatian lebih pada disiplin")

    ' Engine LLamaSharp di-load sekali (lazy, saat Generate pertama kali
    ' diminta) dan disimpan selama aplikasi jalan — supaya generate
    ' berikutnya tidak perlu re-load model ke RAM tiap kali. Dispose
    ' lewat MainWindow_FormClosing.
    Private _engine As AiEngine.Engine = Nothing
    Private ReadOnly _engineLoadLock As New SemaphoreSlim(1, 1)

    Public Sub New()
        InitializeComponent()

        ' Panggil fungsi F# dari VB.NET untuk verifikasi boundary interop.
        lblStatus.Text = CoreInfo.getStatusMessage()
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Dim formInputDataSiswa = New InputDataSiswa(_currentStudent)
        If formInputDataSiswa.ShowDialog() = DialogResult.OK Then
            _currentStudent = formInputDataSiswa.DataSiswa

            Dim formGenerate = New GenerateNarasi(_currentStudent, AddressOf GetOrLoadEngineAsync)
            formGenerate.ShowDialog()
        End If
    End Sub

    ''' <summary>
    ''' "Kasih saya Engine yang siap pakai." Kalau Engine sudah ter-load
    ''' sebelumnya, langsung dikembalikan (reuse, tidak reload). Kalau
    ''' belum, download model dulu (kalau belum di-cache) lalu load ke
    ''' memori — progress kedua tahap dilaporkan lewat onDownloadState/
    ''' onAiState ke caller (form GenerateNarasi yang aktif).
    ''' </summary>
    Private Async Function GetOrLoadEngineAsync(
        onDownloadState As Action(Of DomainModels.DownloadState),
        onAiState As Action(Of DomainModels.AiState),
        ct As CancellationToken
    ) As Task(Of FSharpResult(Of AiEngine.Engine, String))

        Await _engineLoadLock.WaitAsync(ct)
        Try
            If _engine IsNot Nothing Then
                Return FSharpResult(Of AiEngine.Engine, String).NewOk(_engine)
            End If

            If Not ModelManager.isModelCached(ModelManager.defaultModelFileName) Then
                Dim downloadState =
                    Await ModelManager.downloadModelAsync(
                        ModelManager.defaultModelUrl,
                        ModelManager.defaultModelFileName,
                        onDownloadState,
                        ct,
                        ModelManager.defaultSha256)

                If ct.IsCancellationRequested Then
                    Return FSharpResult(Of AiEngine.Engine, String).NewError("Dibatalkan oleh pengguna.")
                End If

                If downloadState.IsError Then
                    Dim errorState = DirectCast(downloadState, DomainModels.DownloadState.Error)
                    Return FSharpResult(Of AiEngine.Engine, String).NewError(errorState.message)
                End If
            End If

            Dim loadResult =
                Await AiEngine.loadModelAsync(
                    ModelManager.getModelPath(ModelManager.defaultModelFileName),
                    768,
                    0,
                    onAiState)

            If loadResult.IsOk Then
                _engine = loadResult.ResultValue
            End If

            Return loadResult
        Finally
            _engineLoadLock.Release()
        End Try
    End Function

    Private Sub MainWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If _engine IsNot Nothing Then
            AiEngine.disposeEngine(_engine)
            _engine = Nothing
        End If
    End Sub
End Class
