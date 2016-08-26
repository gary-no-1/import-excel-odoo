Imports System.IO
Imports System.Text
Imports Npgsql

Public Class CreateStockImportVoucher
    Dim ds As DataSet

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub CreateStockImportVoucher_Load(sender As Object, e As EventArgs) Handles Me.Load
        check_init_working_folder()
        init_db_variables()

        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string

        Dim pgCommand As NpgsqlCommand = New NpgsqlCommand
        pgCommand.Connection = PgCon
        pgCommand.CommandType = CommandType.Text

        pgCommand.CommandText = "select id, complete_name " &
            " from stock_location " &
            " where usage = 'internal' and active " &
            " order by complete_name"

        Dim sda As NpgsqlDataAdapter
        'Dim ds As DataSet
        ds = New DataSet

        Try
            sda = New NpgsqlDataAdapter(pgCommand)
            sda.Fill(ds, "Location")

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        Dim location_count As Integer
        location_count = ds.Tables("Location").Rows.Count

        Dim i As Integer
        For i = 1 To location_count
            'Dim l_value As Integer = ds.Tables("Location")(i - 1)(0)
            Dim l_text As String = ds.Tables("Location")(i - 1)(1)
            cmbLocations.Items.Add(l_text)
        Next

    End Sub

    Private Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click
        If Len(txtVchrName.Text) = 0 Then
            MsgBox("Voucher Name not supplied")
            Return
        End If
        If cmbLocations.SelectedIndex < 0 Then
            MsgBox("Location not selected")
            Return
        End If

        Dim locFull As String = cmbLocations.Items(cmbLocations.SelectedIndex)
        Dim vchr_loc As String = ds.Tables("Location")(cmbLocations.SelectedIndex)(0).ToString
        'MsgBox(locFull)
        'Return

        'Label3.Text = ds.Tables("Location")(cmbLocations.SelectedIndex)(1)
        'Label3.Refresh()

        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string

        Dim pgCommand As NpgsqlCommand = New NpgsqlCommand
        pgCommand.Connection = PgCon
        pgCommand.CommandType = CommandType.Text

        Dim sda As NpgsqlDataAdapter
        Dim ds_new As DataSet
        ds_new = New DataSet
        Dim dq As String = Chr(34)
        Dim sq As String = Chr(39)

        pgCommand.CommandText = "INSERT INTO stock_inventory(" &
            "create_uid, create_date, Name, location_id, company_id, write_uid," &
            "state, write_date, Date, Filter)" &
            " VALUES(" & odoo_user_id.ToString & ", localtimestamp,'" & txtVchrName.Text.Trim & "','" & vchr_loc &
            "', 1, " & odoo_user_id.ToString & ",'draft', localtimestamp, localtimestamp, 'none' ) RETURNING id "

        'MsgBox(pgCommand.CommandText)
        Dim stock_inv_id As Integer

        Dim PgTrans As NpgsqlTransaction
        '= New NpgsqlTransaction(pg_conn_string)

        PgCon.Open()
        PgTrans = PgCon.BeginTransaction()
        Try
            stock_inv_id = pgCommand.ExecuteScalar()
            'pgCommand.ExecuteNonQuery()
            PgTrans.Commit()

        Catch ex As Exception
            MsgBox(ex.ToString)
            PgTrans.Rollback()
        End Try
        PgCon.Close()

        'MsgBox(stock_inv_id.ToString)

        pgCommand.CommandText = "INSERT INTO stock_inventory_line(" &
            "create_uid, create_date, prodlot_name, product_name, location_id," &
            "prod_lot_id, location_name, company_id, write_uid, inventory_id," &
            "write_date, product_qty, product_uom_id,product_id)" &
            " SELECT " & odoo_user_id.ToString & ",localtimestamp, gtr_no, product_code, '" &
            vchr_loc & "'," & " id_serial, '" & locFull.Trim & "', 1," &
            odoo_user_id.ToString & "," & stock_inv_id.ToString & ",localtimestamp,1,1,id_product FROM zz_pvqcdata"

        'MsgBox(pgCommand.CommandText)

        PgCon.Open()
        PgTrans = PgCon.BeginTransaction()
        Try
            pgCommand.ExecuteNonQuery()
            PgTrans.Commit()

        Catch ex As Exception
            MsgBox(ex.ToString)
            PgTrans.Rollback()
        End Try
        PgCon.Close()

        If 1 = 2 Then
            'pgCommand.CommandText = "Select '' as name , 1 as product_qty , " &
            '"'__export__.stock_location_' || " & vchr_loc & " as location_id, " &
            '"'__export__.product_product_' || id_product::text as product_id, " &
            '"'__export__.stock_production_lot_' || id_serial::text as prodlot_id " &
            '" from zz_pvqcdata "

            ''with pvqcdata as (select * , row_number() over () as rnum from zz_pvqcdata)
            ''select case rnum when 1 then 'xyz' else '' end as name, 1 as "line_ids/product_qty" , 
            '''__export__.stock_location_' || 'xyz' as "line_ids/location_id/id" ,
            '''__export__.product_product_' || 'xyz' as "line_ids/product_id/id" ,
            '''__export__.stock_production_lot_' || 'xyz' as "line_ids/prod_lot_id/id" 
            '' from pvqcdata "

            'pgCommand.CommandText = "with pvqcdata as (select * , row_number() over () as rnum from zz_pvqcdata) "
            'pgCommand.CommandText = pgCommand.CommandText &
            '" select case rnum when 1 then " & sq & txtVchrName.Text.Trim & sq & " else '' end " &
            '"as name , 1 as " & dq & "line_ids/product_qty" & dq & " , " &
            '" gtr_no as serial_no , " &
            '" product_code as product , " &
            '"'__export__.stock_location_' || " & vchr_loc & " as " & dq & "line_ids/location_id/id" & dq & ", " &
            '"'__export__.product_product_' || id_product::text as " & dq & "line_ids/product_id/id" & dq & ", " &
            '"'__export__.stock_production_lot_' || id_serial::text as " & dq & "line_ids/prod_lot_id/id" & dq & " " &
            '" from pvqcdata "

            ''pgCommand.CommandText = "select '' as name , 1 as product_qty , " &
            ''    "'__export__.stock_location_' || " & vchr_loc & " as location_id, " &
            ''    "'__export__.product_product_' || id_product::text as product_id, " &
            ''    "product_code as product, " &
            ''    "gtr_no as serial_no, " &
            ''    "'__export__.stock_production_lot_' || id_serial::text as prodlot_id " &
            ''    " from zz_pvqcdata "

            'Try
            '    sda = New NpgsqlDataAdapter(pgCommand)
            '    sda.Fill(ds_new, "Voucher")

            'Catch ex As Exception
            '    MsgBox(ex.ToString)

            'End Try

            'Dim csv_adjustment As String
            'csv_adjustment = WorkingFolder & "\stock_adjustment_voucher.csv"
            'Try
            '    DataTable2CSV(ds_new.Tables("Voucher"), csv_adjustment, ",")

            'Catch ex As Exception
            '    MsgBox(ex.ToString)

            'End Try

            'Dim csv_adj_import As String
            'csv_adj_import = WorkingFolder & "\stock_adjustment_voucher-import.csv"
            ''Dim reader As New StreamReader(csv_adjustment)
            ''Dim writer As New StreamWriter(csv_adj_import)
            'Dim reader As New StreamReader(New FileStream(csv_adjustment, FileMode.Open, FileAccess.Read), Encoding.UTF8)
            'Dim writer As New StreamWriter(New FileStream(csv_adj_import, FileMode.Open, FileAccess.Write), Encoding.UTF8)

            'Try
            '    Dim s = reader.ReadToEnd().Replace("product_id", "line_ids/product_id/id")
            '    s = s.Replace("location_id", "line_ids/location_id/id")
            '    s = s.Replace("prodlot_id", "line_ids/prod_lot_id/id")
            '    s = s.Replace("product_qty", "line_ids/product_qty")

            '    s = Replace(s, ",1,", txtVchrName.Text.Trim & "q    ,1,",, 1)

            '    writer.Write(s)
            '    writer.Close()
            '    reader.Close()

            'Catch ex As Exception
            '    MsgBox(ex.ToString)

            'End Try

        End If

        MsgBox("Over")


    End Sub
End Class