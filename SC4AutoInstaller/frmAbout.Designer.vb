﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.llbSCTB = New System.Windows.Forms.LinkLabel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblThanksList = New System.Windows.Forms.Label()
        Me.llbSCCN = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(297, 328)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "关闭(&C)"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(12, 47)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(51, 20)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = "版本号"
        '
        'lblAuthor
        '
        Me.lblAuthor.AutoSize = True
        Me.lblAuthor.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblAuthor.Location = New System.Drawing.Point(293, 50)
        Me.lblAuthor.Name = "lblAuthor"
        Me.lblAuthor.Size = New System.Drawing.Size(79, 17)
        Me.lblAuthor.TabIndex = 2
        Me.lblAuthor.Text = "作者：n0099"
        '
        'llbBlog
        '
        Me.llbBlog.AutoSize = True
        Me.llbBlog.Location = New System.Drawing.Point(65, 339)
        Me.llbBlog.Name = "llbBlog"
        Me.llbBlog.Size = New System.Drawing.Size(71, 12)
        Me.llbBlog.TabIndex = 7
        Me.llbBlog.TabStop = True
        Me.llbBlog.Text = "n0099的博客"
        '
        'llbReportBug
        '
        Me.llbReportBug.AutoSize = True
        Me.llbReportBug.Location = New System.Drawing.Point(12, 339)
        Me.llbReportBug.Name = "llbReportBug"
        Me.llbReportBug.Size = New System.Drawing.Size(47, 12)
        Me.llbReportBug.TabIndex = 6
        Me.llbReportBug.TabStop = True
        Me.llbReportBug.Text = "BUG反馈"
        '
        'llbSCTB
        '
        Me.llbSCTB.AutoSize = True
        Me.llbSCTB.Location = New System.Drawing.Point(12, 318)
        Me.llbSCTB.Name = "llbSCTB"
        Me.llbSCTB.Size = New System.Drawing.Size(89, 12)
        Me.llbSCTB.TabIndex = 4
        Me.llbSCTB.TabStop = True
        Me.llbSCTB.Text = "百度模拟城市吧"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("微软雅黑", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 12)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(282, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "模拟城市4 豪华版 自动安装程序"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblThanksList
        '
        Me.lblThanksList.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblThanksList.Location = New System.Drawing.Point(12, 75)
        Me.lblThanksList.Name = "lblThanksList"
        Me.lblThanksList.Size = New System.Drawing.Size(360, 223)
        Me.lblThanksList.TabIndex = 3
        Me.lblThanksList.Text = resources.GetString("lblThanksList.Text")
        '
        'llbSCCN
        '
        Me.llbSCCN.AutoSize = True
        Me.llbSCCN.Location = New System.Drawing.Point(107, 318)
        Me.llbSCCN.Name = "llbSCCN"
        Me.llbSCCN.Size = New System.Drawing.Size(137, 12)
        Me.llbSCCN.TabIndex = 5
        Me.llbSCCN.TabStop = True
        Me.llbSCCN.Text = "模拟城市中文网（SCCN）"
        '
        'frmAbout
        '
        Me.AcceptButton = Me.btnClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(384, 362)
        Me.Controls.Add(Me.llbSCCN)
        Me.Controls.Add(Me.lblThanksList)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.llbSCTB)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblAuthor As System.Windows.Forms.Label
    Friend WithEvents llbBlog As System.Windows.Forms.LinkLabel
    Friend WithEvents llbReportBug As System.Windows.Forms.LinkLabel
    Friend WithEvents llbSCTB As System.Windows.Forms.LinkLabel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblThanksList As System.Windows.Forms.Label
    Friend WithEvents llbSCCN As System.Windows.Forms.LinkLabel

End Class
