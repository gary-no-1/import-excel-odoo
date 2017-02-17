<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GetUser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GetUser))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCurrentUser = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtOdooUser = New System.Windows.Forms.TextBox()
        Me.btnCheckUser = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnClose.Location = New System.Drawing.Point(300, 210)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(40, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Current User"
        '
        'txtCurrentUser
        '
        Me.txtCurrentUser.Location = New System.Drawing.Point(146, 24)
        Me.txtCurrentUser.Name = "txtCurrentUser"
        Me.txtCurrentUser.ReadOnly = True
        Me.txtCurrentUser.Size = New System.Drawing.Size(416, 20)
        Me.txtCurrentUser.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(43, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Odoo User Email "
        '
        'txtOdooUser
        '
        Me.txtOdooUser.Location = New System.Drawing.Point(146, 63)
        Me.txtOdooUser.Name = "txtOdooUser"
        Me.txtOdooUser.Size = New System.Drawing.Size(416, 20)
        Me.txtOdooUser.TabIndex = 4
        '
        'btnCheckUser
        '
        Me.btnCheckUser.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCheckUser.Location = New System.Drawing.Point(300, 112)
        Me.btnCheckUser.Name = "btnCheckUser"
        Me.btnCheckUser.Size = New System.Drawing.Size(75, 41)
        Me.btnCheckUser.TabIndex = 5
        Me.btnCheckUser.Text = "Check Odoo User Validity"
        Me.btnCheckUser.UseVisualStyleBackColor = True
        '
        'GetUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(675, 261)
        Me.Controls.Add(Me.btnCheckUser)
        Me.Controls.Add(Me.txtOdooUser)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCurrentUser)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnClose)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "GetUser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GetUser"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnClose As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtCurrentUser As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtOdooUser As TextBox
    Friend WithEvents btnCheckUser As Button
End Class
