Imports Opt = SC4AutoInstaller.InstallOptions '引用SC4AutoInstaller.InstallOptions类以便省略代码中重复的InstallOptions类引用

Public Class frmChangeModuleOptions

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

    Private Sub tvwOptions_BeforeCollapse(sender As Object, e As TreeViewCancelEventArgs) Handles tvwOptions.BeforeCollapse
        e.Cancel = True '禁止折叠树节点
    End Sub

    Private Sub tvwOptions_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvwOptions.NodeMouseClick
        '当使用鼠标左键点击安装组件列表框的项时更新点击的项的图标和ModuleMain.InstallOptions的值
        With ModuleMain.InstallOptions
            If e.Button <> Windows.Forms.MouseButtons.Left Then Exit Sub
            Select Case e.Node.Name
                Case "638补丁", "640补丁", "641补丁", "4GB补丁", "免CD补丁", "模拟城市4 启动器"
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
                        SetNodeChecked("英语", NodeCheckedState.radiounchecked) : .LanguagePatch = Opt.Language.TraditionalChinese
                    End If
                Case "简体中文"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("繁体中文", NodeCheckedState.radiounchecked)
                        SetNodeChecked("英语", NodeCheckedState.radiounchecked) : .LanguagePatch = Opt.Language.SimplifiedChinese
                    End If
                Case "英语"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.radiounchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.radiochecked) : SetNodeChecked("繁体中文", NodeCheckedState.radiounchecked)
                        SetNodeChecked("简体中文", NodeCheckedState.radiounchecked) : .LanguagePatch = Opt.Language.English
                    End If
            End Select
        End With
    End Sub

    Private Sub frmModuleChangeOption_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("确定要退出安装程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then e.Cancel = True
    End Sub

    Private Sub frmModuleChangeOption_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tvwOptions.ExpandAll() '展开所有的树节点
        With ModuleMain.InstallOptions '更新已安装的组件在安装组件列表框里对应项的图标
            If ModuleMain.InstalledModule.Is638PatchInstalled = True Then SetNodeChecked("638补丁", NodeCheckedState.checked) : .Install638Patch = True
            If ModuleMain.InstalledModule.Is640PatchInstalled = True Then SetNodeChecked("640补丁", NodeCheckedState.checked) : .Install640Patch = True
            If ModuleMain.InstalledModule.Is641PatchInstalled = True Then SetNodeChecked("641补丁", NodeCheckedState.checked) : .Install641Patch = True
            If ModuleMain.InstalledModule.Is4GBPatchInstalled = True Then SetNodeChecked("4GB补丁", NodeCheckedState.checked) : .Install4GBPatch = True
            If ModuleMain.InstalledModule.IsNoCDPatchInstalled = True Then SetNodeChecked("免CD补丁", NodeCheckedState.checked) : .InstallNoCDPatch = True
            If ModuleMain.InstalledModule.IsSC4LauncherInstalled = True Then SetNodeChecked("模拟城市4 启动器", NodeCheckedState.checked) : .InstallSC4Launcher = True
            Select Case ModuleMain.InstalledModule.LanguagePatch
                Case InstalledModule.Language.TraditionalChinese
                    SetNodeChecked("繁体中文", NodeCheckedState.radiochecked) : .LanguagePatch = Opt.Language.TraditionalChinese
                Case InstalledModule.Language.SimplifiedChinese
                    SetNodeChecked("简体中文", NodeCheckedState.radiochecked) : .LanguagePatch = Opt.Language.SimplifiedChinese
                Case InstalledModule.Language.English
                    SetNodeChecked("英语", NodeCheckedState.radiochecked) : .LanguagePatch = Opt.Language.English
            End Select
        End With
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        frmMain.Show()
        RemoveHandler Me.FormClosing, AddressOf frmModuleChangeOption_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
        Close()
    End Sub

    Private Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        If ModuleMain.InstallOptions.Install638Patch = False And My.Computer.FileSystem.FileExists("Data\SC4\NoInstall.7z") = False Then
            MessageBox.Show("Data\SC4\NoInstall.7z 文件不存在！" & vbCrLf & "无法卸载638补丁！请使用原始安装程序以添加或删除组件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
        End If
        frmInstalling.Show()
        RemoveHandler Me.FormClosing, AddressOf frmModuleChangeOption_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
        Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Application.Exit()
    End Sub

End Class