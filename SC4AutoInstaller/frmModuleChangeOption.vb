Public Class frmModuleChangeOption

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

    Private Sub tvwOptions_BeforeCollapse(sender As Object, e As TreeViewCancelEventArgs) Handles tvwOptions.BeforeCollapse
        e.Cancel = True
    End Sub

    Private Sub tvwOptions_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvwOptions.NodeMouseClick
        With ModuleMain.InstallOptions
            If e.Button <> Windows.Forms.MouseButtons.Left Then Exit Sub
            Select Case e.Node.Name
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
        End With
    End Sub

    Private Sub frmModuleChangeOption_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.ApplicationExitCall Then
            If MessageBox.Show("确定要退出安装程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then e.Cancel = True
        End If
    End Sub

    Private Sub frmModuleChangeOption_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tvwOptions.ExpandAll()
        With ModuleMain.InstalledModule
            If .Is638PatchInstalled = True Then SetNodeChecked("638补丁", NodeCheckedState.checked) : ModuleMain.InstallOptions.IsInstall638Patch = True
            If .Is640PatchInstalled = True Then SetNodeChecked("640补丁", NodeCheckedState.checked) : ModuleMain.InstallOptions.IsInstall640Patch = True
            If .Is4GBPatchInstalled = True Then SetNodeChecked("4GB补丁", NodeCheckedState.checked) : ModuleMain.InstallOptions.IsInstall4GBPatch = True
            If .IsNoCDPatchInstalled = True Then SetNodeChecked("免CD补丁", NodeCheckedState.checked) : ModuleMain.InstallOptions.IsInstallNoCDPatch = True
            If .IsSC4LauncherInstalled = True Then SetNodeChecked("模拟城市4 启动器", NodeCheckedState.checked) : ModuleMain.InstallOptions.IsInstallSC4Launcher = True
            Select Case .LanguagePatch
                Case InstalledModule.Language.TraditionalChinese
                    SetNodeChecked("繁体中文", NodeCheckedState.radiochecked) : ModuleMain.InstallOptions.LanguagePatch = InstallOptions.Language.TraditionalChinese
                Case InstalledModule.Language.SimplifiedChinese
                    SetNodeChecked("简体中文", NodeCheckedState.radiochecked) : ModuleMain.InstallOptions.LanguagePatch = InstallOptions.Language.SimplifiedChinese
                Case InstalledModule.Language.English
                    SetNodeChecked("英语", NodeCheckedState.radiochecked) : ModuleMain.InstallOptions.LanguagePatch = InstallOptions.Language.English
            End Select
        End With
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099"
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        frmMain.Show()
        RemoveHandler Me.FormClosing, AddressOf frmModuleChangeOption_FormClosing
        Close()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        frmInstalling.Show()
        RemoveHandler Me.FormClosing, AddressOf frmModuleChangeOption_FormClosing
        Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Application.Exit()
    End Sub

End Class