Imports Opt = SC4AutoInstaller.InstallOptions '引用SC4AutoInstaller.InstallOptions类以便省略代码中重复的InstallOptions类引用
Imports Res = SC4AutoInstaller.InstallResult '引用SC4AutoInstaller.InstallResult类以便省略代码中重复的InstallResult类引用

Public Class frmInstalling

    ''' <summary>报告某个组件的安装结果，并更改安装任务列表框内对应项的图标</summary>
    ''' <param name="result">组件的安装结果，必须为InstallResult.Result枚举的值之一</param>
    ''' <param name="item">安装任务列表框的对应项</param>
    Private Sub ReportProgress(ByVal result As Res.Result, ByVal item As ListViewItem)
        With ModuleMain.InstallResult
            If result = Res.Result.Success Then item.ImageKey = "success" Else item.ImageKey = "fail"
            Select Case item.Text
                Case "DAEMON Tools Lite" : .DAEMONToolsInstallResult = result
                Case "638补丁" : ._638PatchInstallResult = result
                    If result = Res.Result.Fail Then
                        ._640PatchInstallResult = Res.Result.Fail : ._641PatchInstallResult = Res.Result.Fail
                        If IsNothing(lvwTask.FindItemWithText("640补丁")) = False Then lvwTask.FindItemWithText("640补丁").ImageKey = "fail"
                        If IsNothing(lvwTask.FindItemWithText("641补丁")) = False Then lvwTask.FindItemWithText("641补丁").ImageKey = "fail"
                    End If
                Case "640补丁" : ._640PatchInstallResult = result
                    If result = Res.Result.Fail Then
                        ._641PatchInstallResult = Res.Result.Fail
                        If IsNothing(lvwTask.FindItemWithText("641补丁")) = False Then lvwTask.FindItemWithText("641补丁").ImageKey = "fail"
                    End If
                Case "641补丁" : ._641PatchInstallResult = result
                Case "4GB补丁" : ._4GBPatchInstallResult = result
                Case "免CD补丁" : .NoCDPatchInstallResult = result
                Case "繁体中文语言补丁", "简体中文语言补丁" : .LanguagePatchInstallResult = result
                Case "添加开始菜单项" : .AddDesktopIconResult = result
                Case "添加桌面图标" : .AddStartMenuItemResult = result
                Case Else
                    If item.Text.Contains("模拟城市4 豪华版") = True And result = Res.Result.Success Then
                        .SC4InstallResult = result
                    ElseIf item.Text.Contains("模拟城市4 豪华版") = True And result = Res.Result.Fail Then
                        For i As Integer = 0 To lvwTask.Items.Count - 1
                            lvwTask.Items(i).ImageKey = "fail"
                        Next
                        .SC4InstallResult = Res.Result.Fail
                        ._638PatchInstallResult = Res.Result.Fail : ._640PatchInstallResult = Res.Result.Fail : ._641PatchInstallResult = Res.Result.Fail
                        ._4GBPatchInstallResult = Res.Result.Fail : .NoCDPatchInstallResult = Res.Result.Fail
                        .SC4LauncherInstallResult = Res.Result.Fail : .LanguagePatchInstallResult = Res.Result.Fail
                        .AddDesktopIconResult = Res.Result.Fail : .AddStartMenuItemResult = Res.Result.Fail
                        bgwInstall.CancelAsync()
                    End If
            End Select
        End With
    End Sub

    Private Sub bgwInstall_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwInstall.DoWork
        '声明一些用于快速访问安装组件列表框项的变量
        Dim DAEMONItem As ListViewItem = lvwTask.FindItemWithText("DAEMON Tools Lite"), SC4Item As ListViewItem = lvwTask.FindItemWithText("模拟城市4 豪华版")
        Dim _638PatchItem As ListViewItem = lvwTask.FindItemWithText("638补丁"), _640PatchItem As ListViewItem = lvwTask.FindItemWithText("640补丁")
        Dim _641PatchItem As ListViewItem = lvwTask.FindItemWithText("641补丁"), _4GBPatchItem As ListViewItem = lvwTask.FindItemWithText("4GB补丁")
        Dim NoCDPatchItem As ListViewItem = lvwTask.FindItemWithText("免CD补丁"), SC4LauncherItem As ListViewItem = lvwTask.FindItemWithText("模拟城市4 启动器")
        Dim AddDesktopIconItem As ListViewItem = lvwTask.FindItemWithText("添加桌面图标"), AddStartMenuIem As ListViewItem = lvwTask.FindItemWithText("添加开始菜单项")
        With ModuleMain.InstallOptions
            If IsNothing(ModuleMain.InstalledModule) = True Then '判断是否已经安装了模拟城市4
                If .InstallDAEMONTools = True Then DAEMONItem.ImageKey = "installing" : ReportProgress(InstallDAEMONTools(.DAEMONInstallDir), DAEMONItem)
                If ModuleMain.InstallResult.DAEMONToolsInstallResult = Res.Result.Success And .SC4Type = Opt.SC4InstallType.ISO Then '安装指定版本的模拟城市4
                    SC4Item.ImageKey = "installing" : lblInstalling.Text = "正在安装 模拟城市4 镜像版："
                    ReportProgress(InstallSC4(.SC4InstallDir, .DAEMONInstallDir, Opt.SC4InstallType.ISO, prgInstall), SC4Item)
                ElseIf ModuleMain.InstallResult.DAEMONToolsInstallResult = Res.Result.Success And .SC4Type = Opt.SC4InstallType.ISO Then
                    ReportProgress(Res.Result.Fail, SC4Item) '如果DAEMON Tools Lite安装失败即将安装镜像版模拟城市4则终止安装
                ElseIf .SC4Type = Opt.SC4InstallType.NoInstall Then
                    SC4Item.ImageKey = "installing" : lblInstalling.Text = "正在安装 模拟城市4 硬盘版："
                    ReportProgress(InstallSC4(.SC4InstallDir, Nothing, Opt.SC4InstallType.NoInstall, prgInstall), SC4Item)
                End If
                If bgwInstall.CancellationPending = True Then Exit Sub '如果模拟城市4安装失败则停止安装后续组件
                If .SC4Type = Opt.SC4InstallType.NoInstall Then SetNoInstallSC4RegValue(.SC4InstallDir) '导入镜像版模拟城市4所添加的注册表项
                SetControlPanelProgramItemRegValue(.SC4InstallDir) '在控制面板的卸载或更改程序里添加模拟城市4 豪华版 自动安装程序项
                '安装指定的组件并将安装组件列表框里对应项的图标改为安装中图标
                If .Install638Patch = True Then : _638PatchItem.ImageKey = "installing" : lblInstalling.Text = "正在安装 638补丁："
                    ReportProgress(Install638Patch(.SC4InstallDir, False, prgInstall), _638PatchItem)
                End If
                prgInstall.Style = ProgressBarStyle.Marquee '将安装进度条的进度条类型改为循环滚动
                If .Install640Patch = True And ModuleMain.InstallResult._638PatchInstallResult = Res.Result.Success Then
                    _640PatchItem.ImageKey = "installing" : lblInstalling.Text = "正在安装 640补丁："
                    ReportProgress(Install640Patch(.SC4InstallDir, False, Nothing), _640PatchItem)
                End If
                If .Install641Patch = True And ModuleMain.InstallResult._640PatchInstallResult = Res.Result.Success Then
                    _641PatchItem.ImageKey = "installing" : lblInstalling.Text = "正在安装 641补丁："
                    ReportProgress(Install641Patch(.SC4InstallDir, False), _641PatchItem)
                End If
                If .InstallNoCDPatch = True Then NoCDPatchItem.ImageKey = "installing" : lblInstalling.Text = "正在安装 免CD补丁：" : ReportProgress(InstallNoCDPatch(.SC4InstallDir, False, Nothing), NoCDPatchItem)
                If .Install4GBPatch = True Then _4GBPatchItem.ImageKey = "installing" : lblInstalling.Text = "正在安装 4GB补丁：" : ReportProgress(Install4GBPatch(.SC4InstallDir, False, ModuleMain.InstallOptions), _4GBPatchItem)
                If .InstallSC4Launcher = True Then SC4LauncherItem.ImageKey = "installing" : lblInstalling.Text = "正在安装 模拟城市4 启动器：" : ReportProgress(InstallSC4Launcher(.SC4InstallDir, False), SC4LauncherItem)
                Select Case .LanguagePatch
                    Case Opt.Language.TraditionalChinese
                        lvwTask.FindItemWithText("繁体中文语言补丁").ImageKey = "installing" : lblInstalling.Text = "正在安装 繁体中文语言补丁："
                        ReportProgress(InstallLanguagePatch(.SC4InstallDir, Opt.Language.TraditionalChinese), lvwTask.FindItemWithText("繁体中文语言补丁"))
                    Case Opt.Language.SimplifiedChinese
                        lvwTask.FindItemWithText("简体中文语言补丁").ImageKey = "installing" : lblInstalling.Text = "正在安装 简体中文语言补丁："
                        ReportProgress(InstallLanguagePatch(.SC4InstallDir, Opt.Language.SimplifiedChinese), lvwTask.FindItemWithText("简体中文语言补丁"))
                    Case Opt.Language.English
                        InstallLanguagePatch(.SC4InstallDir, Opt.Language.English)
                End Select
                If .AddDesktopIcon = True Then AddDesktopIconItem.ImageKey = "installing" : ReportProgress(AddDestopIcon(.SC4InstallDir), AddDesktopIconItem)
                If .AddStartMenuItem = True Then AddStartMenuIem.ImageKey = "installing" : ReportProgress(AddStartMenuItems(.SC4InstallDir), AddStartMenuIem)
            Else
                '安装或卸载指定的组件并将安装组件列表框里对应项的图标改为安装中图标
                '声明3个用于存储638补丁或640补丁或641补丁是否更改的布尔值变量
                Dim Is638PatchChanged As Boolean = ModuleMain.InstalledModule.Is638PatchInstalled <> .Install638Patch
                Dim Is640PatchChanged As Boolean = ModuleMain.InstalledModule.Is640PatchInstalled <> .Install640Patch
                Dim Is641PatchChanged As Boolean = ModuleMain.InstalledModule.Is641PatchInstalled <> .Install641Patch
                If ModuleMain.InstalledModule.IsNoCDPatchInstalled <> .InstallNoCDPatch Then : NoCDPatchItem.ImageKey = "installing"
                    lblInstalling.Text = IIf(.InstallNoCDPatch, "正在安装 免CD补丁：", "正在卸载 免CD补丁：")
                    ReportProgress(InstallNoCDPatch(ModuleMain.InstalledModule.SC4InstallDir, Not .InstallNoCDPatch, IIf(.InstallNoCDPatch, Nothing, prgInstall)), NoCDPatchItem)
                End If
                If ((Is638PatchChanged Or Is640PatchChanged Or Is641PatchChanged) Or ModuleMain.InstalledModule.Is4GBPatchInstalled <> .Install4GBPatch) And .Install4GBPatch = False Then
                    _4GBPatchItem.ImageKey = "installing" : lblInstalling.Text = "正在卸载 4GB补丁："
                    ReportProgress(Install4GBPatch(ModuleMain.InstalledModule.SC4InstallDir, True, ModuleMain.InstallOptions), _4GBPatchItem)
                End If
                '如果要安装638、640或641补丁则按照安装638、安装640和安装641的顺序安装，如果要卸载638、640或641补丁则按照卸载641、卸载640和卸载638补丁的顺序卸载
                If (Is638PatchChanged And .Install638Patch = True) Or (Is640PatchChanged And .Install640Patch = True) Or (Is641PatchChanged And .Install641Patch = True) Then
                    If Is638PatchChanged = True Then : _638PatchItem.ImageKey = "installing" : lblInstalling.Text = "正在安装 638补丁："
                        ReportProgress(Install638Patch(ModuleMain.InstalledModule.SC4InstallDir, False, prgInstall), _638PatchItem)
                    End If
                    If Is640PatchChanged = True And ModuleMain.InstallResult._638PatchInstallResult = Res.Result.Success Then
                        _640PatchItem.ImageKey = "installing" : lblInstalling.Text = "正在安装 640补丁："
                        ReportProgress(Install640Patch(ModuleMain.InstalledModule.SC4InstallDir, False, Nothing), _640PatchItem)
                    End If
                    If Is641PatchChanged = True And ModuleMain.InstallResult._641PatchInstallResult = Res.Result.Success Then
                        _641PatchItem.ImageKey = "installing" : lblInstalling.Text = "正在安装 641补丁："
                        ReportProgress(Install641Patch(ModuleMain.InstalledModule.SC4InstallDir, False), _641PatchItem)
                    End If
                ElseIf (Is638PatchChanged And .Install638Patch = False) Or (Is640PatchChanged And .Install640Patch = False) Or (Is641PatchChanged And .Install641Patch = False) Then
                    If Is641PatchChanged = True Then : _641PatchItem.ImageKey = "installing" : lblInstalling.Text = "正在卸载 641补丁："
                        ReportProgress(Install641Patch(ModuleMain.InstalledModule.SC4InstallDir, True), _641PatchItem)
                    End If
                    If Is640PatchChanged = True And ModuleMain.InstallResult._641PatchInstallResult = Res.Result.Success Then
                        _640PatchItem.ImageKey = "installing" : lblInstalling.Text = "正在卸载 640补丁："
                        ReportProgress(Install640Patch(ModuleMain.InstalledModule.SC4InstallDir, True, prgInstall), _640PatchItem)
                    End If
                    If Is638PatchChanged = True And ModuleMain.InstallResult._640PatchInstallResult = Res.Result.Success Then
                        _638PatchItem.ImageKey = "installing" : lblInstalling.Text = "正在卸载 638补丁："
                        ReportProgress(Install638Patch(ModuleMain.InstalledModule.SC4InstallDir, True, prgInstall), _638PatchItem)
                    End If
                End If
                prgInstall.Style = ProgressBarStyle.Marquee '将安装进度条的进度条类型改为循环滚动
                If ((Is638PatchChanged Or Is640PatchChanged Or Is641PatchChanged) Or ModuleMain.InstalledModule.Is4GBPatchInstalled <> .Install4GBPatch) And .Install4GBPatch = True Then
                    _4GBPatchItem.ImageKey = "installing" : lblInstalling.Text = "正在安装 4GB补丁："
                    ReportProgress(Install4GBPatch(ModuleMain.InstalledModule.SC4InstallDir, False, ModuleMain.InstallOptions), _4GBPatchItem)
                End If
                If ModuleMain.InstalledModule.IsSC4LauncherInstalled <> .InstallSC4Launcher Then : SC4LauncherItem.ImageKey = "installing"
                    lblInstalling.Text = IIf(.InstallSC4Launcher, "正在安装 模拟城市4 启动器：", "正在卸载 模拟城市4 启动器：")
                    ReportProgress(InstallSC4Launcher(ModuleMain.InstalledModule.SC4InstallDir, Not .InstallSC4Launcher), SC4LauncherItem)
                End If
                If ModuleMain.InstalledModule.LanguagePatch <> .LanguagePatch Then
                    Select Case .LanguagePatch
                        Case Opt.Language.TraditionalChinese
                            lvwTask.FindItemWithText("繁体中文语言补丁").ImageKey = "installing" : lblInstalling.Text = "正在安装 繁体中文语言补丁："
                            ReportProgress(InstallLanguagePatch(ModuleMain.InstalledModule.SC4InstallDir, Opt.Language.TraditionalChinese), lvwTask.FindItemWithText("繁体中文语言补丁"))
                        Case Opt.Language.SimplifiedChinese
                            lvwTask.FindItemWithText("简体中文语言补丁").ImageKey = "installing" : lblInstalling.Text = "正在安装 简体中文语言补丁："
                            ReportProgress(InstallLanguagePatch(ModuleMain.InstalledModule.SC4InstallDir, Opt.Language.SimplifiedChinese), lvwTask.FindItemWithText("简体中文语言补丁"))
                        Case Opt.Language.English
                            InstallLanguagePatch(ModuleMain.InstalledModule.SC4InstallDir, Opt.Language.English)
                    End Select
                End If
            End If
            lblInstalling.Text = "" '清空当前安装组件文本
            Threading.Thread.Sleep(500) '挂起当前线程0.5秒以便让用户看到安装结果
        End With
    End Sub

    Private Sub bgwInstall_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwInstall.RunWorkerCompleted
        frmFinish.Show()
        Close()
    End Sub

    Private Sub tmrPic_Tick(sender As Object, e As EventArgs) Handles tmrPic.Tick
        Static i As Integer = 1 '声明一个用于存储图片序号的全局变量
        picSC4.Image = CType(My.Resources.ResourceManager.GetObject("SC4_" & i), Image) '将右侧的图片框的图片改为资源文件里名为SC4_i的图片
        If i = 20 Then i = 1 Else i += 1
    End Sub

    Private Sub frmInstalling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '声明一些用于快速访问安装组件列表框项的变量
        Dim DAEMONItem As ListViewItem = lvwTask.FindItemWithText("DAEMON Tools Lite"), SC4Item As ListViewItem = lvwTask.FindItemWithText("模拟城市4 豪华版")
        Dim _638PatchItem As ListViewItem = lvwTask.FindItemWithText("638补丁"), _640PatchItem As ListViewItem = lvwTask.FindItemWithText("640补丁")
        Dim _641PatchItem As ListViewItem = lvwTask.FindItemWithText("641补丁"), _4GBPatchItem As ListViewItem = lvwTask.FindItemWithText("4GB补丁")
        Dim NoCDPatchItem As ListViewItem = lvwTask.FindItemWithText("免CD补丁"), SC4LauncherItem As ListViewItem = lvwTask.FindItemWithText("模拟城市4 启动器")
        Dim LanguagePatchItem As ListViewItem = lvwTask.FindItemWithText("语言补丁")
        Dim AddDesktopIconItem As ListViewItem = lvwTask.FindItemWithText("添加桌面图标"), AddStartMenuIem As ListViewItem = lvwTask.FindItemWithText("添加开始菜单项")
        With ModuleMain.InstallOptions
            lvwTask.BeginUpdate()
            If IsNothing(ModuleMain.InstalledModule) = True Then '判断是否已经安装了模拟城市4
                '根据安装选项里所选择的模拟城市4版本来更改安装组件列表框里模拟城市4 豪华版项的文本
                If .SC4Type = Opt.SC4InstallType.ISO Then SC4Item.Text = "模拟城市4 豪华版 镜像版"
                If .SC4Type = Opt.SC4InstallType.NoInstall Then SC4Item.Text = "模拟城市4 豪华版 硬盘版"
                '删除安装选项里选择不安装的组件在安装组件列表框里的对应项
                If .InstallDAEMONTools = False Then DAEMONItem.Remove()
                If .Install638Patch = False Then _638PatchItem.Remove()
                If .Install640Patch = False Then _640PatchItem.Remove()
                If .Install641Patch = False Then _641PatchItem.Remove()
                If .Install4GBPatch = False Then _4GBPatchItem.Remove()
                If .InstallNoCDPatch = False Then NoCDPatchItem.Remove()
                If .InstallSC4Launcher = False Then SC4LauncherItem.Remove()
                If .AddDesktopIcon = False Then AddDesktopIconItem.Remove()
                If .AddStartMenuItem = False Then AddStartMenuIem.Remove()
                '根据安装选项里所选择的语言补丁来更改安装组件列表框里对应项的文本
                Select Case .LanguagePatch
                    Case Opt.Language.TraditionalChinese : LanguagePatchItem.Text = "繁体中文语言补丁"
                    Case Opt.Language.SimplifiedChinese : LanguagePatchItem.Text = "简体中文语言补丁"
                    Case Opt.Language.English : LanguagePatchItem.Remove()
                End Select
            Else
                lblTitle.Text = "正在更改组件"
                DAEMONItem.Remove() : SC4Item.Remove() : AddDesktopIconItem.Remove() : AddStartMenuIem.Remove() '删除不适用的项
                '删除安装选项里没有更改的组件在安装组件列表框里对应项
                '声明3个用于存储638补丁或640补丁或641补丁是否更改的布尔值变量
                Dim Is638PatchChanged As Boolean = ModuleMain.InstalledModule.Is638PatchInstalled <> .Install638Patch
                Dim Is640PatchChanged As Boolean = ModuleMain.InstalledModule.Is640PatchInstalled <> .Install640Patch
                Dim Is641PatchChanged As Boolean = ModuleMain.InstalledModule.Is641PatchInstalled <> .Install641Patch
                If Is638PatchChanged = False Then _638PatchItem.Remove()
                If Is640PatchChanged = False Then _640PatchItem.Remove()
                If Is641PatchChanged = False Then _641PatchItem.Remove()
                If Is638PatchChanged = False And Is640PatchChanged = False And Is641PatchChanged = False And
                    ModuleMain.InstalledModule.Is4GBPatchInstalled = .Install4GBPatch Then _4GBPatchItem.Remove()
                If ModuleMain.InstalledModule.IsNoCDPatchInstalled = .InstallNoCDPatch Then NoCDPatchItem.Remove()
                If ModuleMain.InstalledModule.IsSC4LauncherInstalled = .InstallSC4Launcher Then SC4LauncherItem.Remove()
                If ModuleMain.InstalledModule.LanguagePatch = .LanguagePatch Then
                    LanguagePatchItem.Remove()
                Else
                    '根据安装选项里所选择的语言补丁来更改安装组件列表框里对应项的文本
                    Select Case .LanguagePatch
                        Case Opt.Language.TraditionalChinese : LanguagePatchItem.Text = "繁体中文语言补丁"
                        Case Opt.Language.SimplifiedChinese : LanguagePatchItem.Text = "简体中文语言补丁"
                        Case Opt.Language.English : LanguagePatchItem.Remove()
                    End Select
                End If
            End If
            lvwTask.EndUpdate()
        End With
        Control.CheckForIllegalCrossThreadCalls = False '设置不捕捉对错误线程（跨线程）的调用的异常
        Dim ControlBoxHandle As Integer = GetSystemMenu(Me.Handle, 0) '标题栏右上角的菜单的句柄
        Dim ControlBoxCount As Integer = GetMenuItemCount(ControlBoxHandle) '标题栏右上角的菜单项的数量
        RemoveMenu(ControlBoxHandle, ControlBoxCount - 1, MF_DISABLED Or MF_BYPOSITION) '禁用标题栏右上角的X按钮
        DrawMenuBar(Me.Handle) '立即重绘标题栏的右上角菜单
        bgwInstall.RunWorkerAsync() '开始异步安装
        tmrPic_Tick(tmrPic, New EventArgs) '立即更改右侧图片
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

End Class