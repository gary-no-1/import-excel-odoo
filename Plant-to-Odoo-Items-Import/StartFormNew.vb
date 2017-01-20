Imports System.IO
Imports Npgsql
Imports System.Text

Public Class StartFormNew
    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub btnImportExcel_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnImportExcel_Click_1(sender As Object, e As EventArgs) Handles btnImportExcel.Click
        My.Application.Log.WriteEntry("Import from Excel")
        Dim MyForm As New ImportFromExcel
        MyForm.ShowDialog()
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

    Private Sub btnCr8StockVchr_Click(sender As Object, e As EventArgs) Handles btnCr8StockVchr.Click
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

    Private Sub btnDataSettings_Click(sender As Object, e As EventArgs) Handles btnDataSettings.Click
        Dim MyForm As New DataConnectSettings
        MyForm.ShowDialog()

    End Sub

    Private Sub btnIIClose_Click(sender As Object, e As EventArgs) Handles btnIIClose.Click
        Me.Close()
    End Sub

    Private Sub btnSOimport_Click(sender As Object, e As EventArgs) Handles btnSOimport.Click
        My.Application.Log.WriteEntry("Import Sale Order from Excel")
        Dim MyForm As New ImportSaleOrderExcel
        MyForm.ShowDialog()

    End Sub

    Private Sub btnImpCustomers_Click(sender As Object, e As EventArgs) Handles btnImpCustomers.Click

        My.Application.Log.WriteEntry("Check Customers")

        ' check_postgres_connection(pg_conn_string)

        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string

        Dim pgCommand As NpgsqlCommand = New NpgsqlCommand
        pgCommand.Connection = PgCon
        pgCommand.CommandType = CommandType.Text

        ' update every customer with its parent - this program only for customers who are not "is a customer" but have a parent master
        pgCommand.CommandText = "Update zz_saleorder u " &
            "set id_parent = (select id from res_partner s where s.name = u.customer_name And s.is_company)"
        PgCon.Open()
        Try
            pgCommand.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        pgCommand.CommandText = "select count(*) as ctr " &
            " from zz_saleorder where id_parent Is null"
        Dim no_parent_id_count As Integer

        PgCon.Open()

        Try
            no_parent_id_count = pgCommand.ExecuteScalar()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        If no_parent_id_count > 0 Then
            MsgBox("All Orders do not have a Parent Customer. Cannot Proceed. Please Check.")
            My.Application.Log.WriteEntry("Customers Import - All Orders do not have a Parent Customer.")
            Return
        End If

        pgCommand.CommandText = "update zz_saleorder u " &
            "set id_customer = " &
            "(select id from res_partner s where s.name = u.sa_name and s.parent_id = u.id_parent and s.ref = u.import_seq)"

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

        pgCommand.CommandText = "select '' as id , sa_name as name , 'FALSE' as " & dq & "Is a Company" & dq &
            ",'Contact' as " & dq & "Address Type" & dq & ",'base.main_company' as " & dq & "company_id/id" & dq &
            ", 'FALSE' as " & dq & "Use Company Address" & dq &
            ", sa_add_1 as street, sa_add_2 as street2, 'base.in' as " & dq & "country_id/id" & dq & ", sa_city as city" &
            ", mobile as mobile, email as email, 'TRUE' as active, import_seq as " & dq & "Contact Reference" & dq &
            ", sa_zip as zip, sa_state as state, '__export__.res_partner_' || id_parent::text as " & dq & "parent_id/id" & dq &
            " from zz_saleorder where id_customer is null"

        My.Computer.Clipboard.SetText(pgCommand.CommandText)

        Try
            sda = New NpgsqlDataAdapter(pgCommand)
            sda.Fill(ds, "Customer")

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        lblSOprogress.Visible = True
        lblSOprogress.Text = "Got data from customer"
        lblSOprogress.Refresh()

        'MsgBox("Got data from product_template", MsgBoxStyle.MsgBoxSetForeground)

        ' data has been got into dataset
        ' now write dataset into a csv file
        Dim csv_product As String
        csv_product = WorkingFolder & "\customer.csv"
        Try
            DataTable2CSV(ds.Tables("Customer"), csv_product, vbTab)

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        'MsgBox("Written data from product_template", MsgBoxStyle.MsgBoxSetForeground)

        lblSOprogress.Text = "Written data from customer"
        lblSOprogress.Refresh()

        Dim new_product_count As Integer

        new_product_count = ds.Tables("Customer").Rows.Count

        If new_product_count > 0 Then
            lblSOprogress.Text = "New Customers to be imported now available in " + csv_product
            lblSOprogress.Refresh()
        Else
            ' no new customers to be imported.
            lblSOprogress.Visible = False
            lblSOprogress.Refresh()
            MsgBox("There are no New Customers to be imported")
            My.Application.Log.WriteEntry("Check Customers - There are no New Customers to be imported")
        End If

        If System.IO.File.Exists(csv_product) Then
            '    System.IO.File.Delete(csv_product)
        End If

        ' http://www.aspsnippets.com/Articles/Import-Upload-CSV-file-data-to-SQL-Server-database-in-ASPNet-using-C-and-VBNet.aspx

    End Sub

    Private Sub btnISoClose_Click(sender As Object, e As EventArgs) Handles btnISoClose.Click
        Me.Close()

    End Sub

    Private Sub StartFormNew_Load(sender As Object, e As EventArgs) Handles Me.Load
        check_init_working_folder()
        init_db_variables()

    End Sub

    Private Sub btnISOsaleorder_Click(sender As Object, e As EventArgs) Handles btnISOsaleorder.Click
        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string

        Dim pgCommand As NpgsqlCommand = New NpgsqlCommand
        pgCommand.Connection = PgCon
        pgCommand.CommandType = CommandType.Text

        pgCommand.CommandText = "select count(*) as ctr " &
            " from zz_saleorder where id_customer Is null"
        Dim no_customer_id_count As Integer

        PgCon.Open()

        Try
            no_customer_id_count = pgCommand.ExecuteScalar()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        If no_customer_id_count > 0 Then
            MsgBox("All Customers have not been created . Cannot Proceed. Please Check.")
            My.Application.Log.WriteEntry("Sale Order Import - All Orders do not have a Customer.")
            Return
        End If

        lblSOprogress.Visible = True
        lblSOprogress.Text = "Preparing data for Sale Order ...1"
        lblSOprogress.Refresh()

        PgCon.Open()
        Try
            ' update typology for sale order
            pgCommand.CommandText = "Update zz_saleorder u " &
            "set id_typology = (select id from sale_order_type s where s.name = u.typology)"
            pgCommand.ExecuteNonQuery()

            ' update warehouse and account journal for sale order
            pgCommand.CommandText = "Update zz_saleorder u " &
            "set id_journal = s.journal_id, id_warehouse = s.warehouse_id " &
            "from sale_order_type s " &
            "where s.id = u.id_typology "
            pgCommand.ExecuteNonQuery()

            ' update locate from warehouse for sale order
            pgCommand.CommandText = "Update zz_saleorder u " &
            "set id_location = s.lot_stock_id " &
            "from stock_warehouse s " &
            "where s.id = u.id_warehouse "
            pgCommand.ExecuteNonQuery()

            ' get product item to be sold . item_sku is available. item_sku points to product_template NOT product_product.
            ' but all stock is based on id of product_product. view for amazon was made based on product_template - wrong.
            ' now cannot be changed.
            pgCommand.CommandText = "Update zz_saleorder u " &
            "set id_product = (select s.id from product_product s join product_template t " &
            "on s.product_tmpl_id = t.id where u.id_sku = t.id)"
            pgCommand.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()


        lblSOprogress.Visible = True
        lblSOprogress.Text = "Preparing data for Sale Order ...2"
        lblSOprogress.Refresh()

        PgCon.Open()
        Try
            pgCommand.CommandText = "DROP TABLE IF EXISTS zz_amazon "
            pgCommand.ExecuteNonQuery()

            ' now all items which were directly sold online have their product ids
            ' create file from vg_mobile_amazon_make_model_hdd_color
            pgCommand.CommandText = "CREATE TABLE IF NOT EXISTS zz_amazon (" &
                        "amazon_name varchar(100) , " &
                        "product_name varchar(100) , " &
                        "amazon_sku varchar(50) , " &
                        "product_sku varchar(50) , " &
                        "amazon_sku_id integer , " &
                        "product_sku_id integer , " &
                        "id_product integer , " &
                        "qty numeric " &
                        ")"
            pgCommand.ExecuteNonQuery()

            pgCommand.CommandText = "insert into zz_amazon " &
            "( amazon_name,product_name,amazon_sku,product_sku) " &
            "select amazon_name, product_name, amazon_id , product_id " &
            "from vg_mobile_amazon_make_model_hdd_color;"
            pgCommand.ExecuteNonQuery()

            pgCommand.CommandText = "update zz_amazon " &
            "set amazon_sku_id = split_part(split_part(amazon_sku,'__',3),'_',3)::integer , " &
            "product_sku_id = split_part(split_part(product_sku,'__',3),'_',3)::integer ; "
            pgCommand.ExecuteNonQuery()

            ' get product item to be sold . item_sku is available. item_sku points to product_template NOT product_product.
            ' but all stock is based on id of product_product. view for amazon was made based on product_template - wrong.
            ' now cannot be changed.
            pgCommand.CommandText = "Update zz_amazon u " &
            "set id_product = (select s.id from product_product s join product_template t " &
            "on s.product_tmpl_id = t.id where u.product_sku_id = t.id)"
            pgCommand.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        lblSOprogress.Visible = True
        lblSOprogress.Text = "Preparing data for Sale Order ...3"
        lblSOprogress.Refresh()

        PgCon.Open()
        Try
            pgCommand.CommandText = "DROP TABLE IF EXISTS zz_amazon_qty "
            pgCommand.ExecuteNonQuery()

            ' now all items which were directly sold online have their product ids
            ' create file from vg_mobile_amazon_make_model_hdd_color
            pgCommand.CommandText = "CREATE TABLE IF NOT EXISTS zz_amazon_qty (" &
                        "id_location integer , " &
                        "id_product integer , " &
                        "qty numeric " &
                        ")"
            pgCommand.ExecuteNonQuery()

            pgCommand.CommandText = "insert into zz_amazon_qty " &
                "(id_location, id_product, qty) " &
                "select location_id, product_id, name " &
                "from stock_product_by_location;"
            pgCommand.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
        PgCon.Close()

        lblSOprogress.Visible = True
        lblSOprogress.Text = "Preparing data for Sale Order ...Over"
        lblSOprogress.Refresh()

        ' http://stackoverflow.com/questions/21245836/vb-net-pull-in-a-sql-table-row-by-row

        Dim dt_so = New DataTable
        Try
            Using da = New NpgsqlDataAdapter("select id, id_customer, id_parent, id_product, id_location, id_warehouse, id_sku, import_seq from zz_saleorder order by id", PgCon)
                ' you dont need to open/close the connection with a DataAdapter '
                da.Fill(dt_so)
            End Using

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        lblSOprogress.Visible = True
        lblSOprogress.Text = "Working on items for Sale Order ...1"
        lblSOprogress.Refresh()

        Dim so_update_sql As String
        Dim select_sql As String
        Dim amazon_qty_update_sql As String

        For Each so_row As DataRow In dt_so.Rows
            If IsDBNull(so_row("id")) Then
                Continue For
            End If
            ' if id_product is not null , it implies product has been found - it is a direct product (6 attributes) not a summarised product (4 attributes)
            If Not IsDBNull(so_row("id_product")) Then
                Continue For
            End If

            Dim sor_id As Integer = so_row("id")
            lblSOprogress.Text = "Working on items for Sale Order ...2-" & so_row("id")
            lblSOprogress.Refresh()

            so_update_sql = ""
            select_sql = ""
            amazon_qty_update_sql = ""

            Dim sor_location As Integer = so_row("id_location")
            Dim sor_sku As Integer = so_row("id_sku")

            ' select * from zz_amazon where amazon_sku_id = sor_sku
            Dim dt_amazon = New DataTable
            Try
                select_sql = "select amazon_sku_id, product_sku_id, id_product, qty " &
                   "from zz_amazon " &
                   "where amazon_sku_id = " & sor_sku
                Using da = New NpgsqlDataAdapter(select_sql, PgCon)
                    ' you dont need to open/close the connection with a DataAdapter '
                    da.Fill(dt_amazon)
                End Using

            Catch ex As Exception
                MsgBox(ex.ToString)

            End Try

            Dim sor_product As Integer
            ' search zz_amazon for amazon_sku. you could get 1 or 2 rows [NS/NO as of 19/01/2017]
            ' however defensive programming - you may not get any rows
            If dt_amazon Is Nothing Then
                Continue For
            End If
            If dt_amazon.Rows.Count = 0 Then
                Continue For
            End If

            Select Case dt_amazon.Rows.Count
                Case 1
                    For Each so_amazon_row As DataRow In dt_amazon.Rows
                        sor_product = so_amazon_row("id_product")
                        ' update zz_saleorder for id with amazon.id_product
                        so_update_sql = "update zz_saleorder " &
                        "set id_product = " & sor_product &
                        " where id = " & sor_id
                    Next
                Case Else
                    ' here the count is > 1 => multiple id_products for one amazon_sku_id
                    ' for each row
                    '     select sale order.location + amazon.id_product in zz_amazon_qty with zz_amazon_qty.qty > 0
                    '     if found , 
                    '        use amazon.id_product, 
                    '        Update zz_amazon_qty -> zz_amazon_qty.qty = -1
                    '        exit
                    For Each so_amazon_row As DataRow In dt_amazon.Rows
                        sor_product = so_amazon_row("id_product")
                        select_sql = "select id_location, id_product, qty " &
                            "from zz_amazon_qty " &
                            "where id_location = " & sor_location & " and id_product = " & sor_product &
                            " and qty > 0 "
                        Dim dt_amazon_qty = New DataTable
                        Try
                            Using da = New NpgsqlDataAdapter(select_sql, PgCon)
                                ' you dont need to open/close the connection with a DataAdapter '
                                da.Fill(dt_amazon_qty)
                            End Using

                        Catch ex As Exception
                            MsgBox(ex.ToString)

                        End Try

                        If dt_amazon_qty Is Nothing Then
                            Continue For
                        End If
                        If dt_amazon_qty.Rows.Count = 0 Then
                            Continue For
                        End If

                        so_update_sql = "update zz_saleorder " &
                        "set id_product = " & sor_product &
                        " where id = " & sor_id
                        amazon_qty_update_sql = "update zz_amazon_qty " &
                            "set qty = qty - 1 " &
                            "where id_location = " & sor_location & " and id_product = " & sor_product
                        Exit For
                    Next
            End Select
            PgCon.Open()
            Try
                If amazon_qty_update_sql.Length <> 0 Then
                    pgCommand.CommandText = amazon_qty_update_sql
                    pgCommand.ExecuteNonQuery()
                End If
                If so_update_sql.Length <> 0 Then
                    pgCommand.CommandText = so_update_sql
                    pgCommand.ExecuteNonQuery()
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)

            End Try
            PgCon.Close()
        Next

        lblSOprogress.Text = "Working on items for Sale Order ...3       "
        lblSOprogress.Refresh()

        ' update sale price and discount
        PgCon.Open()
        Try
            pgCommand.CommandText = "Update zz_saleorder u " &
                "set item_price = " &
                "(select s.list_price from product_template s join " &
                "product_product t on t.product_tmpl_id = s.id where u.id_product = t.id)"
            pgCommand.ExecuteNonQuery()

            pgCommand.CommandText = "Update zz_saleorder " &
                "set discount = round((item_price - selling_price) * 100 / item_price,4) " &
                "where item_price Is Not null"
            pgCommand.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        PgCon.Close()

        lblSOprogress.Text = "Working on items for Sale Order ...3       "
        lblSOprogress.Refresh()
        ' update tax code

        Dim sda As NpgsqlDataAdapter
        Dim ds As DataSet
        ds = New DataSet
        Dim dq As String = Chr(34)
        Dim sq As String = Chr(39)

        '", id_customer::text as " & dq & "customer/id" & dq &
        '", id_customer::text as " & dq & "contact/id" & dq &
        '",'__export__.product_product_' || id_product::text as " & dq & "order_line/product_id/id" & dq &
        '",id_product::text as " & dq & "order_line/product_id" & dq &

        pgCommand.CommandText = "select '' as id " &
            ", id_customer::text as " & dq & "Customer / Database ID" & dq &
            ", id_customer::text as " & dq & "Ordering Contact / Database ID" & dq &
            ",'product.list0' as " & dq & "pricelist_id/id" & dq &
            ",'Deliver each product when available' as " & dq & "picking_policy" & dq &
            ",'__export__.stock_warehouse_' || id_warehouse::text as " & dq & "warehouse_id/id" & dq &
            ",'__export__.sale_order_type_' || id_typology::text as " & dq & "type_id/id" & dq &
            ",'On Delivery Order' as " & dq & "order_policy" & dq &
            ",'__export__.crm_case_section_2' as " & dq & "section_id/id" & dq &
            ", order_code as " & dq & "client_order_ref" & dq &
            ",id_product::text as " & dq & "Order Lines / Product / Database ID" & dq &
            ",item_price as " & dq & "order_line/price_unit" & dq &
            ",'product.product_uom_unit' as " & dq & "order_line/product_uom/id" & dq &
            ", 1 as " & dq & "order_line/product_uom_qty" & dq &
            ",discount as " & dq & "order_line/discount" & dq &
            " from zz_saleorder where id_product is not null"

        My.Computer.Clipboard.SetText(pgCommand.CommandText)

        Try
            sda = New NpgsqlDataAdapter(pgCommand)
            sda.Fill(ds, "SaleOrder")

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        lblSOprogress.Text = "Got data for Sales Orders"
        lblSOprogress.Refresh()

        ' data has been got into dataset
        ' now write dataset into a csv file
        Dim csv_so As String
        csv_so = WorkingFolder & "\sale_order.csv"
        Try
            DataTable2CSV(ds.Tables("SaleOrder"), csv_so, ",")

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        lblSOprogress.Text = "Sale Order file for import is available at " & csv_so
        lblSOprogress.Refresh()

    End Sub
End Class