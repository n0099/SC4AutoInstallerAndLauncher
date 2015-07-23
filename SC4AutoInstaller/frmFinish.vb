Public Class frmFinish

    ''' <summary>将某个组件标记为安装失败，并将该项从安装成功组件列表框移到到安装失败列表框内</summary>
    ''' <param name="item">要标记为安装失败的项</param>
    Private Sub SubassemblyInstallFail(item As ListViewItem)
        item.ImageKey = "fail"
        item.Remove()
        item.Group = lvwSubassemblyFail.Groups.Item("lvwGroupFail")
        lvwSubassemblyFail.Items.Add(item)
        lvwSubassemblyFail.Visible = True
        lblTitle2.Text = "部分组件安装失败"
    End Sub

    Private Sub llbBlog_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbBlog.LinkClicked
        Process.Start("http://n0099.sinaapp.com")
    End Sub

    Private Sub llbReportBug_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbReportBug.LinkClicked
        Process.Start("http://tieba.baidu.com/p/3802761033")
    End Sub

    Private Sub llbSCB_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSCB.LinkClicked
        Process.Start("http://tieba.baidu.com/f?kw=%C4%A3%C4%E2%B3%C7%CA%D0")
    End Sub

    Private Sub llbSCCN_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSCCN.LinkClicked
        Process.Start("http://www.simcity.cn")
    End Sub

    Private Sub frmFinish_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DAEMONItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("DAEMON Tools Lite"), SC4Item As ListViewItem = lvwSubassemblySuccess.FindItemWithText("模拟城市4 豪华版")
        Dim _638PatchItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("638补丁"), _640PatchItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("640补丁")
        Dim _641PatchItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("641补丁"), _4GBPatchItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("4GB补丁")
        Dim NoCDPatchItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("免CD补丁"), SC4LauncherItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("模拟城市4 启动器")
        Dim LanguagePatchItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("语言补丁")
        Dim AddDesktopIconItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("添加桌面图标"), AddStartMenuIem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("添加开始菜单项")
        With ModuleMain.InstallOptions
            lvwSubassemblySuccess.BeginUpdate() : lvwSubassemblyFail.BeginUpdate()
            If IsNothing(ModuleMain.InstalledModule) = True Then
                If .InstallDAEMONTools = False Then DAEMONItem.Remove() Else If ModuleMain.InstallResult.DAEMONToolsInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(DAEMONItem)
                If ModuleMain.InstallResult.SC4InstallResult = InstallResult.Result.Success And _
                    My.Computer.FileSystem.FileExists(.SC4InstallDir & "\Apps\SimCity 4.exe") = True Then btnRunSC4.Enabled = True
                If ModuleMain.InstallResult.SC4InstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(SC4Item)
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
            Else
                DAEMONItem.Remove() : SC4Item.Remove() : AddDesktopIconItem.Remove() : AddStartMenuIem.Remove()
                If ModuleMain.InstalledModule.Is638PatchInstalled = .Install638Patch Then _638PatchItem.Remove()
                If ModuleMain.InstalledModule.Is640PatchInstalled = .Install640Patch Then _640PatchItem.Remove()
                If ModuleMain.InstalledModule.Is641PatchInstalled = .Install641Patch Then _641PatchItem.Remove()
                If ModuleMain.InstalledModule.Is4GBPatchInstalled = .Install4GBPatch Then _4GBPatchItem.Remove()
                If ModuleMain.InstalledModule.IsNoCDPatchInstalled = .InstallNoCDPatch Then NoCDPatchItem.Remove()
                If ModuleMain.InstalledModule.IsSC4LauncherInstalled = .InstallSC4Launcher Then SC4LauncherItem.Remove()
                If ModuleMain.InstalledModule.LanguagePatch = .LanguagePatch Then LanguagePatchItem.Remove()
            End If
            With ModuleMain.InstallResult
                If ._638PatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(_638PatchItem)
                If ._640PatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(_640PatchItem)
                If ._641PatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(_641PatchItem)
                If ._4GBPatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(_4GBPatchItem)
                If .NoCDPatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(NoCDPatchItem)
                If .SC4LauncherInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(SC4LauncherItem)
                If .LanguagePatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(LanguagePatchItem)
                If .AddDesktopIconResult = InstallResult.Result.Fail Then SubassemblyInstallFail(AddDesktopIconItem)
                If .AddStartMenuItemResult = InstallResult.Result.Fail Then SubassemblyInstallFail(AddStartMenuIem)
            End With
            Select Case .LanguagePatch
                Case InstallOptions.Language.TraditionalChinese : LanguagePatchItem.Text = "繁体中文语言补丁"
                Case InstallOptions.Language.SimplifiedChinese : LanguagePatchItem.Text = "简体中文语言补丁"
                Case InstallOptions.Language.English : LanguagePatchItem.Remove()
            End Select
            lvwSubassemblySuccess.EndUpdate() : lvwSubassemblyFail.EndUpdate()
        End With
        Dim FlashInfo As New FLASHINFO With {.cbSize = Convert.ToInt32(Runtime.InteropServices.Marshal.SizeOf(FlashInfo)) _
                                             , .uCount = 5, .dwTimeout = 0, .hwnd = Me.Handle, .dwFlags = FLASHW_ALL}
        FlashWindowEx(FlashInfo)
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099"
    End Sub

    Private Sub btnRunSC4_Click(sender As Object, e As EventArgs) Handles btnRunSC4.Click
        If My.Computer.FileSystem.FileExists(ModuleMain.InstallOptions.SC4InstallDir & "\SC4Launcher.exe") = True Then
            Process.Start(ModuleMain.InstallOptions.SC4InstallDir & "\SC4Launcher.exe")
        Else
            Process.Start(ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe")
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

End Class