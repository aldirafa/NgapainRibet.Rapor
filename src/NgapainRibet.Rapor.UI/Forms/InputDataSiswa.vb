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

    Private Sub TextboxNamaPesertaDidik_TextChanged(sender As Object, e As EventArgs) Handles TextBox_NamaPesertaDidik.TextChanged
        If Not String.IsNullOrWhiteSpace(TextBox_NamaPesertaDidik.Text) Then
            DataSiswa = New DomainModels.Student(TextBox_NamaPesertaDidik.Text, DataSiswa.Strengths, DataSiswa.Weaknesses, DataSiswa.Tone, DataSiswa.Notes)
        End If
    End Sub

    Private Sub InputDataSiswa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Populate some sample tones
        ComboBox_GayaTeksLaporan.Items.Clear()
        ComboBox_GayaTeksLaporan.Items.AddRange({"Wali kelas yang formal dan profesional, gaya akademisi guru di sekolah", "Wali kelas yang santai dan ramah, gaya teman sebaya", "Wali kelas yang akrab baik dengan orang tua dan dengan peserta didiknya", "Wali kelas yang sedang serius karena peserta didik ini terlibat kasus", "Wali kelas yang penuh perhatian dan peduli terhadap perkembangan peserta didiknya", DataSiswa.Tone}.Distinct().OrderBy(Function(x) x).ToArray())

        ' Isi combo box dan textbox dengan data siswa yang ada
        TextBox_NamaPesertaDidik.Text = DataSiswa.Name
        ComboBox_GayaTeksLaporan.SelectedItem = DataSiswa.Tone
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        Me.DialogResult = DialogResult.Cancel
        DataSiswa = OriginalDataSiswa
        Me.Close()
    End Sub

    Private Sub ButtonSimpan_Click(sender As Object, e As EventArgs) Handles Button_Simpan.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub ComboBoxGayaTeksLaporan_TextUpdate(sender As Object, e As EventArgs) Handles ComboBox_GayaTeksLaporan.TextUpdate, ComboBox_GayaTeksLaporan.SelectedIndexChanged
        DataSiswa = New DomainModels.Student(DataSiswa.Name, DataSiswa.Strengths, DataSiswa.Weaknesses, ComboBox_GayaTeksLaporan.Text, DataSiswa.Notes)
        Debug.WriteLine($"Tone updated to: {DataSiswa.Tone}")
    End Sub

    Private Sub Input_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox_NamaPesertaDidik.KeyDown, ComboBox_GayaTeksLaporan.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevent the ding sound
            e.Handled = True
            Button_Simpan.PerformClick()
        ElseIf e.KeyCode = Keys.Escape Then
            e.SuppressKeyPress = True
            e.Handled = True
            Button_Cancel.PerformClick()
        End If
    End Sub
End Class