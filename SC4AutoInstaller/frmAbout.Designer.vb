<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbout
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

    Friend WithEvents btnClose As System.Windows.Forms.Button

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblAuthor = New System.Windows.Forms.Label()
        Me.llbBlog = New System.Windows.Forms.LinkLabel()
        Me.llbReportBug = New System.Windows.Forms.LinkLabel()
        Me.llbSCB = New System.Windows.Forms.LinkLabel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.lblThanksList = New System.Windows.Forms.Label()
        Me.llbSCCN = New System.Windows.Forms.LinkLabel()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(397, 278)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "关闭(&C)"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(142, 54)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(44, 17)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = "版本号"
        '
        'lblAuthor
        '
        Me.lblAuthor.AutoSize = True
        Me.lblAuthor.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblAuthor.Location = New System.Drawing.Point(333, 54)
        Me.lblAuthor.Name = "lblAuthor"
        Me.lblAuthor.Size = New System.Drawing.Size(79, 17)
        Me.lblAuthor.TabIndex = 2
        Me.lblAuthor.Text = "作者：n0099"
        '
        'llbBlog
        '
        Me.llbBlog.AutoSize = True
        Me.llbBlog.Location = New System.Drawing.Point(303, 283)
        Me.llbBlog.Name = "llbBlog"
        Me.llbBlog.Size = New System.Drawing.Size(71, 12)
        Me.llbBlog.TabIndex = 5
        Me.llbBlog.TabStop = True
        Me.llbBlog.Text = "n0099的博客"
        '
        'llbReportBug
        '
        Me.llbReportBug.AutoSize = True
        Me.llbReportBug.Location = New System.Drawing.Point(250, 283)
        Me.llbReportBug.Name = "llbReportBug"
        Me.llbReportBug.Size = New System.Drawing.Size(47, 12)
        Me.llbReportBug.TabIndex = 6
        Me.llbReportBug.TabStop = True
        Me.llbReportBug.Text = "BUG反馈"
        '
        'llbSCB
        '
        Me.llbSCB.AutoSize = True
        Me.llbSCB.Location = New System.Drawing.Point(12, 283)
        Me.llbSCB.Name = "llbSCB"
        Me.llbSCB.Size = New System.Drawing.Size(89, 12)
        Me.llbSCB.TabIndex = 4
        Me.llbSCB.TabStop = True
        Me.llbSCB.Text = "百度模拟城市吧"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(139, 20)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(282, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "模拟城市4 豪华版 自动安装程序"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picLogo
        '
        Me.picLogo.Image = CType(resources.GetObject("picLogo.Image"), System.Drawing.Image)
        Me.picLogo.Location = New System.Drawing.Point(12, 11)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(124, 233)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picLogo.TabIndex = 12
        Me.picLogo.TabStop = False
        '
        'lblThanksList
        '
        Me.lblThanksList.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblThanksList.Location = New System.Drawing.Point(142, 82)
        Me.lblThanksList.Name = "lblThanksList"
        Me.lblThanksList.Size = New System.Drawing.Size(330, 193)
        Me.lblThanksList.TabIndex = 3
        Me.lblThanksList.Text = resources.GetString("lblThanksList.Text")
        '
        'llbSCCN
        '
        Me.llbSCCN.AutoSize = True
        Me.llbSCCN.Location = New System.Drawing.Point(107, 283)
        Me.llbSCCN.Name = "llbSCCN"
        Me.llbSCCN.Size = New System.Drawing.Size(137, 12)
        Me.llbSCCN.TabIndex = 13
        Me.llbSCCN.TabStop = True
        Me.llbSCCN.Text = "模拟城市中文网（SCCN）"
        '
        'frmAbout
        '
        Me.AcceptButton = Me.btnClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(484, 312)
        Me.Controls.Add(Me.llbSCCN)
        Me.Controls.Add(Me.lblThanksList)
        Me.Controls.Add(Me.picLogo)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.llbSCB)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.llbReportBug)
        Me.Controls.Add(Me.lblAuthor)
        Me.Controls.Add(Me.llbBlog)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.Padding = New System.Windows.Forms.Padding(9, 8, 9, 8)
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "关于"
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblAuthor As System.Windows.Forms.Label
    Friend WithEvents llbBlog As System.Windows.Forms.LinkLabel
    Friend WithEvents llbReportBug As System.Windows.Forms.LinkLabel
    Friend WithEvents llbSCB As System.Windows.Forms.LinkLabel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblThanksList As System.Windows.Forms.Label
    Friend WithEvents llbSCCN As System.Windows.Forms.LinkLabel

End Class
