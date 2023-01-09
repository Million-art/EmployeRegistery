Imports System.Data.OleDb
Public Class Form1
    Dim con As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Private bitmap As Bitmap

    Private Sub Viewer()
        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()

        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        da = New OleDbDataAdapter("select * from employee", con)
        da.Fill(dt)
        DataGridView1.DataSource = dt
        con.Close()

        DataGridView1.Columns(0).Width = 150
        DataGridView1.Columns(1).Width = 150
        DataGridView1.Columns(2).Width = 150
        DataGridView1.Columns(3).Width = 150
        DataGridView1.Columns(4).Width = 150
        DataGridView1.Columns(5).Width = 150
        DataGridView1.Columns(6).Width = 150



    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\bebi\Documents\Database1.accdb"


        Viewer()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        Try


            con.Open()
            cmd = con.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "INSERT INTO Employee(id,EmployeeName,Age,Address,Phone,JobPosition,Salary)values(
            
            '" + TextBoxId.Text + "',
            '" + TextBoxName.Text + "',
            '" + TextBoxAge.Text + "',
            '" + TextBoxAddress.Text + "',
            '" + TextBoxPhone.Text + "',
            '" + TextBoxJob_Position.Text + "',
            '" + TextBoxSalary.Text + "'
            )"
            cmd.ExecuteNonQuery()
            con.Close()
            MessageBox.Show("record saved", "employee saved", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "employee saved", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles ButtonReset.Click
        TextBoxId.Clear()
        TextBoxName.Clear()
        TextBoxAge.Clear()
        TextBoxAddress.Clear()
        TextBoxPhone.Clear()
        TextBoxSalary.Clear()
        TextBoxJob_Position.Clear()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles ButtonExit.Click
        Dim result = MessageBox.Show("are you sure do you wont to exit ?", "exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If (result = DialogResult.Yes) Then
            Application.Exit()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonPrint.Click
        Dim height As Integer = DataGridView1.Height
        Try
            DataGridView1.Height = DataGridView1.RowCount * DataGridView1.RowTemplate.Height
            bitmap = New Bitmap(Me.DataGridView1.Width, Me.DataGridView1.Height)
            DataGridView1.DrawToBitmap(bitmap, New Rectangle(0, 0, Me.DataGridView1.Width, Me.DataGridView1.Height))
            PrintPreviewDialog1.Document = PrintDocument1
            PrintPreviewDialog1.ShowDialog()
            DataGridView1.Height = height
        Catch ex As Exception
            MessageBox.Show(ex.Message, "database1", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        Try


            con.Open()
            cmd = con.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "update employee set EmployeeId=" + TextBoxId.Text + ",
            Name='" + TextBoxName.Text + "',
             Age='" + TextBoxAge.Text + "',

              Address='" + TextBoxAddress.Text + "',
                            phone='" + TextBoxPhone.Text + "',
                           JobPosition='" + TextBoxJob_Position.Text + "',
                            Salary='" + TextBoxSalary.Text + "'






                where name='" + TextBoxName.Text + "' and phone='" + TextBoxPhone.Text + "'"

            cmd.ExecuteNonQuery()
            con.Close()
            MessageBox.Show("record Updated", "employee saved", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "employee saved", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try






        Viewer()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            TextBoxId.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
            TextBoxName.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
            TextBoxAge.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
            TextBoxAddress.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
            TextBoxPhone.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString()
            TextBoxJob_Position.Text = DataGridView1.SelectedRows(0).Cells(5).Value.ToString()
            TextBoxSalary.Text = DataGridView1.SelectedRows(0).Cells(6).Value.ToString()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs)


        Try
            Dim checker As Integer
            con.Open()
            cmd = con.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from employee where EmployeeName=@TextBoxName.Text "


            cmd.ExecuteNonQuery()
            dt = New DataTable()
            da = New OleDbDataAdapter(cmd)
            da.Fill(dt)
            checker = Convert.ToInt32(dt.Rows.Count.ToString)
            DataGridView1.DataSource = dt

            con.Close()

            If (checker = 0) Then
                MessageBox.Show("record Updated", "employee saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "employee saved", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try






        Viewer()
    End Sub

    Private Sub ButtonView_Click(sender As Object, e As EventArgs)
        Viewer()
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        Try


            con.Open()
            cmd = con.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete * from employee where id= " & TextBoxId.Text & "

                "

            cmd.ExecuteNonQuery()
            con.Close()
            MessageBox.Show("record Deleted", "employee saved", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "employee Delete", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2Exit_Click(sender As Object, e As EventArgs) Handles Button2Exit.Click
        Dim result = MessageBox.Show("are you sure do you wont to exit ?", "exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If (result = DialogResult.Yes) Then
            Application.Exit()
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBoxUsername.Text = "admin" And TextBoxPassword.Text = "1234" Then
            GroupBox1.Visible = False
        Else
            MessageBox.Show("incorect username or password")
            TextBoxUsername.Clear()
            TextBoxPassword.Clear()
            TextBoxUsername.Focus()
        End If
    End Sub

    Private Sub Reset_Click(sender As Object, e As EventArgs) Handles Reset.Click
        TextBoxUsername.Clear()
        TextBoxPassword.Clear()
    End Sub
End Class
