Imports Microsoft.FSharp.Core
Imports NgapainRibet.Rapor.Core

''' <summary>
''' Layar "sekali generate" untuk seorang siswa: pilih mata pelajaran,
''' tulis catatan tambahan sekali pakai, lalu generate narasi lewat
''' model AI lokal. Form ini TIDAK memuat/menyimpan Engine sendiri —
''' lifecycle Engine sepenuhnya dipegang MainWindow lewat delegate
''' `engineProvider` (lihat MainWindow.GetOrLoadEngineAsync), supaya
''' model cuma di-load sekali selama aplikasi jalan.
''' </summary>
Public Class GenerateNarasi

    ''' <summary>
    ''' "Tolong siapkan Engine yang siap pakai" — argumennya adalah callback
    ''' progress download & status AI yang dipanggil MainWindow saat
    ''' download/load model berjalan, plus CancellationToken untuk batal.
    ''' Lihat MainWindow.GetOrLoadEngineAsync untuk implementasinya.
    ''' </summary>
    Public Delegate Function EngineProvider(
        onDownloadState As Action(Of DomainModels.DownloadState),
        onAiState As Action(Of DomainModels.AiState),
        ct As CancellationToken
    ) As Task(Of FSharpResult(Of AiEngine.Engine, String))

    Private ReadOnly _engineProvider As EngineProvider

    Private ReadOnly _mataPelajaranOptions As String() = {
        "Matematika", "Bahasa Indonesia", "Bahasa Inggris", "IPA", "IPS",
        "PPKn", "Seni Budaya", "PJOK", "Prakarya"
    }

    Private _cts As CancellationTokenSource

    Public ReadOnly Property DataSiswa As DomainModels.Student

    Public Sub New(student As DomainModels.Student, engineProvider As EngineProvider)
        InitializeComponent()

        DataSiswa = student
        _engineProvider = engineProvider
    End Sub

    Private Sub GenerateNarasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = $"Buat Narasi Rapor — {DataSiswa.Name}"

        ComboBox_MataPelajaran.Items.Clear()
        ComboBox_MataPelajaran.Items.AddRange(_mataPelajaranOptions)
        ComboBox_MataPelajaran.AutoCompleteCustomSource.Clear()
        ComboBox_MataPelajaran.AutoCompleteCustomSource.AddRange(_mataPelajaranOptions)

        DownloadProgressControl1.Visible = Not ModelManager.isModelCached(ModelManager.defaultModelFileName)

        DownloadProgressControl1.ApplyState(DomainModels.DownloadState.NotStarted)
        InferenceProgressControl1.ApplyState(DomainModels.AiState.Uninitialized)
    End Sub

    ''' <summary>
    ''' Helper marshal ke UI thread. Callback dari downloadModelAsync/
    ''' loadModelAsync/generateAsync TIDAK dijamin jalan di UI thread
    ''' (continuation Task bisa resume di thread pool) — setiap sentuhan
    ''' ke control WinForms dari callback ini wajib lewat sini, kalau
    ''' tidak bakal kena InvalidOperationException: Cross-thread operation.
    ''' </summary>
    Private Sub UiThread(action As Action)
        If InvokeRequired Then
            BeginInvoke(action)
        Else
            action()
        End If
    End Sub

    Private Sub HandleDownloadState(state As DomainModels.DownloadState)
        UiThread(Sub() DownloadProgressControl1.ApplyState(state))
    End Sub

    Private Sub HandleAiState(state As DomainModels.AiState)
        UiThread(Sub() InferenceProgressControl1.ApplyState(state))
    End Sub

    Private Sub HandleToken(token As String)
        UiThread(Sub() InferenceProgressControl1.AppendToken(token))
    End Sub

    Private Sub DownloadProgressControl1_CancelRequested(sender As Object, e As EventArgs) Handles DownloadProgressControl1.CancelRequested
        _cts?.Cancel()
    End Sub

    Private Sub InferenceProgressControl1_CancelRequested(sender As Object, e As EventArgs) Handles InferenceProgressControl1.CancelRequested
        _cts?.Cancel()
    End Sub

    Private Async Sub IconButton_Generate_Click(sender As Object, e As EventArgs) Handles IconButton_Generate.Click
        If String.IsNullOrWhiteSpace(ComboBox_MataPelajaran.Text) Then
            MessageBox.Show("Mata pelajaran tidak boleh kosong.", "Data Belum Lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ComboBox_MataPelajaran.Focus()
            Return
        End If

        IconButton_Generate.Enabled = False
        _cts = New CancellationTokenSource()

        Try
            Dim engineResult = Await _engineProvider(AddressOf HandleDownloadState, AddressOf HandleAiState, _cts.Token)

            If engineResult.IsError Then
                If Not _cts.IsCancellationRequested Then
                    MessageBox.Show($"Model AI gagal disiapkan: {engineResult.ErrorValue}", "Gagal Menyiapkan Model AI", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Return
            End If

            Dim engine = engineResult.ResultValue

            Dim sysPrompt = PromptBuilder.buildSystemPrompt(ComboBox_MataPelajaran.Text.Trim(), DataSiswa)
            Dim notesOpt =
                If(String.IsNullOrWhiteSpace(TextBox_CatatanTambahan.Text),
                   FSharpOption(Of String).None,
                   FSharpOption(Of String).Some(TextBox_CatatanTambahan.Text))
            Dim userPrompt = PromptBuilder.buildUserPrompt(DataSiswa, notesOpt)

            Dim genResult = Await AiEngine.generateAsync(engine, sysPrompt, userPrompt, 512, AddressOf HandleToken, AddressOf HandleAiState, _cts.Token)

            If genResult.IsError AndAlso Not _cts.IsCancellationRequested Then
                MessageBox.Show($"Narasi gagal dibuat: {genResult.ErrorValue}", "Gagal Membuat Narasi", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            IconButton_Generate.Enabled = True
            _cts?.Dispose()
            _cts = Nothing
        End Try
    End Sub

    Private Sub IconButton_Tutup_Click(sender As Object, e As EventArgs) Handles IconButton_Tutup.Click
        Me.Close()
    End Sub
End Class
