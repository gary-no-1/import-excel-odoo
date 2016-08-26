Imports Ini.Net
Imports System.IO

Module AllSubsModule
    Public WorkingFolder As String
    Public sqlce_conn_string As String
    Public sqlCe_full_path As String
    Public pg_conn_string As String
    Public odoo_user_id As Integer
    Public odoo_user_email As String

    Sub DataTable2CSV(ByVal table As DataTable, ByVal filename As String,
        ByVal sepChar As String)
        ' copied from http://www.devx.com/vb2themax/Tip/19703
        Dim writer As System.IO.StreamWriter
        Try
            writer = New System.IO.StreamWriter(filename)

            ' first write a line with the columns name
            Dim sep As String = ""
            Dim builder As New System.Text.StringBuilder
            For Each col As DataColumn In table.Columns
                builder.Append(sep).Append(col.ColumnName)
                sep = sepChar
            Next
            writer.WriteLine(builder.ToString())

            ' then write all the rows
            For Each row As DataRow In table.Rows
                sep = ""
                builder = New System.Text.StringBuilder

                For Each col As DataColumn In table.Columns
                    builder.Append(sep).Append(row(col.ColumnName))
                    sep = sepChar
                Next
                writer.WriteLine(builder.ToString())
            Next
        Finally
            If Not writer Is Nothing Then writer.Close()
        End Try
    End Sub

    Sub init_db_variables()
        ' refer http://stackoverflow.com/questions/4529862/declare-global-variables-in-visual-studio-2010-and-vb-net

        pg_conn_string = ""

        If System.IO.File.Exists(WorkingFolder + "\P-to-O.ini") Then
            Dim iniFile = New IniFile(WorkingFolder + "\P-to-O.ini")

            pg_conn_string = "Server = " & iniFile.ReadString("Server", "Server Name").Trim & ";"
            pg_conn_string = pg_conn_string & "Port=" &
                iniFile.ReadString("Server", "Server Port").Trim & ";"
            pg_conn_string = pg_conn_string & "Database=" &
                iniFile.ReadString("Server", "Database Name").Trim & ";"
            pg_conn_string = pg_conn_string & "User Id=" &
                iniFile.ReadString("Server", "User Id").Trim & ";"
        End If
        'pg_conn_string = "Server = 192.168.1.37;Port=5432;" _
        '    + "Database=grpl-prod;User Id=odoo;"

    End Sub

    Sub check_init_working_folder()
        Dim folderName As String = "PlantToOdoo"
        'WorkingFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\" & folderName
        WorkingFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\" & folderName

        Try
            If (Not System.IO.Directory.Exists(WorkingFolder)) Then
                System.IO.Directory.CreateDirectory(WorkingFolder)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        With My.Application.Log.DefaultFileLogWriter
            ' Change the location of default log file to the User's temp directory.
            ' Note, user may not have access to other locations.
            '.Location = Microsoft.VisualBasic.Logging.LogFileLocation.TempDirectory
            .CustomLocation = WorkingFolder
            .Delimiter = ","
            ' To append instead of overwriting the file each time
            .Append = True
            ' This is good feature, include today's date in the log file 
            ' Every day new file will be created.
            .LogFileCreationSchedule = Microsoft.VisualBasic.Logging.LogFileCreationScheduleOption.Daily
        End With

    End Sub
End Module
