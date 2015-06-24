<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInstalling
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
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("游戏", System.Windows.Forms.HorizontalAlignment.Center)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("组件", System.Windows.Forms.HorizontalAlignment.Center)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("附加任务", System.Windows.Forms.HorizontalAlignment.Center)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("模拟城市4 豪华版", "(无)")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("DAEMON Tools Lite", "(无)")
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("638补丁", "(无)")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("640补丁", "(无)")
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("免CD补丁", "(无)")
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("4GB补丁")
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("模拟城市4 启动器")
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("语言补丁", "(无)")
        Dim ListViewItem9 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("添加开始菜单项")
        Dim ListViewItem10 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("添加桌面图标")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInstalling))
        Me.lvwTask = New System.Windows.Forms.ListView()
        Me.imgTask = New System.Windows.Forms.ImageList(Me.components)
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.bgwInstall = New System.ComponentModel.BackgroundWorker()
        Me.picSC4 = New System.Windows.Forms.PictureBox()
        CType(Me.picSC4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lvwTask
        '
        Me.lvwTask.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvwTask.Enabled = False
        ListViewGroup1.Header = "游戏"
        ListViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center
        ListViewGroup1.Name = "lvwGroupSC4"
        ListViewGroup2.Header = "组件"
        ListViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center
        ListViewGroup2.Name = "lvwGroupSubassembly"
        ListViewGroup3.Header = "附加任务"
        ListViewGroup3.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center
        ListViewGroup3.Name = "lvwGroupAdditionTask"
        Me.lvwTask.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3})
        ListViewItem1.Group = ListViewGroup1
        ListViewItem1.StateImageIndex = 0
        ListViewItem2.Group = ListViewGroup2
        ListViewItem2.StateImageIndex = 0
        ListViewItem3.Group = ListViewGroup2
        ListViewItem3.StateImageIndex = 0
        ListViewItem4.Group = ListViewGroup2
        ListViewItem4.StateImageIndex = 0
        ListViewItem5.Group = ListViewGroup2
        ListViewItem5.StateImageIndex = 0
        ListViewItem6.Group = ListViewGroup2
        ListViewItem7.Group = ListViewGroup2
        ListViewItem8.Group = ListViewGroup2
        ListViewItem8.StateImageIndex = 0
        ListViewItem9.Group = ListViewGroup3
        ListViewItem9.StateImageIndex = 0
        ListViewItem10.Group = ListViewGroup3
        ListViewItem10.StateImageIndex = 0
        Me.lvwTask.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9, ListViewItem10})
        Me.lvwTask.LargeImageList = Me.imgTask
        Me.lvwTask.Location = New System.Drawing.Point(12, 45)
        Me.lvwTask.Name = "lvwTask"
        Me.lvwTask.Size = New System.Drawing.Size(176, 447)
        Me.lvwTask.TabIndex = 1
        Me.lvwTask.TileSize = New System.Drawing.Size(170, 25)
        Me.lvwTask.UseCompatibleStateImageBehavior = False
        Me.lvwTask.View = System.Windows.Forms.View.Tile
        '
        'imgTask
        '
        Me.imgTask.ImageStream = CType(resources.GetObject("imgTask.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTask.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTask.Images.SetKeyName(0, "success")
        Me.imgTask.Images.SetKeyName(1, "fail")
        Me.imgTask.Images.SetKeyName(2, "installing")
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(92, 27)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "正在安装"
        '
        'bgwInstall
        '
        Me.bgwInstall.WorkerSupportsCancellation = True
        '
        'picSC4
        '
        Me.picSC4.Location = New System.Drawing.Point(194, 12)
        Me.picSC4.Name = "picSC4"
        Me.picSC4.Size = New System.Drawing.Size(640, 480)
        Me.picSC4.TabIndex = 12
        Me.picSC4.TabStop = False
        '
        'frmInstalling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 502)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.picSC4)
        Me.Controls.Add(Me.lvwTask)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmInstalling"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "模拟城市4 豪华版 自动安装程序"
        CType(Me.picSC4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lvwTask As System.Windows.Forms.ListView
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents bgwInstall As System.ComponentModel.BackgroundWorker
    Friend WithEvents imgTask As System.Windows.Forms.ImageList
    Friend WithEvents picSC4 As System.Windows.Forms.PictureBox
End Class
