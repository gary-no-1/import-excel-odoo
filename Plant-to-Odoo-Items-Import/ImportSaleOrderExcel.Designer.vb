<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImportSaleOrderExcel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImportSaleOrderExcel))
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnLocateExcelFile = New System.Windows.Forms.Button()
        Me.txtExcelFileName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OpenFD = New System.Windows.Forms.OpenFileDialog()
        Me.lblRecordsImported = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(508, 124)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(75, 23)
        Me.btnImport.TabIndex = 9
        Me.btnImport.Text = "Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(634, 125)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnLocateExcelFile
        '
        Me.btnLocateExcelFile.Location = New System.Drawing.Point(634, 28)
        Me.btnLocateExcelFile.Name = "btnLocateExcelFile"
        Me.btnLocateExcelFile.Size = New System.Drawing.Size(75, 23)
        Me.btnLocateExcelFile.TabIndex = 7
        Me.btnLocateExcelFile.Text = "Select"
        Me.btnLocateExcelFile.UseVisualStyleBackColor = True
        '
        'txtExcelFileName
        '
        Me.txtExcelFileName.Location = New System.Drawing.Point(111, 29)
        Me.txtExcelFileName.Name = "txtExcelFileName"
        Me.txtExcelFileName.Size = New System.Drawing.Size(493, 20)
        Me.txtExcelFileName.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "CSV File to Import"
        '
        'OpenFD
        '
        Me.OpenFD.FileName = "OpenFileDialog1"
        '
        'lblRecordsImported
        '
        Me.lblRecordsImported.AutoSize = True
        Me.lblRecordsImported.Location = New System.Drawing.Point(46, 129)
        Me.lblRecordsImported.Name = "lblRecordsImported"
        Me.lblRecordsImported.Size = New System.Drawing.Size(22, 13)
        Me.lblRecordsImported.TabIndex = 10
        Me.lblRecordsImported.Text = "-----"
        '
        'ImportSaleOrderExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.PaleGreen
        Me.ClientSize = New System.Drawing.Size(716, 182)
        Me.Controls.Add(Me.lblRecordsImported)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnLocateExcelFile)
        Me.Controls.Add(Me.txtExcelFileName)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ImportSaleOrderExcel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import SaleOrders from CSV File"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnImport As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnLocateExcelFile As Button
    Friend WithEvents txtExcelFileName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents OpenFD As OpenFileDialog
    Friend WithEvents lblRecordsImported As Label
End Class
