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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("模拟城市4 豪华版")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("DAEMON Tools Lite")
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("638补丁")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("640补丁")
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("641补丁")
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("免CD补丁")
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("4GB补丁")
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("模拟城市4 启动器")
        Dim ListViewItem9 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("语言补丁")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInstalling))
        Me.lvwTask = New System.Windows.Forms.ListView()
        Me.imgTasksIcon = New System.Windows.Forms.ImageList(Me.components)
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.bgwInstall = New System.ComponentModel.BackgroundWorker()
        Me.picSC4 = New System.Windows.Forms.PictureBox()
        Me.tmrPic = New System.Windows.Forms.Timer(Me.components)
        Me.lblInstalling = New System.Windows.Forms.Label()
        Me.prgInstall = New System.Windows.Forms.ProgressBar()
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
        Me.lvwTask.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        ListViewItem1.Group = ListViewGroup1
        ListViewItem2.Group = ListViewGroup2
        ListViewItem3.Group = ListViewGroup2
        ListViewItem4.Group = ListViewGroup2
        ListViewItem5.Group = ListViewGroup2
        ListViewItem6.Group = ListViewGroup2
        ListViewItem7.Group = ListViewGroup2
        ListViewItem8.Group = ListViewGroup2
        ListViewItem9.Group = ListViewGroup2
        Me.lvwTask.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9})
        Me.lvwTask.LargeImageList = Me.imgTasksIcon
        Me.lvwTask.Location = New System.Drawing.Point(12, 54)
        Me.lvwTask.Name = "lvwTask"
        Me.lvwTask.Size = New System.Drawing.Size(176, 378)
        Me.lvwTask.TabIndex = 1
        Me.lvwTask.TileSize = New System.Drawing.Size(170, 25)
        Me.lvwTask.UseCompatibleStateImageBehavior = False
        Me.lvwTask.View = System.Windows.Forms.View.Tile
        '
        'imgTasksIcon
        '
        Me.imgTasksIcon.ImageStream = CType(resources.GetObject("imgTasksIcon.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTasksIcon.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTasksIcon.Images.SetKeyName(0, "Success")
        Me.imgTasksIcon.Images.SetKeyName(1, "Fail")
        Me.imgTasksIcon.Images.SetKeyName(2, "Installing")
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
        Me.picSC4.Location = New System.Drawing.Point(194, 0)
        Me.picSC4.Name = "picSC4"
        Me.picSC4.Size = New System.Drawing.Size(640, 480)
        Me.picSC4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picSC4.TabIndex = 12
        Me.picSC4.TabStop = False
        '
        'tmrPic
        '
        Me.tmrPic.Enabled = True
        Me.tmrPic.Interval = 5000
        '
        'lblInstalling
        '
        Me.lblInstalling.AutoSize = True
        Me.lblInstalling.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblInstalling.Location = New System.Drawing.Point(12, 435)
        Me.lblInstalling.Name = "lblInstalling"
        Me.lblInstalling.Size = New System.Drawing.Size(68, 17)
        Me.lblInstalling.TabIndex = 2
        Me.lblInstalling.Text = "正在安装："
        '
        'prgInstall
        '
        Me.prgInstall.Location = New System.Drawing.Point(12, 455)
        Me.prgInstall.Name = "prgInstall"
        Me.prgInstall.Size = New System.Drawing.Size(176, 13)
        Me.prgInstall.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.prgInstall.TabIndex = 3
        '
        'frmInstalling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 480)
        Me.Controls.Add(Me.picSC4)
        Me.Controls.Add(Me.prgInstall)
        Me.Controls.Add(Me.lblInstalling)
        Me.Controls.Add(Me.lblTitle)
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
    Friend WithEvents imgTasksIcon As System.Windows.Forms.ImageList
    Friend WithEvents picSC4 As System.Windows.Forms.PictureBox
    Friend WithEvents tmrPic As System.Windows.Forms.Timer
    Friend WithEvents lblInstalling As Label
    Friend WithEvents prgInstall As ProgressBar
End Class
