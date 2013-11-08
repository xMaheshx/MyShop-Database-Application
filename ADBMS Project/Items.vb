Imports MySql.Data.MySqlClient

Public Class Items
    Dim connStr As String = "server=localhost;user=root;database=myshop;port=3306;password=;"
    Dim conn As New MySqlConnection(connStr)

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Dispose()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Text = ""
        conn.Open()
        Dim stm As String = "SELECT * FROM items WHERE item_name ='" + TextBox2.Text + "'"
        Dim cmd As MySqlCommand = New MySqlCommand(stm, conn)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()
        While reader.Read()
            TextBox1.Text = reader.GetInt32("item_id")
            TextBox3.Text = reader.GetInt32("sup_id")
            TextBox4.Text = reader.GetString("item_category")
            TextBox5.Text = reader.GetInt32("price")
            Exit While
        End While
        conn.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim Query As String
        If (TextBox1.Text = "") Then
            Query = "INSERT INTO `items`(`item_name`, `item_category`, `sup_id`, `price`) VALUES ('"
            Query = Query + TextBox2.Text + "','" + TextBox4.Text + "'," + TextBox3.Text + "," + TextBox5.Text + ")"
            conn.Open()

            Dim cmd As MySqlCommand = New MySqlCommand(Query, conn)
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            Dim i As Integer = cmd.ExecuteNonQuery()
            If (i > 0) Then
                Label6.Text = "Success"
            Else
                Label6.Text = "Failed"
            End If
            conn.Close()
            End If
        Catch ex As Exception
            Label6.Text = "Error"
            MsgBox("Error, No Such Seller!")
            conn.Close()
        End Try
    End Sub

    Private Sub Items_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If Not TextBox1.Text = "" Then
                Dim Query As String
                conn.Open()
                Query = "UPDATE items SET item_name ='" + TextBox2.Text + "',"
                Query = Query + " item_category = '" + TextBox4.Text + "', sup_id = " + TextBox3.Text + ", price = " + TextBox5.Text
                Query = Query + " WHERE item_id = " + TextBox1.Text
                Dim cmd As MySqlCommand = New MySqlCommand(Query, conn)
                Dim i As Integer = cmd.ExecuteNonQuery()
                If (i > 0) Then
                    Label6.Text = "Success"
                Else
                    Label6.Text = "Failed"
                End If
                conn.Close()
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
            Else
                MsgBox("Item doesnt exist")
            End If
        Catch ex As Exception
            Label6.Text = "Error"
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If Not TextBox1.Text = "" Then
                Dim Query As String
                conn.Open()
                Query = "DELETE FROM items"
                Query = Query + " WHERE item_id = " + TextBox1.Text
                Dim cmd As MySqlCommand = New MySqlCommand(Query, conn)
                Dim i As Integer = cmd.ExecuteNonQuery()
                If (i > 0) Then
                    Label6.Text = "Success"
                Else
                    Label6.Text = "Failed"
                End If
                conn.Close()
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
            Else
                MsgBox("Item doesnt exist")
            End If
        Catch ex As Exception
            Label6.Text = "Error"
        End Try
    End Sub
End Class