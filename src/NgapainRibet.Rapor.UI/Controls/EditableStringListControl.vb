Imports Microsoft.FSharp.Collections
Imports System.ComponentModel

''' <summary>
''' Daftar string yang bisa ditambah/dihapus lewat UI. Dipakai untuk
''' Strengths & Weaknesses di InputDataSiswa — belum ada database topik
''' Kurikulum Merdeka, jadi guru isi/hapus sendiri lewat textbox + tombol,
''' bukan checklist dari katalog tetap.
''' </summary>
Public Class EditableStringListControl

    Public Event ItemsChanged(sender As Object, e As EventArgs)

    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Caption As String
        Get
            Return Label_Caption.Text
        End Get
        Set(value As String)
            Label_Caption.Text = value
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Items As FSharpList(Of String)
        Get
            Dim values = CheckedListBox_Items.Items.Cast(Of Object)().Select(Function(o) o.ToString())
            Return ListModule.OfSeq(Of String)(values)
        End Get
        Set(value As FSharpList(Of String))
            CheckedListBox_Items.Items.Clear()
            If value IsNot Nothing Then
                For Each item In value
                    CheckedListBox_Items.Items.Add(item)
                Next
            End If
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub AddCurrentItem()
        Dim candidate = TextBox_NewItem.Text.Trim()
        If String.IsNullOrWhiteSpace(candidate) Then Return

        Dim alreadyExists =
            CheckedListBox_Items.Items.Cast(Of Object)().
            Any(Function(o) String.Equals(o.ToString(), candidate, StringComparison.OrdinalIgnoreCase))

        If alreadyExists Then Return

        CheckedListBox_Items.Items.Add(candidate)
        TextBox_NewItem.Clear()
        TextBox_NewItem.Focus()
        RaiseEvent ItemsChanged(Me, EventArgs.Empty)
    End Sub

    Private Sub IconButton_Tambah_Click(sender As Object, e As EventArgs) Handles IconButton_Tambah.Click
        AddCurrentItem()
    End Sub

    Private Sub TextBox_NewItem_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox_NewItem.KeyDown
        ' UserControl tidak punya AcceptButton form-level, jadi Enter
        ' di-handle manual di sini supaya tetap konsisten dengan UX
        ' "Enter = aksi utama" yang dipakai di Form lain.
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            e.Handled = True
            AddCurrentItem()
        End If
    End Sub

    Private Sub IconButton_HapusTerpilih_Click(sender As Object, e As EventArgs) Handles IconButton_HapusTerpilih.Click
        ' Snapshot dulu ke array sebelum remove, supaya tidak mutate
        ' koleksi yang sedang di-iterate.
        Dim toRemove = CheckedListBox_Items.CheckedItems.Cast(Of Object)().ToArray()
        For Each item In toRemove
            CheckedListBox_Items.Items.Remove(item)
        Next
        If toRemove.Length > 0 Then
            RaiseEvent ItemsChanged(Me, EventArgs.Empty)
        End If
    End Sub
End Class
