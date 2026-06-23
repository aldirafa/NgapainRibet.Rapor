Imports Microsoft.FSharp.Collections
Imports NgapainRibet.Rapor.Core
Imports System.ComponentModel

''' <summary>
''' Formulir untuk input atau ubah data siswa yang di-load ke aplikasi.
''' </summary>
Public Class InputDataSiswa
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property DataSiswa As DomainModels.Student
    Public ReadOnly Property OriginalDataSiswa As DomainModels.Student

    Private ReadOnly _toneOptions As String() = {
        "Wali kelas yang formal dan profesional, gaya akademisi guru di sekolah",
        "Wali kelas yang santai dan ramah, gaya teman sebaya",
        "Wali kelas yang akrab baik dengan orang tua dan dengan peserta didiknya",
        "Wali kelas yang sedang serius karena peserta didik ini terlibat kasus",
        "Wali kelas yang penuh perhatian dan peduli terhadap perkembangan peserta didiknya"
    }

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Dummy student data
        DataSiswa = New DomainModels.Student(String.Empty, FSharpList(Of String).Empty, FSharpList(Of String).Empty, String.Empty, String.Empty)
        OriginalDataSiswa = DataSiswa
    End Sub

    Public Sub New(data As DomainModels.Student)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        DataSiswa = data
        OriginalDataSiswa = data
    End Sub

    Private Sub InputDataSiswa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Populate pilihan tone (termasuk tone siswa ini kalau custom/belum ada di daftar)
        Dim toneItems =
            _toneOptions.
            Append(DataSiswa.Tone).
            Where(Function(t) Not String.IsNullOrWhiteSpace(t)).
            Distinct().
            OrderBy(Function(x) x).
            ToArray()

        ComboBox_GayaTeksLaporan.Items.Clear()
        ComboBox_GayaTeksLaporan.Items.AddRange(toneItems)

        ' AutoCompleteSource = CustomSource butuh AutoCompleteCustomSource diisi manual,
        ' kalau tidak autocomplete tidak akan menyarankan apa-apa.
        ComboBox_GayaTeksLaporan.AutoCompleteCustomSource.Clear()
        ComboBox_GayaTeksLaporan.AutoCompleteCustomSource.AddRange(toneItems)

        ' Isi textbox & combobox dengan data siswa yang ada
        TextBox_NamaPesertaDidik.Text = DataSiswa.Name
        ComboBox_GayaTeksLaporan.Text = DataSiswa.Tone
        StrengthsControl.Items = DataSiswa.Strengths
        WeaknessesControl.Items = DataSiswa.Weaknesses
        TextBox_CatatanGuru.Text = DataSiswa.Notes
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        DataSiswa = OriginalDataSiswa
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ButtonSimpan_Click(sender As Object, e As EventArgs) Handles Button_Simpan.Click
        If String.IsNullOrWhiteSpace(TextBox_NamaPesertaDidik.Text) Then
            MessageBox.Show("Nama peserta didik tidak boleh kosong.", "Data belum lengkap", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox_NamaPesertaDidik.Focus()
            Return
        End If

        ' Baca semua field sekaligus di sini (bukan per-keystroke) supaya
        ' DataSiswa selalu konsisten dengan apa yang terlihat di form saat
        ' disimpan, dan supaya nambah field baru nanti (checklist, catatan)
        ' cukup disentuh di satu tempat ini saja.
        DataSiswa =
            New DomainModels.Student(
                TextBox_NamaPesertaDidik.Text.Trim(),
                StrengthsControl.Items,
                WeaknessesControl.Items,
                ComboBox_GayaTeksLaporan.Text,
                TextBox_CatatanGuru.Text
            )

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class
