Imports ChangeOption = SC4AutoInstaller.ChangeOptions.ChangeOption

Public Class frmChangeOptions

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

    ''' <summary>设置安装组件列表框里项的图标</summary>
    ''' <param name="NodeName">安装组件列表框项的Name属性值</param>
    ''' <param name="value">要设置的图标，必须为NodeCheckedState枚举的值之一</param>
    Private Sub SetNodeChecked(ByVal NodeName As String, ByVal value As NodeCheckedState)
        With tvwOptions
            Select Case value
                Case NodeCheckedState.Checked
                    .Nodes.Find(NodeName, True)(0).ImageKey = "checked"
                    .Nodes.Find(NodeName, True)(0).SelectedImageKey = "checked"
                Case NodeCheckedState.Unchecked
                    .Nodes.Find(NodeName, True)(0).ImageKey = "unchecked"
                    .Nodes.Find(NodeName, True)(0).SelectedImageKey = "unchecked"
                Case NodeCheckedState.RadioChecked
                    .Nodes.Find(NodeName, True)(0).ImageKey = "radiochecked"
                    .Nodes.Find(NodeName, True)(0).SelectedImageKey = "radiochecked"
                Case NodeCheckedState.RadioUnchecked
                    .Nodes.Find(NodeName, True)(0).ImageKey = "radiounchecked"
                    .Nodes.Find(NodeName, True)(0).SelectedImageKey = "radiounchecked"
            End Select
        End With
    End Sub

    ''' <summary>获取安装组件列表框里项的图标</summary>
    ''' <param name="NodeName">安装组件列表框项的Name属性值</param>
    ''' <returns>返回NodeCheckedState枚举的值之一</returns>
    Private Function GetNodeChecked(ByVal NodeName As String) As NodeCheckedState
        With tvwOptions
            Select Case .Nodes.Find(NodeName, True)(0).ImageKey
                Case "checked"
                    Return NodeCheckedState.Checked
                Case "unchecked"
                    Return NodeCheckedState.Unchecked
                Case "radiochecked"
                    Return NodeCheckedState.RadioChecked
                Case "radiounchecked"
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
        '当使用鼠标左键点击安装组件列表框的项时更新点击的项的图标和ModuleDeclare.ChangeOptions的值
        If e.Button <> Windows.Forms.MouseButtons.Left Then Exit Sub
        With ModuleDeclare.ChangeOptions
            Select Case e.Node.Name
                Case "638补丁", "640补丁", "641补丁", "4GB补丁", "免CD补丁", "模拟城市4 启动器"
                    If GetNodeChecked(e.Node.Name) = NodeCheckedState.Checked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.Unchecked)
                        Select Case e.Node.Name
                            Case "638补丁" : ._638PatchOption = ChangeOption.Uninstall '选择卸载638补丁时同时选择卸载640和641补丁
                                SetNodeChecked("640补丁", NodeCheckedState.Unchecked) : ._640PatchOption = ChangeOption.Uninstall
                                SetNodeChecked("641补丁", NodeCheckedState.Unchecked) : ._641PatchOption = ChangeOption.Uninstall
                            Case "640补丁" : ._640PatchOption = ChangeOption.Uninstall '选择卸载640补丁时同时选择卸载641补丁
                                SetNodeChecked("641补丁", NodeCheckedState.Unchecked) : ._641PatchOption = ChangeOption.Uninstall
                            Case "641补丁" : ._641PatchOption = ChangeOption.Uninstall
                            Case "4GB补丁" : ._4GBPatchOption = ChangeOption.Uninstall
                            Case "免CD补丁" : .NoCDPatchOption = ChangeOption.Uninstall
                            Case "模拟城市4 启动器" : .SC4LauncherOption = ChangeOption.Uninstall
                        End Select
                    ElseIf GetNodeChecked(e.Node.Name) = NodeCheckedState.Unchecked Then
                        SetNodeChecked(e.Node.Name, NodeCheckedState.Checked)
                        Select Case e.Node.Name
                            Case "638补丁" : ._638PatchOption = ChangeOption.Install '选择卸载638补丁时同时选择卸载免CD补丁
                                SetNodeChecked("免CD补丁", NodeCheckedState.Unchecked) : .NoCDPatchOption = ChangeOption.Uninstall
                            Case "640补丁" : ._640PatchOption = ChangeOption.Install '选择安装640补丁时同时选择安装638补丁和卸载免CD补丁
                                SetNodeChecked("638补丁", NodeCheckedState.Checked) : ._638PatchOption = ChangeOption.Install
                                SetNodeChecked("免CD补丁", NodeCheckedState.Unchecked) : .NoCDPatchOption = ChangeOption.Uninstall
                            Case "641补丁" : ._641PatchOption = ChangeOption.Install '选择安装641补丁时同时选择安装638和640补丁和卸载免CD补丁
                                SetNodeChecked("638补丁", NodeCheckedState.Checked) : ._638PatchOption = ChangeOption.Install
                                SetNodeChecked("640补丁", NodeCheckedState.Checked) : ._640PatchOption = ChangeOption.Install
                                SetNodeChecked("免CD补丁", NodeCheckedState.Unchecked) : .NoCDPatchOption = ChangeOption.Uninstall
                            Case "4GB补丁" : ._4GBPatchOption = ChangeOption.Install
                            Case "免CD补丁" : .NoCDPatchOption = ChangeOption.Install '选择安装免CD补丁时同时选择卸载638、640和641补丁
                                SetNodeChecked("638补丁", NodeCheckedState.Unchecked) : ._638PatchOption = ChangeOption.Uninstall
                                SetNodeChecked("640补丁", NodeCheckedState.Unchecked) : ._640PatchOption = ChangeOption.Uninstall
                                SetNodeChecked("641补丁", NodeCheckedState.Unchecked) : ._641PatchOption = ChangeOption.Uninstall
                            Case "模拟城市4 启动器" : .SC4LauncherOption = ChangeOption.Install
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
            If .IsSameAsInstalledModule(ModuleDeclare.InstalledModule) Then btnInstall.Enabled = False Else btnInstall.Enabled = True '判断是否更改了安装选项
        End With
    End Sub

    Private Sub frmModuleChangeOption_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("确定要退出安装程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then e.Cancel = True
    End Sub

    Private Sub frmModuleChangeOption_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tvwOptions.ExpandAll() '展开所有的树节点
        With ModuleDeclare.ChangeOptions '更新已安装的组件在安装组件列表框里对应项的图标
            If ModuleDeclare.InstalledModule.Is638PatchInstalled Then SetNodeChecked("638补丁", NodeCheckedState.Checked)
            ._638PatchOption = If(ModuleDeclare.InstalledModule.Is638PatchInstalled, ChangeOption.Install, ChangeOption.Uninstall)
            If ModuleDeclare.InstalledModule.Is640PatchInstalled Then SetNodeChecked("640补丁", NodeCheckedState.Checked)
            ._640PatchOption = If(ModuleDeclare.InstalledModule.Is640PatchInstalled, ChangeOption.Install, ChangeOption.Uninstall)
            If ModuleDeclare.InstalledModule.Is641PatchInstalled Then SetNodeChecked("641补丁", NodeCheckedState.Checked)
            ._641PatchOption = If(ModuleDeclare.InstalledModule.Is641PatchInstalled, ChangeOption.Install, ChangeOption.Uninstall)
            If ModuleDeclare.InstalledModule.Is4GBPatchInstalled Then SetNodeChecked("4GB补丁", NodeCheckedState.Checked)
            ._4GBPatchOption = If(ModuleDeclare.InstalledModule.Is4GBPatchInstalled, ChangeOption.Install, ChangeOption.Uninstall)
            If ModuleDeclare.InstalledModule.IsNoCDPatchInstalled Then SetNodeChecked("免CD补丁", NodeCheckedState.Checked)
            .NoCDPatchOption = If(ModuleDeclare.InstalledModule.IsNoCDPatchInstalled, ChangeOption.Install, ChangeOption.Uninstall)
            If ModuleDeclare.InstalledModule.IsSC4LauncherInstalled Then SetNodeChecked("模拟城市4 启动器", NodeCheckedState.Checked)
            .SC4LauncherOption = If(ModuleDeclare.InstalledModule.IsSC4LauncherInstalled, ChangeOption.Install, ChangeOption.Uninstall)
            Select Case ModuleDeclare.InstalledModule.LanguagePatchOption
                Case SC4Language.TraditionalChinese : SetNodeChecked("繁体中文", NodeCheckedState.RadioChecked) : .LanguagePatchOption = SC4Language.TraditionalChinese
                Case SC4Language.SimplifiedChinese : SetNodeChecked("简体中文", NodeCheckedState.RadioChecked) : .LanguagePatchOption = SC4Language.SimplifiedChinese
                Case SC4Language.English : SetNodeChecked("英语", NodeCheckedState.RadioChecked) : .LanguagePatchOption = SC4Language.English
            End Select
        End With
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ModuleDeclare.ChangeOptions = New ChangeOptions '恢复已更改的ModuleDeclare.ChangeOptions类实例
        frmMain.Show()
        RemoveHandler Me.FormClosing, AddressOf frmModuleChangeOption_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
        Close()
    End Sub

    Private Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        With ModuleDeclare.ChangeOptions '判断是否更改组件
            If (ModuleDeclare.InstalledModule.Is638PatchInstalled AndAlso ._638PatchOption = ChangeOption.Install) OrElse
                (ModuleDeclare.InstalledModule.Is638PatchInstalled = False AndAlso ._638PatchOption = ChangeOption.Uninstall) Then ._638PatchOption = ChangeOption.Unchanged
            If (ModuleDeclare.InstalledModule.Is640PatchInstalled AndAlso ._640PatchOption = ChangeOption.Install) OrElse
                (ModuleDeclare.InstalledModule.Is640PatchInstalled = False AndAlso ._640PatchOption = ChangeOption.Uninstall) Then ._640PatchOption = ChangeOption.Unchanged
            If (ModuleDeclare.InstalledModule.Is641PatchInstalled AndAlso ._641PatchOption = ChangeOption.Install) OrElse
                (ModuleDeclare.InstalledModule.Is641PatchInstalled = False AndAlso ._641PatchOption = ChangeOption.Uninstall) Then ._641PatchOption = ChangeOption.Unchanged
            If (ModuleDeclare.InstalledModule.Is4GBPatchInstalled AndAlso ._4GBPatchOption = ChangeOption.Install) AndAlso
                (._638PatchOption <> ChangeOption.Unchanged OrElse ._640PatchOption <> ChangeOption.Unchanged OrElse ._641PatchOption <> ChangeOption.Unchanged) Then
                ._4GBPatchOption = ChangeOption.Install '如果更改了638、640或641补丁且不卸载4GB补丁则强制重装4GB补丁
            Else
                If (ModuleDeclare.InstalledModule.Is4GBPatchInstalled AndAlso ._4GBPatchOption = ChangeOption.Install) OrElse
                    (ModuleDeclare.InstalledModule.Is4GBPatchInstalled = False AndAlso ._4GBPatchOption = ChangeOption.Uninstall) Then ._4GBPatchOption = ChangeOption.Unchanged
            End If
            If (ModuleDeclare.InstalledModule.IsNoCDPatchInstalled AndAlso .NoCDPatchOption = ChangeOption.Install) OrElse
                (ModuleDeclare.InstalledModule.IsNoCDPatchInstalled = False AndAlso .NoCDPatchOption = ChangeOption.Uninstall) Then .NoCDPatchOption = ChangeOption.Unchanged
            If (ModuleDeclare.InstalledModule.IsSC4LauncherInstalled AndAlso .SC4LauncherOption = ChangeOption.Install) OrElse
                (ModuleDeclare.InstalledModule.IsSC4LauncherInstalled = False AndAlso .SC4LauncherOption = ChangeOption.Uninstall) Then .SC4LauncherOption = ChangeOption.Unchanged
        End With
        frmInstalling.Show()
        RemoveHandler Me.FormClosing, AddressOf frmModuleChangeOption_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
        Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Application.Exit()
    End Sub

End Class