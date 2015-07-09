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

    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As System.Windows.Forms.Button

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblAuthor = New System.Windows.Forms.Label()
        Me.llbBlog = New System.Windows.Forms.LinkLabel()
        Me.llbReportBug = New System.Windows.Forms.LinkLabel()
        Me.llbSCB = New System.Windows.Forms.LinkLabel()
        Me.lblName = New System.Windows.Forms.Label()
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox()
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDescription.Location = New System.Drawing.Point(145, 103)
        Me.txtDescription.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReadOnly = True
        Me.txtDescription.Size = New System.Drawing.Size(255, 76)
        Me.txtDescription.TabIndex = 3
        Me.txtDescription.TabStop = False
        Me.txtDescription.Text = "鸣谢：虚无中在飘渺、铁木真的使臣、绿色食品台风、坐在鸭子、周瑜K了Zero、cjak007"
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(327, 221)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "关闭(&C)"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(142, 54)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(29, 12)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = "版本"
        '
        'lblAuthor
        '
        Me.lblAuthor.AutoSize = True
        Me.lblAuthor.Location = New System.Drawing.Point(142, 75)
        Me.lblAuthor.Name = "lblAuthor"
        Me.lblAuthor.Size = New System.Drawing.Size(71, 12)
        Me.lblAuthor.TabIndex = 2
        Me.lblAuthor.Text = "作者：n0099"
        '
        'llbBlog
        '
        Me.llbBlog.AutoSize = True
        Me.llbBlog.Location = New System.Drawing.Point(237, 193)
        Me.llbBlog.Name = "llbBlog"
        Me.llbBlog.Size = New System.Drawing.Size(71, 12)
        Me.llbBlog.TabIndex = 11
        Me.llbBlog.TabStop = True
        Me.llbBlog.Text = "n0099的博客"
        '
        'llbReportBug
        '
        Me.llbReportBug.AutoSize = True
        Me.llbReportBug.Location = New System.Drawing.Point(314, 193)
        Me.llbReportBug.Name = "llbReportBug"
        Me.llbReportBug.Size = New System.Drawing.Size(47, 12)
        Me.llbReportBug.TabIndex = 10
        Me.llbReportBug.TabStop = True
        Me.llbReportBug.Text = "BUG反馈"
        '
        'llbSCB
        '
        Me.llbSCB.AutoSize = True
        Me.llbSCB.Location = New System.Drawing.Point(142, 193)
        Me.llbSCB.Name = "llbSCB"
        Me.llbSCB.Size = New System.Drawing.Size(89, 12)
        Me.llbSCB.TabIndex = 9
        Me.llbSCB.TabStop = True
        Me.llbSCB.Text = "百度模拟城市吧"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblName.Location = New System.Drawing.Point(139, 20)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(219, 25)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "模拟城市4 自动安装程序"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.Image = CType(resources.GetObject("LogoPictureBox.Image"), System.Drawing.Image)
        Me.LogoPictureBox.Location = New System.Drawing.Point(12, 11)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.LogoPictureBox.Size = New System.Drawing.Size(124, 233)
        Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.LogoPictureBox.TabIndex = 12
        Me.LogoPictureBox.TabStop = False
        '
        'frmAbout
        '
        Me.AcceptButton = Me.btnClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(414, 255)
        Me.Controls.Add(Me.LogoPictureBox)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblName)
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
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblAuthor As System.Windows.Forms.Label
    Friend WithEvents llbBlog As System.Windows.Forms.LinkLabel
    Friend WithEvents llbReportBug As System.Windows.Forms.LinkLabel
    Friend WithEvents llbSCB As System.Windows.Forms.LinkLabel
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox

End Class
