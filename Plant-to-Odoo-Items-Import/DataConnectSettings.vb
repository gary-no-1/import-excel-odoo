Imports Ini.Net
Imports System.IO

Public Class DataConnectSettings
    Private Sub DataConnectSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        check_init_working_folder()

        If System.IO.File.Exists(WorkingFolder + "\P-to-O.ini") Then
            Dim iniFile = New IniFile(WorkingFolder + "\P-to-O.ini")

            txtServer.Text = iniFile.ReadString("Server", "Server Name")
            txtPort.Text = iniFile.ReadString("Server", "Server Port")
            txtDatabase.Text = iniFile.ReadString("Server", "Database Name")
            txtUser.Text = iniFile.ReadString("Server", "User Id")
            txtPassword.Text = iniFile.ReadString("Server", "Password")
        End If


    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim iniFile = New IniFile(WorkingFolder + "\P-to-O.ini")
        iniFile.WriteString("Server", "Server Name", txtServer.Text)
        iniFile.WriteString("Server", "Server Port", txtPort.Text)
        iniFile.WriteString("Server", "Database Name", txtDatabase.Text)
        iniFile.WriteString("Server", "User Id", txtUser.Text)
        iniFile.WriteString("Server", "Password", txtPassword.Text)

        Me.Close()

    End Sub
End Class