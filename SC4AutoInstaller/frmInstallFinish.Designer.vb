<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInstallFinish
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInstallFinish))
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("以下组件安装成功", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("模拟城市4 豪华版 镜像版", "Success")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("DAEMON Tools Lite", "Success")
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("638补丁", "Success")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("640补丁", "Success")
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("641补丁", "Success")
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("4GB补丁", "Success")
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("免CD补丁", "Success")
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("模拟城市4 启动器", "Success")
        Dim ListViewItem9 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("语言补丁", "Success")
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("以下组件安装失败", System.Windows.Forms.HorizontalAlignment.Left)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.imgResultIcon = New System.Windows.Forms.ImageList(Me.components)
        Me.lvwSubassemblySuccess = New System.Windows.Forms.ListView()
        Me.btnRunSC4 = New System.Windows.Forms.Button()
        Me.llbSCTB = New System.Windows.Forms.LinkLabel()
        Me.llbSCCN = New System.Windows.Forms.LinkLabel()
        Me.lblTitle2 = New System.Windows.Forms.Label()
        Me.llbReportBug = New System.Windows.Forms.LinkLabel()
        Me.llbBlog = New System.Windows.Forms.LinkLabel()
        Me.lvwSubassemblyFail = New System.Windows.Forms.ListView()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(532, 407)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 23)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "关闭(&C)"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("微软雅黑", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(117, 28)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "安装已完成"
        '
        'imgResultIcon
        '
        Me.imgResultIcon.ImageStream = CType(resources.GetObject("imgResultIcon.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgResultIcon.TransparentColor = System.Drawing.Color.Transparent
        Me.imgResultIcon.Images.SetKeyName(0, "Success")
        Me.imgResultIcon.Images.SetKeyName(1, "Fail")
        '
        'lvwSubassemblySuccess
        '
        Me.lvwSubassemblySuccess.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvwSubassemblySuccess.Enabled = False
        ListViewGroup1.Header = "以下组件安装成功"
        ListViewGroup1.Name = "lvwGroupInstallSuccess"
        Me.lvwSubassemblySuccess.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1})
        ListViewItem1.Group = ListViewGroup1
        ListViewItem2.Group = ListViewGroup1
        ListViewItem3.Group = ListViewGroup1
        ListViewItem4.Group = ListViewGroup1
        ListViewItem5.Group = ListViewGroup1
        ListViewItem6.Group = ListViewGroup1
        ListViewItem7.Group = ListViewGroup1
        ListViewItem8.Group = ListViewGroup1
        ListViewItem9.Group = ListViewGroup1
        Me.lvwSubassemblySuccess.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9})
        Me.lvwSubassemblySuccess.LargeImageList = Me.imgResultIcon
        Me.lvwSubassemblySuccess.Location = New System.Drawing.Point(12, 71)
        Me.lvwSubassemblySuccess.Name = "lvwSubassemblySuccess"
        Me.lvwSubassemblySuccess.Size = New System.Drawing.Size(296, 319)
        Me.lvwSubassemblySuccess.TabIndex = 2
        Me.lvwSubassemblySuccess.TileSize = New System.Drawing.Size(250, 25)
        Me.lvwSubassemblySuccess.UseCompatibleStateImageBehavior = False
        Me.lvwSubassemblySuccess.View = System.Windows.Forms.View.Tile
        '
        'btnRunSC4
        '
        Me.btnRunSC4.Enabled = False
        Me.btnRunSC4.Location = New System.Drawing.Point(346, 407)
        Me.btnRunSC4.Name = "btnRunSC4"
        Me.btnRunSC4.Size = New System.Drawing.Size(180, 23)
        Me.btnRunSC4.TabIndex = 8
        Me.btnRunSC4.Text = "打开模拟城市4 豪华版(&R)"
        Me.btnRunSC4.UseVisualStyleBackColor = True
        '
        'llbSCTB
        '
        Me.llbSCTB.AutoSize = True
        Me.llbSCTB.Location = New System.Drawing.Point(12, 399)
        Me.llbSCTB.Name = "llbSCTB"
        Me.llbSCTB.Size = New System.Drawing.Size(89, 12)
        Me.llbSCTB.TabIndex = 4
        Me.llbSCTB.TabStop = True
        Me.llbSCTB.Text = "百度模拟城市吧"
        '
        'llbSCCN
        '
        Me.llbSCCN.AutoSize = True
        Me.llbSCCN.Location = New System.Drawing.Point(107, 399)
        Me.llbSCCN.Name = "llbSCCN"
        Me.llbSCCN.Size = New System.Drawing.Size(137, 12)
        Me.llbSCCN.TabIndex = 5
        Me.llbSCCN.TabStop = True
        Me.llbSCCN.Text = "模拟城市中文网（SCCN）"
        '
        'lblTitle2
        '
        Me.lblTitle2.AutoSize = True
        Me.lblTitle2.Location = New System.Drawing.Point(12, 46)
        Me.lblTitle2.Name = "lblTitle2"
        Me.lblTitle2.Size = New System.Drawing.Size(389, 12)
        Me.lblTitle2.TabIndex = 1
        Me.lblTitle2.Text = "所有组件均已成功安装，您可以随后使用本安装程序来安装或卸载组件。"
        '
        'llbReportBug
        '
        Me.llbReportBug.AutoSize = True
        Me.llbReportBug.Location = New System.Drawing.Point(12, 418)
        Me.llbReportBug.Name = "llbReportBug"
        Me.llbReportBug.Size = New System.Drawing.Size(47, 12)
        Me.llbReportBug.TabIndex = 6
        Me.llbReportBug.TabStop = True
        Me.llbReportBug.Text = "BUG反馈"
        '
        'llbBlog
        '
        Me.llbBlog.AutoSize = True
        Me.llbBlog.Location = New System.Drawing.Point(65, 418)
        Me.llbBlog.Name = "llbBlog"
        Me.llbBlog.Size = New System.Drawing.Size(71, 12)
        Me.llbBlog.TabIndex = 7
        Me.llbBlog.TabStop = True
        Me.llbBlog.Text = "n0099的博客"
        '
        'lvwSubassemblyFail
        '
        Me.lvwSubassemblyFail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvwSubassemblyFail.Enabled = False
        ListViewGroup2.Header = "以下组件安装失败"
        ListViewGroup2.Name = "lvwGroupInstallFail"
        Me.lvwSubassemblyFail.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup2})
        Me.lvwSubassemblyFail.LargeImageList = Me.imgResultIcon
        Me.lvwSubassemblyFail.Location = New System.Drawing.Point(316, 71)
        Me.lvwSubassemblyFail.Name = "lvwSubassemblyFail"
        Me.lvwSubassemblyFail.Size = New System.Drawing.Size(296, 319)
        Me.lvwSubassemblyFail.TabIndex = 3
        Me.lvwSubassemblyFail.TileSize = New System.Drawing.Size(250, 25)
        Me.lvwSubassemblyFail.UseCompatibleStateImageBehavior = False
        Me.lvwSubassemblyFail.View = System.Windows.Forms.View.Tile
        Me.lvwSubassemblyFail.Visible = False
        '
        'frmInstallFinish
        '
        Me.AcceptButton = Me.btnRunSC4
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(624, 442)
        Me.Controls.Add(Me.lvwSubassemblyFail)
        Me.Controls.Add(Me.llbBlog)
        Me.Controls.Add(Me.llbReportBug)
        Me.Controls.Add(Me.llbSCCN)
        Me.Controls.Add(Me.llbSCTB)
        Me.Controls.Add(Me.btnRunSC4)
        Me.Controls.Add(Me.lblTitle2)
        Me.Controls.Add(Me.lvwSubassemblySuccess)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.btnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmInstallFinish"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "模拟城市4 豪华版 自动安装程序"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents imgResultIcon As System.Windows.Forms.ImageList
    Friend WithEvents lvwSubassemblySuccess As System.Windows.Forms.ListView
    Friend WithEvents btnRunSC4 As System.Windows.Forms.Button
    Friend WithEvents llbSCTB As System.Windows.Forms.LinkLabel
    Friend WithEvents llbSCCN As System.Windows.Forms.LinkLabel
    Friend WithEvents lblTitle2 As System.Windows.Forms.Label
    Friend WithEvents llbReportBug As System.Windows.Forms.LinkLabel
    Friend WithEvents llbBlog As System.Windows.Forms.LinkLabel
    Friend WithEvents lvwSubassemblyFail As System.Windows.Forms.ListView
End Class
