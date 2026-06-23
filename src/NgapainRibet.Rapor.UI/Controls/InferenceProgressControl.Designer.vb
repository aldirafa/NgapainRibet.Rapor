<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InferenceProgressControl
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim TableLayoutPanel1 As TableLayoutPanel
        Dim FlowLayoutPanel1 As FlowLayoutPanel
        Label_Status = New Label()
        ProgressBar_Inference = New ProgressBar()
        RichTextBox_Hasil = New RichTextBox()
        IconButton_CopyClipboard = New FontAwesome.Sharp.IconButton()
        IconButton_Batal = New FontAwesome.Sharp.IconButton()
        TableLayoutPanel1 = New TableLayoutPanel()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        TableLayoutPanel1.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        SuspendLayout()
        '
        ' TableLayoutPanel1
        '
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        TableLayoutPanel1.Controls.Add(Label_Status, 0, 0)
        TableLayoutPanel1.Controls.Add(ProgressBar_Inference, 0, 1)
        TableLayoutPanel1.Controls.Add(RichTextBox_Hasil, 0, 2)
        TableLayoutPanel1.Controls.Add(FlowLayoutPanel1, 0, 3)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 4
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.Size = New System.Drawing.Size(420, 260)
        TableLayoutPanel1.TabIndex = 0
        '
        ' Label_Status
        '
        Label_Status.AutoSize = True
        Label_Status.Location = New System.Drawing.Point(3, 0)
        Label_Status.Margin = New Padding(3, 0, 3, 0)
        Label_Status.Name = "Label_Status"
        Label_Status.Size = New System.Drawing.Size(120, 15)
        Label_Status.TabIndex = 0
        Label_Status.Text = "Model AI belum dimuat."
        '
        ' ProgressBar_Inference
        '
        ProgressBar_Inference.Dock = DockStyle.Fill
        ProgressBar_Inference.Location = New System.Drawing.Point(3, 18)
        ProgressBar_Inference.Name = "ProgressBar_Inference"
        ProgressBar_Inference.Size = New System.Drawing.Size(414, 18)
        ProgressBar_Inference.Style = ProgressBarStyle.Marquee
        ProgressBar_Inference.TabIndex = 1
        ProgressBar_Inference.Visible = False
        '
        ' RichTextBox_Hasil
        '
        RichTextBox_Hasil.Dock = DockStyle.Fill
        RichTextBox_Hasil.Location = New System.Drawing.Point(3, 42)
        RichTextBox_Hasil.Name = "RichTextBox_Hasil"
        RichTextBox_Hasil.ReadOnly = True
        RichTextBox_Hasil.Size = New System.Drawing.Size(414, 180)
        RichTextBox_Hasil.TabIndex = 2
        RichTextBox_Hasil.Text = ""
        '
        ' FlowLayoutPanel1
        '
        FlowLayoutPanel1.Controls.Add(IconButton_Batal)
        FlowLayoutPanel1.Controls.Add(IconButton_CopyClipboard)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft
        FlowLayoutPanel1.Location = New System.Drawing.Point(3, 228)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New System.Drawing.Size(414, 30)
        FlowLayoutPanel1.TabIndex = 3
        '
        ' IconButton_CopyClipboard
        '
        IconButton_CopyClipboard.Enabled = False
        IconButton_CopyClipboard.IconChar = FontAwesome.Sharp.IconChar.Copy
        IconButton_CopyClipboard.IconColor = Drawing.SystemColors.ControlText
        IconButton_CopyClipboard.IconFont = FontAwesome.Sharp.IconFont.Solid
        IconButton_CopyClipboard.IconSize = 16
        IconButton_CopyClipboard.Location = New System.Drawing.Point(304, 3)
        IconButton_CopyClipboard.Name = "IconButton_CopyClipboard"
        IconButton_CopyClipboard.Size = New System.Drawing.Size(107, 27)
        IconButton_CopyClipboard.TabIndex = 0
        IconButton_CopyClipboard.Text = "&Copy"
        IconButton_CopyClipboard.TextImageRelation = TextImageRelation.ImageBeforeText
        IconButton_CopyClipboard.UseVisualStyleBackColor = True
        '
        ' IconButton_Batal
        '
        IconButton_Batal.IconChar = FontAwesome.Sharp.IconChar.Ban
        IconButton_Batal.IconColor = Drawing.Color.Firebrick
        IconButton_Batal.IconFont = FontAwesome.Sharp.IconFont.Solid
        IconButton_Batal.IconSize = 16
        IconButton_Batal.Location = New System.Drawing.Point(417, 3)
        IconButton_Batal.Margin = New Padding(3, 3, 0, 3)
        IconButton_Batal.Name = "IconButton_Batal"
        IconButton_Batal.Size = New System.Drawing.Size(80, 27)
        IconButton_Batal.TabIndex = 1
        IconButton_Batal.Text = "&Batal"
        IconButton_Batal.TextImageRelation = TextImageRelation.ImageBeforeText
        IconButton_Batal.UseVisualStyleBackColor = True
        IconButton_Batal.Visible = False
        '
        ' InferenceProgressControl
        '
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(TableLayoutPanel1)
        Name = "InferenceProgressControl"
        Size = New System.Drawing.Size(420, 260)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        FlowLayoutPanel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label_Status As Label
    Friend WithEvents ProgressBar_Inference As ProgressBar
    Friend WithEvents RichTextBox_Hasil As RichTextBox
    Friend WithEvents IconButton_CopyClipboard As FontAwesome.Sharp.IconButton
    Friend WithEvents IconButton_Batal As FontAwesome.Sharp.IconButton
End Class
