Imports System.Threading
Imports System.IO
Imports System.Data.SqlServerCe
Imports System.Data.Common
Imports System.Data

Public Class Form3
    Dim sql_conn As SqlCeConnection
    Dim sql_cmd As SqlCeCommand
    Dim sql_adapter As SqlCeDataAdapter
    Dim tabela As DataTable
    Dim conn_str As String

    Dim id_marki As Integer = 0
    Dim id_modelu As Integer = 0
    Dim id_silnika As Integer = 0
    Dim id_powiazanie As Integer = 0
    Dim id_wyposazenie As Integer = 0
    Dim id_kolor As Integer = 0
    Dim wyposazenie As String = " "
    Dim kolor As String = " "
    Dim marka As String = " "
    Dim model As String = " "
    Dim silnik As String = " "





    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Select Case Form1.numer_komisu
            Case 1
                conn_str = "Data Source=auta1.sdf"
            Case 2
                conn_str = "Data Source=auta2.sdf"
            Case 3
                conn_str = "Data Source=auta3.sdf"
            Case Else
                MessageBox.Show("Komis nie został wybrany")
        End Select

        sql_conn = New SqlCeConnection(conn_str)
        Try
            sql_conn.Open()
        Catch ex As Exception
            MessageBox.Show("Wyjątek: " + ex.Message)
        End Try
    End Sub




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.login = TextBox2.Text
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.haslo = TextBox1.Text
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click




        marka = TextBox3.Text
        model = TextBox4.Text.ToString
        silnik = TextBox5.Text


        If marka = "" Or marka = " " Or model = "" Or model = " " Then
            MessageBox.Show("Wypełnij wszystkie pola!")
        Else




            Dim ds_marka = New DataSet("id")
            Dim adapter1 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM marka WHERE nazwa_marki = '" & marka & "'", sql_conn)
            adapter1.Fill(ds_marka, "id")
            id_marki = ds_marka.Tables("id").Rows.Count
            If id_marki = 0 Then
                Dim tab_marka = New DataTable
                Dim adapter2 As DbDataAdapter = New SqlCeDataAdapter("SELECT max(id) FROM marka", sql_conn)
                adapter2.Fill(tab_marka)
                id_marki = tab_marka.Rows(0).Item(0) + 1

                Dim cmd3 = sql_conn.CreateCommand()
                cmd3.CommandText = "INSERT INTO marka VALUES('" & id_marki & "', '" & marka & "')"
                cmd3.ExecuteNonQuery()
            Else
                id_marki = ds_marka.Tables("id").Rows(0).Item(0)
            End If


            Dim ds_model = New DataSet("id")
            Dim adapter3 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM model WHERE nazwa_modelu = '" & model & "'", sql_conn)
            adapter3.Fill(ds_model, "id")
            id_modelu = ds_model.Tables("id").Rows.Count()
            If id_modelu = 0 Then
                Dim tab_model = New DataTable
                Dim adapter4 As DbDataAdapter = New SqlCeDataAdapter("SELECT max(id) FROM model", sql_conn)
                adapter4.Fill(tab_model)
                id_modelu = tab_model.Rows(0).Item(0) + 1

                Dim cmd4 = sql_conn.CreateCommand()
                cmd4.CommandText = "INSERT INTO model VALUES('" & id_modelu & "', '" & model & "','" & id_marki & "' )"
                cmd4.ExecuteNonQuery()
            End If


            For Each strLine As String In TextBox5.Text.Split(vbNewLine)
                Dim ds_silnik = New DataSet("id")
                Dim adapter5 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM silnik WHERE nazwa_silnika = '" & strLine & "'", sql_conn)
                adapter5.Fill(ds_silnik, "id")
                id_silnika = ds_silnik.Tables("id").Rows.Count()
                If id_silnika = 0 Then
                    Dim tab_silnik = New DataTable
                    Dim adapter6 As DbDataAdapter = New SqlCeDataAdapter("SELECT max(id) FROM silnik", sql_conn)
                    adapter6.Fill(tab_silnik)
                    id_silnika = tab_silnik.Rows(0).Item(0) + 1

                    Dim cmd5 = sql_conn.CreateCommand()
                    cmd5.CommandText = "INSERT INTO silnik (id, nazwa_silnika) VALUES('" & id_silnika & "', '" & strLine & "' )"
                    cmd5.ExecuteNonQuery()

                    Dim id_powiaz As Integer
                    Dim tab_model_silnik = New DataTable
                    Dim adapter7 As DbDataAdapter = New SqlCeDataAdapter("SELECT max(id_powiazania) FROM model_silnik", sql_conn)
                    adapter7.Fill(tab_model_silnik)
                    id_powiaz = tab_model_silnik.Rows(0).Item(0) + 1

                    Dim cmd6 = sql_conn.CreateCommand()
                    cmd6.CommandText = "INSERT INTO model_silnik (id_modelu, id_silnika, id_powiazania) VALUES('" & id_modelu & "', '" & id_silnika & "', '" & id_powiaz & "')"
                    cmd6.ExecuteNonQuery()
                End If
            Next
        End If



    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click




        wyposazenie = TextBox6.Text
        kolor = TextBox7.Text


        Dim tab_wyposazenie = New DataTable
        Dim adapter10 As DbDataAdapter = New SqlCeDataAdapter("SELECT max(id) FROM dodatkowe_wyposazenie", sql_conn)
        adapter10.Fill(tab_wyposazenie)
        id_wyposazenie = tab_wyposazenie.Rows(0).Item(0) + 1
        Dim cmd8 = sql_conn.CreateCommand()
        cmd8.CommandText = "INSERT INTO dodatkowe_wyposazenie (id, nazwa_wyposazenia) VALUES('" & id_wyposazenie & "','" & wyposazenie & "')"
        cmd8.ExecuteNonQuery()

        Dim tab_kolor = New DataTable
        Dim adapter11 As DbDataAdapter = New SqlCeDataAdapter("SELECT max(id) FROM kolor", sql_conn)
        adapter11.Fill(tab_kolor)
        id_kolor = tab_kolor.Rows(0).Item(0) + 1
        Dim cmd9 = sql_conn.CreateCommand()
        cmd9.CommandText = "INSERT INTO kolor (id, nazwa_koloru) VALUES('" & id_kolor & "','" & kolor & "')"
        cmd9.ExecuteNonQuery()


    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        marka = TextBox3.Text
        model = TextBox4.Text
        silnik = TextBox5.Text
        wyposazenie = TextBox6.Text
        kolor = TextBox7.Text

        Dim zmienna As Integer

        For i As Byte = 0 To 4
            If i = 0 And TextBox3.Text.Length() > 0 Then
                zmienna = 1

            ElseIf i = 1 And TextBox4.Text.Length() > 0 Then
                zmienna = 2

            ElseIf i = 2 And TextBox5.Text.Length() > 0 Then
                zmienna = 3

            ElseIf i = 3 And TextBox6.Text.Length() > 0 Then
                zmienna = 4

            ElseIf i = 4 And TextBox7.Text.Length() > 0 Then
                zmienna = 5
            End If

            Select Case zmienna
                Case 1
                    MessageBox.Show("Nie można usunąc marki")

                Case 2
                    If MessageBox.Show("Czy napewno usunąć model", caption:=4, buttons:=4) = DialogResult.Yes Then
                        Dim tab_id_modelu = New DataTable()

                        Dim adapter1 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM model WHERE nazwa_modelu='" & model & "'", sql_conn)

                            adapter1.Fill(tab_id_modelu)
                            If tab_id_modelu.Rows.Count = 0 Then
                                MessageBox.Show("Nie ma takiego modelu w bazie!")
                            Else

                            Dim id_modelu As String = tab_id_modelu.Rows(0).Item("id")
                            Dim cmd1 = sql_conn.CreateCommand()
                                cmd1.CommandText = "DELETE FROM model_silnik WHERE id_modelu = '" & id_modelu & "'"
                                cmd1.ExecuteNonQuery()

                                cmd1.CommandText = "DELETE FROM model WHERE id = '" & id_modelu & "'"
                            cmd1.ExecuteNonQuery()
                        End If

                        MessageBox.Show("Model usunięty")
                    End If
                Case 3
                    If MessageBox.Show("Czy napewno usunąć silnik", caption:=4, buttons:=4) = DialogResult.Yes Then
                        Dim tab_id_silnika = New DataTable()

                        Dim adapter1 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM silnik WHERE nazwa_silnika='" & silnik & "'", sql_conn)

                        adapter1.Fill(tab_id_silnika)
                        If tab_id_silnika.Rows.Count = 0 Then
                            MessageBox.Show("Nie ma takiego silnika w bazie!")
                        Else

                            Dim id_silnika As String = tab_id_silnika.Rows(0).Item("id")
                            Dim cmd1 = sql_conn.CreateCommand()
                            cmd1.CommandText = "DELETE FROM model_silnik WHERE id_silnika = '" & id_silnika & "'"
                            cmd1.ExecuteNonQuery()

                            cmd1.CommandText = "DELETE FROM silnik WHERE id = '" & id_silnika & "'"
                            cmd1.ExecuteNonQuery()
                        End If
                    End If
                Case 4
                    If MessageBox.Show("Czy napewno usunąć wyposażenie", caption:=4, buttons:=4) = DialogResult.Yes Then
                        Dim tab_id_wyposazenia = New DataTable()

                        Dim adapter1 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM dodatkowe_wyposazenie WHERE nazwa_wyposazenia='" & wyposazenie & "'", sql_conn)

                        adapter1.Fill(tab_id_wyposazenia)
                        If tab_id_wyposazenia.Rows.Count = 0 Then
                            MessageBox.Show("Nie ma takiego wyposazenia w bazie!")
                        Else

                            Dim id_wyposazenia As String = tab_id_wyposazenia.Rows(0).Item("id")
                            Dim cmd1 = sql_conn.CreateCommand()
                            cmd1.CommandText = "DELETE FROM dodatkowe_wyposazenie WHERE id = '" & id_wyposazenia & "'"
                            cmd1.ExecuteNonQuery()
                        End If
                    End If
                Case 5

                    If MessageBox.Show("Czy napewno usunąć kolor", caption:=4, buttons:=4) = DialogResult.Yes Then
                        Dim tab_kolor = New DataTable()

                        Dim adapter1 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM kolor WHERE nazwa_koloru='" & kolor & "'", sql_conn)

                        adapter1.Fill(tab_kolor)
                        If tab_kolor.Rows.Count = 0 Then
                            MessageBox.Show("Nie ma takiego koloru w bazie!")
                        Else

                            Dim id_koloru As String = tab_kolor.Rows(0).Item("id")
                            Dim cmd1 = sql_conn.CreateCommand()
                            cmd1.CommandText = "DELETE FROM kolor WHERE id = '" & id_koloru & "'"
                            cmd1.ExecuteNonQuery()
                        End If
                    End If

            End Select

            zmienna = 0
        Next





    End Sub
End Class