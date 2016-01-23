<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUninstalling
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUninstalling))
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.prgUninstall = New System.Windows.Forms.ProgressBar()
        Me.bgwUninstall = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(92, 27)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "正在卸载"
        '
        'prgUninstall
        '
        Me.prgUninstall.Location = New System.Drawing.Point(12, 48)
        Me.prgUninstall.Name = "prgUninstall"
        Me.prgUninstall.Size = New System.Drawing.Size(460, 23)
        Me.prgUninstall.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prgUninstall.TabIndex = 1
        '
        'bgwUninstall
        '
        Me.bgwUninstall.WorkerReportsProgress = True
        '
        'frmUninstalling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 83)
        Me.Controls.Add(Me.prgUninstall)
        Me.Controls.Add(Me.lblTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmUninstalling"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "模拟城市4 豪华版 自动安装程序"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents prgUninstall As System.Windows.Forms.ProgressBar
    Friend WithEvents bgwUninstall As System.ComponentModel.BackgroundWorker
End Class
