Imports System
Imports System.Windows.Forms

''' <summary>
''' Entry point aplikasi. Hanya bertugas membuat dan menjalankan Form utama.
''' Tidak ada logic bisnis di sini — sesuai pemisahan tanggung jawab proyek.
''' </summary>
Module Program

    <STAThread()>
    Sub Main()
        Application.SetHighDpiMode(HighDpiMode.SystemAware)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New Form1())
    End Sub

End Module
