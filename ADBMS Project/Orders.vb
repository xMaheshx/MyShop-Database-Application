Imports MySql.Data.MySqlClient

Public Class Orders
    Dim connStr As String = "server=localhost;user=root;database=myshop;port=3306;password=;"
    Dim conn As New MySqlConnection(connStr)
    Private Sub Search_Click(sender As Object, e As EventArgs) Handles Search.Click
        conn.Open()
        Dim stm As String = "SELECT * FROM orders WHERE bill_no =" + TextBox1.Text
        Dim cmd As MySqlCommand = New MySqlCommand(stm, conn)
        Dim reader As MySqlDataReader = cmd.ExecuteReader()
        While reader.Read()
            TextBox2.Text = reader.GetMySqlDateTime("date_time")
            TextBox3.Text = reader.GetString("customer_name")
            TextBox5.Text = reader.GetString("cashier")
            TextBox4.Text = reader.GetInt32("total_amount")
            Exit While
        End While
        conn.Close()
        conn.Open()
        stm = "SELECT * FROM records WHERE bill_no =" + TextBox1.Text
        Dim cmd1 As MySqlCommand = New MySqlCommand(stm, conn)
        Dim ds As DataSet = New DataSet()
        Dim DataAdapter1 As MySqlDataAdapter = New MySqlDataAdapter()
        DataAdapter1.SelectCommand = cmd1
        DataAdapter1.Fill(ds, "Product")
        DataGridView1.DataSource = ds
        DataGridView1.DataMember = "Product"
        Me.DataGridView1.Columns(0).HeaderText = "Tuple no."
        Me.DataGridView1.Columns(1).HeaderText = "Bill No."
        Me.DataGridView1.Columns(2).HeaderText = "Item ID"
        Me.DataGridView1.Columns(3).HeaderText = "Item Name"
        Me.DataGridView1.Columns(4).HeaderText = "Price"

        conn.Close()
    End Sub
End Class