Imports InstallResult = SC4AutoInstaller.InstallResults.InstallResult
Imports ChangeOption = SC4AutoInstaller.ChangeOptions.ChangeOption

Public Class frmInstalling

    ''' <summary>报告某个组件的安装结果，并更改安装任务列表框内对应项的图标</summary>
    ''' <param name="InstallResult">组件的安装结果，必须为InstallResults.InstallResult枚举的值之一</param>
    ''' <param name="ItemName">安装任务列表框的对应项名</param>
    Private Sub ReportInstallResult(ByVal InstallResult As InstallResult, ByVal ItemName As String)
        With ModuleDeclare.InstallResults
            lvwTask.FindItemWithText(ItemName).ImageKey = If(InstallResult = InstallResult.Success, "Success", "Fail")
            Select Case ItemName
                Case "DAEMON Tools Lite" : .DAEMONToolsResult = InstallResult
                Case "模拟城市4 豪华版 镜像版", "模拟城市4 豪华版 硬盘版"
                    If InstallResult = InstallResult.Success Then : .SC4InstallResult = InstallResult
                    Else '如果模拟城市4安装失败则报告所有组件安装失败并取消安装
                        For Each i As ListViewItem In lvwTask.Items '将安装任务列表框的除DAEMON Tools Lite以外的所有项图标更改为失败图标
                            If i.Text <> "DAEMON Tools Lite" Then i.ImageKey = "Fail"
                        Next
                        For Each i As Reflection.FieldInfo In ModuleDeclare.InstallResults.GetType.GetFields '通过反射将ModuleDeclare.InstallResults的除DAEMONToolsResult以外的所有字段值设为InstallResult.Fail
                            If i.Name <> "DAEMONToolsResult" Then i.SetValue(ModuleDeclare.InstallResults, InstallResult.Fail)
                        Next
                        bgwInstall.CancelAsync() '取消异步安装
                    End If
                Case "638补丁" : ._638PatchResult = InstallResult
                    If InstallResult = InstallResult.Fail Then '如果638补丁安装失败则报告640和641补丁安装失败
                        ._640PatchResult = InstallResult.Fail : ._641PatchResult = InstallResult.Fail
                        If lvwTask.FindItemWithText("640补丁") IsNot Nothing Then lvwTask.FindItemWithText("640补丁").ImageKey = "Fail"
                        If lvwTask.FindItemWithText("641补丁") IsNot Nothing Then lvwTask.FindItemWithText("641补丁").ImageKey = "Fail"
                    End If
                Case "640补丁" : ._640PatchResult = InstallResult
                    If InstallResult = InstallResult.Fail Then '如果640补丁安装失败则报告641补丁安装失败
                        ._641PatchResult = InstallResult.Fail
                        If lvwTask.FindItemWithText("641补丁") IsNot Nothing Then lvwTask.FindItemWithText("641补丁").ImageKey = "Fail"
                    End If
                Case "641补丁" : ._641PatchResult = InstallResult
                Case "4GB补丁" : ._4GBPatchResult = InstallResult
                Case "免CD补丁" : .NoCDPatchResult = InstallResult
                Case "模拟城市4 启动器" : .SC4LauncherResult = InstallResult
                Case "繁体中文语言补丁", "简体中文语言补丁", "英语语言补丁" : .LanguagePatchResult = InstallResult
            End Select
        End With
    End Sub

    Private Sub bgwInstall_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwInstall.DoWork
        '声明一些用于快速访问安装组件列表框项的变量
        Dim DAEMONToolsItem As ListViewItem = lvwTask.FindItemWithText("DAEMON Tools Lite"), SC4Item As ListViewItem = lvwTask.FindItemWithText("模拟城市4 豪华版")
        Dim _638PatchItem As ListViewItem = lvwTask.FindItemWithText("638补丁"), _640PatchItem As ListViewItem = lvwTask.FindItemWithText("640补丁")
        Dim _641PatchItem As ListViewItem = lvwTask.FindItemWithText("641补丁"), _4GBPatchItem As ListViewItem = lvwTask.FindItemWithText("4GB补丁")
        Dim NoCDPatchItem As ListViewItem = lvwTask.FindItemWithText("免CD补丁"), SC4LauncherItem As ListViewItem = lvwTask.FindItemWithText("模拟城市4 启动器")
        If ModuleDeclare.InstalledModules Is Nothing Then '判断是否已经安装了模拟城市4
            With ModuleDeclare.InstallOptions
                If .IsInstallDAEMONTools Then
                    bgwInstall.ReportProgress(0)
                    DAEMONToolsItem.ImageKey = "Installing"
                    ReportInstallResult(InstallDAEMONTools(.DAEMONToolsInstallDir), DAEMONToolsItem.Text)
                End If
                If ModuleDeclare.InstallResults.DAEMONToolsResult = InstallResult.Success AndAlso .SC4InstallType = InstallOptions.SC4Type.CD Then '安装指定版本的模拟城市4
                    SC4Item.ImageKey = "Installing" : lblInstalling.Text = "正在安装 模拟城市4 镜像版"
                    Dim DAEMONToolsInstallDir As String = If(.IsInstallDAEMONTools, .DAEMONToolsInstallDir, My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Disc Soft\DAEMON Tools Lite", "Path", Nothing))
                    ReportInstallResult(InstallSC4(.SC4InstallDir, DAEMONToolsInstallDir, InstallOptions.SC4Type.CD), SC4Item.Text)
                ElseIf ModuleDeclare.InstallResults.DAEMONToolsResult = InstallResult.Fail AndAlso .SC4InstallType = InstallOptions.SC4Type.CD Then
                    ReportInstallResult(InstallResult.Fail, SC4Item.Text) '如果DAEMON Tools Lite安装失败则不安装镜像版模拟城市4并显示为安装失败
                ElseIf .SC4InstallType = InstallOptions.SC4Type.NoInstall Then
                    SC4Item.ImageKey = "Installing" : lblInstalling.Text = "正在安装 模拟城市4 硬盘版"
                    ReportInstallResult(InstallSC4(.SC4InstallDir, Nothing, InstallOptions.SC4Type.NoInstall), SC4Item.Text)
                    If ModuleDeclare.InstallResults.SC4InstallResult = InstallResult.Success Then SetNoInstallSC4RegValue(.SC4InstallDir) '如果硬盘版模拟城市4安装成功则导入镜像版模拟城市4安装程序所添加或更改的注册表项和值
                End If
                If bgwInstall.CancellationPending Then Exit Sub '如果模拟城市4安装失败则停止安装
                SetControlPanelProgramItemRegValue(.SC4InstallDir) '在控制面板的卸载或更改程序里添加模拟城市4 豪华版 自动安装程序项
                '安装指定的组件并将安装组件列表框里对应项的图标改为安装中图标
                If .IsInstall638Patch Then : _638PatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在安装 638补丁"
                    ReportInstallResult(Change638Patch(.SC4InstallDir, False), _638PatchItem.Text)
                    '询问用户是否使用GOG版本的Graphics Rules.sgr文件
                    If .IsQuickInstall = False AndAlso MessageBox.Show("是否使用GOG版本的Graphics Rules.sgr文件？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        IO.File.SetAttributes(.SC4InstallDir & "\Graphics Rules.sgr", IO.FileAttributes.Normal) '将InstallDir\Graphics Rules.sgr文件设置为正常属性
                        Do Until IsFileUsing(.SC4InstallDir & "\Graphics Rules.sgr") = False : Loop
                        My.Computer.FileSystem.CopyFile("Data\Patch\Graphics Rules GOG.sgr", .SC4InstallDir & "\Graphics Rules.sgr", True)
                    End If
                End If
                If .IsInstall640Patch AndAlso ModuleDeclare.InstallResults._638PatchResult = InstallResult.Success Then
                    '询问用户是否使用638版本的SimCity_1.dat文件
                    Dim IsUse638File As DialogResult
                    If .IsQuickInstall = False AndAlso .IsInstall641Patch = False Then
                        IsUse638File = MessageBox.Show("是否使用638版本的SimCity_1.dat文件？" & vbCrLf & "使用638版本的SimCity_1.dat文件可以避免可能出现的细节缺失显示问题", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        '如果用户同意使用638版本的SimCity_1.dat文件则先备份638版本的SimCity_1.dat文件
                        If IsUse638File = DialogResult.Yes Then My.Computer.FileSystem.CopyFile(.SC4InstallDir & "\SimCity_1.dat", .SC4InstallDir & "\SimCity_1 638.dat")
                    End If
                    _640PatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在安装 640补丁"
                    ReportInstallResult(Change640Patch(.SC4InstallDir, False), _640PatchItem.Text)
                    '如果用户同意使用638版本的SimCity_1.dat文件则将之前备份的638版本的SimCity_1.dat文件替换原文件
                    If IsUse638File = DialogResult.Yes Then My.Computer.FileSystem.MoveFile(.SC4InstallDir & "\SimCity_1 638.dat", .SC4InstallDir & "\SimCity_1.dat", True)
                End If
                If .IsInstall641Patch AndAlso ModuleDeclare.InstallResults._640PatchResult = InstallResult.Success Then
                    _641PatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在安装 641补丁"
                    ReportInstallResult(Change641Patch(.SC4InstallDir, False), _641PatchItem.Text)
                End If
                If .IsInstallNoCDPatch Then
                    NoCDPatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在安装 免CD补丁"
                    ReportInstallResult(ChangeNoCDPatch(.SC4InstallDir, False), NoCDPatchItem.Text)
                End If
                If .IsInstall4GBPatch Then
                    _4GBPatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在安装 4GB补丁"
                    ReportInstallResult(Change4GBPatch(.SC4InstallDir, False, Nothing), _4GBPatchItem.Text)
                End If
                If .IsInstallSC4Launcher Then
                    SC4LauncherItem.ImageKey = "Installing" : lblInstalling.Text = "正在安装 模拟城市4 启动器"
                    ReportInstallResult(ChangeSC4Launcher(.SC4InstallDir, False), SC4LauncherItem.Text)
                End If
                Select Case .LanguagePatchOption
                    Case SC4Language.TraditionalChinese
                        lvwTask.FindItemWithText("繁体中文语言补丁").ImageKey = "Installing" : lblInstalling.Text = "正在安装 繁体中文语言补丁"
                        ReportInstallResult(ChangeLanguage(.SC4InstallDir, SC4Language.TraditionalChinese), "繁体中文语言补丁")
                    Case SC4Language.SimplifiedChinese
                        lvwTask.FindItemWithText("简体中文语言补丁").ImageKey = "Installing" : lblInstalling.Text = "正在安装 简体中文语言补丁"
                        ReportInstallResult(ChangeLanguage(.SC4InstallDir, SC4Language.SimplifiedChinese), "简体中文语言补丁")
                    Case SC4Language.English
                        lvwTask.FindItemWithText("英语语言补丁").ImageKey = "Installing" : lblInstalling.Text = "正在安装 英语语言补丁"
                        ReportInstallResult(ChangeLanguage(.SC4InstallDir, SC4Language.English), "英语语言补丁")
                End Select
                If .IsAddDesktopIcon Then lblInstalling.Text = "正在添加桌面图标" : AddDestopIcon(.SC4InstallDir, .IsInstallSC4Launcher)
                If .IsAddStartMenuItem Then lblInstalling.Text = "正在添加开始菜单项" : AddStartMenuItems(.SC4InstallDir, .IsInstallSC4Launcher)
            End With
        Else '已安装模拟城市4
            '声明一个用于存储已安装的模拟城市4安装目录的字符串变量
            Dim SC4InstallDir As String = ModuleDeclare.InstalledModules.SC4InstallDir
            With ModuleDeclare.ChangeOptions
                '安装或卸载指定的组件并将安装组件列表框里对应项的图标改为安装中图标
                If ._4GBPatchOption <> ChangeOption.Unchanged AndAlso ._4GBPatchOption = ChangeOption.Uninstall Then
                    _4GBPatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在卸载 4GB补丁"
                    ReportInstallResult(Change4GBPatch(SC4InstallDir, True, ModuleDeclare.ChangeOptions), _4GBPatchItem.Text)
                End If
                If .NoCDPatchOption <> ChangeOption.Unchanged AndAlso .NoCDPatchOption = ChangeOption.Uninstall Then
                    NoCDPatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在卸载 免CD补丁"
                    ReportInstallResult(ChangeNoCDPatch(SC4InstallDir, True), NoCDPatchItem.Text)
                End If
                '如果要安装638、640或641补丁则按照安装638、640和641的顺序安装，如果要卸载638、640或641补丁则按照卸载641、640和638补丁的顺序卸载
                If ._638PatchOption = ChangeOption.Install OrElse ._640PatchOption = ChangeOption.Install OrElse ._641PatchOption = ChangeOption.Install Then
                    If ._638PatchOption <> ChangeOption.Unchanged Then
                        _638PatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在安装 638补丁"
                        ReportInstallResult(Change638Patch(SC4InstallDir, False), _638PatchItem.Text)
                        '询问用户是否使用GOG版本的Graphics Rules.sgr文件
                        If MessageBox.Show("是否使用GOG版本的Graphics Rules.sgr文件？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            IO.File.SetAttributes(SC4InstallDir & "\Graphics Rules.sgr", IO.FileAttributes.Normal) '将InstallDir\Graphics Rules.sgr文件设置为正常属性
                            Do Until IsFileUsing(SC4InstallDir & "\Graphics Rules.sgr") = False : Loop
                            My.Computer.FileSystem.CopyFile("Data\Patch\Graphics Rules GOG.sgr", SC4InstallDir & "\Graphics Rules.sgr", True)
                        End If
                    End If
                    If ._640PatchOption <> ChangeOption.Unchanged AndAlso ModuleDeclare.InstallResults._638PatchResult = InstallResult.Success Then
                        '询问用户是否使用638版本的SimCity_1.dat文件
                        Dim IsUse638File As DialogResult
                        If ._641PatchOption <> ChangeOption.Install Then
                            IsUse638File = MessageBox.Show("是否使用638版本的SimCity_1.dat文件？" & vbCrLf & "使用638版本的SimCity_1.dat文件可以避免可能出现的细节缺失显示问题", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            '如果用户同意使用638版本的SimCity_1.dat文件则先备份638版本的SimCity_1.dat文件
                            If IsUse638File = DialogResult.Yes Then My.Computer.FileSystem.CopyFile(SC4InstallDir & "\SimCity_1.dat", SC4InstallDir & "\SimCity_1 638.dat")
                        End If
                        _640PatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在安装 640补丁"
                        ReportInstallResult(Change640Patch(SC4InstallDir, False), _640PatchItem.Text)
                        '如果用户同意使用638版本的SimCity_1.dat文件则将之前备份的638版本的SimCity_1.dat文件替换原文件
                        If IsUse638File = DialogResult.Yes Then My.Computer.FileSystem.MoveFile(SC4InstallDir & "\SimCity_1 638.dat", SC4InstallDir & "\SimCity_1.dat", True)
                    End If
                    If ._641PatchOption <> ChangeOption.Unchanged AndAlso ModuleDeclare.InstallResults._640PatchResult = InstallResult.Success Then
                        _641PatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在安装 641补丁"
                        ReportInstallResult(Change641Patch(SC4InstallDir, False), _641PatchItem.Text)
                    End If
                ElseIf ._638PatchOption = ChangeOption.Uninstall OrElse ._640PatchOption = ChangeOption.Uninstall OrElse ._641PatchOption = ChangeOption.Uninstall Then
                    If ._641PatchOption <> ChangeOption.Unchanged Then
                        '询问用户是否使用638版本的SimCity_1.dat文件
                        Dim IsUse638File As DialogResult = MessageBox.Show("是否使用638版本的SimCity_1.dat文件？" & vbCrLf & "使用638版本的SimCity_1.dat文件可以避免可能出现的细节缺失显示问题", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        '如果用户同意使用638版本的SimCity_1.dat文件则先备份638版本的SimCity_1.dat文件
                        If IsUse638File = DialogResult.Yes Then My.Computer.FileSystem.CopyFile(SC4InstallDir & "\SimCity_1.dat", SC4InstallDir & "\SimCity_1 638.dat")
                        _641PatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在卸载 641补丁"
                        ReportInstallResult(Change641Patch(SC4InstallDir, True), _641PatchItem.Text)
                        '如果用户同意使用638版本的SimCity_1.dat文件则将之前备份的638版本的SimCity_1.dat文件替换原文件
                        If IsUse638File = DialogResult.Yes Then My.Computer.FileSystem.MoveFile(SC4InstallDir & "\SimCity_1 638.dat", SC4InstallDir & "\SimCity_1.dat", True)
                    End If
                    If ._640PatchOption <> ChangeOption.Unchanged AndAlso ModuleDeclare.InstallResults._641PatchResult = InstallResult.Success Then
                        _640PatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在卸载 640补丁"
                        ReportInstallResult(Change640Patch(SC4InstallDir, True), _640PatchItem.Text)
                        '如果不卸载638补丁则询问用户是否使用GOG版本的Graphics Rules.sgr文件
                        If ._638PatchOption <> ChangeOption.Uninstall AndAlso MessageBox.Show("是否使用GOG版本的Graphics Rules.sgr文件？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            IO.File.SetAttributes(SC4InstallDir & "\Graphics Rules.sgr", IO.FileAttributes.Normal) '将InstallDir\Graphics Rules.sgr文件设置为正常属性
                            Do Until IsFileUsing(SC4InstallDir & "\Graphics Rules.sgr") = False : Loop
                            My.Computer.FileSystem.CopyFile("Data\Patch\Graphics Rules GOG.sgr", SC4InstallDir & "\Graphics Rules.sgr", True)
                        End If
                    End If
                    If ._638PatchOption <> ChangeOption.Unchanged AndAlso ModuleDeclare.InstallResults._640PatchResult = InstallResult.Success Then
                        _638PatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在卸载 638补丁"
                        ReportInstallResult(Change638Patch(SC4InstallDir, True), _638PatchItem.Text)
                    End If
                End If
                If .NoCDPatchOption <> ChangeOption.Unchanged AndAlso .NoCDPatchOption = ChangeOption.Install Then
                    NoCDPatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在安装 免CD补丁"
                    ReportInstallResult(ChangeNoCDPatch(SC4InstallDir, False), NoCDPatchItem.Text)
                End If
                If ._4GBPatchOption <> ChangeOption.Unchanged AndAlso ._4GBPatchOption = ChangeOption.Install Then
                    _4GBPatchItem.ImageKey = "Installing" : lblInstalling.Text = "正在安装 4GB补丁"
                    ReportInstallResult(Change4GBPatch(SC4InstallDir, False, ModuleDeclare.ChangeOptions), _4GBPatchItem.Text)
                End If
                If .SC4LauncherOption <> ChangeOption.Unchanged Then : SC4LauncherItem.ImageKey = "Installing"
                    lblInstalling.Text = If(.SC4LauncherOption = ChangeOption.Install, "正在安装 模拟城市4 启动器", "正在卸载 模拟城市4 启动器")
                    ReportInstallResult(ChangeSC4Launcher(SC4InstallDir, .SC4LauncherOption = ChangeOption.Uninstall), SC4LauncherItem.Text)
                End If
                If .LanguagePatchOption <> ModuleDeclare.InstalledModules.LanguagePatchOption Then
                    Select Case .LanguagePatchOption
                        Case SC4Language.TraditionalChinese
                            lvwTask.FindItemWithText("繁体中文语言补丁").ImageKey = "Installing" : lblInstalling.Text = "正在安装 繁体中文语言补丁"
                            ReportInstallResult(ChangeLanguage(SC4InstallDir, SC4Language.TraditionalChinese), "繁体中文语言补丁")
                        Case SC4Language.SimplifiedChinese
                            lvwTask.FindItemWithText("简体中文语言补丁").ImageKey = "Installing" : lblInstalling.Text = "正在安装 简体中文语言补丁"
                            ReportInstallResult(ChangeLanguage(SC4InstallDir, SC4Language.SimplifiedChinese), "简体中文语言补丁")
                        Case SC4Language.English
                            lvwTask.FindItemWithText("英语语言补丁").ImageKey = "Installing" : lblInstalling.Text = "正在安装 英语语言补丁"
                            ReportInstallResult(ChangeLanguage(SC4InstallDir, SC4Language.English), "英语语言补丁")
                    End Select
                End If
            End With
        End If
        lblInstalling.Text = "" ： Threading.Thread.Sleep(500) '清空当前安装组件文本并挂起当前线程0.5秒以便让用户看到安装结果
    End Sub

    Private Sub bgwInstall_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwInstall.RunWorkerCompleted
        frmInstallFinish.Show()
        Dispose() '直接释放窗口以避免触发FormClosing事件
    End Sub

    Private Sub tmrChangePicture_Tick(sender As Object, e As EventArgs) Handles tmrChangePicture.Tick
        Static i As Integer = 1 '声明一个用于存储图片序号的全局变量
        picSC4.Image = CType(My.Resources.ResourceManager.GetObject("SC4_" & i), Image) '将右侧的图片框的图片改为资源文件里名为SC4_i的图片
        If i = 20 Then i = 1 Else i += 1
    End Sub

    Private Sub frmInstalling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '声明一些用于快速访问安装组件列表框项的变量
        Dim DAEMONToolsItem As ListViewItem = lvwTask.FindItemWithText("DAEMON Tools Lite"), SC4Item As ListViewItem = lvwTask.FindItemWithText("模拟城市4 豪华版")
        Dim _638PatchItem As ListViewItem = lvwTask.FindItemWithText("638补丁"), _640PatchItem As ListViewItem = lvwTask.FindItemWithText("640补丁")
        Dim _641PatchItem As ListViewItem = lvwTask.FindItemWithText("641补丁"), _4GBPatchItem As ListViewItem = lvwTask.FindItemWithText("4GB补丁")
        Dim NoCDPatchItem As ListViewItem = lvwTask.FindItemWithText("免CD补丁"), SC4LauncherItem As ListViewItem = lvwTask.FindItemWithText("模拟城市4 启动器")
        Dim LanguageItem As ListViewItem = lvwTask.FindItemWithText("语言补丁")
        lvwTask.BeginUpdate()
        If ModuleDeclare.InstalledModules Is Nothing Then '判断是否已经安装了模拟城市4
            With ModuleDeclare.InstallOptions
                '根据安装选项里所选择的模拟城市4版本来更改安装组件列表框里模拟城市4 豪华版项的文本
                If .SC4InstallType = InstallOptions.SC4Type.CD Then SC4Item.Text = "模拟城市4 豪华版 镜像版"
                If .SC4InstallType = InstallOptions.SC4Type.NoInstall Then SC4Item.Text = "模拟城市4 豪华版 硬盘版"
                '删除安装选项里选择不安装的组件在安装组件列表框里的对应项
                If .IsInstallDAEMONTools = False Then DAEMONToolsItem.Remove()
                If .IsInstall638Patch = False Then _638PatchItem.Remove()
                If .IsInstall640Patch = False Then _640PatchItem.Remove()
                If .IsInstall641Patch = False Then _641PatchItem.Remove()
                If .IsInstall4GBPatch = False Then _4GBPatchItem.Remove()
                If .IsInstallNoCDPatch = False Then NoCDPatchItem.Remove()
                If .IsInstallSC4Launcher = False Then SC4LauncherItem.Remove()
            End With
        Else '已安装模拟城市4
            lblTitle.Text = "正在更改组件"
            DAEMONToolsItem.Remove() : SC4Item.Remove() '删除不适用的项
            With ModuleDeclare.ChangeOptions
                '删除安装选项里未更改的组件在安装组件列表框里对应项
                If ._638PatchOption = ChangeOption.Unchanged Then _638PatchItem.Remove()
                If ._640PatchOption = ChangeOption.Unchanged Then _640PatchItem.Remove()
                If ._641PatchOption = ChangeOption.Unchanged Then _641PatchItem.Remove()
                If ._4GBPatchOption = ChangeOption.Unchanged Then _4GBPatchItem.Remove()
                If .NoCDPatchOption = ChangeOption.Unchanged Then NoCDPatchItem.Remove()
                If .SC4LauncherOption = ChangeOption.Unchanged Then SC4LauncherItem.Remove()
                If .LanguagePatchOption = ModuleDeclare.InstalledModules.LanguagePatchOption Then LanguageItem.Remove()
            End With
        End If
        '根据安装选项里所选择的语言补丁来更改安装组件列表框里对应项的文本
        Select Case If(ModuleDeclare.InstalledModules Is Nothing, ModuleDeclare.InstallOptions.LanguagePatchOption, ModuleDeclare.ChangeOptions.LanguagePatchOption)
            Case SC4Language.TraditionalChinese : LanguageItem.Text = "繁体中文语言补丁"
            Case SC4Language.SimplifiedChinese : LanguageItem.Text = "简体中文语言补丁"
            Case SC4Language.English : LanguageItem.Text = "英语语言补丁"
        End Select
        lvwTask.EndUpdate()
        Control.CheckForIllegalCrossThreadCalls = False '设置不捕捉对错误线程（跨线程）调用的异常
        '禁用标题栏上的关闭按钮
        Dim ControlBoxHandle As Integer = GetSystemMenu(Me.Handle, 0)
        Dim ControlBoxCount As Integer = GetMenuItemCount(ControlBoxHandle)
        RemoveMenu(ControlBoxHandle, ControlBoxCount - 1, MF_DISABLED Or MF_BYPOSITION)
        DrawMenuBar(Me.Handle)
        bgwInstall.RunWorkerAsync() '开始异步安装
        tmrChangePicture_Tick(tmrChangePicture, New EventArgs) '立即更改右侧图片
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

End Class