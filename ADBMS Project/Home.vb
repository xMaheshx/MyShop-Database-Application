Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class Home
    Dim connStr As String = "server=localhost;user=root;database=myshop;port=3306;password=;"
    Dim conn As New MySqlConnection(connStr)
    Dim loginflag As Boolean
    Public LoginName As String
    Dim myTable As New DataTable
    Dim myArray(-1, 6) As String
    Dim IndexT As Integer
    Private Sub Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshConnection()
        DateLabel.Text = DateTime.Today
        'Label8.Text = Chr(169) + " Yashica, Shreya & Mahesh"
        If loginflag Then
            For i As Integer = 1 To 7
                myTable.Columns.Add()
            Next
            Loader()
        End If
        Label9.Text = "Hello " + LoginName + ","
    End Sub


    Dim WithEvents tm As New Timer With {.Interval = 1000, .Enabled = True}

    Private Sub Tm_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tm.Tick
        TimeLabel.Text = Now.ToString("HH:mm:ss")
    End Sub

    Private Sub Loader()
        Dim iRows As Integer = myArray.GetLength(0)
        Dim iCols As Integer = myArray.GetLength(1)

        For iRow As Integer = 0 To iRows - 1
            Dim newRow As DataRow = myTable.NewRow
            For iCol As Integer = 0 To 6
                newRow(iCol) = myArray(iRow, iCol)
            Next
            myTable.Rows.Add(newRow)
        Next
        DataGridView1.DataSource = myTable
        Me.DataGridView1.Columns(0).HeaderText = "SR. No."
        Me.DataGridView1.Columns(1).HeaderText = "Item ID"
        Me.DataGridView1.Columns(2).HeaderText = "Item Name"
        Me.DataGridView1.Columns(3).HeaderText = "Item Category"
        Me.DataGridView1.Columns(4).HeaderText = "Price"
        Me.DataGridView1.Columns(5).HeaderText = "Quantity"
        Me.DataGridView1.Columns(6).HeaderText = "Sub Total"
    End Sub


    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        Application.Exit()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        LoginPanel.Show()

    End Sub

    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        Items.Show()
    End Sub

    Public Sub LoginSuccess()
        loginflag = True
        Home_Load(Me, New System.EventArgs)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        conn.Open()
        If TextBox2.Text <> "" Then
            If loginflag Then
                Dim stm As String = "SELECT * FROM items WHERE Item_id =" + TextBox2.Text
                Dim cmd As MySqlCommand = New MySqlCommand(stm, conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    If TextBox2.Text = reader.GetString("Item_id") Then
                        IndexT += 1
                        myArray = New String(0, 6) {{IndexT, reader.GetString("Item_id"), reader.GetString("Item_name"), reader.GetString("Item_category"), reader.GetString("Price"), "1", reader.GetString("Price")}}
                        Loader()
                        SetTotal()
                        Exit While
                    Else
                        MsgBox("Invalid Item ID")
                    End If
                End While
                reader.Close()
                
            Else
                MsgBox("Please Login First")
            End If
        End If
        conn.Close()
    End Sub

    Private Sub SetTotal()
        Dim tot As Decimal
        For y As Integer = 0 To DataGridView1.Rows.Count - 1
            tot = tot + Convert.ToDecimal(DataGridView1.Rows(y).Cells(6).Value)
        Next
        GrandTotaltext.Text = tot

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        IndexT = 0
        GrandTotaltext.Text = "0"
        myTable.Clear()
        myTable.Columns.Clear()

    End Sub

    Private Sub RefreshConnection()
        Try
            conn.Open()
            Dim stm As String = "SELECT MAX(bill_no) as bill_no FROM orders"
            Dim cmd As MySqlCommand = New MySqlCommand(stm, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                Billno.Text = reader.GetInt32("bill_no") + 1
                    Exit While
            End While
            reader.Close()
            Label8.Text = "Connected"
        Catch ex As Exception
            Label8.Text = "Error in DB connection"
        End Try
        conn.Close()
    End Sub

    
    Private Sub ItemsListToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ItemsListToolStripMenuItem1.Click
        Orders.Show()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Query As String
        conn.Open()
        For y As Integer = 0 To DataGridView1.Rows.Count - 1

            Query = "INSERT INTO `records`(`bill_no`, `item_id`, `item_name`, `price`) VALUES ( "
            Query = Query + Billno.Text + "," + DataGridView1.Rows(y).Cells(1).Value + ",'" + DataGridView1.Rows(y).Cells(2).Value + "'," + DataGridView1.Rows(y).Cells(4).Value + ")"


            Dim cmd As MySqlCommand = New MySqlCommand(Query, conn)

            Dim k As Integer = cmd.ExecuteNonQuery()
            If (k > 0) Then
                Label8.Text = "Success"
            Else
                Label8.Text = "Failed"
            End If
        Next

        Query = "INSERT INTO `orders`(`bill_no`, `date_time`, `customer_name`, `cashier`, `total_amount`) VALUES ( "
        Query = Query + Billno.Text + ",NOW(),'" + TextBox1.Text + "','" + LoginName + "','" + GrandTotaltext.Text + "')"

        Dim cmd1 As MySqlCommand = New MySqlCommand(Query, conn)

        Dim i As Integer = cmd1.ExecuteNonQuery()
        If (i > 0) Then
            Label8.Text = "Success"
        Else
            Label8.Text = "Failed"
        End If
        conn.Close()
        TextBox1.Text = ""
        TextBox2.Text = ""
        IndexT = 0
        GrandTotaltext.Text = "0"
        myTable.Clear()
    End Sub

    Private Sub SuppliersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SuppliersToolStripMenuItem.Click
        Suppliers.Show()

    End Sub
End Class
