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
        Me.btnLaunch = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnSetting = New System.Windows.Forms.Button()
        Me.fbdSC4InstallDir = New System.Windows.Forms.FolderBrowserDialog()
        Me.bgwCheckUpdate = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout()
        '
        'btnLaunch
        '
        Me.btnLaunch.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnLaunch.Location = New System.Drawing.Point(432, 350)
        Me.btnLaunch.Name = "btnLaunch"
        Me.btnLaunch.Size = New System.Drawing.Size(90, 80)
        Me.btnLaunch.TabIndex = 0
        Me.btnLaunch.Text = "启动"
        Me.btnLaunch.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnExit.Location = New System.Drawing.Point(537, 395)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 35)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "退出"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSetting
        '
        Me.btnSetting.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnSetting.Location = New System.Drawing.Point(537, 350)
        Me.btnSetting.Name = "btnSetting"
        Me.btnSetting.Size = New System.Drawing.Size(75, 35)
        Me.btnSetting.TabIndex = 1
        Me.btnSetting.Text = "设置"
        Me.btnSetting.UseVisualStyleBackColor = True
        '
        'fbdSC4InstallDir
        '
        Me.fbdSC4InstallDir.Description = "请选择模拟城市4 安装目录"
        Me.fbdSC4InstallDir.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'bgwCheckUpdate
        '
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(640, 480)
        Me.Controls.Add(Me.btnSetting)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnLaunch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "模拟城市4 启动器"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnLaunch As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSetting As System.Windows.Forms.Button
    Friend WithEvents fbdSC4InstallDir As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents bgwCheckUpdate As System.ComponentModel.BackgroundWorker
End Class
