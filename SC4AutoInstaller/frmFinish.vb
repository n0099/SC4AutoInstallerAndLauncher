Public Class frmFinish

    Private Sub SubassemblyInstallFail(item As ListViewItem)
        item.ImageKey = "fail"
        item.Group = lvwSubassembly.Groups.Item(1)
        lblTitle2.Text = "部分组件安装失败"
    End Sub

    Private Sub llbSCB_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSCB.LinkClicked
        Process.Start("http://tieba.baidu.com/f?kw=%C4%A3%C4%E2%B3%C7%CA%D0")
    End Sub

    Private Sub llbSCCN_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSCCN.LinkClicked
        Process.Start("http://www.simcity.cn")
    End Sub

    Private Sub frmFinish_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DAEMONItem As ListViewItem = lvwSubassembly.FindItemWithText("DAEMON Tools Lite")
        Dim SC4Item As ListViewItem = lvwSubassembly.FindItemWithText("模拟城市4 豪华版")
        Dim _638PatchItem As ListViewItem = lvwSubassembly.FindItemWithText("638补丁")
        Dim _640PatchItem As ListViewItem = lvwSubassembly.FindItemWithText("640补丁")
        Dim _4GBPatchItem As ListViewItem = lvwSubassembly.FindItemWithText("4GB补丁")
        Dim NoCDPatchItem As ListViewItem = lvwSubassembly.FindItemWithText("免CD补丁")
        Dim SC4LauncherItem As ListViewItem = lvwSubassembly.FindItemWithText("模拟城市4 启动器")
        Dim LanguagePatchItem As ListViewItem = lvwSubassembly.FindItemWithText("语言补丁")
        With ModuleMain.InstallOptions
            If .IsInstallDAEMONTools = False Then DAEMONItem.Remove()
            If .IsInstallDAEMONTools = True Then If ModuleMain.InstallResult.DAEMONToolsInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(DAEMONItem)
            If ModuleMain.InstallResult.SC4InstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(SC4Item) : btnRunSC4.Enabled = False
            If .SC4Type = InstallOptions.SC4InstallType.ISO Then SC4Item.Text = "模拟城市4 豪华版 镜像版"
            If .SC4Type = InstallOptions.SC4InstallType.NoInstall Then SC4Item.Text = "模拟城市4 豪华版 硬盘版"
            With ModuleMain.InstallResult
                If ModuleMain.InstallOptions.IsInstall638Patch = False Then _638PatchItem.Remove() Else If ._638PatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(_638PatchItem)
                If ModuleMain.InstallOptions.IsInstall640Patch = False Then _640PatchItem.Remove() Else If ._640PatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(_640PatchItem)
                If ModuleMain.InstallOptions.IsInstall4GBPatch = False Then _4GBPatchItem.Remove() Else If ._4GBPatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(_4GBPatchItem)
                If ModuleMain.InstallOptions.IsInstallNoCDPatch = False Then NoCDPatchItem.Remove() Else If .NoCDPatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(NoCDPatchItem)
                If ModuleMain.InstallOptions.IsInstallSC4Launcher = False Then SC4LauncherItem.Remove() Else If .SC4LauncherInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(SC4LauncherItem)
            End With
            If ModuleMain.InstallResult.LanguagePatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(LanguagePatchItem)
            Select Case .LanguagePatch
                Case InstallOptions.Language.TraditionalChinese
                    LanguagePatchItem.Text = "繁体中文语言补丁"
                Case InstallOptions.Language.SimplifiedChinese
                    LanguagePatchItem.Text = "简体中文语言补丁"
                Case InstallOptions.Language.English
                    LanguagePatchItem.Remove()
            End Select
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