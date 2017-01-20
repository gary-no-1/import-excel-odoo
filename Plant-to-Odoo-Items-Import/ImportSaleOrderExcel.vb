Imports System.Text.RegularExpressions
Imports Npgsql
Imports FileHelpers


Public Class ImportSaleOrderExcel
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

        'MsgBox("pg conn 1")

        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string
        Dim pgCommand As NpgsqlCommand = New NpgsqlCommand
        pgCommand.Connection = PgCon
        pgCommand.CommandType = CommandType.Text

        PgCon.Open()
        Try
            pgCommand.CommandText = "DROP TABLE IF EXISTS zz_saleorder"
            pgCommand.ExecuteNonQuery()

            pgCommand.CommandText = "CREATE TABLE IF NOT EXISTS zz_saleorder (" &
                        "id serial Not NULL, " &
                        "id_unicommerce varchar(50) , " &
                        "order_code varchar(50) , " &
                        "email varchar(100) , " &
                        "mobile varchar(20) , " &
                        "sa_name text , " &
                        "sa_add_1 text , " &
                        "sa_add_2 text , " &
                        "sa_city text , " &
                        "sa_state varchar(2) , " &
                        "sa_zip varchar(10) , " &
                        "ba_state varchar(2) , " &
                        "ba_zip varchar(10) , " &
                        "item_sku varchar(50) , " &
                        "item_name varchar(100) , " &
                        "chanel_name varchar(50) , " &
                        "selling_price numeric , " &
                        "item_price numeric , " &
                        "discount numeric , " &
                        "sale_order_code varchar(50) , " &
                        "sale_journal varchar(100) , " &
                        "typology varchar(100) , " &
                        "sale_tax_code varchar(100) , " &
                        "customer_name varchar(150) , " &
                        "id_customer integer , " &
                        "id_parent integer , " &
                        "id_typology integer , " &
                        "id_product integer , " &
                        "id_location integer , " &
                        "id_warehouse integer , " &
                        "id_journal integer , " &
                        "id_tax integer , " &
                        "id_sku integer , " &
                        "import_seq varchar(30)" &
                        ")"
            pgCommand.ExecuteNonQuery()

            pgCommand.CommandText = "TRUNCATE zz_saleorder"
            pgCommand.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        'MsgBox("pg conn 2")

        ' http://www.filehelpers.net/example/QuickStart/ReadFileDelimited/

        Dim rowCount As Integer
        Dim engine = New FileHelperEngine(Of SaleOrderImportClass)()
        Dim records = engine.ReadFile(txtExcelFileName.Text)
        rowCount = records.LongCount

        Dim cleanString As String

        For Each q As SaleOrderImportClass In records
            cleanString = Regex.Replace(q.sa_add_1, "[^A-Za-z0-9\-/,#@ ]", "")
            q.sa_add_1 = cleanString
            cleanString = Regex.Replace(q.sa_add_2, "[^A-Za-z0-9\-/,#@ ]", "")
            q.sa_add_2 = cleanString
        Next q

        MsgBox("Ready to import")

        Dim rowsDone As Integer

        Dim sqlInsert As String
        Dim dq As String = Chr(39)

        PgCon.Open()
        Try
            For Each q As SaleOrderImportClass In records
                sqlInsert = ""
                sqlInsert = "Insert into zz_saleorder " &
                "(id_unicommerce, order_code, email, mobile, sa_name, sa_add_1, sa_add_2, sa_city, " &
                "sa_state, sa_zip, ba_state, ba_zip, item_sku, item_name, chanel_name, " &
                "selling_price, sale_order_code, sale_journal, typology, sale_tax_code, customer_name) " &
                " values (" &
                dq & q.id_unicommerce & dq & "," &
                dq & q.order_code & dq & "," &
                dq & q.email & dq & "," &
                dq & q.mobile & dq & "," &
                dq & q.sa_name & dq & "," &
                dq & q.sa_add_1 & dq & "," &
                dq & q.sa_add_2 & dq & "," &
                dq & q.sa_city & dq & "," &
                dq & q.sa_state & dq & "," &
                dq & q.sa_zip & dq & "," &
                dq & q.ba_state & dq & "," &
                dq & q.ba_zip & dq & "," &
                dq & q.item_sku & dq & "," &
                dq & q.item_name & dq & "," &
                dq & q.chanel_name & dq & "," &
                q.selling_price & "," &
                dq & q.sale_order_code & dq & "," &
                dq & q.sale_journal & dq & "," &
                dq & q.typology & dq & "," &
                dq & q.sale_tax_code & dq & "," &
                dq & q.customer & dq & ")"

                'MsgBox(sqlInsert)
                My.Computer.Clipboard.SetText(sqlInsert)

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

        My.Application.Log.WriteEntry("Sale Order " & rowsDone.ToString.Trim & " of " & rowCount.ToString.ToString & " Imported ")

        PgCon.Open()
        pgCommand.CommandText = "update zz_saleorder " &
            "set sa_name = initcap(sa_name) , sa_add_1 = initcap(sa_add_1) , " &
            "sa_add_2 = initcap(sa_add_2) , sa_city = upper(sa_city) , " &
            "id_sku = split_part(split_part(item_sku,'__',3),'_',3)::integer , " &
            "import_seq = to_char(current_timestamp,'DD-MM-YYYY HH12:MI:SS') || '-' || right('0000' || trim(to_char(id,'9999')),4);"

        Try
            pgCommand.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        PgCon.Close()

        MsgBox("Import Over")

    End Sub

    Private Sub ImportSaleOrderExcel_Load(sender As Object, e As EventArgs) Handles Me.Load
        check_init_working_folder()
        init_db_variables()

    End Sub
End Class