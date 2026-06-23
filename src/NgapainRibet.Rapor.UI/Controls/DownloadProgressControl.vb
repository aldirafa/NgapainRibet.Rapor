Imports NgapainRibet.Rapor.Core

''' <summary>
''' Render pasif untuk DomainModels.DownloadState — control ini tidak
''' pernah memunculkan dialog sendiri, cuma menampilkan progress &
''' meneruskan permintaan batal lewat event. Host form yang putuskan
''' kapan munculkan MessageBox error.
''' </summary>
Public Class DownloadProgressControl

    Public Event CancelRequested(sender As Object, e As EventArgs)

    Public Sub New()
        InitializeComponent()
        ApplyState(DomainModels.DownloadState.NotStarted)
    End Sub

    ''' <summary>
    ''' Catatan interop F# DU dari VB.NET: DomainModels.DownloadState adalah
    ''' discriminated union F#, dikonsumsi lewat properti auto-generated
    ''' IsNotStarted/IsDownloading/IsCompleted/IsError (VB tidak punya
    ''' pattern matching native untuk DU). Field bernama pada case
    ''' (`progress`, `message`) cuma ada di tipe nested case-nya (mis.
    ''' DownloadState.Downloading), bukan di base type — jadi perlu
    ''' DirectCast ke tipe case-nya dulu sebelum baca field-nya.
    ''' </summary>
    Public Sub ApplyState(state As DomainModels.DownloadState)
        If state.IsNotStarted Then
            ProgressBar_Download.Value = 0
            Label_Status.Text = "Belum mulai mengunduh."
            IconButton_Batal.Visible = False
        ElseIf state.IsDownloading Then
            Dim downloading = DirectCast(state, DomainModels.DownloadState.Downloading)
            Dim pct = CInt(Math.Min(100.0, Math.Max(0.0, downloading.progress * 100.0)))
            ProgressBar_Download.Value = pct
            Label_Status.Text = $"Mengunduh model AI... {pct}%"
            IconButton_Batal.Visible = True
        ElseIf state.IsCompleted Then
            ProgressBar_Download.Value = 100
            Label_Status.Text = "Model siap digunakan."
            IconButton_Batal.Visible = False
        ElseIf state.IsError Then
            Dim errorState = DirectCast(state, DomainModels.DownloadState.Error)
            Label_Status.Text = $"Gagal mengunduh: {errorState.message}"
            IconButton_Batal.Visible = False
        End If
    End Sub

    Private Sub IconButton_Batal_Click(sender As Object, e As EventArgs) Handles IconButton_Batal.Click
        RaiseEvent CancelRequested(Me, EventArgs.Empty)
    End Sub
End Class
