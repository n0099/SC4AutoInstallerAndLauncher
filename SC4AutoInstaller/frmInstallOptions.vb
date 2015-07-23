Public Class frmInstallOptions

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

    ''' <summary>设置安装组件列表框里项的图标</summary>
    ''' <param name="NodeName">安装组件列表框项的Name属性值</param>
    ''' <param name="value">要设置的图标，必须为 NodeCheckedState 枚举的值之一</param>
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

    ''' <summary>验证指定文本框的文本是否为有效的目录路径</summary>
    ''' <param name="TextBox">要验证的文本框</param>
    ''' <returns>如果文本框的文本为有效的目录路径，则为True；否则为False</returns>
    Private Function IsPathValidated(ByVal TextBox As TextBox) As Boolean
        With TextBox
            Dim PathName As String = IIf(.Name = "txtDAEMONlInstallDir", "DAEMON Tools Lite", "模拟城市4")
            If .Text = Nothing Then MessageBox.Show("安装路径不能为空！" & vbCrLf & "您必须输入一个带驱动器卷标的完整路径！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
            If System.Text.RegularExpressions.Regex.IsMatch(.Text, "[A-Za-z]\:\\") = False Then
                MessageBox.Show("安装路径格式不正确！" & vbCrLf & "您必须输入一个带驱动器卷标和安装文件夹名的完整路径！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
            ElseIf System.Text.RegularExpressions.Regex.IsMatch(.Text.Remove(0, .Text.IndexOf("\")), "[\,/,:,*,?,"",<,>,|]") = True Then
                MessageBox.Show(PathName & "安装文件夹名不能包含下列任何字符：" & vbCrLf & "\ / : * ? "" < > |", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
            ElseIf IsNothing(My.Computer.FileSystem.GetDriveInfo(.Text.Substring(0, 1))) = True Then
                MessageBox.Show("安装路径的驱动器不存在！" & vbCrLf & "请检查拼写是否错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
                Select Case My.Computer.FileSystem.GetDriveInfo(.Text.Substring(0, 1)).DriveType
                    Case IO.DriveType.CDRom
                        MessageBox.Show("不能将" & PathName & "安装在光驱驱动器上！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
                    Case IO.DriveType.Network
                        MessageBox.Show("不能将" & PathName & "安装在网络驱动器上！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
                    Case IO.DriveType.Removable
                        MessageBox.Show("不能将" & PathName & "安装在可移动驱动器上！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
                    Case IO.DriveType.Ram
                        MessageBox.Show("不能将" & PathName & "安装在内存驱动器上！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
                End Select
            Else
                Try
                    If My.Computer.FileSystem.DirectoryExists(.Text) = True Then
                        My.Computer.FileSystem.WriteAllText(.Text & "\test.txt", "", False)
                        If My.Computer.FileSystem.FileExists(.Text & "\test.txt") = True Then My.Computer.FileSystem.DeleteFile(.Text & "\test.txt")
                    Else
                        My.Computer.FileSystem.CreateDirectory(TextBox.Text)
                    End If
                Catch ex As IO.PathTooLongException : MessageBox.Show(PathName & "的安装文件夹名太长！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
                Catch ex As IO.IOException : MessageBox.Show(PathName & "的安装路径无法访问！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
                Catch ex As UnauthorizedAccessException : MessageBox.Show(PathName & "的安装路径无法访问！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
                Catch ex As Exception : If My.Computer.FileSystem.FileExists(.Text & "\test.txt") = True Then My.Computer.FileSystem.DeleteFile(.Text & "\test.txt") : Return False
                End Try
            End If
            Return True
        End With
    End Function

    Private Sub tvwOptions_BeforeCollapse(sender As Object, e As TreeViewCancelEventArgs) Handles tvwOptions.BeforeCollapse
        e.Cancel = True
    End Sub

    Private Sub tvwOptions_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvwOptions.NodeMouseClick
        With ModuleMain.InstallOptions
            If e.Button <> Windows.Forms.MouseButtons.Left Then Exit Sub
            lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpaceByGB() & "GB 的硬盘空间"
            tvwOptions.BeginUpdate()
            Select Case e.Node.Name
                Case "模拟城市4 豪华版 镜像版"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("模拟城市4 豪华版 硬盘版", NodeCheckedState.radiounchecked)
                        If tvwOptions.Nodes.Find("DAEMON Tools Lite", True).Length <> 0 Then
                            SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.checked) : .InstallDAEMONTools = True
                        End If
                        .SC4Type = InstallOptions.SC4InstallType.ISO
                    End If
                Case "模拟城市4 豪华版 硬盘版"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("模拟城市4 豪华版 镜像版", NodeCheckedState.radiounchecked)
                        .SC4Type = InstallOptions.SC4InstallType.NoInstall
                    End If
                Case "DAEMON Tools Lite"
                    If GetNodeChecked("模拟城市4 豪华版 硬盘版") = NodeCheckedState.radiochecked Then
                        If GetNodeChecked(e.Node.Name) = NodeCheckedState.checked Then
                            SetNodeChecked(e.Node.Name, NodeCheckedState.unchecked) : .InstallDAEMONTools = False
                        ElseIf GetNodeChecked(e.Node.Name) = NodeCheckedState.unchecked Then
                            SetNodeChecked(e.Node.Name, NodeCheckedState.checked) : .InstallDAEMONTools = True
                        End If
                    End If
                Case "DAEMON Tools Lite", "638补丁", "640补丁", "641补丁", "4GB补丁", "免CD补丁", "模拟城市4 启动器", "添加桌面图标", "添加开始菜单项"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.checked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.unchecked)
                        Select Case e.Node.Name
                            Case "638补丁" : .Install638Patch = False
                                SetNodeChecked("640补丁", NodeCheckedState.unchecked) : .Install640Patch = False
                                SetNodeChecked("641补丁", NodeCheckedState.unchecked) : .Install641Patch = False
                            Case "640补丁" : .Install640Patch = False
                                SetNodeChecked("638补丁", NodeCheckedState.unchecked) : .Install638Patch = False
                                SetNodeChecked("641补丁", NodeCheckedState.unchecked) : .Install641Patch = False
                            Case "641补丁" : .Install641Patch = False
                                SetNodeChecked("638补丁", NodeCheckedState.unchecked) : .Install638Patch = False
                                SetNodeChecked("640补丁", NodeCheckedState.unchecked) : .Install640Patch = False
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
                                SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : .InstallNoCDPatch = False
                            Case "640补丁" : .Install640Patch = True
                                SetNodeChecked("638补丁", NodeCheckedState.checked) : .Install638Patch = True
                                SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : .InstallNoCDPatch = False
                            Case "641补丁" : .Install641Patch = True
                                SetNodeChecked("638补丁", NodeCheckedState.checked) : .Install638Patch = True
                                SetNodeChecked("640补丁", NodeCheckedState.checked) : .Install640Patch = True
                                SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : .InstallNoCDPatch = False
                            Case "4GB补丁" : .Install4GBPatch = True
                            Case "免CD补丁" : .InstallNoCDPatch = True
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

    Private Sub tvwOptions_MouseLeave(sender As Object, e As EventArgs) Handles tvwOptions.MouseLeave
        lblOptionsDetail.Text = "请将鼠标放在组件名上以查看组件详情。" : lblOptionsDiskSpace.Text = ""
        lblDAEMONlInstallDir.Visible = False : txtDAEMONlInstallDir.Visible = False : btnDAEMONlInstallDir.Visible = False
    End Sub

    Private Sub tvwOptions_NodeMouseHover(sender As Object, e As TreeNodeMouseHoverEventArgs) Handles tvwOptions.NodeMouseHover
        lblDAEMONlInstallDir.Visible = e.Node.Name = "DAEMON Tools Lite"
        txtDAEMONlInstallDir.Visible = e.Node.Name = "DAEMON Tools Lite"
        btnDAEMONlInstallDir.Visible = e.Node.Name = "DAEMON Tools Lite"
        Select Case e.Node.Name
            Case "必选组件", "可选组件", "附加任务", "语言补丁"
                lblOptionsDetail.Text = "请将鼠标放在组件名上以查看组件详情。" : lblOptionsDiskSpace.Text = ""
            Case "模拟城市4 豪华版 镜像版"
                lblOptionsDetail.Text = "安装模拟城市4 豪华版 镜像版" & vbCrLf & vbCrLf & "安装镜像版时必须同时安装DAEMON Tools" & vbCrLf & vbCrLf & "建议安装镜像版模拟城市4，不建议安装硬盘版模拟城市4。"
                lblOptionsDiskSpace.Text = "此组件需要 " & Math.Round(InstallOptions.SC4NeedsDiskSpace / 1024 / 1024 / 1024, 2) & "GB 的硬盘空间"
            Case "模拟城市4 豪华版 硬盘版"
                lblOptionsDetail.Text = "安装模拟城市4 豪华版 硬盘版" & vbCrLf & vbCrLf & "安装硬盘版时不必同时安装DAEMON Tools" & vbCrLf & vbCrLf & "建议安装镜像版模拟城市4，不建议安装硬盘版模拟城市4。"
                lblOptionsDiskSpace.Text = "此组件需要 " & Math.Round(InstallOptions.SC4NeedsDiskSpace / 1024 / 1024 / 1024, 2) & "GB 的硬盘空间"
            Case "DAEMON Tools Lite"
                lblOptionsDetail.Text = "安装DAEMON Tools Lite 5.0" & vbCrLf & vbCrLf & "DAEMON Tools用于加载虚拟光驱" & vbCrLf & vbCrLf & "如果要安装镜像版模拟城市4，则必须安装此项。"
                lblOptionsDiskSpace.Text = "此组件需要 " & InstallOptions.DAEMONNeedsDiskSpace \ 1024 \ 1024 & "MB 的硬盘空间"
            Case "638补丁"
                lblOptionsDetail.Text = "安装638补丁" & vbCrLf & vbCrLf & "638补丁修复了一些bug" & vbCrLf & vbCrLf & "建议同时安装640灯光补丁。"
                lblOptionsDiskSpace.Text = "此组件需要 " & InstallOptions._638NeedsDiskSpace \ 1024 & "KB 的硬盘空间"
            Case "640补丁"
                lblOptionsDetail.Text = "安装640灯光补丁" & vbCrLf & vbCrLf & "如果不安装640补丁大部分的插件在晚上不会显示灯光" & vbCrLf & vbCrLf & "安装此补丁必须同时安装638补丁，建议安装。"
                lblOptionsDiskSpace.Text = "此组件需要 " & InstallOptions._640NeedsDiskSpace \ 1024 & "KB 的硬盘空间"
            Case "641补丁"
                lblOptionsDetail.Text = "安装641官方免CD补丁" & vbCrLf & vbCrLf & "安装此补丁后打开游戏前不需要加载CD2虚拟光驱" & vbCrLf & vbCrLf & "安装此补丁必须同时安装640补丁和638补丁" & vbCrLf & vbCrLf & "建议安装此补丁，不建议安装非官方免CD补丁。"
                lblOptionsDiskSpace.Text = "此组件需要 " & InstallOptions._641NeedsDiskSpace \ 1024 & "KB 的硬盘空间"
            Case "4GB补丁"
                lblOptionsDetail.Text = "安装4GB补丁" & vbCrLf & vbCrLf & "安装此补丁后游戏跳出的几率会大大降低" & vbCrLf & vbCrLf & "仅64位系统可以安装。"
                lblOptionsDiskSpace.Text = "此组件需要 0KB 的硬盘空间"
            Case "免CD补丁"
                lblOptionsDetail.Text = "安装非官方免CD补丁" & vbCrLf & vbCrLf & "安装此补丁后打开游戏前不需要加载CD2虚拟光驱" & vbCrLf & vbCrLf & "安装此补丁的同时不能安装638、640和641补丁" & vbCrLf & vbCrLf & "641补丁同样有免CD的功能，建议安装641补丁，不建议安装此补丁。"
                lblOptionsDiskSpace.Text = "此组件需要 " & InstallOptions.NoCDNeedsDiskSpace \ 1024 & "KB 的硬盘空间"
            Case "模拟城市4 启动器"
                lblOptionsDetail.Text = "安装模拟城市4 启动器" & vbCrLf & vbCrLf & "启动器可以方便地以带参数的模式启动游戏" & vbCrLf & vbCrLf & "建议安装。"
                lblOptionsDiskSpace.Text = "此组件需要 " & InstallOptions.SC4LauncherNeedsDiskSpace \ 1024 & "KB 的硬盘空间"
            Case "语言补丁"
                lblOptionsDetail.Text = "安装语言补丁" & vbCrLf & vbCrLf & "如果不安装语言补丁" & vbCrLf & vbCrLf & "默认语言为英语" & vbCrLf & vbCrLf & "建议安装。"
                lblOptionsDiskSpace.Text = ""
            Case "繁体中文"
                lblOptionsDetail.Text = "安装繁体中文语言补丁" & vbCrLf & vbCrLf & "建议使用繁体中文语言补丁。"
                lblOptionsDiskSpace.Text = "此组件需要 0KB 的硬盘空间"
            Case "简体中文"
                lblOptionsDetail.Text = "安装简体中文语言补丁" & vbCrLf & vbCrLf & "建议使用繁体中文语言补丁，不建议使用简体中文语言补丁。"
                lblOptionsDiskSpace.Text = "此组件需要 " & InstallOptions.LanguageSimplifiedChineseNeedsDiskSpace \ 1024 & "KB 的硬盘空间"
            Case "英语"
                lblOptionsDetail.Text = "安装英语语言补丁" & vbCrLf & vbCrLf & "建议使用繁体中文语言补丁。"
                lblOptionsDiskSpace.Text = "此组件需要 0KB 的硬盘空间"
            Case "添加桌面图标"
                lblOptionsDetail.Text = "添加一个快捷方式到桌面上。" : lblOptionsDiskSpace.Text = ""
            Case "添加开始菜单项"
                lblOptionsDetail.Text = "在开始菜单里添加程序快捷方式。" : lblOptionsDiskSpace.Text = ""
        End Select
    End Sub

    Private Sub cmbOptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOptions.SelectedIndexChanged
        tvwOptions.BeginUpdate()
        Select Case cmbOptions.SelectedItem
            Case "完全安装"
                SetNodeChecked("模拟城市4 豪华版 镜像版", NodeCheckedState.radiochecked) : SetNodeChecked("模拟城市4 豪华版 硬盘版", NodeCheckedState.radiounchecked)
                SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.checked)
                SetNodeChecked("638补丁", NodeCheckedState.checked) : SetNodeChecked("640补丁", NodeCheckedState.checked) : SetNodeChecked("641补丁", NodeCheckedState.checked)
                SetNodeChecked("4GB补丁", NodeCheckedState.checked) : SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : SetNodeChecked("模拟城市4 启动器", NodeCheckedState.checked)
                SetNodeChecked("繁体中文", NodeCheckedState.radiochecked) : SetNodeChecked("简体中文", NodeCheckedState.radiounchecked) : SetNodeChecked("英语", NodeCheckedState.radiounchecked)
                SetNodeChecked("添加桌面图标", NodeCheckedState.unchecked) : SetNodeChecked("添加开始菜单项", NodeCheckedState.checked)
                With ModuleMain.InstallOptions
                    .SC4Type = InstallOptions.SC4InstallType.ISO : .InstallDAEMONTools = True
                    .Install638Patch = True : .Install640Patch = True : .Install641Patch = True
                    .Install4GBPatch = True : .InstallNoCDPatch = False : .InstallSC4Launcher = True
                    .LanguagePatch = InstallOptions.Language.TraditionalChinese : .AddDesktopIcon = False : .AddStartMenuItem = True
                    lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpaceByGB() & "GB 的硬盘空间"
                End With
                tvwOptions.Enabled = False
            Case "推荐安装"
                SetNodeChecked("模拟城市4 豪华版 镜像版", NodeCheckedState.radiochecked) : SetNodeChecked("模拟城市4 豪华版 硬盘版", NodeCheckedState.radiounchecked)
                SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.checked)
                SetNodeChecked("638补丁", NodeCheckedState.checked) : SetNodeChecked("640补丁", NodeCheckedState.checked) : SetNodeChecked("641补丁", NodeCheckedState.checked)
                SetNodeChecked("4GB补丁", NodeCheckedState.unchecked) : SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : SetNodeChecked("模拟城市4 启动器", NodeCheckedState.checked)
                SetNodeChecked("繁体中文", NodeCheckedState.radiochecked) : SetNodeChecked("简体中文", NodeCheckedState.radiounchecked) : SetNodeChecked("英语", NodeCheckedState.radiounchecked)
                SetNodeChecked("添加桌面图标", NodeCheckedState.unchecked) : SetNodeChecked("添加开始菜单项", NodeCheckedState.checked)
                With ModuleMain.InstallOptions
                    .SC4Type = InstallOptions.SC4InstallType.ISO : .InstallDAEMONTools = True
                    .Install638Patch = True : .Install640Patch = True : .Install641Patch = True
                    .Install4GBPatch = False : .InstallNoCDPatch = False : .InstallSC4Launcher = True
                    .LanguagePatch = InstallOptions.Language.TraditionalChinese : .AddDesktopIcon = False : .AddStartMenuItem = True
                    lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpaceByGB() & "GB 的硬盘空间"
                End With
                tvwOptions.Enabled = False
            Case "精简安装"
                SetNodeChecked("模拟城市4 豪华版 镜像版", NodeCheckedState.radiochecked) : SetNodeChecked("模拟城市4 豪华版 硬盘版", NodeCheckedState.radiounchecked)
                SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.checked)
                SetNodeChecked("638补丁", NodeCheckedState.unchecked) : SetNodeChecked("640补丁", NodeCheckedState.unchecked) : SetNodeChecked("641补丁", NodeCheckedState.unchecked)
                SetNodeChecked("4GB补丁", NodeCheckedState.unchecked) : SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : SetNodeChecked("模拟城市4 启动器", NodeCheckedState.unchecked)
                SetNodeChecked("繁体中文", NodeCheckedState.radiochecked) : SetNodeChecked("简体中文", NodeCheckedState.radiounchecked) : SetNodeChecked("英语", NodeCheckedState.radiounchecked)
                SetNodeChecked("添加桌面图标", NodeCheckedState.unchecked) : SetNodeChecked("添加开始菜单项", NodeCheckedState.checked)
                With ModuleMain.InstallOptions
                    .SC4Type = InstallOptions.SC4InstallType.ISO : .InstallDAEMONTools = True
                    .Install638Patch = False : .Install640Patch = False : .Install641Patch = False
                    .Install4GBPatch = False : .InstallNoCDPatch = False : .InstallSC4Launcher = False
                    : .LanguagePatch = InstallOptions.Language.TraditionalChinese : .AddDesktopIcon = False : .AddStartMenuItem = True
                    lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpaceByGB() & "GB 的硬盘空间"
                End With
                tvwOptions.Enabled = False
            Case "自定义"
                tvwOptions.Enabled = True
        End Select
        tvwOptions.EndUpdate()
    End Sub

    Private Sub btnSC4InstallDir_Click(sender As Object, e As EventArgs) Handles btnSC4InstallDir.Click
        fbdSC4InstallDir.RootFolder = Environment.SpecialFolder.MyComputer : fbdSC4InstallDir.SelectedPath = txtSC4InstallDir.Text
        If fbdSC4InstallDir.ShowDialog = Windows.Forms.DialogResult.OK And fbdSC4InstallDir.SelectedPath <> Nothing Then txtSC4InstallDir.Text = fbdSC4InstallDir.SelectedPath
    End Sub

    Private Sub btnDAEMONlInstallDir_Click(sender As Object, e As EventArgs) Handles btnDAEMONlInstallDir.Click
        fbdDAEMONlInstallDir.RootFolder = Environment.SpecialFolder.MyComputer : fbdDAEMONlInstallDir.SelectedPath = txtSC4InstallDir.Text
        If fbdDAEMONlInstallDir.ShowDialog = Windows.Forms.DialogResult.OK And fbdDAEMONlInstallDir.SelectedPath <> Nothing Then txtDAEMONlInstallDir.Text = fbdDAEMONlInstallDir.SelectedPath
    End Sub

    Private Sub frmInstallOptions_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.ApplicationExitCall Then
            If MessageBox.Show("确定要退出安装程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then e.Cancel = True
        End If
    End Sub

    Private Sub frmInstallOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tvwOptions.ExpandAll()
        tvwOptions.ResumeLayout()
        With ModuleMain.InstallOptions
            cmbOptions.SelectedItem = cmbOptions.Items(1)
            lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpaceByGB() & "GB 的硬盘空间"
            If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Disc Soft\DAEMON Tools Lite", "Path", Nothing) <> Nothing Then tvwOptions.Nodes.Find("DAEMON Tools Lite", True)(0).Remove() : .InstallDAEMONTools = False
            If Environment.Is64BitOperatingSystem = False Then tvwOptions.Nodes.Find("4GB补丁", True)(0).Remove() : .Install4GBPatch = False
        End With
        lblOptionsDetail.Text = "请将鼠标放在组件名上以查看组件详情。" : lblOptionsDiskSpace.Text = ""
        txtSC4InstallDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\Maxis\SimCity 4 Deluxe"
        txtDAEMONlInstallDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\DAEMON Tools Lite"
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099"
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        frmMain.Show()
        RemoveHandler Me.FormClosing, AddressOf frmInstallOptions_FormClosing
        Close()
    End Sub

    Private Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        With ModuleMain.InstallOptions
            If IsPathValidated(txtSC4InstallDir) = False Then Exit Sub
            If txtSC4InstallDir.Text.EndsWith("\") = True Then .SC4InstallDir = txtSC4InstallDir.Text.Remove(txtSC4InstallDir.Text.Length - 1) Else .SC4InstallDir = txtSC4InstallDir.Text
            If tvwOptions.Nodes.Find("DAEMON Tools Lite", True).Length <> 0 Then
                If IsPathValidated(txtDAEMONlInstallDir) = False Then Exit Sub
                If txtDAEMONlInstallDir.Text.EndsWith("\") = True Then .DAEMONInstallDir = txtDAEMONlInstallDir.Text.Remove(txtDAEMONlInstallDir.Text.Length - 1) Else .DAEMONInstallDir = txtDAEMONlInstallDir.Text
            End If
        End With
        frmInstalling.Show()
        RemoveHandler Me.FormClosing, AddressOf frmInstallOptions_FormClosing
        Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Application.Exit()
    End Sub

End Class