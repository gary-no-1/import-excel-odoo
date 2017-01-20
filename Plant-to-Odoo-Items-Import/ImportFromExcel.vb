Imports System.IO
Imports Npgsql


Public Class ImportFromExcel
    Private Sub btnLocateExcelFile_Click(sender As Object, e As EventArgs) Handles btnLocateExcelFile.Click
        Dim strFileName As String

        OpenFD.InitialDirectory = "C:\My Documents"
        OpenFD.Title = "Open the CSV File to Import"
        OpenFD.Filter = "CSV Files|*.csv"
        OpenFD.ShowDialog()
        strFileName = OpenFD.FileName
        txtExcelFileName.Text = strFileName

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        If Me.txtExcelFileName.Text.Trim = "" Then
            MsgBox("No File for import")
            Return
        End If

        Dim msg As String = ""

        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string
        Dim pgCommand As NpgsqlCommand = New NpgsqlCommand
        pgCommand.Connection = PgCon
        pgCommand.CommandType = CommandType.Text

        PgCon.Open()
        Try
            pgCommand.CommandText = "DROP TABLE IF EXISTS zz_pvqcdata"
            pgCommand.ExecuteNonQuery()

            pgCommand.CommandText = "CREATE TABLE IF NOT EXISTS zz_pvqcdata (" &
                        "gtr_no varchar(20) , " &
                        "imei_no varchar(30) , " &
                        "product_code varchar(150) , " &
                        "make varchar(50) , " &
                        "model varchar(50) , " &
                        "transfer_price numeric , " &
                        "list_price numeric , " &
                        "id_serial integer , " &
                        "id_product integer , " &
                        "id_product_template integer , " &
                        "lot_no varchar(20) )"
            pgCommand.ExecuteNonQuery()

            pgCommand.CommandText = "TRUNCATE zz_pvqcdata"
            pgCommand.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        ' http://www.aspsnippets.com/Articles/Import-Upload-CSV-file-data-to-SQL-Server-database-in-ASPNet-using-C-and-VBNet.aspx

        Dim dt As New DataTable()
        dt.Columns.AddRange(New DataColumn(5) _
            {New DataColumn("gtr_no", GetType(String)),
            New DataColumn("product_code", GetType(String)),
            New DataColumn("transfer_price", GetType(Integer)),
            New DataColumn("list_price", GetType(Integer)),
            New DataColumn("imei_no", GetType(String)),
            New DataColumn("lot_no", GetType(String))})

        My.Application.Log.WriteEntry("Import from Excel - Import from " + txtExcelFileName.Text)

        Dim csvData As String = File.ReadAllText(txtExcelFileName.Text)
        Dim firstRow As Boolean = vbTrue
        Dim rowCount As Integer
        rowCount = 0

        For Each row As String In csvData.Split(ControlChars.Lf)
            If Not String.IsNullOrEmpty(row) Then
                '  skip first row since it is header
                If Not firstRow Then
                    dt.Rows.Add()
                    Dim i As Integer = 0
                    For Each cell As String In row.Split(","c)
                        dt.Rows(dt.Rows.Count - 1)(i) = cell.Replace(vbCr, "")
                        i += 1
                    Next
                    rowCount += 1
                End If
                firstRow = vbFalse
            End If
        Next

        'MsgBox("Ready to import")

        Dim rowsDone As Integer

        Dim sqlInsert As String
        Dim q As DataRow
        Dim dq As String = Chr(39)

        PgCon.Open()
        Try
            For Each q In dt.Rows
                sqlInsert = ""
                sqlInsert = "Insert into zz_pvqcdata " &
                    "(gtr_no, imei_no, product_code, lot_no, transfer_price, list_price) values(" &
                    dq & q("gtr_no") & dq & "," &
                    dq & q("imei_no") & dq & "," &
                    dq & q("product_code") & dq & "," &
                    dq & q("lot_no") & dq & "," &
                    q("transfer_price") & "," & q("list_price") & ")"
                'MsgBox(sqlInsert)
                pgCommand.CommandText = sqlInsert
                pgCommand.ExecuteNonQuery()
                rowsDone += 1
                If (rowsDone Mod 10 = 0) Then
                    lblRecordsImported.Text = rowsDone.ToString.Trim & " of " & rowCount.ToString.ToString & " Imported "
                    lblRecordsImported.Refresh()
                End If

            Next q

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        lblRecordsImported.Text = rowsDone.ToString.Trim & " of " & rowCount.ToString.ToString & " Imported "
        lblRecordsImported.Refresh()

        My.Application.Log.WriteEntry(rowsDone.ToString.Trim & " of " & rowCount.ToString.ToString & " Imported ")

        MsgBox("Import Over")
    End Sub

    Private Sub ImportFromExcel_Load(sender As Object, e As EventArgs) Handles Me.Load
        check_init_working_folder()
        init_db_variables()
        'check_delete_ce_db()

    End Sub
End Class