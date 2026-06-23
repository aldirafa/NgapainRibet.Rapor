<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditableStringListControl
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
        Label_Caption = New Label()
        TextBox_NewItem = New TextBox()
        IconButton_Tambah = New FontAwesome.Sharp.IconButton()
        CheckedListBox_Items = New CheckedListBox()
        IconButton_HapusTerpilih = New FontAwesome.Sharp.IconButton()
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
        TableLayoutPanel1.Controls.Add(Label_Caption, 0, 0)
        TableLayoutPanel1.Controls.Add(FlowLayoutPanel1, 0, 1)
        TableLayoutPanel1.Controls.Add(CheckedListBox_Items, 0, 2)
        TableLayoutPanel1.Controls.Add(IconButton_HapusTerpilih, 0, 3)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 4
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.Size = New System.Drawing.Size(420, 190)
        TableLayoutPanel1.TabIndex = 0
        '
        ' Label_Caption
        '
        Label_Caption.AutoSize = True
        Label_Caption.Location = New System.Drawing.Point(3, 0)
        Label_Caption.Margin = New Padding(3, 0, 3, 5)
        Label_Caption.Name = "Label_Caption"
        Label_Caption.Size = New System.Drawing.Size(50, 15)
        Label_Caption.TabIndex = 0
        Label_Caption.Text = "Caption"
        '
        ' FlowLayoutPanel1
        '
        FlowLayoutPanel1.Controls.Add(TextBox_NewItem)
        FlowLayoutPanel1.Controls.Add(IconButton_Tambah)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.Location = New System.Drawing.Point(3, 23)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New System.Drawing.Size(414, 29)
        FlowLayoutPanel1.TabIndex = 1
        '
        ' TextBox_NewItem
        '
        TextBox_NewItem.Location = New System.Drawing.Point(3, 3)
        TextBox_NewItem.Margin = New Padding(3, 3, 5, 3)
        TextBox_NewItem.Name = "TextBox_NewItem"
        TextBox_NewItem.Size = New System.Drawing.Size(280, 23)
        TextBox_NewItem.TabIndex = 0
        '
        ' IconButton_Tambah
        '
        IconButton_Tambah.IconChar = FontAwesome.Sharp.IconChar.Plus
        IconButton_Tambah.IconColor = Drawing.SystemColors.ControlText
        IconButton_Tambah.IconFont = FontAwesome.Sharp.IconFont.Solid
        IconButton_Tambah.IconSize = 16
        IconButton_Tambah.Location = New System.Drawing.Point(291, 3)
        IconButton_Tambah.Name = "IconButton_Tambah"
        IconButton_Tambah.Size = New System.Drawing.Size(95, 27)
        IconButton_Tambah.TabIndex = 1
        IconButton_Tambah.Text = "&Tambah"
        IconButton_Tambah.TextImageRelation = TextImageRelation.ImageBeforeText
        IconButton_Tambah.UseVisualStyleBackColor = True
        '
        ' CheckedListBox_Items
        '
        CheckedListBox_Items.Dock = DockStyle.Fill
        CheckedListBox_Items.FormattingEnabled = True
        CheckedListBox_Items.Location = New System.Drawing.Point(3, 58)
        CheckedListBox_Items.Name = "CheckedListBox_Items"
        CheckedListBox_Items.Size = New System.Drawing.Size(414, 94)
        CheckedListBox_Items.TabIndex = 2
        '
        ' IconButton_HapusTerpilih
        '
        IconButton_HapusTerpilih.Anchor = AnchorStyles.Right
        IconButton_HapusTerpilih.IconChar = FontAwesome.Sharp.IconChar.Trash
        IconButton_HapusTerpilih.IconColor = Drawing.SystemColors.ControlText
        IconButton_HapusTerpilih.IconFont = FontAwesome.Sharp.IconFont.Solid
        IconButton_HapusTerpilih.IconSize = 16
        IconButton_HapusTerpilih.Location = New System.Drawing.Point(291, 158)
        IconButton_HapusTerpilih.Name = "IconButton_HapusTerpilih"
        IconButton_HapusTerpilih.Size = New System.Drawing.Size(126, 27)
        IconButton_HapusTerpilih.TabIndex = 3
        IconButton_HapusTerpilih.Text = "Hapus Terpilih"
        IconButton_HapusTerpilih.TextImageRelation = TextImageRelation.ImageBeforeText
        IconButton_HapusTerpilih.UseVisualStyleBackColor = True
        '
        ' EditableStringListControl
        '
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(TableLayoutPanel1)
        Name = "EditableStringListControl"
        Size = New System.Drawing.Size(420, 190)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        FlowLayoutPanel1.ResumeLayout(False)
        FlowLayoutPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label_Caption As Label
    Friend WithEvents TextBox_NewItem As TextBox
    Friend WithEvents IconButton_Tambah As FontAwesome.Sharp.IconButton
    Friend WithEvents CheckedListBox_Items As CheckedListBox
    Friend WithEvents IconButton_HapusTerpilih As FontAwesome.Sharp.IconButton
End Class
