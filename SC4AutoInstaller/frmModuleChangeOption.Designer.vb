<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmModuleChangeOption
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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("638补丁")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("640补丁")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("4GB补丁")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("免CD补丁")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("模拟城市4 启动器")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("繁体中文")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("简体中文")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("英语")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("语言补丁", New System.Windows.Forms.TreeNode() {TreeNode6, TreeNode7, TreeNode8})
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmModuleChangeOption))
        Me.tvwOptions = New System.Windows.Forms.TreeView()
        Me.imgOptions = New System.Windows.Forms.ImageList(Me.components)
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'tvwOptions
        '
        Me.tvwOptions.ImageKey = "rootnodebackground.png"
        Me.tvwOptions.ImageList = Me.imgOptions
        Me.tvwOptions.ItemHeight = 15
        Me.tvwOptions.Location = New System.Drawing.Point(12, 51)
        Me.tvwOptions.Name = "tvwOptions"
        TreeNode1.ImageKey = "unchecked"
        TreeNode1.Name = "638补丁"
        TreeNode1.SelectedImageKey = "unchecked"
        TreeNode1.Text = "638补丁"
        TreeNode2.ImageKey = "unchecked"
        TreeNode2.Name = "640补丁"
        TreeNode2.SelectedImageKey = "unchecked"
        TreeNode2.Text = "640补丁"
        TreeNode3.ImageKey = "unchecked"
        TreeNode3.Name = "4GB补丁"
        TreeNode3.SelectedImageKey = "unchecked"
        TreeNode3.Text = "4GB补丁"
        TreeNode4.ImageKey = "unchecked"
        TreeNode4.Name = "免CD补丁"
        TreeNode4.SelectedImageKey = "unchecked"
        TreeNode4.Text = "免CD补丁"
        TreeNode5.ImageKey = "unchecked"
        TreeNode5.Name = "模拟城市4 启动器"
        TreeNode5.SelectedImageKey = "unchecked"
        TreeNode5.Text = "模拟城市4 启动器"
        TreeNode6.ImageKey = "radiounchecked"
        TreeNode6.Name = "繁体中文"
        TreeNode6.SelectedImageKey = "radiounchecked"
        TreeNode6.Text = "繁体中文"
        TreeNode7.ImageKey = "radiounchecked"
        TreeNode7.Name = "简体中文"
        TreeNode7.SelectedImageKey = "radiounchecked"
        TreeNode7.Text = "简体中文"
        TreeNode8.ImageKey = "radiounchecked"
        TreeNode8.Name = "英语"
        TreeNode8.SelectedImageKey = "radiounchecked"
        TreeNode8.Text = "英语"
        TreeNode9.ImageKey = "rootnodebackground"
        TreeNode9.Name = "语言补丁"
        TreeNode9.SelectedImageKey = "rootnodebackground"
        TreeNode9.Text = "语言补丁"
        Me.tvwOptions.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3, TreeNode4, TreeNode5, TreeNode9})
        Me.tvwOptions.SelectedImageIndex = 0
        Me.tvwOptions.ShowPlusMinus = False
        Me.tvwOptions.ShowRootLines = False
        Me.tvwOptions.Size = New System.Drawing.Size(440, 221)
        Me.tvwOptions.TabIndex = 17
        '
        'imgOptions
        '
        Me.imgOptions.ImageStream = CType(resources.GetObject("imgOptions.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgOptions.TransparentColor = System.Drawing.Color.Transparent
        Me.imgOptions.Images.SetKeyName(0, "rootnodebackground")
        Me.imgOptions.Images.SetKeyName(1, "checked")
        Me.imgOptions.Images.SetKeyName(2, "unchecked")
        Me.imgOptions.Images.SetKeyName(3, "radiochecked")
        Me.imgOptions.Images.SetKeyName(4, "radiounchecked")
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(152, 27)
        Me.lblTitle.TabIndex = 14
        Me.lblTitle.Text = "添加或删除组件"
        '
        'btnBack
        '
        Me.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnBack.Location = New System.Drawing.Point(205, 287)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(80, 23)
        Me.btnBack.TabIndex = 25
        Me.btnBack.Text = "< 返回(&B)"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(291, 287)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(80, 23)
        Me.btnNext.TabIndex = 26
        Me.btnNext.Text = "确定(&N) >"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(377, 287)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 27
        Me.btnCancel.Text = "取消(&C)"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmModuleChangeOption
        '
        Me.AcceptButton = Me.btnNext
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(464, 322)
        Me.Controls.Add(Me.tvwOptions)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnCancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmModuleChangeOption"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "模拟城市4 豪华版 自动安装程序"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tvwOptions As System.Windows.Forms.TreeView
    Friend WithEvents imgOptions As System.Windows.Forms.ImageList
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
