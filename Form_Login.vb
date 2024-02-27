Imports System.Data.Odbc
Public Class Form_Login
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()

    End Sub
    'ini codingan sebelumnya 
    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    'Call koneksi()
    ' cmd = New OdbcCommand("select * from user where id_user='" & TextBox1.Text & "' password='" & TextBox2.Text & "' and status='" & ComboBox1.Text & "'", conn)
    ' dr = cmd.ExecuteReader
    '  dr.Read()
    '   If dr.HasRows Then
    '        Call bukakunci()
    '       Me.Hide()
    '       Menu_Utama.Show()

    '  Else
    '       MsgBox("ID User atau Password Salah!!!")

    '   End If
    '  End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call koneksi()

        ' Gunakan parameterized query untuk mencegah SQL injection
        cmd = New OdbcCommand("SELECT * FROM user WHERE id_user=? AND password=? AND status=?", conn)
        cmd.Parameters.AddWithValue("id_user", TextBox1.Text)
        cmd.Parameters.AddWithValue("password", TextBox2.Text)
        cmd.Parameters.AddWithValue("status", ComboBox1.Text)

        dr = cmd.ExecuteReader

        If dr.HasRows Then

            'Me.Close() ' Jika form ini perlu ditutup setelah login
            Call bukakunci()
            Me.Hide()
            Menu_Utama.Show()
            Menu_Utama.Panel1.Text = dr.Item("id_user")
           
        Else
            MsgBox("ID User atau Password Salah!!!")
        End If
        Call kosongkan()
        ' Pastikan untuk menutup reader dan koneksi
        dr.Close()
        conn.Close()
    End Sub


    Sub bukakunci()
        Menu_Utama.LoginToolStripMenuItem.Enabled = False
        Menu_Utama.LogoutToolStripMenuItem.Enabled = True
        Menu_Utama.MasterToolStripMenuItem.Enabled = True
        Menu_Utama.TransaksiToolStripMenuItem.Enabled = True
        Menu_Utama.UtilityToolStripMenuItem.Enabled = True
        Menu_Utama.LaporanToolStripMenuItem.Enabled = True
        If ComboBox1.Text = "ADMINISTRATOR" Then 'validasi yg tdk bs di akses admin
            Menu_Utama.AnggotaToolStripMenuItem.Enabled = False
        End If
        If ComboBox1.Text = "PETUGAS" Then
            ''button yang bisa di akses 
        End If
        If ComboBox1.Text = "PEMINJAMAN" Then
            Menu_Utama.LaporanDataMasterToolStripMenuItem.Enabled = False
            Menu_Utama.LaporanPeminjamanToolStripMenuItem.Enabled = False

        End If


    End Sub


    Sub kosongkan()
        TextBox1.Clear()
        TextBox2.Clear()
        ComboBox1.Text = ""
        TextBox1.Focus()
    End Sub


    Private Sub Form_Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = ""
        TextBox2.Text = "123"
        TextBox2.PasswordChar = "*"
        ComboBox1.Items.Add("ADMINISTRATOR")
        ComboBox1.Items.Add("PETUGAS")
        ComboBox1.Items.Add("PEMINJAM")

        Call koneksi()
        lbltanggal.Text = Format(Today)




    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lbljam.Text = Format(TimeOfDay)
    End Sub
End Class