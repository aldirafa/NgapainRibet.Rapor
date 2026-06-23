<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GenerateNarasi
    Inherits System.Windows.Forms.Form

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
        Dim Label1 As Label
        Dim Label2 As Label
        Dim FlowLayoutPanel1 As FlowLayoutPanel
        ComboBox_MataPelajaran = New ComboBox()
        TextBox_CatatanTambahan = New TextBox()
        DownloadProgressControl1 = New DownloadProgressControl()
        InferenceProgressControl1 = New InferenceProgressControl()
        IconButton_Generate = New FontAwesome.Sharp.IconButton()
        IconButton_Tutup = New FontAwesome.Sharp.IconButton()
        TableLayoutPanel1 = New TableLayoutPanel()
        Label1 = New Label()
        Label2 = New Label()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        TableLayoutPanel1.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        SuspendLayout()
        '
        ' TableLayoutPanel1
        '
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        TableLayoutPanel1.Controls.Add(Label1, 0, 0)
        TableLayoutPanel1.Controls.Add(ComboBox_MataPelajaran, 1, 0)
        TableLayoutPanel1.Controls.Add(Label2, 0, 1)
        TableLayoutPanel1.Controls.Add(TextBox_CatatanTambahan, 1, 1)
        TableLayoutPanel1.Controls.Add(DownloadProgressControl1, 0, 2)
        TableLayoutPanel1.Controls.Add(InferenceProgressControl1, 0, 3)
        TableLayoutPanel1.Controls.Add(FlowLayoutPanel1, 0, 4)
        TableLayoutPanel1.SetColumnSpan(DownloadProgressControl1, 2)
        TableLayoutPanel1.SetColumnSpan(InferenceProgressControl1, 2)
        TableLayoutPanel1.SetColumnSpan(FlowLayoutPanel1, 2)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 5
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Size = New System.Drawing.Size(560, 520)
        TableLayoutPanel1.TabIndex = 0
        '
        ' Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(10, 10)
        Label1.Margin = New Padding(10)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(96, 15)
        Label1.TabIndex = 0
        Label1.Text = "Mata Pelajaran:"
        '
        ' ComboBox_MataPelajaran
        '
        ComboBox_MataPelajaran.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBox_MataPelajaran.AutoCompleteSource = AutoCompleteSource.CustomSource
        ComboBox_MataPelajaran.Dock = DockStyle.Fill
        ComboBox_MataPelajaran.FormattingEnabled = True
        ComboBox_MataPelajaran.Location = New System.Drawing.Point(141, 5)
        ComboBox_MataPelajaran.Margin = New Padding(5)
        ComboBox_MataPelajaran.Name = "ComboBox_MataPelajaran"
        ComboBox_MataPelajaran.Size = New System.Drawing.Size(414, 23)
        ComboBox_MataPelajaran.Sorted = True
        ComboBox_MataPelajaran.TabIndex = 1
        '
        ' Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(10, 40)
        Label2.Margin = New Padding(10)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(180, 15)
        Label2.TabIndex = 2
        Label2.Text = "Catatan tambahan (sekali pakai):"
        '
        ' TextBox_CatatanTambahan
        '
        TextBox_CatatanTambahan.Dock = DockStyle.Fill
        TextBox_CatatanTambahan.Location = New System.Drawing.Point(141, 38)
        TextBox_CatatanTambahan.Margin = New Padding(5)
        TextBox_CatatanTambahan.Multiline = True
        TextBox_CatatanTambahan.Name = "TextBox_CatatanTambahan"
        TextBox_CatatanTambahan.Size = New System.Drawing.Size(414, 60)
        TextBox_CatatanTambahan.TabIndex = 3
        '
        ' DownloadProgressControl1
        '
        DownloadProgressControl1.Dock = DockStyle.Fill
        DownloadProgressControl1.Location = New System.Drawing.Point(10, 113)
        DownloadProgressControl1.Margin = New Padding(10, 5, 10, 5)
        DownloadProgressControl1.Name = "DownloadProgressControl1"
        DownloadProgressControl1.Size = New System.Drawing.Size(540, 80)
        DownloadProgressControl1.TabIndex = 4
        '
        ' InferenceProgressControl1
        '
        InferenceProgressControl1.Dock = DockStyle.Fill
        InferenceProgressControl1.Location = New System.Drawing.Point(10, 203)
        InferenceProgressControl1.Margin = New Padding(10, 5, 10, 5)
        InferenceProgressControl1.Name = "InferenceProgressControl1"
        InferenceProgressControl1.Size = New System.Drawing.Size(540, 260)
        InferenceProgressControl1.TabIndex = 5
        '
        ' FlowLayoutPanel1
        '
        FlowLayoutPanel1.Controls.Add(IconButton_Tutup)
        FlowLayoutPanel1.Controls.Add(IconButton_Generate)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft
        FlowLayoutPanel1.Location = New System.Drawing.Point(3, 473)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New System.Drawing.Size(554, 44)
        FlowLayoutPanel1.TabIndex = 6
        '
        ' IconButton_Tutup
        '
        IconButton_Tutup.IconChar = FontAwesome.Sharp.IconChar.Xmark
        IconButton_Tutup.IconColor = Drawing.Color.Black
        IconButton_Tutup.IconFont = FontAwesome.Sharp.IconFont.Solid
        IconButton_Tutup.IconSize = 16
        IconButton_Tutup.Location = New System.Drawing.Point(474, 3)
        IconButton_Tutup.Name = "IconButton_Tutup"
        IconButton_Tutup.Size = New System.Drawing.Size(77, 32)
        IconButton_Tutup.TabIndex = 1
        IconButton_Tutup.Text = "&Tutup"
        IconButton_Tutup.TextImageRelation = TextImageRelation.ImageBeforeText
        IconButton_Tutup.UseVisualStyleBackColor = True
        '
        ' IconButton_Generate
        '
        IconButton_Generate.IconChar = FontAwesome.Sharp.IconChar.PaperPlane
        IconButton_Generate.IconColor = Drawing.SystemColors.ControlText
        IconButton_Generate.IconFont = FontAwesome.Sharp.IconFont.Solid
        IconButton_Generate.IconSize = 16
        IconButton_Generate.Location = New System.Drawing.Point(364, 3)
        IconButton_Generate.Name = "IconButton_Generate"
        IconButton_Generate.Size = New System.Drawing.Size(104, 32)
        IconButton_Generate.TabIndex = 0
        IconButton_Generate.Text = "&Generate"
        IconButton_Generate.TextImageRelation = TextImageRelation.ImageBeforeText
        IconButton_Generate.UseVisualStyleBackColor = True
        '
        ' GenerateNarasi
        '
        AcceptButton = IconButton_Generate
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = IconButton_Tutup
        ClientSize = New System.Drawing.Size(560, 520)
        Controls.Add(TableLayoutPanel1)
        Name = "GenerateNarasi"
        Text = "Buat Narasi Rapor"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        FlowLayoutPanel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ComboBox_MataPelajaran As ComboBox
    Friend WithEvents TextBox_CatatanTambahan As TextBox
    Friend WithEvents DownloadProgressControl1 As DownloadProgressControl
    Friend WithEvents InferenceProgressControl1 As InferenceProgressControl
    Friend WithEvents IconButton_Generate As FontAwesome.Sharp.IconButton
    Friend WithEvents IconButton_Tutup As FontAwesome.Sharp.IconButton
End Class
