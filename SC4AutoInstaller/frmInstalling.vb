Public Class frmInstalling

    Private Sub ReportProgress(ByVal result As InstallResult.Result, ByVal item As ListViewItem)
        With ModuleMain.InstallResult
            If result = InstallResult.Result.Success Then item.ImageKey = "success" Else item.ImageKey = "fail"
            Select Case item.Text
                Case "DAEMON Tools Lite" : .DAEMONToolsInstallResult = result
                Case "638补丁" : ._638PatchInstallResult = result
                Case "640补丁" : ._640PatchInstallResult = result
                Case "641补丁" : ._641PatchInstallResult = result
                Case "4GB补丁" : ._4GBPatchInstallResult = result
                Case "免CD补丁" : .NoCDPatchInstallResult = result
                Case "繁体中文语言补丁", "简体中文语言补丁" : .LanguagePatchInstallResult = result
                Case "添加开始菜单项" : .AddDesktopIconResult = result
                Case "添加桌面图标" : .AddStartMenuItemResult = result
                Case Else
                    If item.Text.Contains("模拟城市4 豪华版") = True And result = InstallResult.Result.Success Then
                        .SC4InstallResult = result
                    ElseIf item.Text.Contains("模拟城市4 豪华版") = True And result = InstallResult.Result.Fail Then
                        For i As Integer = 0 To lvwTask.Items.Count - 1
                            lvwTask.Items(i).ImageKey = "fail"
                        Next
                        .SC4InstallResult = InstallResult.Result.Fail
                        ._638PatchInstallResult = InstallResult.Result.Fail : ._640PatchInstallResult = InstallResult.Result.Fail : ._641PatchInstallResult = InstallResult.Result.Fail
                        ._4GBPatchInstallResult = InstallResult.Result.Fail : .NoCDPatchInstallResult = InstallResult.Result.Fail
                        .SC4LauncherInstallResult = InstallResult.Result.Fail : .LanguagePatchInstallResult = InstallResult.Result.Fail
                        .AddDesktopIconResult = InstallResult.Result.Fail : .AddStartMenuItemResult = InstallResult.Result.Fail
                        bgwInstall.CancelAsync()
                    End If
            End Select
        End With
    End Sub

    Private Sub bgwInstall_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwInstall.DoWork
        'lvwTask.Items(0).ImageKey = "installing"
        'For i As Integer = 0 To lvwTask.Items.Count - 1
        '    Threading.Thread.Sleep(1000)
        '    Console.WriteLine("1")
        '    ReportProgress(InstallResult.Result.Success, lvwTask.Items(i))
        'Next
        ''ReportProgress(ModuleInstallModule.InstallDAEMONTools(), lvwTask.FindItemWithText("DAEMON Tools Lite"))
        'lvwTask.FindItemWithText("模拟城市4 豪华版 硬盘版").ImageKey = "installing"
        'ReportProgress(ModuleInstallModule.InstallSC4(InstallOptions.SC4InstallType.NoInstall), lvwTask.FindItemWithText("模拟城市4 豪华版 硬盘版"))
        'ModuleInstallModule.SetNoInstallSC4RegValue()
        'ModuleInstallModule.SetControlPanelProgramItemRegValue()
        'lvwTask.FindItemWithText("638补丁").ImageKey = "installing"
        'ReportProgress(ModuleInstallModule.Install638Patch(False), lvwTask.FindItemWithText("638补丁"))
        'Threading.Thread.Sleep(1000)

        With ModuleMain.InstallOptions
            If IsNothing(ModuleMain.InstalledModule) = True Then
                lvwTask.FindItemWithText("DAEMON Tools Lite").ImageKey = "installing"
                If .IsInstallDAEMONTools = True Then ReportProgress(ModuleInstallModule.InstallDAEMONTools(), lvwTask.FindItemWithText("DAEMON Tools Lite"))
                If .SC4Type = InstallOptions.SC4InstallType.ISO Then
                    lvwTask.FindItemWithText("模拟城市4 豪华版 镜像版").ImageKey = "installing"
                    ReportProgress(ModuleInstallModule.InstallSC4(InstallOptions.SC4InstallType.ISO), lvwTask.FindItemWithText("模拟城市4 豪华版 镜像版"))
                ElseIf .SC4Type = InstallOptions.SC4InstallType.NoInstall Then
                    lvwTask.FindItemWithText("模拟城市4 豪华版 硬盘版").ImageKey = "installing"
                    ReportProgress(ModuleInstallModule.InstallSC4(InstallOptions.SC4InstallType.NoInstall), lvwTask.FindItemWithText("模拟城市4 豪华版 硬盘版"))
                    ModuleInstallModule.SetNoInstallSC4RegValue()
                End If
                ModuleInstallModule.SetControlPanelProgramItemRegValue()
                If bgwInstall.CancellationPending = True Then Exit Sub
                If .IsInstall641Patch = True Then
                    lvwTask.FindItemWithText("638补丁").ImageKey = "installing"
                    ReportProgress(ModuleInstallModule.Install638Patch(.SC4InstallDir, Not .IsInstall638Patch), lvwTask.FindItemWithText("638补丁"))
                    lvwTask.FindItemWithText("640补丁").ImageKey = "installing"
                    ReportProgress(ModuleInstallModule.Install640Patch(.SC4InstallDir, Not .IsInstall640Patch), lvwTask.FindItemWithText("640补丁"))
                    lvwTask.FindItemWithText("641补丁").ImageKey = "installing"
                    ReportProgress(ModuleInstallModule.Install641Patch(.SC4InstallDir, Not .IsInstall641Patch), lvwTask.FindItemWithText("641补丁"))
                ElseIf .IsInstall640Patch = True Then
                    lvwTask.FindItemWithText("638补丁").ImageKey = "installing"
                    ReportProgress(ModuleInstallModule.Install638Patch(.SC4InstallDir, Not .IsInstall638Patch), lvwTask.FindItemWithText("638补丁"))
                    lvwTask.FindItemWithText("640补丁").ImageKey = "installing"
                    ReportProgress(ModuleInstallModule.Install640Patch(.SC4InstallDir, Not .IsInstall640Patch), lvwTask.FindItemWithText("640补丁"))
                ElseIf .IsInstall638Patch = True Then
                    lvwTask.FindItemWithText("638补丁").ImageKey = "installing"
                    ReportProgress(ModuleInstallModule.Install638Patch(.SC4InstallDir, Not .IsInstall638Patch), lvwTask.FindItemWithText("638补丁"))
                End If
                lvwTask.FindItemWithText("免CD补丁").ImageKey = "installing"
                ReportProgress(ModuleInstallModule.InstallNoCDPatch(.SC4InstallDir, Not .IsInstallNoCDPatch), lvwTask.FindItemWithText("免CD补丁"))
                lvwTask.FindItemWithText("4GB补丁").ImageKey = "installing"
                ReportProgress(ModuleInstallModule.Install4GBPatch(.SC4InstallDir, Not .IsInstall4GBPatch), lvwTask.FindItemWithText("4GB补丁"))
                lvwTask.FindItemWithText("模拟城市4 启动器").ImageKey = "installing"
                ReportProgress(ModuleInstallModule.InstallSC4Launcher(.SC4InstallDir, Not .IsInstallSC4Launcher), lvwTask.FindItemWithText("模拟城市4 启动器"))
                If .IsAddDesktopIcon = True Then lvwTask.FindItemWithText("添加桌面图标").ImageKey = "installing" : ModuleInstallModule.AddDestopIcon()
                If .IsAddStartMenuItem = True Then lvwTask.FindItemWithText("添加开始菜单项").ImageKey = "installing" : ModuleInstallModule.AddStartMenuItems()
                Select Case .LanguagePatch
                    Case InstallOptions.Language.TraditionalChinese
                        lvwTask.FindItemWithText("繁体中文语言补丁").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.InstallLanguagePatch(.SC4InstallDir, InstallOptions.Language.TraditionalChinese), lvwTask.FindItemWithText("繁体中文语言补丁"))
                    Case InstallOptions.Language.SimplifiedChinese
                        lvwTask.FindItemWithText("简体中文语言补丁").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.InstallLanguagePatch(.SC4InstallDir, InstallOptions.Language.SimplifiedChinese), lvwTask.FindItemWithText("简体中文语言补丁"))
                    Case InstallOptions.Language.English
                        ModuleInstallModule.InstallLanguagePatch(.SC4InstallDir, InstallOptions.Language.English)
                End Select
            Else
                With ModuleMain.InstalledModule
                    If .Is641PatchInstalled = False And ModuleMain.InstallOptions.IsInstall641Patch = True Then
                        If .Is638PatchInstalled = False Then
                            lvwTask.Items.Insert(lvwTask.FindItemWithText("641补丁").Index, New ListViewItem("638补丁", lvwTask.Groups.Item("lvwGroupSubassembly")))
                            lvwTask.FindItemWithText("638补丁").ImageKey = "installing"
                            ReportProgress(ModuleInstallModule.Install638Patch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstall638Patch), lvwTask.FindItemWithText("638补丁"))
                        ElseIf .Is640PatchInstalled = False Then
                            lvwTask.Items.Insert(lvwTask.FindItemWithText("641补丁").Index, New ListViewItem("640补丁", lvwTask.Groups.Item("lvwGroupSubassembly")))
                            lvwTask.FindItemWithText("640补丁").ImageKey = "installing"
                            ReportProgress(ModuleInstallModule.Install640Patch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstall640Patch), lvwTask.FindItemWithText("640补丁"))
                        End If
                        lvwTask.FindItemWithText("641补丁").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.Install641Patch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstall641Patch), lvwTask.FindItemWithText("641补丁"))
                    ElseIf .Is641PatchInstalled = True And ModuleMain.InstallOptions.IsInstall641Patch = False Then
                        lvwTask.Items.Insert(lvwTask.FindItemWithText("641补丁").Index, New ListViewItem("638补丁", lvwTask.Groups.Item("lvwGroupSubassembly")))
                        lvwTask.Items.Insert(lvwTask.FindItemWithText("641补丁").Index, New ListViewItem("640补丁", lvwTask.Groups.Item("lvwGroupSubassembly")))
                        lvwTask.FindItemWithText("641补丁").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.Install641Patch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstall641Patch), lvwTask.FindItemWithText("641补丁"))
                        lvwTask.FindItemWithText("640补丁").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.Install640Patch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstall640Patch), lvwTask.FindItemWithText("640补丁"))
                        lvwTask.FindItemWithText("638补丁").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.Install638Patch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstall638Patch), lvwTask.FindItemWithText("638补丁"))
                    ElseIf .Is640PatchInstalled = False And ModuleMain.InstallOptions.IsInstall640Patch = True Then
                        If .Is638PatchInstalled = False Then
                            lvwTask.Items.Insert(lvwTask.FindItemWithText("640补丁").Index, New ListViewItem("638补丁", lvwTask.Groups.Item("lvwGroupSubassembly")))
                            lvwTask.FindItemWithText("638补丁").ImageKey = "installing"
                            ReportProgress(ModuleInstallModule.Install638Patch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstall638Patch), lvwTask.FindItemWithText("638补丁"))
                        End If
                        lvwTask.FindItemWithText("640补丁").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.Install640Patch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstall640Patch), lvwTask.FindItemWithText("640补丁"))
                    ElseIf .Is640PatchInstalled = True And ModuleMain.InstallOptions.IsInstall640Patch = False Then
                        lvwTask.Items.Insert(lvwTask.FindItemWithText("640补丁").Index, New ListViewItem("638补丁", lvwTask.Groups.Item("lvwGroupSubassembly")))
                        lvwTask.FindItemWithText("640补丁").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.Install640Patch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstall640Patch), lvwTask.FindItemWithText("640补丁"))
                        lvwTask.FindItemWithText("638补丁").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.Install638Patch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstall638Patch), lvwTask.FindItemWithText("638补丁"))
                    ElseIf (.Is638PatchInstalled = False And ModuleMain.InstallOptions.IsInstall638Patch = True) _
                        Or (.Is638PatchInstalled = True And ModuleMain.InstallOptions.IsInstall638Patch = False) Then
                        lvwTask.FindItemWithText("638补丁").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.Install638Patch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstall638Patch), lvwTask.FindItemWithText("638补丁"))
                    End If
                    If .IsNoCDPatchInstalled <> ModuleMain.InstallOptions.IsInstallNoCDPatch Then
                        lvwTask.FindItemWithText("免CD补丁").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.InstallNoCDPatch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstallNoCDPatch), lvwTask.FindItemWithText("免CD补丁"))
                    ElseIf .Is4GBPatchInstalled <> ModuleMain.InstallOptions.IsInstall4GBPatch Then
                        lvwTask.FindItemWithText("4GB补丁").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.Install4GBPatch(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstall4GBPatch), lvwTask.FindItemWithText("4GB补丁"))
                    ElseIf .IsSC4LauncherInstalled <> ModuleMain.InstallOptions.IsInstallSC4Launcher Then
                        lvwTask.FindItemWithText("模拟城市4 启动器").ImageKey = "installing"
                        ReportProgress(ModuleInstallModule.InstallSC4Launcher(.SC4InstallDir, Not ModuleMain.InstallOptions.IsInstallSC4Launcher), lvwTask.FindItemWithText("模拟城市4 启动器"))
                    End If
                    If .LanguagePatch <> .LanguagePatch Then
                        Select Case .LanguagePatch
                            Case InstallOptions.Language.TraditionalChinese
                                lvwTask.FindItemWithText("繁体中文语言补丁").ImageKey = "installing"
                                ReportProgress(ModuleInstallModule.InstallLanguagePatch(.SC4InstallDir, InstallOptions.Language.TraditionalChinese), lvwTask.FindItemWithText("繁体中文语言补丁"))
                            Case InstallOptions.Language.SimplifiedChinese
                                lvwTask.FindItemWithText("简体中文语言补丁").ImageKey = "installing"
                                ReportProgress(ModuleInstallModule.InstallLanguagePatch(.SC4InstallDir, InstallOptions.Language.SimplifiedChinese), lvwTask.FindItemWithText("简体中文语言补丁"))
                            Case InstallOptions.Language.English
                                ModuleInstallModule.InstallLanguagePatch(.SC4InstallDir, InstallOptions.Language.English)
                        End Select
                    End If
                End With
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
        Dim DAEMONItem As ListViewItem = lvwTask.FindItemWithText("DAEMON Tools Lite")
        Dim SC4Item As ListViewItem = lvwTask.FindItemWithText("模拟城市4 豪华版")
        Dim _638PatchItem As ListViewItem = lvwTask.FindItemWithText("638补丁")
        Dim _640PatchItem As ListViewItem = lvwTask.FindItemWithText("640补丁")
        Dim _641PatchItem As ListViewItem = lvwTask.FindItemWithText("641补丁")
        Dim _4GBPatchItem As ListViewItem = lvwTask.FindItemWithText("4GB补丁")
        Dim NoCDPatchItem As ListViewItem = lvwTask.FindItemWithText("免CD补丁")
        Dim SC4LauncherItem As ListViewItem = lvwTask.FindItemWithText("模拟城市4 启动器")
        Dim LanguagePatchItem As ListViewItem = lvwTask.FindItemWithText("语言补丁")
        Dim AddDesktopIconItem As ListViewItem = lvwTask.FindItemWithText("添加桌面图标")
        Dim AddStartMenuIem As ListViewItem = lvwTask.FindItemWithText("添加开始菜单项")
        With ModuleMain.InstallOptions
            If IsNothing(ModuleMain.InstalledModule) = True Then
                If .IsInstallDAEMONTools = False Then DAEMONItem.Remove()
                If .SC4Type = InstallOptions.SC4InstallType.ISO Then SC4Item.Text = "模拟城市4 豪华版 镜像版"
                If .SC4Type = InstallOptions.SC4InstallType.NoInstall Then SC4Item.Text = "模拟城市4 豪华版 硬盘版"
                If .IsInstall638Patch = False Then _638PatchItem.Remove() : If .IsInstall640Patch = False Then _640PatchItem.Remove()
                If .IsInstall641Patch = False Then _641PatchItem.Remove() : If .IsInstall4GBPatch = False Then _4GBPatchItem.Remove()
                If .IsInstallNoCDPatch = False Then NoCDPatchItem.Remove() : If .IsInstallSC4Launcher = False Then SC4LauncherItem.Remove()
                If .IsAddDesktopIcon = False Then AddDesktopIconItem.Remove() : If .IsAddStartMenuItem = False Then AddStartMenuIem.Remove()
                Select Case .LanguagePatch
                    Case InstallOptions.Language.TraditionalChinese : LanguagePatchItem.Text = "繁体中文语言补丁"
                    Case InstallOptions.Language.SimplifiedChinese : LanguagePatchItem.Text = "简体中文语言补丁"
                    Case InstallOptions.Language.English : LanguagePatchItem.Remove()
                End Select
            Else
                lblTitle.Text = "正在更改组件"
                DAEMONItem.Remove() : SC4Item.Remove() : AddDesktopIconItem.Remove() : AddStartMenuIem.Remove()
                If ModuleMain.InstalledModule.Is638PatchInstalled = .IsInstall638Patch Then _638PatchItem.Remove()
                If ModuleMain.InstalledModule.Is640PatchInstalled = .IsInstall640Patch Then _640PatchItem.Remove()
                If ModuleMain.InstalledModule.Is641PatchInstalled = .IsInstall641Patch Then _641PatchItem.Remove()
                If ModuleMain.InstalledModule.Is4GBPatchInstalled = .IsInstall4GBPatch Then _4GBPatchItem.Remove()
                If ModuleMain.InstalledModule.IsNoCDPatchInstalled = .IsInstallNoCDPatch Then NoCDPatchItem.Remove()
                If ModuleMain.InstalledModule.IsSC4LauncherInstalled = .IsInstallSC4Launcher Then SC4LauncherItem.Remove()
                If ModuleMain.InstalledModule.LanguagePatch = .LanguagePatch Then
                    LanguagePatchItem.Remove()
                Else
                    Select Case .LanguagePatch
                        Case InstallOptions.Language.TraditionalChinese : LanguagePatchItem.Text = "繁体中文语言补丁"
                        Case InstallOptions.Language.SimplifiedChinese : LanguagePatchItem.Text = "简体中文语言补丁"
                        Case InstallOptions.Language.English : LanguagePatchItem.Remove()
                    End Select
                End If
            End If
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