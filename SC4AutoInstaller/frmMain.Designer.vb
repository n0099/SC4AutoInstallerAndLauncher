<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.btnInstall = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.btnChangeModule = New System.Windows.Forms.Button()
        Me.btnUninstall = New System.Windows.Forms.Button()
        Me.bgwVerifySC4Version = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout()
        '
        'btnInstall
        '
        Me.btnInstall.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnInstall.Location = New System.Drawing.Point(260, 171)
        Me.btnInstall.Name = "btnInstall"
        Me.btnInstall.Size = New System.Drawing.Size(120, 40)
        Me.btnInstall.TabIndex = 0
        Me.btnInstall.Text = "安装"
        Me.btnInstall.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnExit.Location = New System.Drawing.Point(270, 285)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 30)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "退出"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnAbout
        '
        Me.btnAbout.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAbout.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnAbout.Location = New System.Drawing.Point(270, 233)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(100, 30)
        Me.btnAbout.TabIndex = 3
        Me.btnAbout.Text = "关于"
        Me.btnAbout.UseVisualStyleBackColor = True
        '
        'btnChangeModule
        '
        Me.btnChangeModule.Enabled = False
        Me.btnChangeModule.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnChangeModule.Location = New System.Drawing.Point(260, 171)
        Me.btnChangeModule.Name = "btnChangeModule"
        Me.btnChangeModule.Size = New System.Drawing.Size(120, 40)
        Me.btnChangeModule.TabIndex = 1
        Me.btnChangeModule.Text = "更改"
        Me.btnChangeModule.UseVisualStyleBackColor = True
        Me.btnChangeModule.Visible = False
        '
        'btnUninstall
        '
        Me.btnUninstall.Enabled = False
        Me.btnUninstall.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnUninstall.Location = New System.Drawing.Point(260, 226)
        Me.btnUninstall.Name = "btnUninstall"
        Me.btnUninstall.Size = New System.Drawing.Size(120, 40)
        Me.btnUninstall.TabIndex = 2
        Me.btnUninstall.Text = "卸载"
        Me.btnUninstall.UseVisualStyleBackColor = True
        Me.btnUninstall.Visible = False
        '
        'bgwVerifySC4Version
        '
        '
        'frmMain
        '
        Me.AcceptButton = Me.btnInstall
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SC4AutoInstaller.My.Resources.Resources.background
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(635, 453)
        Me.Controls.Add(Me.btnUninstall)
        Me.Controls.Add(Me.btnChangeModule)
        Me.Controls.Add(Me.btnAbout)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnInstall)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "模拟城市4 豪华版 自动安装程序"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnInstall As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnAbout As System.Windows.Forms.Button
    Friend WithEvents btnChangeModule As System.Windows.Forms.Button
    Friend WithEvents btnUninstall As System.Windows.Forms.Button
    Friend WithEvents bgwVerifySC4Version As System.ComponentModel.BackgroundWorker

End Class
