Public Class frmInstalling

    Private Sub ReportProgress(ByVal result As InstallResult.Result, ByVal item As ListViewItem)
        With ModuleMain.InstallResult
            If result = InstallResult.Result.Success Then
                item.ImageKey = "success"
                Select Case item.Text
                    Case "DAEMON Tools Lite" : .DAEMONToolsInstallResult = result
                    Case "638补丁" : ._638PatchInstallResult = result
                    Case "640补丁" : ._640PatchInstallResult = result
                    Case "免CD补丁" : .NoCDPatchInstallResult = result
                    Case "4GB补丁" : ._4GBPatchInstallResult = result
                    Case "繁体中文语言补丁", "简体中文语言补丁" : .LanguagePatchInstallResult = result
                    Case "添加开始菜单项" : .AddDesktopIconResult = result
                    Case "添加桌面图标" : .AddStartMenuItemResult = result
                    Case Else : If item.Text.Contains("模拟城市4 豪华版") = True Then .SC4InstallResult = result
                End Select
            Else
                item.ImageKey = "fail"
                Select Case item.Text
                    Case "DAEMON Tools Lite" : .DAEMONToolsInstallResult = result
                    Case "638补丁" : ._638PatchInstallResult = result
                    Case "640补丁" : ._640PatchInstallResult = result
                    Case "免CD补丁" : .NoCDPatchInstallResult = result
                    Case "4GB补丁" : ._4GBPatchInstallResult = result
                    Case "繁体中文语言补丁", "简体中文语言补丁" : .LanguagePatchInstallResult = result
                    Case "添加开始菜单项" : .AddDesktopIconResult = result
                    Case "添加桌面图标" : .AddStartMenuItemResult = result
                    Case Else
                        If item.Text.Contains("模拟城市4 豪华版") = True Then
                            For i As Integer = 0 To lvwTask.Items.Count - 1
                                lvwTask.Items(i).ImageKey = "fail"
                            Next
                            .SC4InstallResult = InstallResult.Result.Fail
                            ._638PatchInstallResult = InstallResult.Result.Fail
                            ._640PatchInstallResult = InstallResult.Result.Fail
                            ._4GBPatchInstallResult = InstallResult.Result.Fail
                            .NoCDPatchInstallResult = InstallResult.Result.Fail
                            .SC4LauncherInstallResult = InstallResult.Result.Fail
                            .LanguagePatchInstallResult = InstallResult.Result.Fail
                            .AddDesktopIconResult = InstallResult.Result.Fail
                            .AddStartMenuItemResult = InstallResult.Result.Fail
                            bgwInstall.CancelAsync()
                        End If
                End Select
            End If
            If item.Index < lvwTask.Items.Count - 1 Then
                lvwTask.Items(item.Index + 1).ImageKey = "installing"
            End If
        End With
    End Sub

    Private Sub bgwInstall_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwInstall.DoWork
        'For i As Integer = 0 To lvwTask.Items.Count - 1
        '    Threading.Thread.Sleep(1000)
        '    Console.WriteLine("1")
        '    ReportProgress(InstallResult.Result.Success, lvwTask.Items(i))
        'Next
        'Threading.Thread.Sleep(1000)
        'ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("模拟城市4 豪华版"))
        My.Computer.FileSystem.CopyFile(Application.ExecutablePath, ModuleMain.InstallOptions.SC4InstallDir & "\SC4AutoInstaller.exe")
        ModuleInstallModule.SetControlPanelProgramItemRegValue()
        With ModuleMain.InstallOptions
            If IsNothing(ModuleMain.InstalledModule) = True Then
                If .IsInstallDAEMONTools = True Then ReportProgress(ModuleInstallModule.InstallDAEMONTools(), lvwTask.FindItemWithText("DAEMON Tools Lite"))
                If .SC4Type = InstallOptions.SC4InstallType.ISO Then
                    ReportProgress(ModuleInstallModule.InstallSC4(InstallOptions.SC4InstallType.ISO), lvwTask.FindItemWithText("模拟城市4 豪华版 镜像版"))
                    ModuleInstallModule.SetControlPanelProgramItemRegValue()
                ElseIf .SC4Type = InstallOptions.SC4InstallType.NoInstall Then
                    ReportProgress(ModuleInstallModule.InstallSC4(InstallOptions.SC4InstallType.NoInstall), lvwTask.FindItemWithText("模拟城市4 豪华版 硬盘版"))
                    ModuleInstallModule.SetControlPanelProgramItemRegValue() : ModuleInstallModule.SetNoInstallSC4RegValue()
                End If
            End If
            If bgwInstall.CancellationPending = True Then Exit Sub
            If .IsInstall638Patch = True Then ReportProgress(ModuleInstallModule.Install638Patch(), lvwTask.FindItemWithText("638补丁"))
            If .IsInstall640Patch = True Then ReportProgress(ModuleInstallModule.Install640Patch(), lvwTask.FindItemWithText("640补丁"))
            If .IsInstallNoCDPatch = True Then ReportProgress(ModuleInstallModule.InstallNoCDPatch(), lvwTask.FindItemWithText("免CD补丁"))
            If .IsInstall4GBPatch = True Then ReportProgress(ModuleInstallModule.Install4GBPatch(), lvwTask.FindItemWithText("4GB补丁"))
            If .IsInstallSC4Launcher = True Then ReportProgress(ModuleInstallModule.InstallSC4Launcher(), lvwTask.FindItemWithText("模拟城市4 启动器"))
            Select Case .LanguagePatch
                Case InstallOptions.Language.TraditionalChinese
                    ReportProgress(ModuleInstallModule.InstallLanguagePatch(InstallOptions.Language.TraditionalChinese), lvwTask.FindItemWithText("繁体中文语言补丁"))
                Case InstallOptions.Language.SimplifiedChinese
                    ReportProgress(ModuleInstallModule.InstallLanguagePatch(InstallOptions.Language.SimplifiedChinese), lvwTask.FindItemWithText("简体中文语言补丁"))
                Case InstallOptions.Language.English
                    ModuleInstallModule.InstallLanguagePatch(InstallOptions.Language.English)
            End Select
            If IsNothing(ModuleMain.InstalledModule) = True Then
                If .IsAddDesktopIcon = True Then If .IsInstallSC4Launcher = True Then ModuleInstallModule.AddDestopIcon() Else ModuleInstallModule.AddDestopIcon()
                If .IsAddStartMenuItem = True Then If .IsInstallSC4Launcher = True Then ModuleInstallModule.AddStartMenuItems() Else ModuleInstallModule.AddStartMenuItems()
            End If
            Threading.Thread.Sleep(1000)
        End With
    End Sub

    Private Sub bgwInstall_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwInstall.RunWorkerCompleted
        frmFinish.Show()
        Close()
    End Sub

    Private Sub tmrPic_Tick(sender As Object, e As EventArgs) Handles tmrPic.Tick
        Static i As Integer = 1
        picSC4.Image = CType(My.Resources.ResourceManager.GetObject("SC4_" & i), Image)
        If i = 20 Then i = 1 Else i += 1
    End Sub

    Private Sub frmInstalling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With ModuleMain.InstallOptions
            If IsNothing(ModuleMain.InstalledModule) = True Then
                If .IsInstallDAEMONTools = False Then lvwTask.FindItemWithText("DAEMON Tools Lite").Remove()
                If .SC4Type = InstallOptions.SC4InstallType.ISO Then
                    lvwTask.FindItemWithText("模拟城市4 豪华版").Text = "模拟城市4 豪华版 镜像版"
                ElseIf .SC4Type = InstallOptions.SC4InstallType.NoInstall Then
                    lvwTask.FindItemWithText("模拟城市4 豪华版").Text = "模拟城市4 豪华版 硬盘版"
                End If
                If .IsAddDesktopIcon = False Then lvwTask.FindItemWithText("添加桌面图标").Remove()
                If .IsAddStartMenuItem = False Then lvwTask.FindItemWithText("添加开始菜单项").Remove()
            Else
                lvwTask.FindItemWithText("DAEMON Tools Lite").Remove()
                lvwTask.FindItemWithText("模拟城市4 豪华版").Remove()
                lvwTask.FindItemWithText("添加桌面图标").Remove()
                lvwTask.FindItemWithText("添加开始菜单项").Remove()
            End If
            If .IsInstall638Patch = False Then lvwTask.FindItemWithText("638补丁").Remove()
            If .IsInstall640Patch = False Then lvwTask.FindItemWithText("640补丁").Remove()
            If .IsInstall4GBPatch = False Then lvwTask.FindItemWithText("4GB补丁").Remove()
            If .IsInstallNoCDPatch = False Then lvwTask.FindItemWithText("免CD补丁").Remove()
            If .IsInstallSC4Launcher = False Then lvwTask.FindItemWithText("模拟城市4 启动器").Remove()
            Select Case .LanguagePatch
                Case InstallOptions.Language.TraditionalChinese
                    lvwTask.FindItemWithText("语言补丁").Text = "繁体中文语言补丁"
                Case InstallOptions.Language.SimplifiedChinese
                    lvwTask.FindItemWithText("语言补丁").Text = "简体中文语言补丁"
                Case InstallOptions.Language.English
                    lvwTask.FindItemWithText("语言补丁").Remove()
            End Select
        End With
        Control.CheckForIllegalCrossThreadCalls = False
        Dim ControlBoxHandle As Integer = GetSystemMenu(Me.Handle, 0)
        Dim ControlBoxCount As Integer = GetMenuItemCount(ControlBoxHandle)
        RemoveMenu(ControlBoxHandle, ControlBoxCount - 1, MF_DISABLED Or MF_BYPOSITION)
        DrawMenuBar(Me.Handle)
        bgwInstall.RunWorkerAsync()
        tmrPic_Tick(tmrPic, New EventArgs)
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099"
    End Sub

End Class