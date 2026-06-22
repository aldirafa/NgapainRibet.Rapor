Imports Microsoft.FSharp.Collections
Imports NgapainRibet.Rapor.Core


''' <summary>
''' Form utama — saat ini hanya skeleton untuk membuktikan bahwa
''' UI (VB.NET) berhasil mereferensikan dan memanggil Core (F#).
''' Logic UI sesungguhnya (input siswa, checklist, dsb.) akan
''' ditambahkan pada langkah pengembangan berikutnya.
''' </summary>
Public Class MainWindow

    Dim dummyStrengths = New FSharpList(Of String)("Kreatif", New FSharpList(Of String)("Inovatif", FSharpList(Of String).Empty))
    Dim dummyWeaknesses = New FSharpList(Of String)("Kurang disiplin", New FSharpList(Of String)("Terlalu santai", FSharpList(Of String).Empty))
    Dim dummyStudentData As New DomainModels.Student("Budi Santoso", dummyStrengths, dummyWeaknesses, "Wali kelas yang santai dan ramah, gaya teman sebaya", "Perlu perhatian lebih pada disiplin")

    Public Sub New()
        InitializeComponent()

        ' Panggil fungsi F# dari VB.NET untuk verifikasi boundary interop.
        lblStatus.Text = CoreInfo.getStatusMessage()
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Dim formInputDataSiswa = New InputDataSiswa(dummyStudentData)
        If formInputDataSiswa.ShowDialog() = DialogResult.OK Then
            dummyStudentData = formInputDataSiswa.DataSiswa
            MessageBox.Show($"Data siswa berhasil disimpan. Nama: {dummyStudentData.Name}, Tone: {dummyStudentData.Tone}")
        Else
            dummyStudentData = formInputDataSiswa.DataSiswa
            MessageBox.Show($"Input data siswa dibatalkan. Nama: {dummyStudentData.Name}, Tone: {dummyStudentData.Tone}")
        End If
    End Sub
End Class

