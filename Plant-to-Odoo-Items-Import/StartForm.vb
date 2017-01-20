Imports System.IO
Imports Npgsql
Imports System.Text

Public Class StartForm
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnImportExcel.Click
        My.Application.Log.WriteEntry("Import from Excel")
        Dim MyForm As New ImportFromExcel
        MyForm.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnCheckMake.Click

    End Sub

    Private Sub btnCheckProduct_Click(sender As Object, e As EventArgs) Handles btnCheckProduct.Click
        My.Application.Log.WriteEntry("Check Products")

        ' check_postgres_connection(pg_conn_string)

        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string

        Dim pgCommand As NpgsqlCommand = New NpgsqlCommand
        pgCommand.Connection = PgCon
        pgCommand.CommandType = CommandType.Text

        pgCommand.CommandText = "update zz_pvqcdata u " &
            "set id_product_template = " &
            "(select id from product_template s where s.name = u.product_code and s.active limit 1)"

        PgCon.Open()
        Try
            pgCommand.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        Dim sda As NpgsqlDataAdapter
        Dim ds As DataSet
        ds = New DataSet
        Dim dq As String = Chr(34)
        Dim sq As String = Chr(39)

        'pgCommand.CommandText = "select distinct '' as id , product_code as name , 'TRUE' as sale_ok, " &
        '    "'TRUE' as purchase_ok, 'TRUE' as active, 'TRUE' as track_all, " &
        '    "'product.product_category_all' as category_id, " &
        '    "'Stockable Product' as type " &
        '    " from zz_pvqcdata where id_product is null"

        pgCommand.CommandText = "select distinct '' as id , product_code as name , 'TRUE' as sale_ok, " &
            "'TRUE' as purchase_ok, 'TRUE' as active, 'TRUE' as track_all, " &
            "'product.product_category_all' as " & dq & "categ_id/id" & dq & " , " &
            "'Stockable Product' as type " &
            " from zz_pvqcdata where id_product_template is null"

        Try
            sda = New NpgsqlDataAdapter(pgCommand)
            sda.Fill(ds, "Product")

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        lblProgressInfo.Visible = True
        lblProgressInfo.Text = "Got data from product_template"
        lblProgressInfo.Refresh()

        'MsgBox("Got data from product_template", MsgBoxStyle.MsgBoxSetForeground)

        ' data has been got into dataset
        ' now write dataset into a csv file
        Dim csv_product As String
        csv_product = WorkingFolder & "\product.csv"
        Try
            DataTable2CSV(ds.Tables("Product"), csv_product, ",")

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        'MsgBox("Written data from product_template", MsgBoxStyle.MsgBoxSetForeground)

        lblProgressInfo.Text = "Written data from product_template"
        lblProgressInfo.Refresh()

        Dim new_product_count As Integer

        new_product_count = ds.Tables("Product").Rows.Count

        If new_product_count > 0 Then
            If 1 = 2 Then
                '' http://stackoverflow.com/questions/2050054/overwrite-a-specific-line-in-a-text-file-using-vb-net

                'My.Application.Log.WriteEntry("Check Products - Product.csv with " + new_product_count.ToString + " created")
                'Dim csv_product_import As String
                'csv_product_import = WorkingFolder & "\product-import.csv"
                ''Dim reader As New StreamReader(csv_product)
                ''Dim writer As New StreamWriter(csv_product_import)
                'Dim reader As New StreamReader(New FileStream(csv_product, FileMode.Open, FileAccess.Read), Encoding.UTF8)
                'Dim writer As New StreamWriter(New FileStream(csv_product_import, FileMode.Open, FileAccess.Write), Encoding.UTF8)

                'Try
                '    Dim s = reader.ReadToEnd().Replace("category_id", "categ_id/id")
                '    writer.Write(s)
                '    writer.Close()
                '    reader.Close()

                'Catch ex As Exception
                '    MsgBox(ex.ToString)

                'End Try

                ''MsgBox("New products to be imported now available in " + csv_product_import)

            End If
            lblProgressInfo.Text = "New products to be imported now available in " + csv_product
            lblProgressInfo.Refresh()
        Else
            ' no new products to be imported.
            ' now all new products have been imported into product_template and automatically
            '    into product_products
            ' so update id_product with equivalent name in product_products

            ' there could be a chance that some names are duplicate - hence the limit 1
            'pgCommand.CommandText = "update zz_pvqcdata u " &
            '"set id_product = " &
            '"(select id from product_product s where s.name_template = u.product_code and s.active limit 1)"

            pgCommand.CommandText = "update zz_pvqcdata u " &
            "set id_product = " &
            "(select s.id from product_product s join product_template t on " &
            "s.product_tmpl_id = t.id where t.name = u.product_code And s.active limit 1)"

            PgCon.Open()
            Try
                pgCommand.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox(ex.ToString)

            End Try
            PgCon.Close()

            lblProgressInfo.Visible = False
            lblProgressInfo.Refresh()
            MsgBox("There are no New Products to be imported")
            My.Application.Log.WriteEntry("Check Products - There are no New Products to be imported")

        End If

        If System.IO.File.Exists(csv_product) Then
            '    System.IO.File.Delete(csv_product)
        End If

        ' http://www.aspsnippets.com/Articles/Import-Upload-CSV-file-data-to-SQL-Server-database-in-ASPNet-using-C-and-VBNet.aspx



    End Sub

    Private Sub StartForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim FileProperties As FileVersionInfo = FileVersionInfo.GetVersionInfo(Application.ExecutablePath)
        Me.Text = Me.Text + "  " + FileProperties.FileVersion

        '        If (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) Then
        'With System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion
        'Me.Text = Me.Text & " V" & .Major & "." & .Minor & "." & .Build
        'End With
        '       End If
        'Dim x As String = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Major.ToString

        check_init_working_folder()
        init_db_variables()
        lblProgressInfo.Visible = False
        If pg_conn_string = "" Then
            btnCheckMake.Enabled = False
            btnImportExcel.Enabled = False
            btnCheckProduct.Enabled = False
            btnCheckSerialNos.Enabled = False
            btnCr8StockVchr.Enabled = False
        End If

        'My.Application.Log.DefaultFileLogWriter.BaseFileName = "hello"
        'Dim x As String = My.Application.Log.DefaultFileLogWriter.FullLogFileName

        'lblProgressInfo.Visible = True
        'lblProgressInfo.Text = x
        'lblProgressInfo.Refresh()

    End Sub

    Private Sub check_postgres_connection(pg_conn_string As String)
        MsgBox(pg_conn_string)
        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string

        Try
            PgCon.Open()
            If PgCon.State = ConnectionState.Open Then
                MsgBox("Connected To PostGres", MsgBoxStyle.MsgBoxSetForeground)
            End If
            PgCon.Close()

        Catch ex As Exception
            'MsgBox(ex.ToString)
            MsgBox("Cannot connect to Postgres Server. Closing Down.")
            'Environment.Exit(0)
            'Me.Close()
        End Try

    End Sub

    Private Sub btnCheckSerialNos_Click(sender As Object, e As EventArgs) Handles btnCheckSerialNos.Click
        My.Application.Log.WriteEntry("Check Serial Nos")
        ' first check that there is no missing product_id
        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string

        Dim pgCommand As NpgsqlCommand = New NpgsqlCommand
        pgCommand.Connection = PgCon
        pgCommand.CommandType = CommandType.Text

        pgCommand.CommandText = "select count(*) as ctr " &
            " from zz_pvqcdata where id_product Is null"
        Dim no_product_id_count As Integer

        PgCon.Open()

        Try
            no_product_id_count = pgCommand.ExecuteScalar()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        If no_product_id_count > 0 Then
            MsgBox("All Products have Not been imported. Please Check")
            My.Application.Log.WriteEntry("Check Serial Nos - All Products have Not been imported.")
            Return
        End If

        pgCommand.CommandText = "update zz_pvqcdata u " &
            "set id_serial = " &
            "(select id from stock_production_lot s " &
            " where s.name = u.gtr_no " &
            " And s.product_id = u.id_product)"

        PgCon.Open()
        Try
            pgCommand.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        Dim sda As NpgsqlDataAdapter
        Dim ds As DataSet
        ds = New DataSet
        Dim dq As String = Chr(34)
        Dim sq As String = Chr(39)

        'pgCommand.CommandText = "Select '' as id , gtr_no as name , " &
        '    "'__export__.product_product_' || id_product::text as product_id, " &
        '    "imei_no as x_greentek_imei, " &
        '    "lot_no as x_greentek_lot " &
        '    " from zz_pvqcdata where id_serial is null "

        pgCommand.CommandText = "Select '' as id , gtr_no as name , " &
            "product_code as product , " &
            "'__export__.product_product_' || id_product::text as product_id, " &
            "imei_no as x_greentek_imei, " &
            "lot_no as x_greentek_lot " &
            " from zz_pvqcdata where id_serial is null "

        pgCommand.CommandText = "Select '' as id , gtr_no as name , " &
            "product_code as product , " &
            "'__export__.product_product_' || id_product::text as " & dq & "product_id/id" & dq & " , " &
            "imei_no as x_greentek_imei, " &
            "lot_no as x_greentek_lot " &
            " from zz_pvqcdata where id_serial is null "

        Try
            sda = New NpgsqlDataAdapter(pgCommand)
            sda.Fill(ds, "SerialNos")

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        lblProgressInfo.Visible = True
        lblProgressInfo.Text = "Got data for Serial Numbers Import"
        lblProgressInfo.Refresh()

        Dim csv_serial As String
        csv_serial = WorkingFolder & "\serialnos.csv"
        Try
            DataTable2CSV(ds.Tables("SerialNos"), csv_serial, ",")

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        'MsgBox("Written data from product_template", MsgBoxStyle.MsgBoxSetForeground)

        lblProgressInfo.Text = "Creating Serial Numbers for Import"
        lblProgressInfo.Refresh()

        Dim new_serial_count As Integer

        new_serial_count = ds.Tables("SerialNos").Rows.Count

        If new_serial_count > 0 Then
            Dim csv_serial_import As String
            csv_serial_import = WorkingFolder & "\serial-import.csv"
            'Dim reader As New StreamReader(csv_serial)
            'Dim writer As New StreamWriter(csv_serial_import)
            Dim reader As New StreamReader(New FileStream(csv_serial, FileMode.Open, FileAccess.Read), Encoding.UTF8)
            Dim writer As New StreamWriter(New FileStream(csv_serial_import, FileMode.Open, FileAccess.Write), Encoding.UTF8)

            Try
                Dim s = reader.ReadToEnd().Replace("product_id", "product_id/id")
                writer.Write(s)
                writer.Close()
                reader.Close()

            Catch ex As Exception
                MsgBox(ex.ToString)

            End Try

            'MsgBox("New products to be imported now available in " + csv_product_import)
            lblProgressInfo.Text = "New serial nos to be imported now available in " + csv_serial_import
            lblProgressInfo.Refresh()
            My.Application.Log.WriteEntry("Check Serial Nos - New serial nos to be imported now available in " + csv_serial_import)
            My.Application.Log.WriteEntry("Check Serial Nos - Import " + new_serial_count.ToString + " Serial nos")
        Else

            lblProgressInfo.Visible = False
            lblProgressInfo.Refresh()
            MsgBox("There are no new Serial Numbers to be imported")
            My.Application.Log.WriteEntry("Check Serial Nos - There are no new serial nos to be imported")

        End If

        'If System.IO.File.Exists(csv_serial) Then
        'System.IO.File.Delete(csv_serial)
        'End If

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles btnCr8StockVchr.Click
        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string

        Dim pgCommand As NpgsqlCommand = New NpgsqlCommand
        pgCommand.Connection = PgCon
        pgCommand.CommandType = CommandType.Text

        pgCommand.CommandText = "select count(*) as ctr " &
            " from zz_pvqcdata where id_serial Is null"
        Dim no_serial_id_count As Integer

        PgCon.Open()

        Try
            no_serial_id_count = pgCommand.ExecuteScalar()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        If no_serial_id_count > 0 Then
            MsgBox("All Serial nos have not been imported.")
            My.Application.Log.WriteEntry("Check Serial Nos - Serial Nos are missing .")
            Return
        End If

        Dim MyForm As New CreateStockImportVoucher
        MyForm.ShowDialog()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim MyForm As New DataConnectSettings
        MyForm.ShowDialog()

    End Sub

    Private Sub btnImportSerialNos_Click(sender As Object, e As EventArgs) Handles btnImportSerialNos.Click
        My.Application.Log.WriteEntry("Check Serial Nos")
        ' first check that there is no missing product_id
        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string

        Dim pgCommand As NpgsqlCommand = New NpgsqlCommand
        pgCommand.Connection = PgCon
        pgCommand.CommandType = CommandType.Text

        pgCommand.CommandText = "select count(*) as ctr " &
            " from zz_pvqcdata where id_product Is null"
        Dim no_product_id_count As Integer

        PgCon.Open()

        Try
            no_product_id_count = pgCommand.ExecuteScalar()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        If no_product_id_count > 0 Then
            MsgBox("All Products have not been imported. Please Check")
            My.Application.Log.WriteEntry("Check Serial Nos - All Products have not been imported.")
            Return
        End If

        pgCommand.CommandText = "update zz_pvqcdata u " &
            "set id_serial = " &
            "(select id from stock_production_lot s " &
            " where s.name = u.gtr_no " &
            " and s.product_id = u.id_product)"

        PgCon.Open()
        Try
            pgCommand.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        ' check if any serial nos are null - only then proceed else return

        pgCommand.CommandText = "select count(*) as ctr " &
            " from zz_pvqcdata where id_serial Is null"
        Dim no_serial_id_count As Integer

        PgCon.Open()

        Try
            no_serial_id_count = pgCommand.ExecuteScalar()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        If no_serial_id_count = 0 Then
            MsgBox("All Serial nos have been imported.")
            My.Application.Log.WriteEntry("Check Serial Nos - No Serial Nos are missing .")
            Return
        End If

        ' creating a cvs file works and does not work . have not figured it out
        ' lets try to insert this data directly

        pgCommand.CommandText = "insert into stock_production_lot " &
            "(name,product_id,x_greentek_imei,x_greentek_lot,x_transfer_price_cost,create_uid,write_uid,create_date,write_date) " &
            "select gtr_no, id_product, imei_no, lot_no , transfer_price , " &
            odoo_user_id.ToString & " , " & odoo_user_id.ToString & " , localtimestamp, localtimestamp " &
            "From zz_pvqcdata where id_serial is null"

        PgCon.Open()

        Dim PgTrans As NpgsqlTransaction
        '= New NpgsqlTransaction(pg_conn_string)
        PgTrans = PgCon.BeginTransaction()
        Try
            pgCommand.ExecuteNonQuery()
            PgTrans.Commit()

        Catch ex As Exception
            MsgBox(ex.ToString)
            PgTrans.Rollback()
        End Try
        PgCon.Close()

        MsgBox("Serial Nos Imported")
        My.Application.Log.WriteEntry("Check Serial Nos - All Serial Nos have been directly imported.")

    End Sub

    Private Sub btnSpecifyUser_Click(sender As Object, e As EventArgs) Handles btnSpecifyUser.Click
        Dim MyForm As New GetUser
        MyForm.ShowDialog()

        If odoo_user_email = "" Then
            btnImportSerialNos.Enabled = False
            btnCr8StockVchr.Enabled = False
        Else
            Me.Text = Me.Text + " -- " + odoo_user_email
            btnImportSerialNos.Enabled = True
            btnCr8StockVchr.Enabled = True
        End If
    End Sub

    Private Sub btnListPrice_Click(sender As Object, e As EventArgs) Handles btnListPrice.Click
        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string

        Dim pgCommand As NpgsqlCommand = New NpgsqlCommand
        pgCommand.Connection = PgCon
        pgCommand.CommandType = CommandType.Text

        pgCommand.CommandText = "select count(*) as ctr " &
            " from zz_pvqcdata where id_product_template Is null"
        Dim no_product_id_count As Integer

        PgCon.Open()

        Try
            no_product_id_count = pgCommand.ExecuteScalar()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        If no_product_id_count > 0 Then
            MsgBox("All Products have not been imported. Please Check")
            My.Application.Log.WriteEntry("List Price - All Products have not been imported.")
            Return
        End If

        'pgCommand.CommandText = "update zz_pvqcdata u " &
        '    "set id_product_template = " &
        '    "(select id from product_template s where s.name = u.product_code and s.active limit 1)"

        'PgCon.Open()
        'Try
        '    pgCommand.ExecuteNonQuery()

        'Catch ex As Exception
        '    MsgBox(ex.ToString)

        'End Try
        'PgCon.Close()

        Dim sda As NpgsqlDataAdapter
        Dim ds As DataSet
        ds = New DataSet
        Dim dq As String = Chr(34)
        Dim sq As String = Chr(39)

        pgCommand.CommandText = "Select distinct " &
            "'__export__.product_template_' || id_product_template::text as id , " &
            "list_price As lst_price " &
            " from zz_pvqcdata "

        Try
            sda = New NpgsqlDataAdapter(pgCommand)
            sda.Fill(ds, "ListPrice")

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        lblProgressInfo.Visible = True
        lblProgressInfo.Text = "Got data For List Price Import"
        lblProgressInfo.Refresh()

        Dim csv_serial As String
        csv_serial = WorkingFolder & "\list_price.csv"
        Try
            DataTable2CSV(ds.Tables("ListPrice"), csv_serial, ",")

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        lblProgressInfo.Visible = True
        lblProgressInfo.Text = "List Price Import available at " & csv_serial
        lblProgressInfo.Refresh()

    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs)

    End Sub
End Class