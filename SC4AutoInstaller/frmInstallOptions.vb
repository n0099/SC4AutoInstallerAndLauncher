Public Class frmInstallOptions

    ''' <summary>获取或设置是否已经安装DAEMON Tools Lite</summary>
    Dim IsDAEMONToolsInstalled As Boolean
    ''' <summary>获取或设置Data\SC4\CD文件夹是否存在</summary>
    Dim IsCDDirectoryExists As Boolean
    ''' <summary>获取或设置Data\SC4\NoInstall.rar文件是否存在</summary>
    Dim IsSC4FileExists As Boolean

    ''' <summary>指定安装组件列表框项的图标</summary>
    Private Enum NodeCheckedState
        ''' <summary>已选择的复选框</summary>
        checked
        ''' <summary>未选择的复选框</summary>
        unchecked
        ''' <summary>已选择的单选框</summary>
        radiochecked
        ''' <summary>未选择的单选框</summary>
        radiounchecked
    End Enum

    ''' <summary>设置安装组件列表框里项的图标</summary>
    ''' <param name="NodeName">安装组件列表框项的Name属性值</param>
    ''' <param name="value">NodeCheckedState 枚举的值之一，要设置的图标</param>
    Private Sub SetNodeChecked(ByVal NodeName As String, ByVal value As NodeCheckedState)
        With tvwOptions
            Select Case value
                Case NodeCheckedState.checked
                    .Nodes.Find(NodeName, True)(0).ImageKey = "checked"
                    .Nodes.Find(NodeName, True)(0).SelectedImageKey = "checked"
                Case NodeCheckedState.unchecked
                    .Nodes.Find(NodeName, True)(0).ImageKey = "unchecked"
                    .Nodes.Find(NodeName, True)(0).SelectedImageKey = "unchecked"
                Case NodeCheckedState.radiochecked
                    .Nodes.Find(NodeName, True)(0).ImageKey = "radiochecked"
                    .Nodes.Find(NodeName, True)(0).SelectedImageKey = "radiochecked"
                Case NodeCheckedState.radiounchecked
                    .Nodes.Find(NodeName, True)(0).ImageKey = "radiounchecked"
                    .Nodes.Find(NodeName, True)(0).SelectedImageKey = "radiounchecked"
            End Select
        End With
    End Sub

    ''' <summary>获取安装组件列表框里项的图标</summary>
    ''' <param name="NodeName">安装组件列表框项的Name属性值</param>
    ''' <returns>返回 NodeCheckedState 枚举的值之一</returns>
    Private Function GetNodeChecked(ByVal NodeName As String) As NodeCheckedState
        With tvwOptions
            Select Case .Nodes.Find(NodeName, True)(0).ImageKey
                Case "checked"
                    Return NodeCheckedState.checked
                Case "unchecked"
                    Return NodeCheckedState.unchecked
                Case "radiochecked"
                    Return NodeCheckedState.radiochecked
                Case "radiounchecked"
                    Return NodeCheckedState.radiounchecked
                Case Else
                    Return Nothing
            End Select
        End With
    End Function

    ''' <summary>验证指定文本框的文本是否为有效的目录路径</summary>
    ''' <param name="TextBox">要验证的文本框</param>
    ''' <returns>如果文本框的文本为有效的目录路径，则为True；否则为False</returns>
    Private Function IsPathValidated(ByVal TextBox As TextBox) As Boolean
        '声明一个用于存储要验证的安装目录是属于哪个组件的的字符串变量和一个存储要验证的安装目录的路径的字符串变量
        Dim TextBoxName As String = IIf(TextBox.Name = "txtDAEMONlInstallDir", "DAEMON Tools Lite", "模拟城市4"), Path As String = TextBox.Text.Trim
        If Path.Substring(1, Path.Length - 1).EndsWith(":\") = False And Path.EndsWith("\") = True Then Path = Path.TrimEnd("\").Trim() '如果安装路径以\结尾且不是分区根路径则去掉结尾的\
        If Path.Substring(1, Path.Length - 1).StartsWith(":\") = False Then '确定安装目录路径以分区盘符开头
            MessageBox.Show(TextBoxName & "的安装目录路径必须以分区盘符开头！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Try '创建安装目录以便验证路径有效
            My.Computer.FileSystem.CreateDirectory(Path)
        Catch ex As ArgumentNullException
            MessageBox.Show("请输入" & TextBoxName & "的安装目录路径！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As ArgumentException
            MessageBox.Show(TextBoxName & "的安装目录路径无效！" & vbCrLf & vbCrLf & "可能的原因：" & vbCrLf & "您输入了一个没有分区卷标的路径" & vbCrLf & "您输入了一个含有\ / : * ? "" < > |这些字符的路径" & vbCrLf & "您输入了一个只有空格的路径", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As IO.PathTooLongException
            MessageBox.Show(TextBoxName & "的安装目录名过长！" & vbCrLf & "目录名不能超过260个字符", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As NotSupportedException
            MessageBox.Show(TextBoxName & "的安装目录路径不能含有冒号！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As IO.IOException
            MessageBox.Show("无法创建" & TextBoxName & "的安装目录！" & vbCrLf & vbCrLf & "可能的原因：" & vbCrLf & "其父目录不可写", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As UnauthorizedAccessException
            MessageBox.Show("无法创建" & TextBoxName & "的安装目录！" & vbCrLf & vbCrLf & "可能的原因：" & vbCrLf & "其父目录不可写")
        Catch ex As Exception
            Return False
        End Try
        If My.Computer.FileSystem.DirectoryExists(Path) = False Then Return False '确定安装目录存在
        Try '向安装目录里写一个空文件以确定该目录可写
            My.Computer.FileSystem.WriteAllText(Path & "\test", Nothing, False)
        Catch ex As Security.SecurityException
            MessageBox.Show(TextBoxName & "的安装目录无法访问！" & vbCrLf & vbCrLf & "可能的原因：" & vbCrLf & "当前用户没有足够的权限访问安装目录或其父目录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As UnauthorizedAccessException
            MessageBox.Show(TextBoxName & "的安装目录无法访问！" & vbCrLf & vbCrLf & "可能的原因：" & vbCrLf & "当前用户没有足够的权限访问安装目录或其父目录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Return False
        Finally
            If My.Computer.FileSystem.FileExists(Path & "\test") Then My.Computer.FileSystem.DeleteFile(Path & "\test")
        End Try
        Return True
    End Function

    Private Sub tvwOptions_BeforeCollapse(sender As Object, e As TreeViewCancelEventArgs) Handles tvwOptions.BeforeCollapse
        e.Cancel = True '禁止折叠树节点
    End Sub

    Private Sub tvwOptions_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvwOptions.NodeMouseClick
        '当使用鼠标左键点击安装组件列表框的项时更新点击的项的图标和ModuleMain.InstallOptions的值
        With ModuleMain.InstallOptions
            If e.Button <> Windows.Forms.MouseButtons.Left Then Exit Sub
            lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpaceByGB() & "GB 的硬盘空间"
            tvwOptions.BeginUpdate()
            Select Case e.Node.Name
                Case "模拟城市4 豪华版 镜像版"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("模拟城市4 豪华版 硬盘版", NodeCheckedState.radiounchecked)
                        If IsDAEMONToolsInstalled = False Then SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.checked) : .InstallDAEMONTools = True
                        .SC4Type = InstallOptions.SC4InstallType.ISO
                    End If
                Case "模拟城市4 豪华版 硬盘版"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("模拟城市4 豪华版 镜像版", NodeCheckedState.radiounchecked)
                        .SC4Type = InstallOptions.SC4InstallType.NoInstall
                    End If
                Case "DAEMON Tools Lite"
                    '只有在Data\SC4\CD文件夹存在或Data\SC4\NoInstall.rar文件存在且已选中模拟城市4 豪华版 硬盘版项时才更新
                    If (My.Computer.FileSystem.DirectoryExists("Data\SC4\CD") = True Or My.Computer.FileSystem.FileExists("Data\SC4\NoInstall.rar") = True) And _
                        GetNodeChecked("模拟城市4 豪华版 硬盘版") = NodeCheckedState.radiochecked Then
                        If GetNodeChecked(e.Node.Name) = NodeCheckedState.checked Then
                            SetNodeChecked(e.Node.Name, NodeCheckedState.unchecked) : .InstallDAEMONTools = False
                        ElseIf GetNodeChecked(e.Node.Name) = NodeCheckedState.unchecked Then
                            SetNodeChecked(e.Node.Name, NodeCheckedState.checked) : .InstallDAEMONTools = True
                        End If
                    End If
                Case "638补丁", "640补丁", "641补丁", "4GB补丁", "免CD补丁", "模拟城市4 启动器", "添加桌面图标", "添加开始菜单项"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.checked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.unchecked)
                        Select Case e.Node.Name
                            Case "638补丁" : .Install638Patch = False
                                '取消安装638补丁时同时取消640和641补丁的安装
                                SetNodeChecked("640补丁", NodeCheckedState.unchecked) : .Install640Patch = False
                                SetNodeChecked("641补丁", NodeCheckedState.unchecked) : .Install641Patch = False
                            Case "640补丁" : .Install640Patch = False
                                '取消安装640补丁时同时取消641补丁的安装
                                SetNodeChecked("641补丁", NodeCheckedState.unchecked) : .Install641Patch = False
                            Case "641补丁" : .Install641Patch = False
                            Case "4GB补丁" : .Install4GBPatch = False
                            Case "免CD补丁" : .InstallNoCDPatch = False
                            Case "模拟城市4 启动器" : .InstallSC4Launcher = False
                            Case "添加桌面图标" : .AddDesktopIcon = False
                            Case "添加开始菜单项" : .AddStartMenuItem = False
                        End Select
                    ElseIf GetNodeChecked(e.Node.Name) = NodeCheckedState.unchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.checked)
                        Select Case e.Node.Name
                            Case "638补丁" : .Install638Patch = True
                                '选择安装638补丁时同时取消免CD补丁的安装
                                SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : .InstallNoCDPatch = False
                            Case "640补丁" : .Install640Patch = True
                                '选择安装640补丁时同时选择安装638补丁以及取消免CD补丁的安装
                                SetNodeChecked("638补丁", NodeCheckedState.checked) : .Install638Patch = True
                                SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : .InstallNoCDPatch = False
                            Case "641补丁" : .Install641Patch = True
                                '选择安装641补丁时同时选择安装638和640补丁以及取消免CD补丁的安装
                                SetNodeChecked("638补丁", NodeCheckedState.checked) : .Install638Patch = True
                                SetNodeChecked("640补丁", NodeCheckedState.checked) : .Install640Patch = True
                                SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : .InstallNoCDPatch = False
                            Case "4GB补丁" : .Install4GBPatch = True
                            Case "免CD补丁" : .InstallNoCDPatch = True
                                '选择安装免CD补丁时同时取消638、640和641补丁的安装
                                SetNodeChecked("638补丁", NodeCheckedState.unchecked) : .Install638Patch = False
                                SetNodeChecked("640补丁", NodeCheckedState.unchecked) : .Install640Patch = False
                                SetNodeChecked("641补丁", NodeCheckedState.unchecked) : .Install641Patch = False
                            Case "模拟城市4 启动器" : .InstallSC4Launcher = True
                            Case "添加桌面图标" : .AddDesktopIcon = True
                            Case "添加开始菜单项" : .AddStartMenuItem = True
                        End Select
                    End If
                Case "繁体中文"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("简体中文", NodeCheckedState.radiounchecked)
                        SetNodeChecked("英语", NodeCheckedState.radiounchecked) : .LanguagePatch = InstallOptions.Language.TraditionalChinese
                    End If
                Case "简体中文"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("繁体中文", NodeCheckedState.radiounchecked)
                        SetNodeChecked("英语", NodeCheckedState.radiounchecked) : .LanguagePatch = InstallOptions.Language.SimplifiedChinese
                    End If
                Case "英语"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("繁体中文", NodeCheckedState.radiounchecked)
                        SetNodeChecked("简体中文", NodeCheckedState.radiounchecked) : .LanguagePatch = InstallOptions.Language.English
                    End If
            End Select
            tvwOptions.EndUpdate()
        End With
    End Sub

    Private Sub tvwOptions_NodeMouseHover(sender As Object, e As TreeNodeMouseHoverEventArgs) Handles tvwOptions.NodeMouseHover
        lblDAEMONlInstallDir.Visible = (e.Node.Name = "DAEMON Tools Lite") '隐藏或显示选择DAEMON Tools Lite安装路径文本框和按钮
        txtDAEMONlInstallDir.Visible = (e.Node.Name = "DAEMON Tools Lite")
        btnDAEMONlInstallDir.Visible = (e.Node.Name = "DAEMON Tools Lite")
        Select Case e.Node.Name '更新组件信息文本和该组件所需要的磁盘空间文本
            Case "必选组件", "可选组件", "附加任务", "语言补丁"
                lblOptionDetail.Text = "请将鼠标放在组件名上以查看组件详情" : lblOptionDiskSpace.Text = ""
            Case "模拟城市4 豪华版 镜像版"
                lblOptionDetail.Text = "安装模拟城市4 豪华版 镜像版" & vbCrLf & vbCrLf & "安装镜像版时必须同时安装DAEMON Tools" & vbCrLf & vbCrLf & "建议安装镜像版模拟城市4，不建议安装硬盘版模拟城市4。"
                lblOptionDiskSpace.Text = "此组件需要 " & Math.Round(InstallOptions.SC4NeedsDiskSpace / 1024 / 1024 / 1024, 2) & "GB 的硬盘空间"
            Case "模拟城市4 豪华版 硬盘版"
                lblOptionDetail.Text = "安装模拟城市4 豪华版 硬盘版" & vbCrLf & vbCrLf & "安装硬盘版时不必同时安装DAEMON Tools" & vbCrLf & vbCrLf & "建议安装镜像版模拟城市4，不建议安装硬盘版模拟城市4。"
                lblOptionDiskSpace.Text = "此组件需要 " & Math.Round(InstallOptions.SC4NeedsDiskSpace / 1024 / 1024 / 1024, 2) & "GB 的硬盘空间"
            Case "DAEMON Tools Lite"
                lblOptionDetail.Text = "安装DAEMON Tools Lite 5.0" & vbCrLf & vbCrLf & "DAEMON Tools用于加载虚拟光驱" & vbCrLf & vbCrLf & "如果要安装镜像版模拟城市4，则必须安装此项。"
                lblOptionDiskSpace.Text = "此组件需要 " & InstallOptions.DAEMONToolsNeedsDiskSpace \ 1024 \ 1024 & "MB 的硬盘空间"
            Case "638补丁"
                lblOptionDetail.Text = "安装638补丁" & vbCrLf & vbCrLf & "638补丁修复了一些bug" & vbCrLf & vbCrLf & "建议同时安装640灯光补丁。"
                lblOptionDiskSpace.Text = "此组件需要 " & InstallOptions._638NeedsDiskSpace \ 1024 & "KB 的硬盘空间"
            Case "640补丁"
                lblOptionDetail.Text = "安装640灯光补丁" & vbCrLf & vbCrLf & "如果不安装640补丁大部分的插件在晚上不会显示灯光" & vbCrLf & vbCrLf & "安装此补丁必须同时安装638补丁，建议安装。"
                lblOptionDiskSpace.Text = "此组件需要 " & InstallOptions._640NeedsDiskSpace \ 1024 & "KB 的硬盘空间"
            Case "641补丁"
                lblOptionDetail.Text = "安装641官方免CD补丁" & vbCrLf & vbCrLf & "安装此补丁后打开游戏前不需要加载CD2虚拟光驱" & vbCrLf & vbCrLf & "安装此补丁必须同时安装640补丁和638补丁" & vbCrLf & vbCrLf & "建议安装此补丁，不建议安装非官方免CD补丁。"
                lblOptionDiskSpace.Text = "此组件需要 " & InstallOptions._641NeedsDiskSpace \ 1024 & "KB 的硬盘空间"
            Case "4GB补丁"
                lblOptionDetail.Text = "安装4GB补丁" & vbCrLf & vbCrLf & "安装此补丁后游戏跳出的几率会大大降低" & vbCrLf & vbCrLf & "仅64位系统可以安装。"
                lblOptionDiskSpace.Text = "此组件需要 0KB 的硬盘空间"
            Case "免CD补丁"
                lblOptionDetail.Text = "安装非官方免CD补丁" & vbCrLf & vbCrLf & "安装此补丁后打开游戏前不需要加载CD2虚拟光驱" & vbCrLf & vbCrLf & "安装此补丁的同时不能安装638、640和641补丁" & vbCrLf & vbCrLf & "641补丁同样有免CD的功能，建议安装641补丁，不建议安装此补丁。"
                lblOptionDiskSpace.Text = "此组件需要 " & InstallOptions.NoCDNeedsDiskSpace \ 1024 & "KB 的硬盘空间"
            Case "模拟城市4 启动器"
                lblOptionDetail.Text = "安装模拟城市4 启动器" & vbCrLf & vbCrLf & "启动器可以方便地以带参数的模式启动游戏" & vbCrLf & vbCrLf & "建议安装。"
                lblOptionDiskSpace.Text = "此组件需要 " & InstallOptions.SC4LauncherNeedsDiskSpace \ 1024 & "KB 的硬盘空间"
            Case "语言补丁"
                lblOptionDetail.Text = "安装语言补丁" & vbCrLf & vbCrLf & "如果不安装语言补丁" & vbCrLf & vbCrLf & "默认语言为英语" & vbCrLf & vbCrLf & "建议安装。"
                lblOptionDiskSpace.Text = ""
            Case "繁体中文"
                lblOptionDetail.Text = "安装繁体中文语言补丁" & vbCrLf & vbCrLf & "建议使用繁体中文语言补丁。"
                lblOptionDiskSpace.Text = "此组件需要 0KB 的硬盘空间"
            Case "简体中文"
                lblOptionDetail.Text = "安装简体中文语言补丁" & vbCrLf & vbCrLf & "建议使用繁体中文语言补丁，不建议使用简体中文语言补丁。"
                lblOptionDiskSpace.Text = "此组件需要 " & InstallOptions.LanguageSimplifiedChineseNeedsDiskSpace \ 1024 & "KB 的硬盘空间"
            Case "英语"
                lblOptionDetail.Text = "安装英语语言补丁" & vbCrLf & vbCrLf & "建议使用繁体中文语言补丁。"
                lblOptionDiskSpace.Text = "此组件需要 0KB 的硬盘空间"
            Case "添加桌面图标"
                lblOptionDetail.Text = "添加一个游戏的快捷方式到桌面上。" : lblOptionDiskSpace.Text = ""
            Case "添加开始菜单项"
                lblOptionDetail.Text = "在开始菜单里添加模拟城市4 豪华版的快捷方式。" : lblOptionDiskSpace.Text = ""
        End Select
    End Sub

    Private Sub cmbOptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOptions.SelectedIndexChanged
        tvwOptions.BeginUpdate()
        Select Case cmbOptions.SelectedItem
            Case "完全安装", "推荐安装", "精简安装"
                If IsCDDirectoryExists = True Then '如果存在Data\SC4\CD文件夹则选择安装镜像版模拟城市4
                    SetNodeChecked("模拟城市4 豪华版 镜像版", NodeCheckedState.radiochecked)
                    If IsSC4FileExists = True Then SetNodeChecked("模拟城市4 豪华版 硬盘版", NodeCheckedState.radiounchecked)
                    ModuleMain.InstallOptions.SC4Type = InstallOptions.SC4InstallType.ISO '更新ModuleMain.InstallOptions.SC4Type的值
                ElseIf IsSC4FileExists = True Then '如果存在Data\SC4\NoInstall.rar文件则选中安装硬盘版模拟城市4项
                    SetNodeChecked("模拟城市4 豪华版 硬盘版", NodeCheckedState.radiochecked)
                    If IsCDDirectoryExists = True Then SetNodeChecked("模拟城市4 豪华版 镜像版", NodeCheckedState.radiounchecked)
                    ModuleMain.InstallOptions.SC4Type = InstallOptions.SC4InstallType.NoInstall '更新ModuleMain.InstallOptions.SC4Type的值
                End If
                If IsDAEMONToolsInstalled = False Then SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.checked) : ModuleMain.InstallOptions.InstallDAEMONTools = True '如果尚未安装DAEMON Tools Lite则选中DAEMON Tools Lite项
                With ModuleMain.InstallOptions
                    Select Case cmbOptions.SelectedItem
                        Case "完成安装"
                            '更新安装组件列表框项的图标
                            SetNodeChecked("638补丁", NodeCheckedState.checked) : SetNodeChecked("640补丁", NodeCheckedState.checked) : SetNodeChecked("641补丁", NodeCheckedState.checked)
                            If Environment.Is64BitOperatingSystem = True Then SetNodeChecked("4GB补丁", NodeCheckedState.checked) : .Install4GBPatch = True Else .Install4GBPatch = False '如果系统为64位系统，则选中4GB补丁项
                            SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : SetNodeChecked("模拟城市4 启动器", NodeCheckedState.checked)
                            SetNodeChecked("繁体中文", NodeCheckedState.radiochecked) : SetNodeChecked("简体中文", NodeCheckedState.radiounchecked) : SetNodeChecked("英语", NodeCheckedState.radiounchecked)
                            SetNodeChecked("添加桌面图标", NodeCheckedState.unchecked) : SetNodeChecked("添加开始菜单项", NodeCheckedState.checked)
                            '更新ModuleMain.InstallOptions的值
                            .Install638Patch = True : .Install640Patch = True : .Install641Patch = True
                            .InstallNoCDPatch = False : .InstallSC4Launcher = True
                            .LanguagePatch = InstallOptions.Language.TraditionalChinese : .AddDesktopIcon = False : .AddStartMenuItem = True
                            lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpaceByGB() & "GB 的硬盘空间"
                        Case "推荐安装"
                            '更新安装组件列表框项的图标
                            SetNodeChecked("638补丁", NodeCheckedState.checked) : SetNodeChecked("640补丁", NodeCheckedState.checked) : SetNodeChecked("641补丁", NodeCheckedState.checked)
                            If Environment.Is64BitOperatingSystem = True Then SetNodeChecked("4GB补丁", NodeCheckedState.unchecked) '如果系统为64位系统，则选中4GB补丁项
                            SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : SetNodeChecked("模拟城市4 启动器", NodeCheckedState.checked)
                            SetNodeChecked("繁体中文", NodeCheckedState.radiochecked) : SetNodeChecked("简体中文", NodeCheckedState.radiounchecked) : SetNodeChecked("英语", NodeCheckedState.radiounchecked)
                            SetNodeChecked("添加桌面图标", NodeCheckedState.unchecked) : SetNodeChecked("添加开始菜单项", NodeCheckedState.checked)
                            '更新ModuleMain.InstallOptions的值
                            .Install638Patch = True : .Install640Patch = True : .Install641Patch = True
                            .Install4GBPatch = False : .InstallNoCDPatch = False : .InstallSC4Launcher = True
                            .LanguagePatch = InstallOptions.Language.TraditionalChinese : .AddDesktopIcon = False : .AddStartMenuItem = True
                            lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpaceByGB() & "GB 的硬盘空间"
                        Case "精简安装"
                            If IsCDDirectoryExists = True Then
                                SetNodeChecked("模拟城市4 豪华版 镜像版", NodeCheckedState.radiochecked)
                                If IsSC4FileExists = True Then SetNodeChecked("模拟城市4 豪华版 硬盘版", NodeCheckedState.radiounchecked)
                                If IsDAEMONToolsInstalled = False Then SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.checked) : ModuleMain.InstallOptions.InstallDAEMONTools = True
                                ModuleMain.InstallOptions.SC4Type = InstallOptions.SC4InstallType.ISO
                            ElseIf IsSC4FileExists = True Then
                                SetNodeChecked("模拟城市4 豪华版 硬盘版", NodeCheckedState.radiochecked)
                                If IsCDDirectoryExists = True Then SetNodeChecked("模拟城市4 豪华版 镜像版", NodeCheckedState.radiounchecked)
                                If IsDAEMONToolsInstalled = False Then SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.unchecked) : ModuleMain.InstallOptions.InstallDAEMONTools = False
                                ModuleMain.InstallOptions.SC4Type = InstallOptions.SC4InstallType.NoInstall
                            End If
                            '更新安装组件列表框项的图标
                            SetNodeChecked("638补丁", NodeCheckedState.unchecked) : SetNodeChecked("640补丁", NodeCheckedState.unchecked) : SetNodeChecked("641补丁", NodeCheckedState.unchecked)
                            If Environment.Is64BitOperatingSystem = True Then SetNodeChecked("4GB补丁", NodeCheckedState.unchecked) '如果系统为64位系统，则选中4GB补丁项
                            SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : SetNodeChecked("模拟城市4 启动器", NodeCheckedState.unchecked)
                            SetNodeChecked("繁体中文", NodeCheckedState.radiochecked) : SetNodeChecked("简体中文", NodeCheckedState.radiounchecked) : SetNodeChecked("英语", NodeCheckedState.radiounchecked)
                            SetNodeChecked("添加桌面图标", NodeCheckedState.unchecked) : SetNodeChecked("添加开始菜单项", NodeCheckedState.checked)
                            '更新ModuleMain.InstallOptions的值
                            .Install638Patch = False : .Install640Patch = False : .Install641Patch = False
                            .Install4GBPatch = False : .InstallNoCDPatch = False : .InstallSC4Launcher = False
                            .LanguagePatch = InstallOptions.Language.TraditionalChinese : .AddDesktopIcon = False : .AddStartMenuItem = True
                            lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpaceByGB() & "GB 的硬盘空间"
                    End Select
                End With
                tvwOptions.Enabled = False
            Case "自定义"
                tvwOptions.Enabled = True
        End Select
        tvwOptions.EndUpdate()
    End Sub

    Private Sub tmrCheckMousePosition_Tick(sender As Object, e As EventArgs) Handles tmrCheckMousePosition.Tick
        Dim rect As Rectangle = pnlOptions.Bounds, x As Integer = MousePosition.X - Me.Left, y As Integer = MousePosition.Y - Me.Top
        If (x <= rect.Left Or x >= rect.Right Or y <= rect.Top Or y >= rect.Bottom) = True Then '判断鼠标是否不在安装组件容器内
            lblOptionDetail.Text = "请将鼠标放在组件名上以查看组件详情" : lblOptionDiskSpace.Text = ""
            lblDAEMONlInstallDir.Visible = False : txtDAEMONlInstallDir.Visible = False : btnDAEMONlInstallDir.Visible = False '隐藏或显示选择DAEMON Tools Lite安装路径文本框和按钮
        End If
    End Sub

    Private Sub btnSC4InstallDir_Click(sender As Object, e As EventArgs) Handles btnSC4InstallDir.Click
        fbdSC4InstallDir.SelectedPath = txtSC4InstallDir.Text '将选择模拟城市4安装路径对话框的选择路径设为模拟城市4安装路径文本框的路径
        If fbdSC4InstallDir.ShowDialog = Windows.Forms.DialogResult.OK And fbdSC4InstallDir.SelectedPath <> Nothing Then txtSC4InstallDir.Text = fbdSC4InstallDir.SelectedPath
    End Sub

    Private Sub btnDAEMONlInstallDir_Click(sender As Object, e As EventArgs) Handles btnDAEMONlInstallDir.Click
        fbdDAEMONlInstallDir.SelectedPath = txtSC4InstallDir.Text '将选择DAEMON Tools Lite安装路径对话框的选择路径设为DAEMON Tools Lite安装路径文本框的路径
        If fbdDAEMONlInstallDir.ShowDialog = Windows.Forms.DialogResult.OK And fbdDAEMONlInstallDir.SelectedPath <> Nothing Then txtDAEMONlInstallDir.Text = fbdDAEMONlInstallDir.SelectedPath
    End Sub

    Private Sub frmInstallOptions_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("确定要退出安装程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then e.Cancel = True
    End Sub

    Private Sub frmInstallOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tvwOptions.ExpandAll() '展开所有的树节点
        With ModuleMain.InstallOptions
            If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Disc Soft\DAEMON Tools Lite", "Path", Nothing) <> Nothing Then '如果已安装DAEMON Tools Lite则删除安装选项列表框的DAEMON Tools Lite项
                tvwOptions.Nodes.Find("DAEMON Tools Lite", True)(0).Remove() : IsDAEMONToolsInstalled = True : .InstallDAEMONTools = False
            End If
            If My.Computer.FileSystem.DirectoryExists("Data\SC4\CD") = False Then '如果不存在Data\SC4\CD文件夹则删除安装选项列表框的模拟城市4 豪华版 镜像版项
                tvwOptions.Nodes.Find("模拟城市4 豪华版 镜像版", True)(0).Remove() : IsCDDirectoryExists = False : .SC4Type = InstallOptions.SC4InstallType.NoInstall
            Else : IsCDDirectoryExists = True
            End If
            If My.Computer.FileSystem.FileExists("Data\SC4\NoInstall.rar") = False Then '如果不存在Data\SC4\NoInstall.rar文件则删除安装选项列表框的模拟城市4 豪华版 硬盘版项
                tvwOptions.Nodes.Find("模拟城市4 豪华版 硬盘版", True)(0).Remove() : IsSC4FileExists = False : .SC4Type = InstallOptions.SC4InstallType.ISO
            Else : IsSC4FileExists = True
            End If
            If Environment.Is64BitOperatingSystem = False Then tvwOptions.Nodes.Find("4GB补丁", True)(0).Remove() : .Install4GBPatch = False '如果系统不是64位系统则删除安装组件列表框里的4GB补丁项
            cmbOptions.SelectedItem = cmbOptions.Items(1) '自动选择安装选项
            lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpaceByGB() & "GB 的硬盘空间" '更新当前选择的组件所需要的磁盘空间的文本
        End With
        lblOptionDetail.Text = "请将鼠标放在组件名上以查看组件详情" : lblOptionDiskSpace.Text = ""
        txtSC4InstallDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\Maxis\SimCity 4 Deluxe" '初始化模拟城市4的安装目录路径
        txtDAEMONlInstallDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\DAEMON Tools Lite" '初始化DAEMON Tools Lite的安装目录路径
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        frmMain.Show()
        RemoveHandler Me.FormClosing, AddressOf frmInstallOptions_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
        Close()
    End Sub

    Private Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        With ModuleMain.InstallOptions
            If IsPathValidated(txtSC4InstallDir) = False Then Exit Sub '判断模拟城市4安装目录的路径是否有效
            If txtSC4InstallDir.Text.Substring(1, txtSC4InstallDir.Text.Length - 1).EndsWith(":\") = False And _
                txtSC4InstallDir.Text.EndsWith("\") = True Then '如果安装路径以\结尾且不是分区根路径则去掉结尾的\
                .SC4InstallDir = txtSC4InstallDir.Text.TrimEnd("\").Trim
            Else : .SC4InstallDir = txtSC4InstallDir.Text.Trim
            End If
            If IsDAEMONToolsInstalled = False Then
                If IsPathValidated(txtDAEMONlInstallDir) = False Then Exit Sub '判断DAEMON Tools Lite安装目录的路径是否有效
                If txtSC4InstallDir.Text.Substring(1, txtSC4InstallDir.Text.Length - 1).EndsWith(":\") = False And _
                    txtDAEMONlInstallDir.Text.EndsWith("\") = True Then '如果安装路径以\结尾且不是分区根路径则去掉结尾的\
                    .DAEMONInstallDir = txtDAEMONlInstallDir.Text.TrimEnd("\").Trim
                Else : .DAEMONInstallDir = txtDAEMONlInstallDir.Text.Trim
                End If
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