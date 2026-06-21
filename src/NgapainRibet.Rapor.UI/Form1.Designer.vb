Namespace NgapainRibet.Rapor.UI

    Partial Class Form1
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
            Me.lblStatus = New Label()
            Me.SuspendLayout()

            ' lblStatus
            Me.lblStatus.AutoSize = True
            Me.lblStatus.Location = New System.Drawing.Point(20, 20)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(200, 17)
            Me.lblStatus.TabIndex = 0
            Me.lblStatus.Text = "lblStatus"

            ' Form1
            Me.ClientSize = New System.Drawing.Size(480, 320)
            Me.Controls.Add(Me.lblStatus)
            Me.Name = "Form1"
            Me.Text = "NgapainRibet.Rapor"
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub

#End Region

    End Class

End Namespace
