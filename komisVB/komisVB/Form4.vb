Imports System.Text

Public Class Form4
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = Form2.ListBox1.SelectedValue.ToString()
        TextBox2.Text = Form2.ListBox2.SelectedValue.ToString()
        TextBox3.Text = Form2.ListBox3.SelectedValue.ToString()
        TextBox4.Text = Form2.ListBox4.SelectedValue.ToString()

        PictureBox1.Image = Form2.PictureBox1.Image

    End Sub

End Class