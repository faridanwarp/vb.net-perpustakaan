Imports System.Data.Odbc
Module Module1
    Public conn As OdbcConnection
    Public da As OdbcDataAdapter
    Public ds As DataSet
    Public dr As OdbcDataReader
    Public cmd As OdbcCommand

    Public Sub koneksi()
        conn = New OdbcConnection("Dsn=dsn_perpuss")
        conn.Open()
    End Sub


End Module
