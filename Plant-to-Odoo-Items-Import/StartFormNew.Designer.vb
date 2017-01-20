<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StartFormNew
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
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.btnSpecifyUser = New System.Windows.Forms.Button()
        Me.btnDataSettings = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lblSOprogress = New System.Windows.Forms.Label()
        Me.btnISoClose = New System.Windows.Forms.Button()
        Me.btnImpCustomers = New System.Windows.Forms.Button()
        Me.btnSOimport = New System.Windows.Forms.Button()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnIIClose = New System.Windows.Forms.Button()
        Me.lblProgressInfo = New System.Windows.Forms.Label()
        Me.btnListPrice = New System.Windows.Forms.Button()
        Me.btnImportSerialNos = New System.Windows.Forms.Button()
        Me.btnCr8StockVchr = New System.Windows.Forms.Button()
        Me.btnCheckProduct = New System.Windows.Forms.Button()
        Me.btnCheckMake = New System.Windows.Forms.Button()
        Me.btnImportExcel = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.btnISOsaleorder = New System.Windows.Forms.Button()
        Me.TabPage3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.Orange
        Me.TabPage3.Controls.Add(Me.btnSpecifyUser)
        Me.TabPage3.Controls.Add(Me.btnDataSettings)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(794, 321)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Setup"
        '
        'btnSpecifyUser
        '
        Me.btnSpecifyUser.Location = New System.Drawing.Point(306, 131)
        Me.btnSpecifyUser.Name = "btnSpecifyUser"
        Me.btnSpecifyUser.Size = New System.Drawing.Size(75, 58)
        Me.btnSpecifyUser.TabIndex = 12
        Me.btnSpecifyUser.Text = "Specify User"
        Me.btnSpecifyUser.UseVisualStyleBackColor = True
        '
        'btnDataSettings
        '
        Me.btnDataSettings.Location = New System.Drawing.Point(413, 131)
        Me.btnDataSettings.Name = "btnDataSettings"
        Me.btnDataSettings.Size = New System.Drawing.Size(75, 58)
        Me.btnDataSettings.TabIndex = 11
        Me.btnDataSettings.Text = "Data Connect Settings"
        Me.btnDataSettings.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.LawnGreen
        Me.TabPage2.Controls.Add(Me.btnISOsaleorder)
        Me.TabPage2.Controls.Add(Me.lblSOprogress)
        Me.TabPage2.Controls.Add(Me.btnISoClose)
        Me.TabPage2.Controls.Add(Me.btnImpCustomers)
        Me.TabPage2.Controls.Add(Me.btnSOimport)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(794, 321)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Import Sales Orders"
        '
        'lblSOprogress
        '
        Me.lblSOprogress.AutoSize = True
        Me.lblSOprogress.Location = New System.Drawing.Point(58, 251)
        Me.lblSOprogress.Name = "lblSOprogress"
        Me.lblSOprogress.Size = New System.Drawing.Size(16, 13)
        Me.lblSOprogress.TabIndex = 28
        Me.lblSOprogress.Text = "---"
        '
        'btnISoClose
        '
        Me.btnISoClose.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnISoClose.Location = New System.Drawing.Point(589, 235)
        Me.btnISoClose.Name = "btnISoClose"
        Me.btnISoClose.Size = New System.Drawing.Size(139, 44)
        Me.btnISoClose.TabIndex = 27
        Me.btnISoClose.Text = "Close"
        Me.btnISoClose.UseVisualStyleBackColor = True
        '
        'btnImpCustomers
        '
        Me.btnImpCustomers.Location = New System.Drawing.Point(265, 37)
        Me.btnImpCustomers.Name = "btnImpCustomers"
        Me.btnImpCustomers.Size = New System.Drawing.Size(163, 23)
        Me.btnImpCustomers.TabIndex = 22
        Me.btnImpCustomers.Text = "2. Check - Create Customers"
        Me.btnImpCustomers.UseVisualStyleBackColor = True
        '
        'btnSOimport
        '
        Me.btnSOimport.Location = New System.Drawing.Point(61, 37)
        Me.btnSOimport.Name = "btnSOimport"
        Me.btnSOimport.Size = New System.Drawing.Size(147, 23)
        Me.btnSOimport.TabIndex = 20
        Me.btnSOimport.Text = "1. Import CSV Data"
        Me.btnSOimport.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Highlight
        Me.TabPage1.Controls.Add(Me.btnIIClose)
        Me.TabPage1.Controls.Add(Me.lblProgressInfo)
        Me.TabPage1.Controls.Add(Me.btnListPrice)
        Me.TabPage1.Controls.Add(Me.btnImportSerialNos)
        Me.TabPage1.Controls.Add(Me.btnCr8StockVchr)
        Me.TabPage1.Controls.Add(Me.btnCheckProduct)
        Me.TabPage1.Controls.Add(Me.btnCheckMake)
        Me.TabPage1.Controls.Add(Me.btnImportExcel)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(794, 321)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Import Items"
        '
        'btnIIClose
        '
        Me.btnIIClose.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnIIClose.Location = New System.Drawing.Point(620, 235)
        Me.btnIIClose.Name = "btnIIClose"
        Me.btnIIClose.Size = New System.Drawing.Size(139, 44)
        Me.btnIIClose.TabIndex = 26
        Me.btnIIClose.Text = "Close"
        Me.btnIIClose.UseVisualStyleBackColor = True
        '
        'lblProgressInfo
        '
        Me.lblProgressInfo.AutoSize = True
        Me.lblProgressInfo.Location = New System.Drawing.Point(77, 266)
        Me.lblProgressInfo.Name = "lblProgressInfo"
        Me.lblProgressInfo.Size = New System.Drawing.Size(39, 13)
        Me.lblProgressInfo.TabIndex = 25
        Me.lblProgressInfo.Text = "Label1"
        '
        'btnListPrice
        '
        Me.btnListPrice.Location = New System.Drawing.Point(473, 101)
        Me.btnListPrice.Name = "btnListPrice"
        Me.btnListPrice.Size = New System.Drawing.Size(147, 44)
        Me.btnListPrice.TabIndex = 24
        Me.btnListPrice.Text = "6. Create List Price csv for Import"
        Me.btnListPrice.UseVisualStyleBackColor = True
        '
        'btnImportSerialNos
        '
        Me.btnImportSerialNos.Enabled = False
        Me.btnImportSerialNos.Location = New System.Drawing.Point(277, 122)
        Me.btnImportSerialNos.Name = "btnImportSerialNos"
        Me.btnImportSerialNos.Size = New System.Drawing.Size(147, 23)
        Me.btnImportSerialNos.TabIndex = 23
        Me.btnImportSerialNos.Text = "4. Import Serial Nos"
        Me.btnImportSerialNos.UseVisualStyleBackColor = True
        '
        'btnCr8StockVchr
        '
        Me.btnCr8StockVchr.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCr8StockVchr.Enabled = False
        Me.btnCr8StockVchr.Location = New System.Drawing.Point(473, 40)
        Me.btnCr8StockVchr.Name = "btnCr8StockVchr"
        Me.btnCr8StockVchr.Size = New System.Drawing.Size(147, 44)
        Me.btnCr8StockVchr.TabIndex = 22
        Me.btnCr8StockVchr.Text = "5. Create Stock Import Voucher"
        Me.btnCr8StockVchr.UseVisualStyleBackColor = True
        '
        'btnCheckProduct
        '
        Me.btnCheckProduct.Location = New System.Drawing.Point(277, 81)
        Me.btnCheckProduct.Name = "btnCheckProduct"
        Me.btnCheckProduct.Size = New System.Drawing.Size(147, 23)
        Me.btnCheckProduct.TabIndex = 21
        Me.btnCheckProduct.Text = "3. Check - Import Products"
        Me.btnCheckProduct.UseVisualStyleBackColor = True
        '
        'btnCheckMake
        '
        Me.btnCheckMake.Enabled = False
        Me.btnCheckMake.Location = New System.Drawing.Point(277, 40)
        Me.btnCheckMake.Name = "btnCheckMake"
        Me.btnCheckMake.Size = New System.Drawing.Size(147, 23)
        Me.btnCheckMake.TabIndex = 20
        Me.btnCheckMake.Text = "2. Check Make"
        Me.btnCheckMake.UseVisualStyleBackColor = True
        '
        'btnImportExcel
        '
        Me.btnImportExcel.Location = New System.Drawing.Point(80, 40)
        Me.btnImportExcel.Name = "btnImportExcel"
        Me.btnImportExcel.Size = New System.Drawing.Size(147, 23)
        Me.btnImportExcel.TabIndex = 19
        Me.btnImportExcel.Text = "1. Import CSV Data"
        Me.btnImportExcel.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(-1, -1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(802, 347)
        Me.TabControl1.TabIndex = 0
        '
        'btnISOsaleorder
        '
        Me.btnISOsaleorder.Location = New System.Drawing.Point(265, 109)
        Me.btnISOsaleorder.Name = "btnISOsaleorder"
        Me.btnISOsaleorder.Size = New System.Drawing.Size(163, 23)
        Me.btnISOsaleorder.TabIndex = 29
        Me.btnISOsaleorder.Text = "3. Check - Create Sales Orders"
        Me.btnISOsaleorder.UseVisualStyleBackColor = True
        '
        'StartFormNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 382)
        Me.Controls.Add(Me.TabControl1)
        Me.MaximizeBox = False
        Me.Name = "StartFormNew"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "StartFormNew"
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents lblProgressInfo As Label
    Friend WithEvents btnListPrice As Button
    Friend WithEvents btnImportSerialNos As Button
    Friend WithEvents btnCr8StockVchr As Button
    Friend WithEvents btnCheckProduct As Button
    Friend WithEvents btnCheckMake As Button
    Friend WithEvents btnImportExcel As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents btnIIClose As Button
    Friend WithEvents btnSpecifyUser As Button
    Friend WithEvents btnDataSettings As Button
    Friend WithEvents btnSOimport As Button
    Friend WithEvents btnImpCustomers As Button
    Friend WithEvents btnISoClose As Button
    Friend WithEvents lblSOprogress As Label
    Friend WithEvents btnISOsaleorder As Button
End Class
