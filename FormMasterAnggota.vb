Imports System.Data.Odbc
Public Class FormMasterPeminjaman
    Sub kondisiAwal()
        Call koneksi()
        Call munculKodeBuku()
        da = New OdbcDataAdapter("SELECT * FROM peminjaman", conn)
        ds = New DataSet
        da.Fill(ds, "peminjaman")
        DataGridView1.DataSource = (ds.Tables("peminjaman"))
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Laki Laki")
        ComboBox1.Items.Add("Perempuan")
    End Sub

    Private Sub FormMasterAnggota_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd" '
        DateTimePicker2.CustomFormat = "yyyy-MM-dd" '
        Call kondisiAwal()


    End Sub

    ''untuk menyimpan data peminjaman''
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call koneksi()
        Dim simpan As String = "insert into peminjaman values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & DateTimePicker1.Text & "','" & DateTimePicker2.Text & "','" & ComboBox1.Text & "')"
        cmd = New OdbcCommand(simpan, conn)
        cmd.ExecuteNonQuery()
        MsgBox("DATA BERHASIL DI SIMPAN")
        Call kondisiAwal()


    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()

    End Sub
    'untuk mengedit data peminjaman'
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call koneksi()
        Dim edit As String = "UPDATE peminjaman SET id_user='" & TextBox2.Text & "', id_buku='" & TextBox3.Text & "', tanggaal_peminjaman='" & DateTimePicker1.Text & "', tanggal_pengembalian='" & DateTimePicker2.Text & "', status_peminjaman='" & ComboBox1.Text & "' WHERE id_peminjaman='" & TextBox1.Text & "'"
        cmd = New OdbcCommand(edit, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data berhasil di edit...", MsgBoxStyle.Information, "INFORMASI")
        Call kondisiAwal()

    End Sub
    'untuk menghapus data peminjaman'
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call koneksi()
        Dim hapus As String = "delete from peminjaman where id_peminjaman='" & TextBox1.Text & "'"
        cmd = New OdbcCommand(hapus, conn)
        cmd.ExecuteNonQuery()
        MsgBox("Data berhasil di hapus...", MsgBoxStyle.Information, "INFORMASI")
        Call kondisiAwal()
    End Sub
    'untuk mencari data peminjaman'
    Sub Caridata()
        Call koneksi()
        da = New OdbcDataAdapter("select * from peminjaman where id_peminjaman like '%" & TextBox7.Text & "' or id_user like '%" & TextBox7.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds)
        DataGridView1.DataSource = ds.Tables(0)
        DataGridView1.ReadOnly = True
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
    Sub Panggil_Kode()
        Call koneksi()
        cmd = New OdbcCommand("select * from peminjaman where id_peminjaman='" & TextBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        Call Caridata()
    End Sub

    Sub Panggil_Data()
        On Error Resume Next
        TextBox2.Text = dr.Item(1)  'iduser
        TextBox3.Text = dr.Item(2)  'idbuku
        DateTimePicker1.Value = Convert.ToDateTime(dr.Item(3))
        DateTimePicker2.Value = Convert.ToDateTime(dr.Item(4))
        ComboBox1.SelectedValue = dr.Item(5)

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        On Error Resume Next
        TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Call Panggil_Kode()
        If dr.HasRows Then
            Panggil_Data()
        End If
    End Sub


    Sub munculKodeBuku()
        Call koneksi()
        TextBox3.Text = "" ' Memastikan TextBox3 kosong sebelum diisi
        cmd = New OdbcCommand("select * from buku", conn)
        dr = cmd.ExecuteReader
        Do While dr.Read
            TextBox3.Text = dr.Item(0)
        Loop
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Call koneksi()
        cmd = New OdbcCommand("select * from buku where id_buku ='" & TextBox3.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            lbljudul.Text = dr!judul
            lblpenerbit.Text = dr!penerbit
            lblpenulis.Text = dr!penulis
            lbltahunterbit.Text = dr!tahun_terbit
        End If


    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        TextBox1.MaxLength = 10
        If e.KeyChar = Chr(13) Then
            TextBox2.Focus()
            Call Panggil_Kode()
            If dr.HasRows Then
                Call Panggil_Data()
            Else
                Call kondisiAwal()
            End If
        End If
    End Sub

    
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Call kondisiAwal()
        TextBox1.Focus()
    End Sub
End Class
