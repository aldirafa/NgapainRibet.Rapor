<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DownloadProgressControl
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
        ProgressBar_Download = New ProgressBar()
        Label_Status = New Label()
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
        TableLayoutPanel1.Controls.Add(ProgressBar_Download, 0, 0)
        TableLayoutPanel1.Controls.Add(Label_Status, 0, 1)
        TableLayoutPanel1.Controls.Add(FlowLayoutPanel1, 0, 2)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 3
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.Size = New System.Drawing.Size(420, 80)
        TableLayoutPanel1.TabIndex = 0
        '
        ' ProgressBar_Download
        '
        ProgressBar_Download.Dock = DockStyle.Fill
        ProgressBar_Download.Location = New System.Drawing.Point(3, 3)
        ProgressBar_Download.Maximum = 100
        ProgressBar_Download.Minimum = 0
        ProgressBar_Download.Name = "ProgressBar_Download"
        ProgressBar_Download.Size = New System.Drawing.Size(414, 23)
        ProgressBar_Download.Style = ProgressBarStyle.Continuous
        ProgressBar_Download.TabIndex = 0
        '
        ' Label_Status
        '
        Label_Status.AutoSize = True
        Label_Status.Location = New System.Drawing.Point(3, 29)
        Label_Status.Margin = New Padding(3, 0, 3, 0)
        Label_Status.Name = "Label_Status"
        Label_Status.Size = New System.Drawing.Size(120, 15)
        Label_Status.TabIndex = 1
        Label_Status.Text = "Belum mulai mengunduh."
        '
        ' FlowLayoutPanel1
        '
        FlowLayoutPanel1.Controls.Add(IconButton_Batal)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft
        FlowLayoutPanel1.Location = New System.Drawing.Point(3, 47)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New System.Drawing.Size(414, 30)
        FlowLayoutPanel1.TabIndex = 2
        '
        ' IconButton_Batal
        '
        IconButton_Batal.IconChar = FontAwesome.Sharp.IconChar.Ban
        IconButton_Batal.IconColor = Drawing.Color.Firebrick
        IconButton_Batal.IconFont = FontAwesome.Sharp.IconFont.Solid
        IconButton_Batal.IconSize = 16
        IconButton_Batal.Location = New System.Drawing.Point(331, 3)
        IconButton_Batal.Name = "IconButton_Batal"
        IconButton_Batal.Size = New System.Drawing.Size(80, 27)
        IconButton_Batal.TabIndex = 0
        IconButton_Batal.Text = "&Batal"
        IconButton_Batal.TextImageRelation = TextImageRelation.ImageBeforeText
        IconButton_Batal.UseVisualStyleBackColor = True
        IconButton_Batal.Visible = False
        '
        ' DownloadProgressControl
        '
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(TableLayoutPanel1)
        Name = "DownloadProgressControl"
        Size = New System.Drawing.Size(420, 80)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        FlowLayoutPanel1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents ProgressBar_Download As ProgressBar
    Friend WithEvents Label_Status As Label
    Friend WithEvents IconButton_Batal As FontAwesome.Sharp.IconButton
End Class
