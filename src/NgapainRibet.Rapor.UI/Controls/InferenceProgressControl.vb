Imports NgapainRibet.Rapor.Core

''' <summary>
''' Render pasif untuk DomainModels.AiState + streaming token hasil
''' generate ke RichTextBox. Tidak pernah memunculkan dialog sendiri —
''' host form yang putuskan kapan munculkan MessageBox error.
''' </summary>
Public Class InferenceProgressControl

    Public Event CancelRequested(sender As Object, e As EventArgs)

    Public Sub New()
        InitializeComponent()
        ApplyState(DomainModels.AiState.Uninitialized)
    End Sub

    ''' <summary>Bersihkan hasil sebelum generate baru dimulai.</summary>
    Public Sub ResetResult()
        RichTextBox_Hasil.Clear()
        IconButton_CopyClipboard.Enabled = False
    End Sub

    ''' <summary>
    ''' Sama seperti DownloadProgressControl.ApplyState: konsumsi DU F#
    ''' lewat properti Is* auto-generated, bukan pattern matching. Field
    ''' `message` di case Failed cuma ada di tipe nested-nya, jadi perlu
    ''' DirectCast dulu.
    ''' </summary>
    Public Sub ApplyState(state As DomainModels.AiState)
        If state.IsUninitialized Then
            Label_Status.Text = "Model AI belum dimuat."
            ProgressBar_Inference.Visible = False
            IconButton_Batal.Visible = False
        ElseIf state.IsLoadingModel Then
            Label_Status.Text = "Memuat model AI ke memori..."
            ProgressBar_Inference.Visible = True
            IconButton_Batal.Visible = False
        ElseIf state.IsReady Then
            Label_Status.Text = "Siap."
            ProgressBar_Inference.Visible = False
            IconButton_Batal.Visible = False
        ElseIf state.IsGenerating Then
            Label_Status.Text = "Menyusun narasi..."
            ProgressBar_Inference.Visible = True
            IconButton_Batal.Visible = True
            ResetResult()
        ElseIf state.IsFailed Then
            Dim failedState = DirectCast(state, DomainModels.AiState.Failed)
            Label_Status.Text = $"Gagal: {failedState.message}"
            ProgressBar_Inference.Visible = False
            IconButton_Batal.Visible = False
        End If
    End Sub

    ''' <summary>Target dari callback onToken — dipanggil per token streaming.</summary>
    Public Sub AppendToken(token As String)
        RichTextBox_Hasil.AppendText(token)
        IconButton_CopyClipboard.Enabled = Not String.IsNullOrEmpty(RichTextBox_Hasil.Text)
    End Sub

    Private Sub IconButton_CopyClipboard_Click(sender As Object, e As EventArgs) Handles IconButton_CopyClipboard.Click
        If Not String.IsNullOrEmpty(RichTextBox_Hasil.Text) Then
            Clipboard.SetText(RichTextBox_Hasil.Text)
        End If
    End Sub

    Private Sub IconButton_Batal_Click(sender As Object, e As EventArgs) Handles IconButton_Batal.Click
        RaiseEvent CancelRequested(Me, EventArgs.Empty)
    End Sub
End Class
