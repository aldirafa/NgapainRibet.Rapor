<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InputDataSiswa
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim TableLayoutPanel1 As TableLayoutPanel
        Dim Label1 As Label
        Dim Label3 As Label
        Dim Label2 As Label
        Dim Label4 As Label
        Dim FlowLayoutPanel1 As FlowLayoutPanel
        TextBox_NamaPesertaDidik = New TextBox()
        ComboBox_GayaTeksLaporan = New ComboBox()
        StrengthsControl = New EditableStringListControl()
        WeaknessesControl = New EditableStringListControl()
        TextBox_CatatanGuru = New TextBox()
        Button_Cancel = New FontAwesome.Sharp.IconButton()
        Button_Simpan = New FontAwesome.Sharp.IconButton()
        TableLayoutPanel1 = New TableLayoutPanel()
        Label1 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        Label4 = New Label()
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
        TableLayoutPanel1.Controls.Add(Label1, 0, 1)
        TableLayoutPanel1.Controls.Add(TextBox_NamaPesertaDidik, 1, 1)
        TableLayoutPanel1.Controls.Add(Label3, 0, 2)
        TableLayoutPanel1.Controls.Add(ComboBox_GayaTeksLaporan, 1, 2)
        TableLayoutPanel1.Controls.Add(Label2, 0, 0)
        TableLayoutPanel1.Controls.Add(StrengthsControl, 0, 3)
        TableLayoutPanel1.Controls.Add(WeaknessesControl, 0, 4)
        TableLayoutPanel1.Controls.Add(Label4, 0, 5)
        TableLayoutPanel1.Controls.Add(TextBox_CatatanGuru, 1, 5)
        TableLayoutPanel1.Controls.Add(FlowLayoutPanel1, 1, 6)
        TableLayoutPanel1.SetColumnSpan(StrengthsControl, 2)
        TableLayoutPanel1.SetColumnSpan(WeaknessesControl, 2)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 7
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Size = New System.Drawing.Size(533, 540)
        TableLayoutPanel1.TabIndex = 0
        '
        ' Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(10, 45)
        Label1.Margin = New Padding(10)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(113, 15)
        Label1.TabIndex = 0
        Label1.Text = "Nama Peserta Didik:"
        '
        ' TextBox_NamaPesertaDidik
        '
        TextBox_NamaPesertaDidik.Dock = DockStyle.Fill
        TextBox_NamaPesertaDidik.Location = New System.Drawing.Point(138, 40)
        TextBox_NamaPesertaDidik.Margin = New Padding(5)
        TextBox_NamaPesertaDidik.Name = "TextBox_NamaPesertaDidik"
        TextBox_NamaPesertaDidik.Size = New System.Drawing.Size(390, 23)
        TextBox_NamaPesertaDidik.TabIndex = 2
        '
        ' Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(10, 80)
        Label3.Margin = New Padding(10)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(103, 15)
        Label3.TabIndex = 3
        Label3.Text = "Gaya teks laporan:"
        '
        ' ComboBox_GayaTeksLaporan
        '
        ComboBox_GayaTeksLaporan.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBox_GayaTeksLaporan.AutoCompleteSource = AutoCompleteSource.CustomSource
        ComboBox_GayaTeksLaporan.Dock = DockStyle.Fill
        ComboBox_GayaTeksLaporan.FormattingEnabled = True
        ComboBox_GayaTeksLaporan.Location = New System.Drawing.Point(138, 75)
        ComboBox_GayaTeksLaporan.Margin = New Padding(5)
        ComboBox_GayaTeksLaporan.Name = "ComboBox_GayaTeksLaporan"
        ComboBox_GayaTeksLaporan.Size = New System.Drawing.Size(390, 23)
        ComboBox_GayaTeksLaporan.Sorted = True
        ComboBox_GayaTeksLaporan.TabIndex = 4
        '
        ' Label2
        '
        Label2.AutoSize = True
        TableLayoutPanel1.SetColumnSpan(Label2, 2)
        Label2.Location = New System.Drawing.Point(10, 10)
        Label2.Margin = New Padding(10)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(410, 15)
        Label2.TabIndex = 1
        Label2.Text = "Gunakan formulir ini untuk memasukkan atau mengubah data peserta didik."
        '
        ' StrengthsControl
        '
        StrengthsControl.Caption = "Capaian Tertinggi (Strengths):"
        StrengthsControl.Dock = DockStyle.Fill
        StrengthsControl.Location = New System.Drawing.Point(10, 115)
        StrengthsControl.Margin = New Padding(10, 5, 10, 5)
        StrengthsControl.Name = "StrengthsControl"
        StrengthsControl.Size = New System.Drawing.Size(513, 190)
        StrengthsControl.TabIndex = 5
        '
        ' WeaknessesControl
        '
        WeaknessesControl.Caption = "Capaian Terendah (Weaknesses):"
        WeaknessesControl.Dock = DockStyle.Fill
        WeaknessesControl.Location = New System.Drawing.Point(10, 315)
        WeaknessesControl.Margin = New Padding(10, 5, 10, 5)
        WeaknessesControl.Name = "WeaknessesControl"
        WeaknessesControl.Size = New System.Drawing.Size(513, 190)
        WeaknessesControl.TabIndex = 6
        '
        ' Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(10, 520)
        Label4.Margin = New Padding(10)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(118, 15)
        Label4.TabIndex = 7
        Label4.Text = "Catatan untuk siswa ini:"
        '
        ' TextBox_CatatanGuru
        '
        TextBox_CatatanGuru.Dock = DockStyle.Fill
        TextBox_CatatanGuru.Location = New System.Drawing.Point(138, 515)
        TextBox_CatatanGuru.Margin = New Padding(5)
        TextBox_CatatanGuru.Multiline = True
        TextBox_CatatanGuru.Name = "TextBox_CatatanGuru"
        TextBox_CatatanGuru.Size = New System.Drawing.Size(390, 60)
        TextBox_CatatanGuru.TabIndex = 8
        '
        ' FlowLayoutPanel1
        '
        TableLayoutPanel1.SetColumnSpan(FlowLayoutPanel1, 2)
        FlowLayoutPanel1.Controls.Add(Button_Cancel)
        FlowLayoutPanel1.Controls.Add(Button_Simpan)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft
        FlowLayoutPanel1.Location = New System.Drawing.Point(3, 583)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New System.Drawing.Size(527, 43)
        FlowLayoutPanel1.TabIndex = 9
        '
        ' Button_Cancel
        '
        Button_Cancel.IconChar = FontAwesome.Sharp.IconChar.Cancel
        Button_Cancel.IconColor = Drawing.Color.Black
        Button_Cancel.IconFont = FontAwesome.Sharp.IconFont.Solid
        Button_Cancel.IconSize = 16
        Button_Cancel.Location = New System.Drawing.Point(447, 3)
        Button_Cancel.Name = "Button_Cancel"
        Button_Cancel.Size = New System.Drawing.Size(77, 32)
        Button_Cancel.TabIndex = 1
        Button_Cancel.Text = "&Batal"
        Button_Cancel.TextImageRelation = TextImageRelation.ImageBeforeText
        Button_Cancel.UseVisualStyleBackColor = True
        '
        ' Button_Simpan
        '
        Button_Simpan.IconChar = FontAwesome.Sharp.IconChar.Save
        Button_Simpan.IconColor = Drawing.SystemColors.ControlText
        Button_Simpan.IconFont = FontAwesome.Sharp.IconFont.Solid
        Button_Simpan.IconSize = 16
        Button_Simpan.Location = New System.Drawing.Point(364, 3)
        Button_Simpan.Name = "Button_Simpan"
        Button_Simpan.Size = New System.Drawing.Size(77, 32)
        Button_Simpan.TabIndex = 0
        Button_Simpan.Text = "&Simpan"
        Button_Simpan.TextAlign = Drawing.ContentAlignment.MiddleRight
        Button_Simpan.TextImageRelation = TextImageRelation.ImageBeforeText
        Button_Simpan.UseVisualStyleBackColor = True
        '
        ' InputDataSiswa
        '
        AcceptButton = Button_Simpan
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        CancelButton = Button_Cancel
        ClientSize = New System.Drawing.Size(533, 540)
        Controls.Add(TableLayoutPanel1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Name = "InputDataSiswa"
        Text = "Input Data Peserta Didik"
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        FlowLayoutPanel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents TextBox_NamaPesertaDidik As TextBox
    Friend WithEvents ComboBox_GayaTeksLaporan As ComboBox
    Friend WithEvents StrengthsControl As EditableStringListControl
    Friend WithEvents WeaknessesControl As EditableStringListControl
    Friend WithEvents TextBox_CatatanGuru As TextBox
    Friend WithEvents Button_Cancel As FontAwesome.Sharp.IconButton
    Friend WithEvents Button_Simpan As FontAwesome.Sharp.IconButton
End Class
