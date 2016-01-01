<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangeOptions
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
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("641补丁")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("4GB补丁")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("免CD补丁")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("模拟城市4 启动器")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("繁体中文")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("简体中文")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("英语")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("语言补丁", New System.Windows.Forms.TreeNode() {TreeNode7, TreeNode8, TreeNode9})
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChangeOptions))
        Me.tvwOptions = New System.Windows.Forms.TreeView()
        Me.imgOptionsIcon = New System.Windows.Forms.ImageList(Me.components)
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnInstall = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblTitle2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tvwOptions
        '
        Me.tvwOptions.ImageIndex = 0
        Me.tvwOptions.ImageList = Me.imgOptionsIcon
        Me.tvwOptions.ItemHeight = 15
        Me.tvwOptions.Location = New System.Drawing.Point(12, 71)
        Me.tvwOptions.Name = "tvwOptions"
        TreeNode1.ImageKey = "Unchecked"
        TreeNode1.Name = "638补丁"
        TreeNode1.SelectedImageKey = "Unchecked"
        TreeNode1.Text = "638补丁"
        TreeNode2.ImageKey = "Unchecked"
        TreeNode2.Name = "640补丁"
        TreeNode2.SelectedImageKey = "Unchecked"
        TreeNode2.Text = "640补丁"
        TreeNode3.ImageKey = "Unchecked"
        TreeNode3.Name = "641补丁"
        TreeNode3.SelectedImageKey = "Unchecked"
        TreeNode3.Text = "641补丁"
        TreeNode4.ImageKey = "Unchecked"
        TreeNode4.Name = "4GB补丁"
        TreeNode4.SelectedImageKey = "Unchecked"
        TreeNode4.Text = "4GB补丁"
        TreeNode5.ImageKey = "Unchecked"
        TreeNode5.Name = "免CD补丁"
        TreeNode5.SelectedImageKey = "Unchecked"
        TreeNode5.Text = "免CD补丁"
        TreeNode6.ImageKey = "Unchecked"
        TreeNode6.Name = "模拟城市4 启动器"
        TreeNode6.SelectedImageKey = "Unchecked"
        TreeNode6.Text = "模拟城市4 启动器"
        TreeNode7.ImageKey = "RadioUnchecked"
        TreeNode7.Name = "繁体中文"
        TreeNode7.SelectedImageKey = "RadioUnchecked"
        TreeNode7.Text = "繁体中文"
        TreeNode8.ImageKey = "RadioUnchecked"
        TreeNode8.Name = "简体中文"
        TreeNode8.SelectedImageKey = "RadioUnchecked"
        TreeNode8.Text = "简体中文"
        TreeNode9.ImageKey = "RadioUnchecked"
        TreeNode9.Name = "英语"
        TreeNode9.SelectedImageKey = "RadioUnchecked"
        TreeNode9.Text = "英语"
        TreeNode10.ImageKey = "RootNodeBackground"
        TreeNode10.Name = "语言补丁"
        TreeNode10.SelectedImageKey = "RootNodeBackground"
        TreeNode10.Text = "语言补丁"
        Me.tvwOptions.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3, TreeNode4, TreeNode5, TreeNode6, TreeNode10})
        Me.tvwOptions.SelectedImageIndex = 0
        Me.tvwOptions.ShowPlusMinus = False
        Me.tvwOptions.ShowRootLines = False
        Me.tvwOptions.Size = New System.Drawing.Size(440, 199)
        Me.tvwOptions.TabIndex = 2
        '
        'imgOptionsIcon
        '
        Me.imgOptionsIcon.ImageStream = CType(resources.GetObject("imgOptionsIcon.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgOptionsIcon.TransparentColor = System.Drawing.Color.Transparent
        Me.imgOptionsIcon.Images.SetKeyName(0, "RootNodeBackground")
        Me.imgOptionsIcon.Images.SetKeyName(1, "Checked")
        Me.imgOptionsIcon.Images.SetKeyName(2, "Unchecked")
        Me.imgOptionsIcon.Images.SetKeyName(3, "RadioChecked")
        Me.imgOptionsIcon.Images.SetKeyName(4, "RadioUnchecked")
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(152, 27)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "安装或卸载组件"
        '
        'btnBack
        '
        Me.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnBack.Location = New System.Drawing.Point(200, 287)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(80, 23)
        Me.btnBack.TabIndex = 3
        Me.btnBack.Text = "< 返回(&B)"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnInstall
        '
        Me.btnInstall.Enabled = False
        Me.btnInstall.Location = New System.Drawing.Point(286, 287)
        Me.btnInstall.Name = "btnInstall"
        Me.btnInstall.Size = New System.Drawing.Size(80, 23)
        Me.btnInstall.TabIndex = 4
        Me.btnInstall.Text = "确定(&N) >"
        Me.btnInstall.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(372, 287)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "取消(&C)"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblTitle2
        '
        Me.lblTitle2.AutoSize = True
        Me.lblTitle2.Location = New System.Drawing.Point(12, 45)
        Me.lblTitle2.Name = "lblTitle2"
        Me.lblTitle2.Size = New System.Drawing.Size(419, 12)
        Me.lblTitle2.TabIndex = 1
        Me.lblTitle2.Text = "请选择要安装的组件，并取消选择要卸载的组件，单击[确定(N) >]按钮继续。"
        '
        'frmChangeOptions
        '
        Me.AcceptButton = Me.btnInstall
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(464, 322)
        Me.Controls.Add(Me.lblTitle2)
        Me.Controls.Add(Me.tvwOptions)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnInstall)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmChangeOptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "模拟城市4 豪华版 自动安装程序"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tvwOptions As System.Windows.Forms.TreeView
    Friend WithEvents imgOptionsIcon As System.Windows.Forms.ImageList
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnInstall As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblTitle2 As System.Windows.Forms.Label
End Class
