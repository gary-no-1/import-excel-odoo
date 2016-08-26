<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StartForm
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
        Me.btnImportExcel = New System.Windows.Forms.Button()
        Me.btnCheckMake = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btnCheckProduct = New System.Windows.Forms.Button()
        Me.lblProgressInfo = New System.Windows.Forms.Label()
        Me.btnCheckSerialNos = New System.Windows.Forms.Button()
        Me.btnCr8StockVchr = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.btnImportSerialNos = New System.Windows.Forms.Button()
        Me.btnSpecifyUser = New System.Windows.Forms.Button()
        Me.btnListPrice = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnImportExcel
        '
        Me.btnImportExcel.Location = New System.Drawing.Point(35, 36)
        Me.btnImportExcel.Name = "btnImportExcel"
        Me.btnImportExcel.Size = New System.Drawing.Size(147, 23)
        Me.btnImportExcel.TabIndex = 0
        Me.btnImportExcel.Text = "1. Import CSV Data"
        Me.btnImportExcel.UseVisualStyleBackColor = True
        '
        'btnCheckMake
        '
        Me.btnCheckMake.Enabled = False
        Me.btnCheckMake.Location = New System.Drawing.Point(209, 36)
        Me.btnCheckMake.Name = "btnCheckMake"
        Me.btnCheckMake.Size = New System.Drawing.Size(147, 23)
        Me.btnCheckMake.TabIndex = 1
        Me.btnCheckMake.Text = "2. Check Make"
        Me.btnCheckMake.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button3.Location = New System.Drawing.Point(607, 231)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(139, 44)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Close"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'btnCheckProduct
        '
        Me.btnCheckProduct.Location = New System.Drawing.Point(209, 80)
        Me.btnCheckProduct.Name = "btnCheckProduct"
        Me.btnCheckProduct.Size = New System.Drawing.Size(147, 23)
        Me.btnCheckProduct.TabIndex = 3
        Me.btnCheckProduct.Text = "3. Check - Import Products"
        Me.btnCheckProduct.UseVisualStyleBackColor = True
        '
        'lblProgressInfo
        '
        Me.lblProgressInfo.AutoSize = True
        Me.lblProgressInfo.Location = New System.Drawing.Point(56, 335)
        Me.lblProgressInfo.Name = "lblProgressInfo"
        Me.lblProgressInfo.Size = New System.Drawing.Size(39, 13)
        Me.lblProgressInfo.TabIndex = 4
        Me.lblProgressInfo.Text = "Label1"
        '
        'btnCheckSerialNos
        '
        Me.btnCheckSerialNos.Enabled = False
        Me.btnCheckSerialNos.Location = New System.Drawing.Point(607, 36)
        Me.btnCheckSerialNos.Name = "btnCheckSerialNos"
        Me.btnCheckSerialNos.Size = New System.Drawing.Size(147, 23)
        Me.btnCheckSerialNos.TabIndex = 5
        Me.btnCheckSerialNos.Text = "Check - Import Serial Nos"
        Me.btnCheckSerialNos.UseVisualStyleBackColor = True
        Me.btnCheckSerialNos.Visible = False
        '
        'btnCr8StockVchr
        '
        Me.btnCr8StockVchr.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCr8StockVchr.Enabled = False
        Me.btnCr8StockVchr.Location = New System.Drawing.Point(209, 168)
        Me.btnCr8StockVchr.Name = "btnCr8StockVchr"
        Me.btnCr8StockVchr.Size = New System.Drawing.Size(147, 44)
        Me.btnCr8StockVchr.TabIndex = 6
        Me.btnCr8StockVchr.Text = "5. Create Stock Import Voucher"
        Me.btnCr8StockVchr.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(659, 101)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 58)
        Me.Button4.TabIndex = 8
        Me.Button4.Text = "Data Connect Settings"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'btnImportSerialNos
        '
        Me.btnImportSerialNos.Enabled = False
        Me.btnImportSerialNos.Location = New System.Drawing.Point(209, 124)
        Me.btnImportSerialNos.Name = "btnImportSerialNos"
        Me.btnImportSerialNos.Size = New System.Drawing.Size(147, 23)
        Me.btnImportSerialNos.TabIndex = 9
        Me.btnImportSerialNos.Text = "4. Import Serial Nos"
        Me.btnImportSerialNos.UseVisualStyleBackColor = True
        '
        'btnSpecifyUser
        '
        Me.btnSpecifyUser.Location = New System.Drawing.Point(552, 101)
        Me.btnSpecifyUser.Name = "btnSpecifyUser"
        Me.btnSpecifyUser.Size = New System.Drawing.Size(75, 58)
        Me.btnSpecifyUser.TabIndex = 10
        Me.btnSpecifyUser.Text = "Specify User"
        Me.btnSpecifyUser.UseVisualStyleBackColor = True
        '
        'btnListPrice
        '
        Me.btnListPrice.Location = New System.Drawing.Point(209, 233)
        Me.btnListPrice.Name = "btnListPrice"
        Me.btnListPrice.Size = New System.Drawing.Size(147, 44)
        Me.btnListPrice.TabIndex = 11
        Me.btnListPrice.Text = "6. Create List Price csv for Import"
        Me.btnListPrice.UseVisualStyleBackColor = True
        '
        'StartForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Highlight
        Me.ClientSize = New System.Drawing.Size(772, 375)
        Me.Controls.Add(Me.btnListPrice)
        Me.Controls.Add(Me.btnSpecifyUser)
        Me.Controls.Add(Me.btnImportSerialNos)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.btnCr8StockVchr)
        Me.Controls.Add(Me.btnCheckSerialNos)
        Me.Controls.Add(Me.lblProgressInfo)
        Me.Controls.Add(Me.btnCheckProduct)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.btnCheckMake)
        Me.Controls.Add(Me.btnImportExcel)
        Me.MaximizeBox = False
        Me.Name = "StartForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Data from Excel to Odoo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnImportExcel As Button
    Friend WithEvents btnCheckMake As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents btnCheckProduct As Button
    Friend WithEvents lblProgressInfo As Label
    Friend WithEvents btnCheckSerialNos As Button
    Friend WithEvents btnCr8StockVchr As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents btnImportSerialNos As Button
    Friend WithEvents btnSpecifyUser As Button
    Friend WithEvents btnListPrice As Button
End Class
