Public Class frmInstalling

    ''' <summary>报告某个组件的安装结果，并更改安装任务列表框内对应项的图标</summary>
    ''' <param name="result">组件的安装结果，必须为 InstallResult.Result 枚举的值之一</param>
    ''' <param name="item">安装任务列表框的对应项</param>
    Private Sub ReportProgress(ByVal result As InstallResult.Result, ByVal item As ListViewItem)
        With ModuleMain.InstallResult
            If result = InstallResult.Result.Success Then item.ImageKey = "success" Else item.ImageKey = "fail"
            Select Case item.Text
                Case "DAEMON Tools Lite" : .DAEMONToolsInstallResult = result
                Case "638补丁" : ._638PatchInstallResult = result
                    If result = InstallResult.Result.Fail Then
                        ._640PatchInstallResult = InstallResult.Result.Fail : ._641PatchInstallResult = InstallResult.Result.Fail
                        If IsNothing(lvwTask.FindItemWithText("640补丁")) = True Then lvwTask.FindItemWithText("640补丁").ImageKey = "fail"
                        If IsNothing(lvwTask.FindItemWithText("641补丁")) = True Then lvwTask.FindItemWithText("641补丁").ImageKey = "fail"
                    End If
                Case "640补丁" : ._640PatchInstallResult = result
                    If result = InstallResult.Result.Fail Then
                        ._641PatchInstallResult = InstallResult.Result.Fail
                        If IsNothing(lvwTask.FindItemWithText("641补丁")) = True Then lvwTask.FindItemWithText("641补丁").ImageKey = "fail"
                    End If
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
        Dim DAEMONItem As ListViewItem = lvwTask.FindItemWithText("DAEMON Tools Lite"), SC4Item As ListViewItem = lvwTask.FindItemWithText("模拟城市4 豪华版")
        Dim _638PatchItem As ListViewItem = lvwTask.FindItemWithText("638补丁"), _640PatchItem As ListViewItem = lvwTask.FindItemWithText("640补丁")
        Dim _641PatchItem As ListViewItem = lvwTask.FindItemWithText("641补丁"), _4GBPatchItem As ListViewItem = lvwTask.FindItemWithText("4GB补丁")
        Dim NoCDPatchItem As ListViewItem = lvwTask.FindItemWithText("免CD补丁"), SC4LauncherItem As ListViewItem = lvwTask.FindItemWithText("模拟城市4 启动器")
        Dim AddDesktopIconItem As ListViewItem = lvwTask.FindItemWithText("添加桌面图标"), AddStartMenuIem As ListViewItem = lvwTask.FindItemWithText("添加开始菜单项")
        With ModuleMain.InstallOptions
            If IsNothing(ModuleMain.InstalledModule) = True Then
                If .InstallDAEMONTools = True Then DAEMONItem.ImageKey = "installing" : ReportProgress(ModuleInstallModule.InstallDAEMONTools(), DAEMONItem)
                If .SC4Type = InstallOptions.SC4InstallType.ISO Then
                    SC4Item.ImageKey = "installing" : ReportProgress(ModuleInstallModule.InstallSC4(InstallOptions.SC4InstallType.ISO), SC4Item)
                ElseIf .SC4Type = InstallOptions.SC4InstallType.NoInstall Then
                    SC4Item.ImageKey = "installing" : ReportProgress(ModuleInstallModule.InstallSC4(InstallOptions.SC4InstallType.NoInstall), SC4Item)
                    ModuleInstallModule.SetNoInstallSC4RegValue()
                End If
                ModuleInstallModule.SetControlPanelProgramItemRegValue()
                If bgwInstall.CancellationPending = True Then Exit Sub
                If .Install638Patch = True Then _638PatchItem.ImageKey = "installing" _
                    : ReportProgress(ModuleInstallModule.Install638Patch(.SC4InstallDir, False), _638PatchItem)
                If .Install640Patch = True And ModuleMain.InstallResult._638PatchInstallResult = InstallResult.Result.Success Then _640PatchItem.ImageKey = "installing" _
                    : ReportProgress(ModuleInstallModule.Install640Patch(.SC4InstallDir, False), _640PatchItem)
                If .Install641Patch = True And ModuleMain.InstallResult._640PatchInstallResult = InstallResult.Result.Success Then _641PatchItem.ImageKey = "installing" _
                      : ReportProgress(ModuleInstallModule.Install641Patch(.SC4InstallDir, False), _641PatchItem)
                If .InstallNoCDPatch = True Then NoCDPatchItem.ImageKey = "installing" _
                    : ReportProgress(ModuleInstallModule.InstallNoCDPatch(.SC4InstallDir, False), NoCDPatchItem)
                If .Install4GBPatch = True Then _4GBPatchItem.ImageKey = "installing" _
                    : ReportProgress(ModuleInstallModule.Install4GBPatch(.SC4InstallDir, False), _4GBPatchItem)
                If .InstallSC4Launcher = True Then SC4LauncherItem.ImageKey = "installing" _
                    : ReportProgress(ModuleInstallModule.InstallSC4Launcher(.SC4InstallDir, False), SC4LauncherItem)
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
                If .AddDesktopIcon = True Then AddDesktopIconItem.ImageKey = "installing" : ReportProgress(ModuleInstallModule.AddDestopIcon(), AddDesktopIconItem)
                If .AddStartMenuItem = True Then AddStartMenuIem.ImageKey = "installing" : ReportProgress(ModuleInstallModule.AddStartMenuItems(), AddStartMenuIem)
            Else
                If ModuleMain.InstalledModule.Is638PatchInstalled <> .Install638Patch Then _638PatchItem.ImageKey = "installing" _
                    : ReportProgress(ModuleInstallModule.Install638Patch(ModuleMain.InstalledModule.SC4InstallDir, Not .Install638Patch), _638PatchItem)
                If ModuleMain.InstalledModule.Is640PatchInstalled <> .Install640Patch And ModuleMain.InstallResult._638PatchInstallResult = InstallResult.Result.Success Then _640PatchItem.ImageKey = "installing" _
                    : ReportProgress(ModuleInstallModule.Install640Patch(ModuleMain.InstalledModule.SC4InstallDir, Not .Install640Patch), _640PatchItem)
                If ModuleMain.InstalledModule.Is641PatchInstalled <> .Install641Patch And ModuleMain.InstallResult._641PatchInstallResult = InstallResult.Result.Success Then _641PatchItem.ImageKey = "installing" _
                    : ReportProgress(ModuleInstallModule.Install641Patch(ModuleMain.InstalledModule.SC4InstallDir, Not .Install641Patch), _641PatchItem)
                If ModuleMain.InstalledModule.IsNoCDPatchInstalled <> .InstallNoCDPatch Then NoCDPatchItem.ImageKey = "installing" _
                    : ReportProgress(ModuleInstallModule.InstallNoCDPatch(ModuleMain.InstalledModule.SC4InstallDir, Not .InstallNoCDPatch), NoCDPatchItem)
                If ModuleMain.InstalledModule.Is4GBPatchInstalled <> .Install4GBPatch Then _4GBPatchItem.ImageKey = "installing" _
                    : ReportProgress(ModuleInstallModule.Install4GBPatch(ModuleMain.InstalledModule.SC4InstallDir, Not .Install4GBPatch), _4GBPatchItem)
                If ModuleMain.InstalledModule.IsSC4LauncherInstalled <> .InstallSC4Launcher Then SC4LauncherItem.ImageKey = "installing" _
                    : ReportProgress(ModuleInstallModule.InstallSC4Launcher(ModuleMain.InstalledModule.SC4InstallDir, Not .InstallSC4Launcher), SC4LauncherItem)
                If ModuleMain.InstalledModule.LanguagePatch <> .LanguagePatch Then
                    Select Case .LanguagePatch
                        Case InstallOptions.Language.TraditionalChinese
                            lvwTask.FindItemWithText("繁体中文语言补丁").ImageKey = "installing"
                            ReportProgress(ModuleInstallModule.InstallLanguagePatch(ModuleMain.InstalledModule.SC4InstallDir, InstallOptions.Language.TraditionalChinese), lvwTask.FindItemWithText("繁体中文语言补丁"))
                        Case InstallOptions.Language.SimplifiedChinese
                            lvwTask.FindItemWithText("简体中文语言补丁").ImageKey = "installing"
                            ReportProgress(ModuleInstallModule.InstallLanguagePatch(ModuleMain.InstalledModule.SC4InstallDir, InstallOptions.Language.SimplifiedChinese), lvwTask.FindItemWithText("简体中文语言补丁"))
                        Case InstallOptions.Language.English
                            ModuleInstallModule.InstallLanguagePatch(ModuleMain.InstalledModule.SC4InstallDir, InstallOptions.Language.English)
                    End Select
                End If
            End If
            ModuleInstallModule.SetControlPanelProgramItemRegValue()
            Threading.Thread.Sleep(500)
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
        Dim DAEMONItem As ListViewItem = lvwTask.FindItemWithText("DAEMON Tools Lite"), SC4Item As ListViewItem = lvwTask.FindItemWithText("模拟城市4 豪华版")
        Dim _638PatchItem As ListViewItem = lvwTask.FindItemWithText("638补丁"), _640PatchItem As ListViewItem = lvwTask.FindItemWithText("640补丁")
        Dim _641PatchItem As ListViewItem = lvwTask.FindItemWithText("641补丁"), _4GBPatchItem As ListViewItem = lvwTask.FindItemWithText("4GB补丁")
        Dim NoCDPatchItem As ListViewItem = lvwTask.FindItemWithText("免CD补丁"), SC4LauncherItem As ListViewItem = lvwTask.FindItemWithText("模拟城市4 启动器")
        Dim LanguagePatchItem As ListViewItem = lvwTask.FindItemWithText("语言补丁")
        Dim AddDesktopIconItem As ListViewItem = lvwTask.FindItemWithText("添加桌面图标"), AddStartMenuIem As ListViewItem = lvwTask.FindItemWithText("添加开始菜单项")
        With ModuleMain.InstallOptions
            lvwTask.BeginUpdate()
            If IsNothing(ModuleMain.InstalledModule) = True Then
                If .InstallDAEMONTools = False Then DAEMONItem.Remove()
                If .SC4Type = InstallOptions.SC4InstallType.ISO Then SC4Item.Text = "模拟城市4 豪华版 镜像版"
                If .SC4Type = InstallOptions.SC4InstallType.NoInstall Then SC4Item.Text = "模拟城市4 豪华版 硬盘版"
                If .Install638Patch = False Then _638PatchItem.Remove()
                If .Install640Patch = False Then _640PatchItem.Remove()
                If .Install641Patch = False Then _641PatchItem.Remove()
                If .Install4GBPatch = False Then _4GBPatchItem.Remove()
                If .InstallNoCDPatch = False Then NoCDPatchItem.Remove()
                If .InstallSC4Launcher = False Then SC4LauncherItem.Remove()
                If .AddDesktopIcon = False Then AddDesktopIconItem.Remove()
                If .AddStartMenuItem = False Then AddStartMenuIem.Remove()
                Select Case .LanguagePatch
                    Case InstallOptions.Language.TraditionalChinese : LanguagePatchItem.Text = "繁体中文语言补丁"
                    Case InstallOptions.Language.SimplifiedChinese : LanguagePatchItem.Text = "简体中文语言补丁"
                    Case InstallOptions.Language.English : LanguagePatchItem.Remove()
                End Select
            Else
                lblTitle.Text = "正在更改组件"
                DAEMONItem.Remove() : SC4Item.Remove() : AddDesktopIconItem.Remove() : AddStartMenuIem.Remove()
                If ModuleMain.InstalledModule.Is638PatchInstalled = .Install638Patch Then _638PatchItem.Remove()
                If ModuleMain.InstalledModule.Is640PatchInstalled = .Install640Patch Then _640PatchItem.Remove()
                If ModuleMain.InstalledModule.Is641PatchInstalled = .Install641Patch Then _641PatchItem.Remove()
                If ModuleMain.InstalledModule.Is4GBPatchInstalled = .Install4GBPatch Then _4GBPatchItem.Remove()
                If ModuleMain.InstalledModule.IsNoCDPatchInstalled = .InstallNoCDPatch Then NoCDPatchItem.Remove()
                If ModuleMain.InstalledModule.IsSC4LauncherInstalled = .InstallSC4Launcher Then SC4LauncherItem.Remove()
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
            lvwTask.EndUpdate()
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