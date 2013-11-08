Imports MySql.Data.MySqlClient

Public Class Suppliers
    Dim connStr As String = "server=localhost;user=root;database=myshop;port=3306;password=;"
    Dim conn As New MySqlConnection(connStr)

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Dispose()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            TextBox1.Text = ""
            conn.Open()
            Dim stm As String = "SELECT * FROM supplier WHERE sup_name ='" + TextBox2.Text + "'"
            Dim cmd As MySqlCommand = New MySqlCommand(stm, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                TextBox1.Text = reader.GetInt32("sup_id")
                TextBox4.Text = reader.GetInt32("sup_phone")
                TextBox3.Text = reader.GetString("sup_location")
                Exit While
            End While
            conn.Close()
        Catch ex As Exception
            Status.Text = "Error"
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim Query As String
            If (TextBox1.Text = "") Then
                Query = "INSERT INTO `supplier`(`sup_name`, `sup_location`, `sup_phone`) VALUES ('"
                Query = Query + TextBox2.Text + "','" + TextBox3.Text + "'," + TextBox4.Text + ")"
                conn.Open()

                Dim cmd As MySqlCommand = New MySqlCommand(Query, conn)
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                Dim i As Integer = cmd.ExecuteNonQuery()
                If (i > 0) Then
                    Status.Text = "Success"
                Else
                    Status.Text = "Failed"
                End If
                conn.Close()
            End If
        Catch ex As Exception
            Status.Text = "Error"
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If Not TextBox1.Text = "" Then
                Dim Query As String
                conn.Open()
                Query = "UPDATE supplier SET sup_name ='" + TextBox2.Text + "',"
                Query = Query + " sup_location = '" + TextBox3.Text + "', sup_phone = " + TextBox4.Text
                Query = Query + " WHERE sup_id = " + TextBox1.Text
                Dim cmd As MySqlCommand = New MySqlCommand(Query, conn)
                Dim i As Integer = cmd.ExecuteNonQuery()
                If (i > 0) Then
                    Status.Text = "Success"
                Else
                    Status.Text = "Failed"
                End If
                conn.Close()
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
            Else
                MsgBox("Supplier doesnt exist")
            End If
        Catch ex As Exception
            Status.Text = "Error"
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If Not TextBox1.Text = "" Then
                Dim Query As String
                conn.Open()
                Query = "DELETE FROM Supplier"
                Query = Query + " WHERE sup_id = " + TextBox1.Text
                Dim cmd As MySqlCommand = New MySqlCommand(Query, conn)
                Dim i As Integer = cmd.ExecuteNonQuery()
                If (i > 0) Then
                    Status.Text = "Success"
                Else
                    Status.Text = "Failed"
                End If
                conn.Close()
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
            Else
                MsgBox("Supplier doesnt exist")
            End If
        Catch ex As Exception
            Status.Text = "Error"
        End Try
    End Sub
   
End Class