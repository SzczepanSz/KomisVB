Imports System.Threading
Imports System.IO
Imports System.Data.SqlServerCe
Imports System.Data.Common
Imports System.Data

Public Class Form2
    Dim sql_conn As SqlCeConnection
    Dim sql_cmd As SqlCeCommand
    Dim sql_adapter As SqlCeDataAdapter
    Dim tabela As DataTable
    Dim conn_str As String
    Dim liczba_wybranych As Integer = 0
    Dim wybrane_dodatkowe_wyposazenie As String = ""
    Dim tab_nazwa_wyposazenia = New DataTable()

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case Form1.numer_komisu
            Case 1
                Form1.numer_komisu = 1
                conn_str = "Data Source=auta1.sdf"
            Case 2
                Form1.numer_komisu = 2
                conn_str = "Data Source=auta2.sdf"
            Case 3
                Form1.numer_komisu = 3
                conn_str = "Data Source=auta3.sdf"
            Case Else
                MessageBox.Show("Problem z wyborem komisu")
        End Select

        sql_conn = New SqlCeConnection(conn_str)
        Try
            sql_conn.Open()
        Catch ex As Exception
            MessageBox.Show("Wyjątek: " + ex.Message)
        End Try
        Dim folder As String = "zdjecia"
        Dim dt = New DataTable()
        Dim adapter As DbDataAdapter = New SqlCeDataAdapter("SELECT nazwa_marki FROM marka", sql_conn)
        adapter.Fill(dt)
        ListBox1.DataSource = dt
        ListBox1.DisplayMember = "nazwa_marki"
        ListBox1.ValueMember = "nazwa_marki"
        ListBox2.Items.Clear()
        PictureBox1.Image = Image.FromFile(folder + "\1\0.jpg")
    End Sub

   
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        liczba_wybranych = 0
        wybrane_dodatkowe_wyposazenie = ""


        Dim marka As String = ListBox1.SelectedValue.ToString()
        Dim id_marka = New DataTable()


        If (marka <> "System.Data.DataRowView") Then
            Dim adapter2 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM marka WHERE nazwa_marki='" & marka & "'", sql_conn)

            adapter2.Fill(id_marka)
            Dim id_marki As String = id_marka.Rows(0).Item("id")
            Dim tab_nazwa_modelu = New DataTable()
            Dim adapter3 As DbDataAdapter = New SqlCeDataAdapter("SELECT nazwa_modelu FROM model WHERE id_marki='" & id_marki & "'", sql_conn)
            adapter3.Fill(tab_nazwa_modelu)
            ListBox2.DataSource = tab_nazwa_modelu
            ListBox2.DisplayMember = "nazwa_modelu"
            ListBox2.ValueMember = "nazwa_modelu"
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        Dim wybrany_model As String = ListBox2.SelectedValue.ToString()
        liczba_wybranych = 0
        wybrane_dodatkowe_wyposazenie = ""

        Dim tab_id_wybranego_modelu = New DataTable()
        If (wybrany_model <> "System.Data.DataRowView") Then

            Dim adapter4 As DbDataAdapter = New SqlCeDataAdapter("SELECT id FROM model WHERE nazwa_modelu='" & wybrany_model & "'", sql_conn)
            adapter4.Fill(tab_id_wybranego_modelu)
            Dim id_modelu As String = tab_id_wybranego_modelu.Rows(0).Item("id")


            Dim folder As String = "zdjecia"
            Try
                PictureBox1.Image = Image.FromFile(folder + "\" + Form1.numer_komisu.ToString + "\" + id_modelu.ToString + ".jpg")
            Catch
                PictureBox1.Image = Image.FromFile(folder + "\" + Form1.numer_komisu.ToString + "\1.jpg")
            End Try


            Dim ds_id_silnika = New DataSet("id_silnika")
            Dim tab_nazwa_silnika = New DataTable()
            Dim adapter5 As DbDataAdapter = New SqlCeDataAdapter("SELECT id_silnika FROM model_silnik WHERE id_modelu ='" & id_modelu & "'", sql_conn)
            adapter5.Fill(ds_id_silnika, "id_silnika")

            Dim liczba_modeli_silnikow As Integer
            liczba_modeli_silnikow = ds_id_silnika.Tables("id_silnika").Rows.Count
            For licznik As Integer = 0 To liczba_modeli_silnikow - 1
                Dim adapter6 As DbDataAdapter = New SqlCeDataAdapter("SELECT nazwa_silnika FROM silnik WHERE id ='" & ds_id_silnika.Tables("id_silnika").Rows(licznik).Item(0) & "'", sql_conn)
                adapter6.Fill(tab_nazwa_silnika)
                ListBox3.DataSource = tab_nazwa_silnika
                ListBox3.DisplayMember = "nazwa_silnika"
                ListBox3.ValueMember = "nazwa_silnika"
            Next
        End If

        Dim tab_wyposazenie = New DataTable("nazwa_wyposazenia")
        Dim adapter7 As DbDataAdapter = New SqlCeDataAdapter("SELECT nazwa_wyposazenia FROM dodatkowe_wyposazenie", sql_conn)
        adapter7.Fill(tab_wyposazenie)
        ListBox5.DataSource = tab_wyposazenie
        ListBox5.DisplayMember = "nazwa_wyposazenia"
        ListBox5.ValueMember = "nazwa_wyposazenia"

        Dim tab_kolor = New DataTable("nazwa_koloru")
        Dim adapter8 As DbDataAdapter = New SqlCeDataAdapter("SELECT nazwa_koloru FROM kolor", sql_conn)
        adapter8.Fill(tab_kolor)
        ListBox4.DataSource = tab_kolor
        ListBox4.DisplayMember = "nazwa_koloru"
        ListBox4.ValueMember = "nazwa_koloru"


    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form4.Show()
    End Sub
End Class