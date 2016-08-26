<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportFromExcel
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtExcelFileName = New System.Windows.Forms.TextBox()
        Me.btnLocateExcelFile = New System.Windows.Forms.Button()
        Me.OpenFD = New System.Windows.Forms.OpenFileDialog()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.lblRecordsImported = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(38, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "CSV File to Import"
        '
        'txtExcelFileName
        '
        Me.txtExcelFileName.Location = New System.Drawing.Point(141, 23)
        Me.txtExcelFileName.Name = "txtExcelFileName"
        Me.txtExcelFileName.Size = New System.Drawing.Size(493, 20)
        Me.txtExcelFileName.TabIndex = 1
        '
        'btnLocateExcelFile
        '
        Me.btnLocateExcelFile.Location = New System.Drawing.Point(664, 19)
        Me.btnLocateExcelFile.Name = "btnLocateExcelFile"
        Me.btnLocateExcelFile.Size = New System.Drawing.Size(75, 23)
        Me.btnLocateExcelFile.TabIndex = 2
        Me.btnLocateExcelFile.Text = "Select"
        Me.btnLocateExcelFile.UseVisualStyleBackColor = True
        '
        'OpenFD
        '
        Me.OpenFD.FileName = "OpenFileDialog1"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(664, 115)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(538, 114)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(75, 23)
        Me.btnImport.TabIndex = 4
        Me.btnImport.Text = "Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'lblRecordsImported
        '
        Me.lblRecordsImported.AutoSize = True
        Me.lblRecordsImported.Location = New System.Drawing.Point(59, 124)
        Me.lblRecordsImported.Name = "lblRecordsImported"
        Me.lblRecordsImported.Size = New System.Drawing.Size(0, 13)
        Me.lblRecordsImported.TabIndex = 5
        '
        'ImportFromExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(762, 261)
        Me.Controls.Add(Me.lblRecordsImported)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnLocateExcelFile)
        Me.Controls.Add(Me.txtExcelFileName)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ImportFromExcel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ImportFromCSV"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtExcelFileName As TextBox
    Friend WithEvents btnLocateExcelFile As Button
    Friend WithEvents OpenFD As OpenFileDialog
    Friend WithEvents btnClose As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents lblRecordsImported As Label
End Class
