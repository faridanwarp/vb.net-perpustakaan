Imports System.Data.Odbc
Public Class FormBuku
    Sub kondisiAwal()
        Call koneksi()
        da = New OdbcDataAdapter("SELECT * FROM buku", conn)
        ds = New DataSet
        da.Fill(ds, "buku")
        DataGridView1.DataSource = (ds.Tables("buku"))
    End Sub

    Private Sub FormMasterAnggota_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd" '
        Call kondisiAwal()
    End Sub

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
    'untuk mengedit data buku'
    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call koneksi()
        Dim edit As String = "UPDATE buku SET id_user='" & TextBox2.Text & "', id_buku='" & TextBox3.Text & "', tanggal_pengembalian='" & DateTimePicker2.Text & "' WHERE id_buku='" & TextBox1.Text & "'"
        cmd = New OdbcCommand(edit, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data berhasil di edit...", MsgBoxStyle.Information, "INFORMASI")
        Call kondisiAwal()

    End Sub
    'untuk menghapus data buku'
    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call koneksi()
        Dim hapus As String = "delete from buku where id_buku='" & TextBox1.Text & "'"
        cmd = New OdbcCommand(hapus, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data berhasil di hapus...", MsgBoxStyle.Information, "INFORMASI")
        Call kondisiAwal()
    End Sub
    'untuk mencari data buku'
    Sub Caridata()
        Call koneksi()
        da = New OdbcDataAdapter("select * from buku where id_buku like '%" & TextBox7.Text & "' or judul like '%" & TextBox7.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
        DataGridView1.ReadOnly = True
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Sub Panggil_Kode()
        Call koneksi()
        cmd = New OdbcCommand("select * from buku where id_buku='" & TextBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub

    Sub Panggil_Data()
        On Error Resume Next
        TextBox2.Text = dr.Item(1) 'nama user
        TextBox3.Text = dr.Item(2) 'password
        DateTimePicker2.Text = dr.Item(4) 'nama lengkap
    End Sub
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        On Error Resume Next
        TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Call Panggil_Kode()
        If dr.HasRows Then
            Call Panggil_Data()
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call koneksi()
        Dim simpan As String = "insert into buku values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & DateTimePicker2.Text & "')"
        cmd = New OdbcCommand(simpan, conn)
        cmd.ExecuteNonQuery()
        MsgBox("DATA BERHASIL DI SIMPAN")
        Call kondisiAwal()
    End Sub



    Private Sub TextBox7_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        Call Caridata()
    End Sub
End Class
