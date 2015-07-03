<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFinish
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFinish))
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("以下组件安装成功", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("以下组件安装失败", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("模拟城市4 豪华版", "success")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("DAEMON Tools Lite", "success")
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("638补丁", "success")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("640补丁", "success")
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("4GB补丁", "success")
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("免CD补丁", "success")
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("模拟城市4 启动器", "success")
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("语言补丁", "success")
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.imgTask = New System.Windows.Forms.ImageList(Me.components)
        Me.lvwSubassembly = New System.Windows.Forms.ListView()
        Me.btnRunSC4 = New System.Windows.Forms.Button()
        Me.llbSCB = New System.Windows.Forms.LinkLabel()
        Me.llbSCCN = New System.Windows.Forms.LinkLabel()
        Me.lblTitle2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(537, 407)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 6
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
        'imgTask
        '
        Me.imgTask.ImageStream = CType(resources.GetObject("imgTask.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTask.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTask.Images.SetKeyName(0, "success")
        Me.imgTask.Images.SetKeyName(1, "fail")
        '
        'lvwSubassembly
        '
        Me.lvwSubassembly.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvwSubassembly.Enabled = False
        ListViewGroup1.Header = "以下组件安装成功"
        ListViewGroup1.Name = "lvwGroupSuccess"
        ListViewGroup2.Header = "以下组件安装失败"
        ListViewGroup2.Name = "lvwGroupFail"
        Me.lvwSubassembly.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        ListViewItem1.Group = ListViewGroup1
        ListViewItem2.Group = ListViewGroup1
        ListViewItem3.Group = ListViewGroup1
        ListViewItem4.Group = ListViewGroup1
        ListViewItem5.Group = ListViewGroup1
        ListViewItem6.Group = ListViewGroup1
        ListViewItem7.Group = ListViewGroup1
        ListViewItem8.Group = ListViewGroup1
        Me.lvwSubassembly.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8})
        Me.lvwSubassembly.LargeImageList = Me.imgTask
        Me.lvwSubassembly.Location = New System.Drawing.Point(12, 80)
        Me.lvwSubassembly.Name = "lvwSubassembly"
        Me.lvwSubassembly.Size = New System.Drawing.Size(600, 321)
        Me.lvwSubassembly.TabIndex = 2
        Me.lvwSubassembly.TileSize = New System.Drawing.Size(590, 28)
        Me.lvwSubassembly.UseCompatibleStateImageBehavior = False
        Me.lvwSubassembly.View = System.Windows.Forms.View.Tile
        '
        'btnRunSC4
        '
        Me.btnRunSC4.Enabled = False
        Me.btnRunSC4.Location = New System.Drawing.Point(350, 407)
        Me.btnRunSC4.Name = "btnRunSC4"
        Me.btnRunSC4.Size = New System.Drawing.Size(180, 23)
        Me.btnRunSC4.TabIndex = 5
        Me.btnRunSC4.Text = "打开模拟城市4 豪华版(&R)"
        Me.btnRunSC4.UseVisualStyleBackColor = True
        '
        'llbSCB
        '
        Me.llbSCB.AutoSize = True
        Me.llbSCB.Location = New System.Drawing.Point(380, 56)
        Me.llbSCB.Name = "llbSCB"
        Me.llbSCB.Size = New System.Drawing.Size(89, 12)
        Me.llbSCB.TabIndex = 3
        Me.llbSCB.TabStop = True
        Me.llbSCB.Text = "百度模拟城市吧"
        '
        'llbSCCN
        '
        Me.llbSCCN.AutoSize = True
        Me.llbSCCN.Location = New System.Drawing.Point(475, 56)
        Me.llbSCCN.Name = "llbSCCN"
        Me.llbSCCN.Size = New System.Drawing.Size(137, 12)
        Me.llbSCCN.TabIndex = 4
        Me.llbSCCN.TabStop = True
        Me.llbSCCN.Text = "模拟城市中文网（SCCN）"
        '
        'lblTitle2
        '
        Me.lblTitle2.AutoSize = True
        Me.lblTitle2.Location = New System.Drawing.Point(12, 56)
        Me.lblTitle2.Name = "lblTitle2"
        Me.lblTitle2.Size = New System.Drawing.Size(125, 12)
        Me.lblTitle2.TabIndex = 1
        Me.lblTitle2.Text = "所有组件均已成功安装"
        '
        'frmFinish
        '
        Me.AcceptButton = Me.btnRunSC4
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(624, 442)
        Me.Controls.Add(Me.llbSCCN)
        Me.Controls.Add(Me.llbSCB)
        Me.Controls.Add(Me.btnRunSC4)
        Me.Controls.Add(Me.lblTitle2)
        Me.Controls.Add(Me.lvwSubassembly)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.btnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmFinish"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "模拟城市4 豪华版 自动安装程序"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents imgTask As System.Windows.Forms.ImageList
    Friend WithEvents lvwSubassembly As System.Windows.Forms.ListView
    Friend WithEvents btnRunSC4 As System.Windows.Forms.Button
    Friend WithEvents llbSCB As System.Windows.Forms.LinkLabel
    Friend WithEvents llbSCCN As System.Windows.Forms.LinkLabel
    Friend WithEvents lblTitle2 As System.Windows.Forms.Label
End Class
