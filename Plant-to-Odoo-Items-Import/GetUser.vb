Imports Npgsql

Public Class GetUser
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub GetUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        check_init_working_folder()
        init_db_variables()

        txtCurrentUser.Text = My.User.Name
    End Sub

    Private Sub btnCheckUser_Click(sender As Object, e As EventArgs) Handles btnCheckUser.Click
        Dim odoo_user As String = txtOdooUser.Text.Trim.ToLower

        Dim PgCon As NpgsqlConnection = New NpgsqlConnection(pg_conn_string)
        PgCon.ConnectionString = pg_conn_string

        Dim pgCommand As NpgsqlCommand = New NpgsqlCommand
        pgCommand.Connection = PgCon
        pgCommand.CommandType = CommandType.Text

        pgCommand.CommandText = "SELECT d.id, e.name " &
            "From res_users d " &
            "Join res_partner e " &
            "ON d.partner_id = e.id " &
            "WHERE d.id IN (SELECT uid " &
            "FROM res_groups_users_rel c " &
            "JOIN res_groups a " &
            "ON c.gid = a.id " &
            "JOIN ir_module_category b " &
            "ON a.category_id = b.id " &
            "WHERE b.name Like '%Warehouse') " &
            "AND d.login = '" & odoo_user & "' " &
            "AND d.active"

        Dim sda As NpgsqlDataAdapter
        Dim ds As DataSet
        ds = New DataSet

        Try
            sda = New NpgsqlDataAdapter(pgCommand)
            sda.Fill(ds, "OdooUsers")

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

        Dim odoo_user_count As Integer = ds.Tables("OdooUsers").Rows.Count

        If odoo_user_count = 0 Then
            MsgBox("No Such Odoo User")
            odoo_user_id = 0
            odoo_user_email = ""
            Return
        End If
        If odoo_user_count > 1 Then
            MsgBox("Too many Odoo Users with this name")
            odoo_user_id = 0
            odoo_user_email = ""
            Return
        End If

        odoo_user_id = ds.Tables("OdooUsers")(0)(0)
        odoo_user_email = ds.Tables("OdooUsers")(0)(1)

        'MsgBox(odoo_user_id)
        'MsgBox(odoo_user_email)

        Me.Close()
    End Sub
End Class