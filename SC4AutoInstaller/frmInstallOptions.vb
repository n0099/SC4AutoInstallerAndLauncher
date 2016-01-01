Public Class frmInstallOptions

    ''' <summary>获取或设置是否已经安装DAEMON Tools Lite</summary>
    Dim IsDAEMONToolsInstalled As Boolean

    ''' <summary>指定安装组件列表框项的图标</summary>
    Private Enum NodeCheckedState
        ''' <summary>已选择的复选框</summary>
        Checked
        ''' <summary>未选择的复选框</summary>
        Unchecked
        ''' <summary>已选择的单选框</summary>
        RadioChecked
        ''' <summary>未选择的单选框</summary>
        RadioUnchecked
    End Enum

    ''' <summary>验证指定目录路径是否为有效的路径</summary>
    ''' <param name="Path">要验证的目录路径</param>
    ''' <param name="PathName">要验证的目录路径名称</param>
    ''' <returns>如果目录路径为有效的目录路径，则为True；否则为False</returns>
    Private Function IsPathValidated(ByVal Path As String, ByVal PathName As String) As Boolean
        Path = If(Path.EndsWith(":\"), Path.Trim, Path.TrimEnd("\").Trim()) '如果目录路径不是分区根路径则去掉结尾的\
        If Path.Substring(1, Path.Length - 1).StartsWith(":\") = False Then '确定目录路径以分区盘符开头
            MessageBox.Show(PathName & "路径必须以分区盘符开头", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Return False
        End If
        Try '创建安装目录以便验证路径是否有效
            My.Computer.FileSystem.CreateDirectory(Path)
        Catch ex As ArgumentNullException
            MessageBox.Show(PathName & "为空" & vbCrLf & "请输入" & PathName & "路径", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Return False
        Catch ex As ArgumentException
            MessageBox.Show(PathName & "路径无效" & vbCrLf & vbCrLf & "可能的原因：" & vbCrLf & "您输入了一个没有分区卷标的路径" & vbCrLf & "您输入了一个含有\ / : * ? "" < > |这些字符的路径" & vbCrLf & "您输入了一个只有空格的路径", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Return False
        Catch ex As IO.PathTooLongException
            MessageBox.Show(PathName & "名称过长" & vbCrLf & "目录名不能超过260个字符", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Return False
        Catch ex As NotSupportedException
            MessageBox.Show(PathName & "路径里不能含有冒号", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Return False
        Catch ex As IO.IOException
            MessageBox.Show("无法创建" & PathName & vbCrLf & vbCrLf & "可能的原因：" & vbCrLf & "其父目录不可写", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Return False
        Catch ex As UnauthorizedAccessException
            MessageBox.Show("无法创建" & PathName & vbCrLf & vbCrLf & "可能的原因：" & vbCrLf & "其父目录不可写", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Return False
        Catch
            Return False
        End Try
        If My.Computer.FileSystem.DirectoryExists(Path) = False Then Return False '确定安装目录存在
        Try '向安装目录里写一个空文件以确定安装目录可写
            My.Computer.FileSystem.WriteAllText(Path & "\test", Nothing, False)
        Catch ex As Security.SecurityException
            MessageBox.Show(PathName & "无法访问" & vbCrLf & vbCrLf & "可能的原因：" & vbCrLf & "当前用户没有足够的权限访问该目录或其父目录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Return False
        Catch ex As UnauthorizedAccessException
            MessageBox.Show(PathName & "无法访问" & vbCrLf & vbCrLf & "可能的原因：" & vbCrLf & "当前用户没有足够的权限访问该目录或其父目录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Return False
        Catch
            Return False
        Finally
            If My.Computer.FileSystem.FileExists(Path & "\test") Then My.Computer.FileSystem.DeleteFile(Path & "\test")
        End Try
        Return True
    End Function

    ''' <summary>设置安装组件列表框里项的图标</summary>
    ''' <param name="NodeName">安装组件列表框项的Name属性值</param>
    ''' <param name="value">NodeCheckedState枚举的值之一，要设置的图标</param>
    Private Sub SetNodeChecked(ByVal NodeName As String, ByVal value As NodeCheckedState)
        With tvwOptions
            Select Case value
                Case NodeCheckedState.Checked
                    .Nodes.Find(NodeName, True)(0).ImageKey = "Checked"
                    .Nodes.Find(NodeName, True)(0).SelectedImageKey = "checked"
                Case NodeCheckedState.Unchecked
                    .Nodes.Find(NodeName, True)(0).ImageKey = "Unchecked"
                    .Nodes.Find(NodeName, True)(0).SelectedImageKey = "Unchecked"
                Case NodeCheckedState.RadioChecked
                    .Nodes.Find(NodeName, True)(0).ImageKey = "RadioChecked"
                    .Nodes.Find(NodeName, True)(0).SelectedImageKey = "RadioChecked"
                Case NodeCheckedState.RadioUnchecked
                    .Nodes.Find(NodeName, True)(0).ImageKey = "RadioUnchecked"
                    .Nodes.Find(NodeName, True)(0).SelectedImageKey = "RadioUnchecked"
            End Select
        End With
    End Sub

    ''' <summary>获取安装组件列表框里项的图标</summary>
    ''' <param name="NodeName">安装组件列表框项的Name属性值</param>
    ''' <returns>返回NodeCheckedState枚举的值之一</returns>
    Private Function GetNodeChecked(ByVal NodeName As String) As NodeCheckedState
        With tvwOptions
            Select Case .Nodes.Find(NodeName, True)(0).ImageKey
                Case "Checked"
                    Return NodeCheckedState.Checked
                Case "Unchecked"
                    Return NodeCheckedState.Unchecked
                Case "RadioChecked"
                    Return NodeCheckedState.RadioChecked
                Case "RadioUnchecked"
                    Return NodeCheckedState.RadioUnchecked
                Case Else
                    Return Nothing
            End Select
        End With
    End Function

    Private Sub tvwOptions_BeforeCollapse(sender As Object, e As TreeViewCancelEventArgs) Handles tvwOptions.BeforeCollapse
        e.Cancel = True '禁止折叠树节点
    End Sub

    Private Sub tvwOptions_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvwOptions.NodeMouseClick
        '当使用鼠标左键点击安装组件列表框的项时更新点击的项的图标和ModuleDeclare.InstallOptions的值
        If e.Button <> Windows.Forms.MouseButtons.Left Then Exit Sub
        tvwOptions.BeginUpdate()
        With ModuleDeclare.InstallOptions
            Dim OriginInstallOptions As InstallOptions = .Clone '声明一个用于判断是否更改了安装选项的InstallOptions类实例变量
            lblNeedsDiskSpace.Text = "安装目录至少需要" & .GetCurrentOptionsNeedsDiskSpaceInGB() & "GB的硬盘空间"
            Select Case e.Node.Name
                Case "模拟城市4 豪华版 镜像版"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.RadioUnchecked Then : .SC4InstallType = InstallOptions.SC4Type.CD
                        SetNodeChecked(e.Node.Name, NodeCheckedState.RadioChecked) : SetNodeChecked("模拟城市4 豪华版 硬盘版", NodeCheckedState.RadioUnchecked)
                        If IsDAEMONToolsInstalled = False Then SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.Checked) : .IsInstallDAEMONTools = True
                    End If
                Case "模拟城市4 豪华版 硬盘版"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.RadioUnchecked Then : .SC4InstallType = InstallOptions.SC4Type.NoInstall
                        SetNodeChecked(e.Node.Name, NodeCheckedState.RadioChecked) : SetNodeChecked("模拟城市4 豪华版 镜像版", NodeCheckedState.RadioUnchecked)
                    End If
                Case "DAEMON Tools Lite"
                    If GetNodeChecked("模拟城市4 豪华版 硬盘版") = NodeCheckedState.RadioChecked Then
                        If GetNodeChecked(e.Node.Name) = NodeCheckedState.Checked Then
                            SetNodeChecked(e.Node.Name, NodeCheckedState.Unchecked) : .IsInstallDAEMONTools = False
                        ElseIf GetNodeChecked(e.Node.Name) = NodeCheckedState.Unchecked Then
                            SetNodeChecked(e.Node.Name, NodeCheckedState.Checked) : .IsInstallDAEMONTools = True
                        End If
                    End If
                Case "638补丁", "640补丁", "641补丁", "4GB补丁", "免CD补丁", "模拟城市4 启动器", "添加桌面图标", "添加开始菜单项"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.Checked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.Unchecked)
                        Select Case e.Node.Name
                            Case "638补丁" : .IsInstall638Patch = False '取消安装638补丁时同时取消安装640和641补丁
                                SetNodeChecked("640补丁", NodeCheckedState.Unchecked) : .IsInstall640Patch = False
                                SetNodeChecked("641补丁", NodeCheckedState.Unchecked) : .IsInstall641Patch = False
                            Case "640补丁" : .IsInstall640Patch = False '取消安装640补丁时同时取消安装641补丁
                                SetNodeChecked("641补丁", NodeCheckedState.Unchecked) : .IsInstall641Patch = False
                            Case "641补丁" : .IsInstall641Patch = False
                            Case "4GB补丁" : .IsInstall4GBPatch = False
                            Case "免CD补丁" : .IsInstallNoCDPatch = False
                            Case "模拟城市4 启动器" : .IsInstallSC4Launcher = False
                            Case "添加桌面图标" : .IsAddDesktopIcon = False
                            Case "添加开始菜单项" : .IsAddStartMenuItem = False
                        End Select
                    ElseIf GetNodeChecked(e.Node.Name) = NodeCheckedState.Unchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.Checked)
                        Select Case e.Node.Name
                            Case "638补丁" : .IsInstall638Patch = True '选择安装638补丁时同时取消安装免CD补丁
                                SetNodeChecked("免CD补丁", NodeCheckedState.Unchecked) : .IsInstallNoCDPatch = False
                            Case "640补丁" : .IsInstall640Patch = True '选择安装640补丁时同时选择安装638补丁并取消安装免CD补丁
                                SetNodeChecked("638补丁", NodeCheckedState.Checked) : .IsInstall638Patch = True
                                SetNodeChecked("免CD补丁", NodeCheckedState.Unchecked) : .IsInstallNoCDPatch = False
                            Case "641补丁" : .IsInstall641Patch = True '选择安装641补丁时同时选择安装638和640补丁并取消安装免CD补丁
                                SetNodeChecked("638补丁", NodeCheckedState.Checked) : .IsInstall638Patch = True
                                SetNodeChecked("640补丁", NodeCheckedState.Checked) : .IsInstall640Patch = True
                                SetNodeChecked("免CD补丁", NodeCheckedState.Unchecked) : .IsInstallNoCDPatch = False
                            Case "4GB补丁" : .IsInstall4GBPatch = True
                            Case "免CD补丁" : .IsInstallNoCDPatch = True '选择安装免CD补丁时同时取消安装638、640和641补丁
                                SetNodeChecked("638补丁", NodeCheckedState.Unchecked) : .IsInstall638Patch = False
                                SetNodeChecked("640补丁", NodeCheckedState.Unchecked) : .IsInstall640Patch = False
                                SetNodeChecked("641补丁", NodeCheckedState.Unchecked) : .IsInstall641Patch = False
                            Case "模拟城市4 启动器" : .IsInstallSC4Launcher = True
                            Case "添加桌面图标" : .IsAddDesktopIcon = True
                            Case "添加开始菜单项" : .IsAddStartMenuItem = True
                        End Select
                    End If
                Case "繁体中文", "简体中文", "英语"
                    SetNodeChecked(e.Node.Name, NodeCheckedState.RadioChecked)
                    Select Case e.Node.Name
                        Case "繁体中文" : .LanguagePatchOption = SC4Language.TraditionalChinese
                            SetNodeChecked("简体中文", NodeCheckedState.RadioUnchecked) : SetNodeChecked("英语", NodeCheckedState.RadioUnchecked)
                        Case "简体中文" : .LanguagePatchOption = SC4Language.SimplifiedChinese
                            SetNodeChecked("繁体中文", NodeCheckedState.RadioUnchecked) : SetNodeChecked("英语", NodeCheckedState.RadioUnchecked)
                        Case "英语" : .LanguagePatchOption = SC4Language.English
                            SetNodeChecked("繁体中文", NodeCheckedState.RadioUnchecked) : SetNodeChecked("简体中文", NodeCheckedState.RadioUnchecked)
                    End Select
            End Select
            If .Equals(OriginInstallOptions) = False Then cmbOptions.SelectedItem = "自定义" '判断是否更改了安装选项
        End With
        tvwOptions.EndUpdate()
    End Sub

    Private Sub tvwOptions_NodeMouseHover(sender As Object, e As TreeNodeMouseHoverEventArgs) Handles tvwOptions.NodeMouseHover
        lblDAEMONToolsInstallDir.Visible = (e.Node.Name = "DAEMON Tools Lite") '隐藏或显示选择DAEMON Tools Lite安装路径文本框和按钮
        txtDAEMONToolsInstallDir.Visible = (e.Node.Name = "DAEMON Tools Lite")
        btnDAEMONToolsInstallDir.Visible = (e.Node.Name = "DAEMON Tools Lite")
        Select Case e.Node.Name '更新组件信息文本和该组件所需要的磁盘空间文本
            Case "必选组件", "可选组件", "附加任务", "语言补丁"
                lblOptionDetail.Text = "请将鼠标指针放在组件名上以查看组件详情" : lblOptionDiskSpace.Text = ""
            Case "模拟城市4 豪华版 镜像版"
                lblOptionDetail.Text = "安装模拟城市4 豪华版 镜像版" & vbCrLf & vbCrLf & "安装镜像版模拟城市4时必须同时安装DAEMON Tools Lite" & vbCrLf & vbCrLf & "建议安装镜像版模拟城市4，不建议安装硬盘版模拟城市4。"
                lblOptionDiskSpace.Text = "此组件需要" & Math.Round(InstallOptions.SC4NeedsDiskSpace / 1024 / 1024 / 1024, 2) & "GB的硬盘空间"
            Case "模拟城市4 豪华版 硬盘版"
                lblOptionDetail.Text = "安装模拟城市4 豪华版 硬盘版" & vbCrLf & vbCrLf & "安装硬盘版模拟城市4时不必同时安装DAEMON Tools Lite" & vbCrLf & vbCrLf & "建议安装镜像版模拟城市4，不建议安装硬盘版模拟城市4。"
                lblOptionDiskSpace.Text = "此组件需要" & Math.Round(InstallOptions.SC4NeedsDiskSpace / 1024 / 1024 / 1024, 2) & "GB的硬盘空间"
            Case "DAEMON Tools Lite"
                lblOptionDetail.Text = "安装DAEMON Tools Lite 5.0" & vbCrLf & vbCrLf & "DAEMON Tools Lite用于加载虚拟光驱" & vbCrLf & vbCrLf & "如果要安装镜像版模拟城市4，则必须安装此项。"
                lblOptionDiskSpace.Text = "此组件需要" & InstallOptions.DAEMONToolsNeedsDiskSpace \ 1024 \ 1024 & "MB的硬盘空间"
            Case "638补丁"
                lblOptionDetail.Text = "安装638补丁" & vbCrLf & vbCrLf & "638补丁修复了一些bug" & vbCrLf & vbCrLf & "建议同时安装640灯光补丁。"
                lblOptionDiskSpace.Text = "此组件需要" & InstallOptions._638PatchNeedsDiskSpace \ 1024 & "KB的硬盘空间"
            Case "640补丁"
                lblOptionDetail.Text = "安装640灯光补丁" & vbCrLf & vbCrLf & "如果不安装640补丁插件模型在晚上不会显示灯光" & vbCrLf & vbCrLf & "安装此补丁前必须先安装638补丁，建议安装。"
                lblOptionDiskSpace.Text = "此组件需要" & InstallOptions._640PatchNeedsDiskSpace \ 1024 & "KB的硬盘空间"
            Case "641补丁"
                lblOptionDetail.Text = "安装641官方免CD补丁" & vbCrLf & vbCrLf & "安装此补丁后打开游戏前不需要再加载CD2虚拟光驱" & vbCrLf & vbCrLf & "安装此补丁前必须先安装640补丁和638补丁" & vbCrLf & vbCrLf & "建议安装此补丁，不建议安装非官方免CD补丁。"
                lblOptionDiskSpace.Text = "此组件需要" & InstallOptions._641PatchNeedsDiskSpace \ 1024 & "KB的硬盘空间"
            Case "4GB补丁"
                lblOptionDetail.Text = "安装4GB补丁" & vbCrLf & vbCrLf & "安装此补丁后游戏跳出的几率会大大降低" & vbCrLf & vbCrLf & "仅64位系统可以安装，建议安装。"
                lblOptionDiskSpace.Text = "此组件需要0KB的硬盘空间"
            Case "免CD补丁"
                lblOptionDetail.Text = "安装非官方免CD补丁" & vbCrLf & vbCrLf & "安装此补丁后打开游戏前不需要再加载CD2虚拟光驱" & vbCrLf & vbCrLf & "安装此补丁后不能安装638、640和641补丁" & vbCrLf & vbCrLf & "641补丁同样有免CD的功能，建议安装641补丁，不建议安装此补丁。"
                lblOptionDiskSpace.Text = "此组件需要" & InstallOptions.NoCDPatchNeedsDiskSpace \ 1024 & "KB的硬盘空间"
            Case "模拟城市4 启动器"
                lblOptionDetail.Text = "安装模拟城市4 启动器" & vbCrLf & vbCrLf & "启动器可快速设置显示方式（窗口化或全屏）、分辨率、CPU核心数、渲染模式等启动参数或设置" & vbCrLf & vbCrLf & "建议安装。"
                lblOptionDiskSpace.Text = "此组件需要" & InstallOptions.SC4LauncherNeedsDiskSpace \ 1024 & "KB的硬盘空间"
            Case "语言补丁"
                lblOptionDetail.Text = "安装语言补丁" & vbCrLf & vbCrLf & "如果不安装语言补丁" & vbCrLf & vbCrLf & "默认语言为英语" & vbCrLf & vbCrLf & "建议安装。"
                lblOptionDiskSpace.Text = ""
            Case "繁体中文"
                lblOptionDetail.Text = "安装繁体中文语言补丁" & vbCrLf & vbCrLf & "建议使用繁体中文语言补丁。"
                lblOptionDiskSpace.Text = "此组件需要 0KB 的硬盘空间"
            Case "简体中文"
                lblOptionDetail.Text = "安装简体中文语言补丁" & vbCrLf & vbCrLf & "建议使用繁体中文语言补丁，不建议使用简体中文语言补丁。"
                lblOptionDiskSpace.Text = "此组件需要" & InstallOptions.SimplifiedChineseLanguageNeedsDiskSpace \ 1024 & "KB的硬盘空间"
            Case "英语"
                lblOptionDetail.Text = "安装英语语言补丁" & vbCrLf & vbCrLf & "建议使用繁体中文语言补丁。"
                lblOptionDiskSpace.Text = "此组件需要 0KB 的硬盘空间"
            Case "添加桌面图标"
                lblOptionDetail.Text = "添加一个打开模拟城市4的快捷方式到桌面上。" : lblOptionDiskSpace.Text = ""
            Case "添加开始菜单项"
                lblOptionDetail.Text = "在开始菜单里添加模拟城市4 豪华版项。" : lblOptionDiskSpace.Text = ""
        End Select
    End Sub

    Private Sub cmbOptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOptions.SelectedIndexChanged
        tvwOptions.BeginUpdate()
        With ModuleDeclare.InstallOptions
            '如果已选择模拟城市4 豪华版 镜像版项且未安装DAEMON Tools Lite则选中DAEMON Tools Lite项
            If GetNodeChecked("模拟城市4 豪华版 镜像版") = NodeCheckedState.RadioChecked AndAlso IsDAEMONToolsInstalled = False Then
                SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.Checked) : .IsInstallDAEMONTools = True
            End If
            Select Case cmbOptions.SelectedItem
                Case "完全安装"
                    '更新安装组件列表框对应项的图标
                    SetNodeChecked("638补丁", NodeCheckedState.Checked) : SetNodeChecked("640补丁", NodeCheckedState.Checked) : SetNodeChecked("641补丁", NodeCheckedState.Checked)
                    If Environment.Is64BitOperatingSystem Then SetNodeChecked("4GB补丁", NodeCheckedState.Checked) : .IsInstall4GBPatch = True '如果为64位系统则取消选中4GB补丁项
                    SetNodeChecked("免CD补丁", NodeCheckedState.Unchecked) : SetNodeChecked("模拟城市4 启动器", NodeCheckedState.Checked)
                    SetNodeChecked("添加桌面图标", NodeCheckedState.Checked) : SetNodeChecked("添加开始菜单项", NodeCheckedState.Checked)
                    '更新ModuleDeclare.InstallOptions对应选项的值
                    .IsInstall638Patch = True : .IsInstall640Patch = True : .IsInstall641Patch = True
                    .IsInstallNoCDPatch = False : .IsInstallSC4Launcher = True
                    .IsAddDesktopIcon = True : .IsAddStartMenuItem = True
                Case "推荐安装"
                    '更新安装组件列表框对应项的图标
                    SetNodeChecked("638补丁", NodeCheckedState.Checked) : SetNodeChecked("640补丁", NodeCheckedState.Checked) : SetNodeChecked("641补丁", NodeCheckedState.Checked)
                    If Environment.Is64BitOperatingSystem Then SetNodeChecked("4GB补丁", NodeCheckedState.Unchecked) '如果为64位系统则取消选中4GB补丁项
                    SetNodeChecked("免CD补丁", NodeCheckedState.Unchecked) : SetNodeChecked("模拟城市4 启动器", NodeCheckedState.Checked)
                    SetNodeChecked("添加桌面图标", NodeCheckedState.Unchecked) : SetNodeChecked("添加开始菜单项", NodeCheckedState.Checked)
                    '更新ModuleDeclare.InstallOptions对应选项的值
                    .IsInstall638Patch = True : .IsInstall640Patch = True : .IsInstall641Patch = True
                    .IsInstall4GBPatch = False : .IsInstallNoCDPatch = False : .IsInstallSC4Launcher = True
                    .IsAddDesktopIcon = False : .IsAddStartMenuItem = True
                Case "精简安装"
                    '更新安装组件列表框对应项的图标
                    SetNodeChecked("638补丁", NodeCheckedState.Unchecked) : SetNodeChecked("640补丁", NodeCheckedState.Unchecked) : SetNodeChecked("641补丁", NodeCheckedState.Unchecked)
                    If Environment.Is64BitOperatingSystem Then SetNodeChecked("4GB补丁", NodeCheckedState.Unchecked) '如果为64位系统则取消选中4GB补丁项
                    SetNodeChecked("免CD补丁", NodeCheckedState.Unchecked) : SetNodeChecked("模拟城市4 启动器", NodeCheckedState.Unchecked)
                    SetNodeChecked("添加桌面图标", NodeCheckedState.Unchecked) : SetNodeChecked("添加开始菜单项", NodeCheckedState.Checked)
                    '更新ModuleDeclare.InstallOptions对应选项的值
                    .IsInstall638Patch = False : .IsInstall640Patch = False : .IsInstall641Patch = False
                    .IsInstall4GBPatch = False : .IsInstallNoCDPatch = False : .IsInstallSC4Launcher = False
                    .IsAddDesktopIcon = False : .IsAddStartMenuItem = True
            End Select
            lblNeedsDiskSpace.Text = "安装目录至少需要" & .GetCurrentOptionsNeedsDiskSpaceInGB() & "GB的硬盘空间"
        End With
        tvwOptions.EndUpdate()
    End Sub

    Private Sub tmrCheckMousePosition_Tick(sender As Object, e As EventArgs) Handles tmrCheckMousePosition.Tick
        Dim rect As Rectangle = pnlOptions.Bounds, x As Integer = MousePosition.X - Me.Left, y As Integer = MousePosition.Y - Me.Top
        If (x <= rect.Left OrElse x >= rect.Right) OrElse (y <= rect.Top OrElse y >= rect.Bottom) Then '判断鼠标是否不在安装组件容器内
            lblOptionDetail.Text = "请将鼠标指针放在组件名上以查看组件详情" : lblOptionDiskSpace.Text = ""
            lblDAEMONToolsInstallDir.Visible = False : txtDAEMONToolsInstallDir.Visible = False : btnDAEMONToolsInstallDir.Visible = False '隐藏选择DAEMON Tools Lite安装路径文本框和按钮
        End If
    End Sub

    Private Sub btnSC4InstallDir_Click(sender As Object, e As EventArgs) Handles btnSC4InstallDir.Click
        With fbdSC4InstallDir
            .SelectedPath = txtSC4InstallDir.Text '将选择模拟城市4安装路径对话框的选择路径设为模拟城市4安装路径文本框的路径
            If .SelectedPath IsNot Nothing AndAlso .ShowDialog = DialogResult.OK Then
                txtSC4InstallDir.Text = If(.SelectedPath.Length = 3, .SelectedPath & "SimCity 4 Deluxe", .SelectedPath & "\SimCity 4 Deluxe") '判断选择的路径是否以分区盘符开头
            End If
        End With
    End Sub

    Private Sub btnDAEMONToolsInstallDir_Click(sender As Object, e As EventArgs) Handles btnDAEMONToolsInstallDir.Click
        With fbdDAEMONToolsInstallDir
            .SelectedPath = txtDAEMONToolsInstallDir.Text '将选择DAEMON Tools Lite安装路径对话框的选择路径设置为DAEMON Tools Lite安装路径文本框的路径
            If .SelectedPath IsNot Nothing AndAlso .ShowDialog = DialogResult.OK Then
                txtDAEMONToolsInstallDir.Text = If(.SelectedPath.Length = 3, .SelectedPath & "DAEMON Tools Lite", .SelectedPath & "\DAEMON Tools Lite") '判断选择的路径是否以分区盘符开头
            End If
        End With
    End Sub

    Private Sub frmInstallOptions_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("确定要退出安装程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then e.Cancel = True
    End Sub

    Private Sub frmInstallOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tvwOptions.ExpandAll() '展开所有的树节点
        With ModuleDeclare.InstallOptions
            If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Disc Soft\DAEMON Tools Lite", "Path", Nothing) IsNot Nothing Then '如果已安装DAEMON Tools Lite则删除安装组件列表框的DAEMON Tools Lite项
                tvwOptions.Nodes.Find("DAEMON Tools Lite", True)(0).Remove()
                IsDAEMONToolsInstalled = True : .IsInstallDAEMONTools = False : .DAEMONToolsInstallDir = Nothing
            End If
            If Environment.Is64BitOperatingSystem = False Then tvwOptions.Nodes.Find("4GB补丁", True)(0).Remove() : .IsInstall4GBPatch = False '如果系统不是64位系统则删除安装组件列表框里的4GB补丁项
            cmbOptions.SelectedItem = cmbOptions.Items(1) '自动选择安装选项
            '选中安装组件列表框的模拟城市4 豪华版 镜像版项并更新ModuleDeclare.InstallOptions对应选项的值
            SetNodeChecked("模拟城市4 豪华版 镜像版", NodeCheckedState.RadioChecked) : .SC4InstallType = InstallOptions.SC4Type.CD
            '选中安装组件列表框的繁体中文项并更新ModuleDeclare.InstallOptions对应选项的值
            SetNodeChecked("繁体中文", NodeCheckedState.RadioChecked) : SetNodeChecked("简体中文", NodeCheckedState.RadioUnchecked)
            SetNodeChecked("英语", NodeCheckedState.RadioUnchecked) : .LanguagePatchOption = SC4Language.TraditionalChinese
            lblNeedsDiskSpace.Text = "安装目录至少需要" & .GetCurrentOptionsNeedsDiskSpaceInGB() & "GB的硬盘空间" '更新当前选择的组件所需要的磁盘空间的文本
        End With
        txtSC4InstallDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\Maxis\SimCity 4 Deluxe" '初始化模拟城市4的安装目录路径
        txtDAEMONToolsInstallDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\DAEMON Tools Lite" '初始化DAEMON Tools Lite的安装目录路径
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ModuleDeclare.InstallOptions = New InstallOptions '恢复已更改的ModuleDeclare.InstallOptions类实例
        frmMain.Show()
        RemoveHandler Me.FormClosing, AddressOf frmInstallOptions_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
        Close()
    End Sub

    Private Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        If IsPathValidated(txtSC4InstallDir.Text, "模拟城市4 安装目录") = False Then Exit Sub '判断模拟城市4安装目录的路径是否有效
        With ModuleDeclare.InstallOptions
            .SC4InstallDir = If(txtSC4InstallDir.Text.EndsWith(":\"), txtSC4InstallDir.Text.Trim, txtSC4InstallDir.Text.TrimEnd("\").Trim) '如果安装路径不是分区根路径则去掉结尾的\
            If IsDAEMONToolsInstalled = False AndAlso GetNodeChecked("DAEMON Tools Lite") = NodeCheckedState.Checked Then
                If IsPathValidated(txtDAEMONToolsInstallDir.Text, "DAEMON Tools Lite 安装目录") = False Then Exit Sub '判断DAEMON Tools Lite安装目录的路径是否有效
                .DAEMONToolsInstallDir = If(txtDAEMONToolsInstallDir.Text.EndsWith(":\"), txtDAEMONToolsInstallDir.Text.Trim, txtDAEMONToolsInstallDir.Text.TrimEnd("\").Trim) '如果安装路径不是分区根路径则去掉结尾的\
            End If
        End With
        frmInstalling.Show()
        RemoveHandler Me.FormClosing, AddressOf frmInstallOptions_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
        Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Application.Exit()
    End Sub

End Class