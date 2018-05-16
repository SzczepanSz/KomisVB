Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Data
Imports System.Data.SqlServerCe
Imports System.Data.Common

Public Class Form1
    Public numer_komisu As Integer = 1

    Public login As String = "admin"
    Public haslo As String = "admin"
    
   
    Public wybrany_komis As Integer = 1
    Private Sub LoginForm1_Load(sender As Object, e As EventArgs)

    End Sub
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If UsernameTextBox.Text = login And PasswordTextBox.Text = haslo Then
            Form3.Show()

        Else
            MessageBox.Show("Login lub hasło niepoprawne")
        End If
        Dim sql_conn As SqlCeConnection
        Dim conn_str As String = "Data Source=auta1.sdf"
        sql_conn = New SqlCeConnection(conn_str)
        Try
            sql_conn.Open()
        Catch ex As Exception
            MessageBox.Show("Wyjątek: " + ex.Message)
        End Try
        If ComboBox1.SelectedItem = "komis pierwszy" Then
            Dim adapter1 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM pracownik WHERE  id_komisu = '" & wybrany_komis & "'", sql_conn)


        ElseIf ComboBox1.SelectedItem = "komis drugi" Then
            Dim adapter2 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM pracownik WHERE  id_komisu = '" & wybrany_komis & "'", sql_conn)

        Else : ComboBox1.SelectedItem = "komis trzeci"
            Dim adapter3 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM pracownik WHERE  id_komisu = '" & wybrany_komis & "'", sql_conn)

        End If

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub UsernameTextBox_TextChanged(sender As Object, e As EventArgs) Handles UsernameTextBox.TextChanged

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedItem = "Komis1"

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        wybrany_komis = ComboBox1.SelectedIndex + 1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sql_conn As SqlCeConnection
        Dim conn_str As String = "Data Source=auta1.sdf"
        sql_conn = New SqlCeConnection(conn_str)
        Try
            sql_conn.Open()
        Catch ex As Exception
            MessageBox.Show("Wyjątek: " + ex.Message)
        End Try
        If ComboBox1.SelectedItem = "komis pierwszy" Then
            Dim adapter1 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM pracownik WHERE  id_komisu = '" & wybrany_komis & "'", sql_conn)
            numer_komisu = 1
            Form2.Show()
        ElseIf ComboBox1.SelectedItem = "komis drugi" Then
            Dim adapter2 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM pracownik WHERE  id_komisu = '" & wybrany_komis & "'", sql_conn)
            numer_komisu = 2
            Form2.Show()
        Else : ComboBox1.SelectedItem = "komis trzeci"
            Dim adapter3 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM pracownik WHERE  id_komisu = '" & wybrany_komis & "'", sql_conn)
            numer_komisu = 3
            Form2.Show()
        End If



    End Sub
End Class
