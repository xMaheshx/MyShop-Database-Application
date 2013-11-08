Imports MySql.Data.MySqlClient

Public Class LoginPanel

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim connStr As String = "server=localhost;user=root;database=myshop;port=3306;password=;"
        Dim conn As New MySqlConnection(connStr)
        conn.Open()
        Dim stm As String = "SELECT * FROM users WHERE username ='" + TextBox1.Text + "'"
        Dim cmd As MySqlCommand = New MySqlCommand(stm, conn)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()
        While reader.Read()
            If TextBox1.Text = reader.GetString("username") Then
                If TextBox2.Text = reader.GetString("pwd") Then
                    Home.LoginName = TextBox1.Text
                    Home.LoginSuccess()
                    Me.Hide()
                    conn.Close()
                Else
                    MsgBox("Invalid Username or Password")
                    conn.Close()
                End If
            End If
            Exit While
        End While
        reader.Close()

    End Sub
End Class