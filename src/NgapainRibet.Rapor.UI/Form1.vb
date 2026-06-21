Imports NgapainRibet.Rapor.Core

Namespace NgapainRibet.Rapor.UI

    ''' <summary>
    ''' Form utama — saat ini hanya skeleton untuk membuktikan bahwa
    ''' UI (VB.NET) berhasil mereferensikan dan memanggil Core (F#).
    ''' Logic UI sesungguhnya (input siswa, checklist, dsb.) akan
    ''' ditambahkan pada langkah pengembangan berikutnya.
    ''' </summary>
    Public Class Form1

        Public Sub New()
            InitializeComponent()

            ' Panggil fungsi F# dari VB.NET untuk verifikasi boundary interop.
            lblStatus.Text = CoreInfo.getStatusMessage()
        End Sub

    End Class

End Namespace
