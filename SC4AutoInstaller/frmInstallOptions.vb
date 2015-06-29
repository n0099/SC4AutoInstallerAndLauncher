Public Class frmInstallOptions

    Private Enum NodeCheckedState
        checked
        unchecked
        radiochecked
        radiounchecked
    End Enum

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

    Private Function IsPathValidated(ByVal TextBox As TextBox) As Boolean
        With TextBox
            Dim ReturnValue As Boolean = True
            If .Text = Nothing Then MessageBox.Show("安装路径不能为空！" & vbCrLf & "您必须输入一个带驱动器卷标的完整路径！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : ReturnValue = False
            If System.Text.RegularExpressions.Regex.IsMatch(.Text, "[A-Za-z]\:\\") = False Then
                MessageBox.Show("安装路径格式不正确！" & vbCrLf & "您必须输入一个带驱动器卷标和安装文件夹名的完整路径！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : ReturnValue = False
            ElseIf System.Text.RegularExpressions.Regex.IsMatch(.Text.Remove(0, .Text.IndexOf("\")), "[\,/,:,*,?,"",<,>,|]") = True Then
                MessageBox.Show(IIf(.Name = "txtDAEMONlInstallDir", "DAEMON Tools Lite", "模拟城市4") & " 安装文件夹名不能包含下列任何字符：" & vbCrLf & "\ / : * ? "" < > |", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : ReturnValue = False
            ElseIf IsNothing(My.Computer.FileSystem.GetDriveInfo(.Text.Substring(0, 1))) = True Then
                MessageBox.Show("安装路径的驱动器不存在！" & vbCrLf & "请检查拼写是否错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : ReturnValue = False
                Select Case My.Computer.FileSystem.GetDriveInfo(.Text.Substring(0, 1)).DriveType
                    Case IO.DriveType.CDRom
                        MessageBox.Show("不能将 " & IIf(.Name = "txtDAEMONlInstallDir", "DAEMON Tools Lite", "模拟城市4") & " 安装在光驱驱动器上！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : ReturnValue = False
                    Case IO.DriveType.Network
                        MessageBox.Show("不能将 " & IIf(.Name = "txtDAEMONlInstallDir", "DAEMON Tools Lite", "模拟城市4") & " 安装在网络驱动器上！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : ReturnValue = False
                    Case IO.DriveType.Removable
                        MessageBox.Show("不能将 " & IIf(.Name = "txtDAEMONlInstallDir", "DAEMON Tools Lite", "模拟城市4") & " 安装在可移动驱动器上！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : ReturnValue = False
                    Case IO.DriveType.Ram
                        MessageBox.Show("不能将 " & IIf(.Name = "txtDAEMONlInstallDir", "DAEMON Tools Lite", "模拟城市4") & " 安装在内存驱动器上！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : ReturnValue = False
                End Select
            Else
                Try
                    My.Computer.FileSystem.CreateDirectory(TextBox.Text)
                Catch ex As IO.PathTooLongException : MessageBox.Show(IIf(.Name = "txtDAEMONlInstallDir", "DAEMON Tools Lite", "模拟城市4") & " 安装文件夹名太长！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : ReturnValue = False
                Catch ex As IO.IOException : MessageBox.Show(IIf(.Name = "txtDAEMONlInstallDir", "DAEMON Tools Lite", "模拟城市4") & " 安装路径无法访问！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : ReturnValue = False
                Catch ex As UnauthorizedAccessException : MessageBox.Show(IIf(.Name = "txtDAEMONlInstallDir", "DAEMON Tools Lite", "模拟城市4") & " 安装路径无法访问！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : ReturnValue = False
                Catch : ReturnValue = False
                End Try
                Try : My.Computer.FileSystem.DeleteDirectory(TextBox.Text, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing) : Catch : End Try
            End If
            Return ReturnValue
        End With
    End Function

    Private Sub chkEAEULA_CheckedChanged(sender As Object, e As EventArgs) Handles chkEAEULA.CheckedChanged
        If chkEAEULA.Checked = True Then btnInstall.Enabled = True Else btnInstall.Enabled = False
    End Sub

    Private Sub llbEAEULA_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbEAEULA.LinkClicked
        Process.Start("Data\EA EULA.txt")
    End Sub

    Private Sub tvwOptions_BeforeCollapse(sender As Object, e As TreeViewCancelEventArgs) Handles tvwOptions.BeforeCollapse
        e.Cancel = True
    End Sub

    Private Sub tvwOptions_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvwOptions.NodeMouseClick
        With ModuleMain.InstallOptions
            If e.Button <> Windows.Forms.MouseButtons.Left Then Exit Sub
            Select Case e.Node.Name
                Case "模拟城市4 豪华版 镜像版"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("模拟城市4 豪华版 硬盘版", NodeCheckedState.radiounchecked)
                        If GetNodeChecked("免CD补丁") = NodeCheckedState.unchecked And tvwOptions.Nodes.Find("DAEMON Tools Lite", True).Length <> 0 Then
                            SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.checked) : .IsInstallDAEMONTools = True
                        End If
                        .SC4Type = InstallOptions.SC4InstallType.ISO
                        lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
                    End If
                Case "模拟城市4 豪华版 硬盘版"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("模拟城市4 豪华版 镜像版", NodeCheckedState.radiounchecked)
                        If GetNodeChecked("免CD补丁") = NodeCheckedState.unchecked And tvwOptions.Nodes.Find("DAEMON Tools Lite", True).Length <> 0 Then
                            SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.checked) : .IsInstallDAEMONTools = True
                        End If
                        .SC4Type = InstallOptions.SC4InstallType.NoInstall
                        lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
                    End If
                Case "638补丁", "640补丁", "4GB补丁", "免CD补丁", "模拟城市4 启动器"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.checked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.unchecked)
                        Select Case e.Node.Name
                            Case "638补丁" : .IsInstall638Patch = False : SetNodeChecked("640补丁", NodeCheckedState.unchecked)
                            Case "640补丁" : .IsInstall640Patch = False
                            Case "4GB补丁" : .IsInstall4GBPatch = False
                            Case "免CD补丁" : .IsInstallNoCDPatch = True
                            Case "模拟城市4 启动器" : .IsInstallSC4Launcher = False
                        End Select
                    ElseIf GetNodeChecked(e.Node.Name) = NodeCheckedState.unchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.checked)
                        Select Case e.Node.Name
                            Case "638补丁" : .IsInstall638Patch = True
                            Case "640补丁" : .IsInstall640Patch = True : SetNodeChecked("638补丁", NodeCheckedState.checked)
                            Case "4GB补丁" : .IsInstall4GBPatch = True
                            Case "免CD补丁" : .IsInstallNoCDPatch = True
                            Case "模拟城市4 启动器" : .IsInstallSC4Launcher = True
                        End Select
                    End If
                    lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
                Case "免CD补丁"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.checked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.unchecked) : .IsInstallNoCDPatch = False
                        If tvwOptions.Nodes.Find("DAEMON Tools Lite", True).Length <> 0 Then
                            SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.checked) : .IsInstallDAEMONTools = True
                        End If
                    ElseIf GetNodeChecked(e.Node.Name) = NodeCheckedState.unchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.checked) : .IsInstallNoCDPatch = True
                        If tvwOptions.Nodes.Find("DAEMON Tools Lite", True).Length <> 0 Then
                            SetNodeChecked("DAEMON Tools Lite", NodeCheckedState.unchecked) : .IsInstallDAEMONTools = False
                        End If
                    End If
                    lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
                Case "繁体中文"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("简体中文", NodeCheckedState.radiounchecked)
                        SetNodeChecked("英语", NodeCheckedState.radiounchecked) : .LanguagePatch = InstallOptions.Language.TraditionalChinese
                        lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
                    End If
                Case "简体中文"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("繁体中文", NodeCheckedState.radiounchecked)
                        SetNodeChecked("英语", NodeCheckedState.radiounchecked) : .LanguagePatch = InstallOptions.Language.SimplifiedChinese
                        lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
                    End If
                Case "英语"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("繁体中文", NodeCheckedState.radiounchecked)
                        SetNodeChecked("简体中文", NodeCheckedState.radiounchecked) : .LanguagePatch = InstallOptions.Language.English
                        lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
                    End If
                Case "添加桌面图标"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.checked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.unchecked) : .IsAddDesktopIcon = False
                    ElseIf GetNodeChecked(e.Node.Name) = NodeCheckedState.unchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.checked) : .IsAddDesktopIcon = True
                    End If
                    lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
                Case "添加开始菜单项"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.checked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.unchecked) : .IsAddStartMenuItem = False
                    ElseIf GetNodeChecked(e.Node.Name) = NodeCheckedState.unchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.checked) : .IsAddStartMenuItem = True
                    End If
                    lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
            End Select
        End With
        cmbOptions.SelectedItem = "自定义"
    End Sub

    Private Sub tvwOptions_NodeMouseHover(sender As Object, e As TreeNodeMouseHoverEventArgs) Handles tvwOptions.NodeMouseHover
        Select Case e.Node.Name
            Case "必选组件"
                lblOptionsDetail.Text = "请将鼠标放在组件名上以查看组件详情。" : lblOptionsDiskSpace.Text = ""
            Case "模拟城市4 豪华版 镜像版"
                lblOptionsDetail.Text = "安装模拟城市4 豪华版 镜像版" & vbCrLf & vbCrLf & "安装镜像版时必须同时安装DAEMON Tools。"
                lblOptionsDiskSpace.Text = "此组件需要 " & Math.Round(InstallOptions.SC4NeedsDiskSpace / 1024 / 1024 / 1024, 2) & "GB 硬盘空间"
            Case "模拟城市4 豪华版 硬盘版"
                lblOptionsDetail.Text = "安装模拟城市4 豪华版 硬盘版" & vbCrLf & vbCrLf & "安装硬盘版时不必同时安装DAEMON Tools。"
                lblOptionsDiskSpace.Text = "此组件需要 " & Math.Round(InstallOptions.SC4NeedsDiskSpace / 1024 / 1024 / 1024, 2) & "GB 硬盘空间"
            Case "DAEMON Tools Lite"
                If tvwOptions.Nodes.Find("DAEMON Tools Lite", True).Length <> 0 Then
                    lblOptionsDetail.Text = "安装DAEMON Tools Lite 5.0" & vbCrLf & vbCrLf & "DAEMON Tools用于加载虚拟光驱" & vbCrLf & vbCrLf & "如果不安装免CD补丁，则必须安装此项。"
                    lblOptionsDiskSpace.Text = "此组件需要 " & Int(InstallOptions.DAEMONNeedsDiskSpace / 1024 / 1024) & "MB 硬盘空间"
                    lblDAEMONlInstallDir.Visible = True : txtDAEMONlInstallDir.Visible = True : btnDAEMONlInstallDir.Visible = True
                End If
            Case "可选组件"
                lblOptionsDetail.Text = "请将鼠标放在组件名上以查看组件详情。" : lblOptionsDiskSpace.Text = ""
            Case "638补丁"
                lblOptionsDetail.Text = "安装638补丁" & vbCrLf & vbCrLf & "638补丁修复了一些bug" & vbCrLf & vbCrLf & "建议同时安装640灯光补丁。"
                lblOptionsDiskSpace.Text = "此组件需要 " & Int(InstallOptions._638NeedsDiskSpace / 1024) & "KB 硬盘空间"
            Case "640补丁"
                lblOptionsDetail.Text = "安装640灯光补丁" & vbCrLf & vbCrLf & "如果不安装640补丁大部分的插件在晚上不会显示灯光" & vbCrLf & vbCrLf & "建议安装。"
                lblOptionsDiskSpace.Text = "此组件需要 " & Int(InstallOptions._640NeedsDiskSpace / 1024) & "KB 硬盘空间"
            Case "4GB补丁"
                lblOptionsDetail.Text = "安装4GB补丁" & vbCrLf & vbCrLf & "安装此补丁后游戏跳出的几率会大大降低" & vbCrLf & vbCrLf & "仅64位系统可以安装。"
                lblOptionsDiskSpace.Text = "此组件需要 0KB 硬盘空间"
            Case "免CD补丁"
                lblOptionsDetail.Text = "安装免CD补丁" & vbCrLf & vbCrLf & "安装此补丁后打开游戏前不需要加载CD2虚拟光驱。"
                lblOptionsDiskSpace.Text = "此组件需要 " & Int(InstallOptions.NoCDNeedsDiskSpace / 1024) & "KB 硬盘空间"
            Case "模拟城市4 启动器"
                lblOptionsDetail.Text = "安装模拟城市4 启动器" & vbCrLf & vbCrLf & "启动器可以方便地以带参数的模式启动游戏" & vbCrLf & vbCrLf & "建议安装。"
                lblOptionsDiskSpace.Text = "此组件需要 " & Int(InstallOptions.SC4LauncherNeedsDiskSpace / 1024) & "KB 硬盘空间"
            Case "语言补丁"
                lblOptionsDetail.Text = "安装语言补丁" & vbCrLf & vbCrLf & "如果不安装语言补丁" & vbCrLf & vbCrLf & "默认语言为英语" & vbCrLf & vbCrLf & "建议安装。"
                lblOptionsDiskSpace.Text = ""
            Case "繁体中文"
                lblOptionsDetail.Text = "安装繁体中文语言补丁" & vbCrLf & vbCrLf & "如果不安装语言补丁" & vbCrLf & vbCrLf & "默认语言为英语" & vbCrLf & vbCrLf & "建议安装。"
                lblOptionsDiskSpace.Text = "此组件需要 0KB 硬盘空间"
            Case "简体中文"
                lblOptionsDetail.Text = "安装简体中文语言补丁" & vbCrLf & vbCrLf & "如果不安装语言补丁" & vbCrLf & vbCrLf & "默认语言为英语" & vbCrLf & vbCrLf & "不建议安装。"
                lblOptionsDiskSpace.Text = "此组件需要 " & Int(InstallOptions.LanguageSimplifiedChineseNeedsDiskSpace / 1024) & "KB 硬盘空间"
            Case "英语"
                lblOptionsDetail.Text = "安装英语语言补丁" & vbCrLf & vbCrLf & "不建议安装。"
                lblOptionsDiskSpace.Text = "此组件需要 0KB 硬盘空间"
            Case "附加任务"
                lblOptionsDetail.Text = "请将鼠标放在组件名上以查看组件详情。" : lblOptionsDiskSpace.Text = ""
            Case "添加桌面图标"
                lblOptionsDetail.Text = "添加一个快捷方式到桌面上。" : lblOptionsDiskSpace.Text = ""
            Case "添加开始菜单项"
                lblOptionsDetail.Text = "在开始菜单里添加程序快捷方式。" : lblOptionsDiskSpace.Text = ""
            Case Not "DAEMON Tools Lite"
                lblDAEMONlInstallDir.Visible = False : txtDAEMONlInstallDir.Visible = False : btnDAEMONlInstallDir.Visible = False
        End Select
    End Sub

    Private Sub cmbOptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOptions.SelectedIndexChanged
        Select Case cmbOptions.SelectedItem
            Case "完全安装"
                SetNodeChecked("638补丁", NodeCheckedState.checked) : SetNodeChecked("640补丁", NodeCheckedState.checked)
                SetNodeChecked("免CD补丁", NodeCheckedState.checked) : SetNodeChecked("4GB补丁", NodeCheckedState.checked)
                SetNodeChecked("模拟城市4 启动器", NodeCheckedState.checked) : tvwOptions.Enabled = False
                SetNodeChecked("添加桌面图标", NodeCheckedState.checked) : SetNodeChecked("添加开始菜单项", NodeCheckedState.checked)
                With ModuleMain.InstallOptions
                    .IsInstall638Patch = True : .IsInstall640Patch = True
                    .IsInstallNoCDPatch = True : .IsInstall4GBPatch = True : .IsInstallSC4Launcher = True
                    .IsAddDesktopIcon = True : .IsAddStartMenuItem = True
                    lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
                End With
            Case "推荐安装"
                SetNodeChecked("638补丁", NodeCheckedState.checked) : SetNodeChecked("640补丁", NodeCheckedState.checked)
                SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : SetNodeChecked("4GB补丁", NodeCheckedState.unchecked)
                SetNodeChecked("模拟城市4 启动器", NodeCheckedState.checked) : tvwOptions.Enabled = False
                SetNodeChecked("添加桌面图标", NodeCheckedState.checked) : SetNodeChecked("添加开始菜单项", NodeCheckedState.checked)
                With ModuleMain.InstallOptions
                    .IsInstall638Patch = True : .IsInstall640Patch = True
                    .IsInstallNoCDPatch = False : .IsInstall4GBPatch = False : .IsInstallSC4Launcher = True
                    .IsAddDesktopIcon = True : .IsAddStartMenuItem = True
                    lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
                End With
            Case "精简安装"
                SetNodeChecked("638补丁", NodeCheckedState.unchecked) : SetNodeChecked("640补丁", NodeCheckedState.unchecked)
                SetNodeChecked("免CD补丁", NodeCheckedState.unchecked) : SetNodeChecked("4GB补丁", NodeCheckedState.unchecked)
                SetNodeChecked("模拟城市4 启动器", NodeCheckedState.unchecked) : tvwOptions.Enabled = False
                SetNodeChecked("添加桌面图标", NodeCheckedState.unchecked) : SetNodeChecked("添加开始菜单项", NodeCheckedState.checked)
                With ModuleMain.InstallOptions
                    .IsInstall638Patch = False : .IsInstall640Patch = False
                    .IsInstallNoCDPatch = False : .IsInstall4GBPatch = False : .IsInstallSC4Launcher = False
                    .IsAddDesktopIcon = False : .IsAddStartMenuItem = True
                    lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
                End With
            Case "自定义"
                tvwOptions.Enabled = True
        End Select
    End Sub

    Private Sub btnSC4InstallDir_Click(sender As Object, e As EventArgs) Handles btnSC4InstallDir.Click
        fbdSC4InstallDir.RootFolder = Environment.SpecialFolder.MyComputer : fbdSC4InstallDir.SelectedPath = txtSC4InstallDir.Text
        If fbdSC4InstallDir.ShowDialog = Windows.Forms.DialogResult.OK And fbdSC4InstallDir.SelectedPath <> Nothing Then
            If IsPathValidated(txtSC4InstallDir) = True Then txtSC4InstallDir.Text = fbdSC4InstallDir.SelectedPath
        End If
    End Sub

    Private Sub btnDAEMONlInstallDir_Click(sender As Object, e As EventArgs) Handles btnDAEMONlInstallDir.Click
        fbdDAEMONlInstallDir.RootFolder = Environment.SpecialFolder.MyComputer : fbdDAEMONlInstallDir.SelectedPath = txtSC4InstallDir.Text
        If fbdDAEMONlInstallDir.ShowDialog = Windows.Forms.DialogResult.OK And fbdDAEMONlInstallDir.SelectedPath <> Nothing Then
            If IsPathValidated(txtDAEMONlInstallDir) = True Then txtDAEMONlInstallDir.Text = fbdDAEMONlInstallDir.SelectedPath
        End If
    End Sub

    Private Sub frmOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tvwOptions.ExpandAll()
        With ModuleMain.InstallOptions
            .SC4Type = InstallOptions.SC4InstallType.ISO : .IsInstallDAEMONTools = True
            .IsInstall638Patch = True : .IsInstall640Patch = True : .IsInstallNoCDPatch = False
            .LanguagePatch = InstallOptions.Language.TraditionalChinese
            .IsAddDesktopIcon = True : .IsAddStartMenuItem = True : cmbOptions.SelectedItem = cmbOptions.Items(1)
            lblNeedsDiskSpace.Text = "安装目录至少需要 " & .GetNeedsDiskSpace("GB") & "GB 的硬盘空间"
            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Disc Soft\DAEMON Tools Lite", "InstallKey", Nothing) <> Nothing Then tvwOptions.Nodes.Find("DAEMON Tools Lite", True)(0).Remove() : .IsInstallDAEMONTools = False
            If Environment.Is64BitOperatingSystem = False Then tvwOptions.Nodes.Find("4GB补丁", True)(0).Remove() : .IsInstall4GBPatch = False
        End With
        lblOptionsDetail.Text = "请将鼠标放在组件名上以查看组件详情。" : lblOptionsDiskSpace.Text = ""
        txtSC4InstallDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\Maxis\SimCity 4 Deluxe"
        txtDAEMONlInstallDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) & "\DAEMON Tools Lite"
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099"
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        frmMain.Show()
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
        Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MessageBox.Show("确定退出安装程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then Application.Exit()
    End Sub

End Class