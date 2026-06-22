Partial Class MainWindow
    Inherits Form

    ''' <summary>
    ''' Wajib ada untuk Designer (komponen non-visual, mis. event handler container).
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Bersihkan resource yang dipakai.
    ''' </summary>
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"

    Private lblStatus As Label

    Private Sub InitializeComponent()
        lblStatus = New Label()
        IconButton1 = New FontAwesome.Sharp.IconButton()
        SuspendLayout()
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New System.Drawing.Point(20, 20)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New System.Drawing.Size(52, 15)
        lblStatus.TabIndex = 0
        lblStatus.Text = "lblStatus"
        ' 
        ' IconButton1
        ' 
        IconButton1.IconChar = FontAwesome.Sharp.IconChar.None
        IconButton1.IconColor = Drawing.Color.Black
        IconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto
        IconButton1.Location = New System.Drawing.Point(20, 49)
        IconButton1.Name = "IconButton1"
        IconButton1.Size = New System.Drawing.Size(175, 23)
        IconButton1.TabIndex = 1
        IconButton1.Text = "Uji Coba Input Siswa"
        IconButton1.UseVisualStyleBackColor = True
        ' 
        ' MainWindow
        ' 
        ClientSize = New System.Drawing.Size(480, 320)
        Controls.Add(IconButton1)
        Controls.Add(lblStatus)
        Name = "MainWindow"
        Text = "NgapainRibet.Rapor"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents IconButton1 As FontAwesome.Sharp.IconButton

#End Region

End Class
