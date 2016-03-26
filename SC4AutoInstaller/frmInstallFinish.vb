Imports InstallResult = SC4AutoInstaller.InstallResults.InstallResult

Public Class frmInstallFinish

    ''' <summary>将某个组件标记为安装失败，并将该项从安装成功组件列表框移到到安装失败列表框内</summary>
    ''' <param name="item">要标记为安装失败的项</param>
    Private Sub SubassemblyInstallFail(item As ListViewItem)
        If item IsNot Nothing Then
            item.ImageKey = "Fail" '将该项的图标改为失败图标
            item.Remove() '将该项从安装成功组件列表框删除
            item.Group = lvwSubassemblyFail.Groups(0) '改变该项的组
            lvwSubassemblyFail.Items.Add(item) '在安装失败组件列表框内添加该项
            lvwSubassemblyFail.Visible = True '显示安装失败组件列表框
            lblTitle2.Text = "部分组件" & If(ModuleDeclare.InstalledModules Is Nothing, "安装", "更改") & "失败，您可以随后使用本安装程序的安装或卸载组件功能来重新安装安装失败的组件。"
        End If
    End Sub

    Private Sub llbBlog_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbBlog.LinkClicked
        Process.Start("http://n0099.coding.io")
    End Sub

    Private Sub llbReportBug_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbReportBug.LinkClicked
        Process.Start("http://tieba.baidu.com/p/3802761033")
    End Sub

    Private Sub llbSCTB_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSCTB.LinkClicked
        Process.Start("http://tieba.baidu.com/模拟城市")
    End Sub

    Private Sub llbSCCN_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSCCN.LinkClicked
        Process.Start("http://www.simcity.cn")
    End Sub

    Private Sub frmInstallFinish_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '声明一些用于快速访问安装组件列表框项的变量
        Dim DAEMONToolsItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("DAEMON Tools Lite"), SC4Item As ListViewItem = lvwSubassemblySuccess.FindItemWithText("模拟城市4 豪华版")
        Dim _638PatchItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("638补丁"), _640PatchItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("640补丁")
        Dim _641PatchItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("641补丁"), _4GBPatchItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("4GB补丁")
        Dim NoCDPatchItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("免CD补丁"), SC4LauncherItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("模拟城市4 启动器")
        Dim LanguageItem As ListViewItem = lvwSubassemblySuccess.FindItemWithText("语言补丁")
        lvwSubassemblySuccess.BeginUpdate() : lvwSubassemblyFail.BeginUpdate()
#Region "同步安装选项跟安装结果列表框里的对应项"
        If ModuleDeclare.InstalledModules Is Nothing Then '判断是否已经安装了模拟城市4
            With ModuleDeclare.InstallOptions
                If .IsInstallDAEMONTools = False Then DAEMONToolsItem.Remove()
                '如果模拟城市4安装失败，则将安装成功的组件列表框的对应项移动到安装失败的组件列表框里
                If ModuleDeclare.InstallResults.SC4InstallResult = InstallResult.Fail Then SubassemblyInstallFail(SC4Item)
                '根据安装选项里所选择的模拟城市4版本来更改安装组件列表框里模拟城市4 豪华版项的文本
                If .SC4InstallType = InstallOptions.SC4Type.CD Then SC4Item.Text = "模拟城市4 豪华版 镜像版"
                If .SC4InstallType = InstallOptions.SC4Type.NoInstall Then SC4Item.Text = "模拟城市4 豪华版 硬盘版"
                '删除安装选项里选择不安装的组件在安装组件列表框里的对应项
                If .IsInstall638Patch = False Then _638PatchItem.Remove() : _638PatchItem = Nothing
                If .IsInstall640Patch = False Then _640PatchItem.Remove() : _640PatchItem = Nothing
                If .IsInstall641Patch = False Then _641PatchItem.Remove() : _641PatchItem = Nothing
                If .IsInstall4GBPatch = False Then _4GBPatchItem.Remove() : _4GBPatchItem = Nothing
                If .IsInstallNoCDPatch = False Then NoCDPatchItem.Remove() : NoCDPatchItem = Nothing
                If .IsInstallSC4Launcher = False Then SC4LauncherItem.Remove() : SC4LauncherItem = Nothing
                '如果模拟城市4安装成功且存在游戏安装目录\Apps\SimCity 4.exe文件则激活启动模拟城市4 豪华版按钮
                If ModuleDeclare.InstallResults.SC4InstallResult = InstallResult.Success AndAlso My.Computer.FileSystem.FileExists(.SC4InstallDir & "\Apps\SimCity 4.exe") Then btnRunSC4.Enabled = True
            End With
        Else '已安装模拟城市4
            '更改标题文本和安装组件列表框组的标题
            lblTitle.Text = "更改已完成" : lblTitle2.Text = "所有组件均已成功更改，您可以随后使用本安装程序来安装或卸载其他组件。"
            lvwSubassemblySuccess.Groups(0).Header = "以下组件更改成功" : lvwSubassemblyFail.Groups(0).Header = "以下组件更改失败"
            DAEMONToolsItem.Remove() : SC4Item.Remove() '删除不适用的项
            With ModuleDeclare.ChangeOptions
                '删除安装选项里未更改的组件在安装组件列表框里对应项
                If ._638PatchOption = ChangeOptions.ChangeOption.Unchanged Then _638PatchItem.Remove()
                If ._640PatchOption = ChangeOptions.ChangeOption.Unchanged Then _640PatchItem.Remove()
                If ._641PatchOption = ChangeOptions.ChangeOption.Unchanged Then _641PatchItem.Remove()
                If ._4GBPatchOption = ChangeOptions.ChangeOption.Unchanged Then _4GBPatchItem.Remove()
                If .NoCDPatchOption = ChangeOptions.ChangeOption.Unchanged Then NoCDPatchItem.Remove()
                If .SC4LauncherOption = ChangeOptions.ChangeOption.Unchanged Then SC4LauncherItem.Remove()
                If .LanguagePatchOption = ModuleDeclare.InstalledModules.LanguagePatchOption Then LanguageItem.Remove()
                '如果存在游戏安装目录\Apps\SimCity 4.exe文件则激活启动模拟城市4 豪华版按钮
                If My.Computer.FileSystem.FileExists(ModuleDeclare.InstalledModules.SC4InstallDir & "\Apps\SimCity 4.exe") Then btnRunSC4.Enabled = True
            End With
        End If
#End Region
        With ModuleDeclare.InstallResults '如果某个组件安装失败，则将安装成功的组件列表框的对应项移动到安装失败的组件列表框里
            If .DAEMONToolsResult = InstallResult.Fail Then SubassemblyInstallFail(DAEMONToolsItem)
            If ._638PatchResult = InstallResult.Fail Then SubassemblyInstallFail(_638PatchItem)
            If ._640PatchResult = InstallResult.Fail Then SubassemblyInstallFail(_640PatchItem)
            If ._641PatchResult = InstallResult.Fail Then SubassemblyInstallFail(_641PatchItem)
            If ._4GBPatchResult = InstallResult.Fail Then SubassemblyInstallFail(_4GBPatchItem)
            If .NoCDPatchResult = InstallResult.Fail Then SubassemblyInstallFail(NoCDPatchItem)
            If .SC4LauncherResult = InstallResult.Fail Then SubassemblyInstallFail(SC4LauncherItem)
            If .LanguagePatchResult = InstallResult.Fail Then SubassemblyInstallFail(LanguageItem)
        End With
        '根据安装选项里所选择的语言补丁来更改安装组件列表框里对应项的文本
        Select Case If(ModuleDeclare.InstalledModules Is Nothing, ModuleDeclare.InstallOptions.LanguagePatchOption, ModuleDeclare.ChangeOptions.LanguagePatchOption)
            Case SC4Language.TraditionalChinese : LanguageItem.Text = "繁体中文语言补丁"
            Case SC4Language.SimplifiedChinese : LanguageItem.Text = "简体中文语言补丁"
            Case SC4Language.English : LanguageItem.Text = "英语语言补丁"
        End Select
        lvwSubassemblySuccess.EndUpdate() : lvwSubassemblyFail.EndUpdate()
        Dim FlashInfo As New FLASHWINFO With {.cbSize = Runtime.InteropServices.Marshal.SizeOf(FlashInfo),
                                              .uCount = 5, .dwTimeout = 0, .hwnd = Me.Handle, .dwFlags = FLASHW_ALL} '创建一个ModuleDeclare.FLASHINFO结构的实例
        FlashWindowEx(FlashInfo) '以FlashInfo实例的选项来闪动窗口
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

    Private Sub btnRunSC4_Click(sender As Object, e As EventArgs) Handles btnRunSC4.Click
        If My.Computer.FileSystem.FileExists(ModuleDeclare.InstallOptions.SC4InstallDir & "\SC4Launcher.exe") Then '如果存在游戏安装目录\SC4Launcher.exe文件则启动该程序
            Process.Start(ModuleDeclare.InstallOptions.SC4InstallDir & "\SC4Launcher.exe")
        Else
            Process.Start(ModuleDeclare.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe")
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

End Class