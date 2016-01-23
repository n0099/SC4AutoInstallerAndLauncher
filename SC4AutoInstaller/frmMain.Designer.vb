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
        Me.btnCustomInstall = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.btnChangeModule = New System.Windows.Forms.Button()
        Me.btnUninstall = New System.Windows.Forms.Button()
        Me.bgwVerifySC4Version = New System.ComponentModel.BackgroundWorker()
        Me.btnQuickInstall = New System.Windows.Forms.Button()
        Me.fbdSC4InstallDir = New System.Windows.Forms.FolderBrowserDialog()
        Me.bgwCheckUpdate = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout()
        '
        'btnCustomInstall
        '
        Me.btnCustomInstall.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnCustomInstall.Location = New System.Drawing.Point(255, 223)
        Me.btnCustomInstall.Name = "btnCustomInstall"
        Me.btnCustomInstall.Size = New System.Drawing.Size(130, 40)
        Me.btnCustomInstall.TabIndex = 1
        Me.btnCustomInstall.Text = "自定义安装"
        Me.btnCustomInstall.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnExit.Location = New System.Drawing.Point(265, 320)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(110, 33)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "退出"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnAbout
        '
        Me.btnAbout.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAbout.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnAbout.Location = New System.Drawing.Point(265, 281)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(110, 33)
        Me.btnAbout.TabIndex = 4
        Me.btnAbout.Text = "关于"
        Me.btnAbout.UseVisualStyleBackColor = True
        '
        'btnChangeModule
        '
        Me.btnChangeModule.Enabled = False
        Me.btnChangeModule.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnChangeModule.Location = New System.Drawing.Point(255, 168)
        Me.btnChangeModule.Name = "btnChangeModule"
        Me.btnChangeModule.Size = New System.Drawing.Size(130, 40)
        Me.btnChangeModule.TabIndex = 2
        Me.btnChangeModule.Text = "更改"
        Me.btnChangeModule.UseVisualStyleBackColor = True
        Me.btnChangeModule.Visible = False
        '
        'btnUninstall
        '
        Me.btnUninstall.Enabled = False
        Me.btnUninstall.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnUninstall.Location = New System.Drawing.Point(255, 223)
        Me.btnUninstall.Name = "btnUninstall"
        Me.btnUninstall.Size = New System.Drawing.Size(130, 40)
        Me.btnUninstall.TabIndex = 3
        Me.btnUninstall.Text = "卸载"
        Me.btnUninstall.UseVisualStyleBackColor = True
        Me.btnUninstall.Visible = False
        '
        'bgwVerifySC4Version
        '
        '
        'btnQuickInstall
        '
        Me.btnQuickInstall.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnQuickInstall.Location = New System.Drawing.Point(255, 168)
        Me.btnQuickInstall.Name = "btnQuickInstall"
        Me.btnQuickInstall.Size = New System.Drawing.Size(130, 40)
        Me.btnQuickInstall.TabIndex = 0
        Me.btnQuickInstall.Text = "快速安装"
        Me.btnQuickInstall.UseVisualStyleBackColor = True
        '
        'fbdSC4InstallDir
        '
        Me.fbdSC4InstallDir.Description = "请选择模拟城市4 安装目录"
        Me.fbdSC4InstallDir.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'bgwCheckUpdate
        '
        Me.bgwCheckUpdate.WorkerSupportsCancellation = True
        '
        'frmMain
        '
        Me.AcceptButton = Me.btnQuickInstall
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SC4AutoInstaller.My.Resources.Resources.background
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(635, 453)
        Me.Controls.Add(Me.btnAbout)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnQuickInstall)
        Me.Controls.Add(Me.btnCustomInstall)
        Me.Controls.Add(Me.btnUninstall)
        Me.Controls.Add(Me.btnChangeModule)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "模拟城市4 豪华版 自动安装程序"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCustomInstall As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnAbout As System.Windows.Forms.Button
    Friend WithEvents btnChangeModule As System.Windows.Forms.Button
    Friend WithEvents btnUninstall As System.Windows.Forms.Button
    Friend WithEvents bgwVerifySC4Version As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnQuickInstall As Button
    Friend WithEvents fbdSC4InstallDir As FolderBrowserDialog
    Friend WithEvents bgwCheckUpdate As System.ComponentModel.BackgroundWorker
End Class
