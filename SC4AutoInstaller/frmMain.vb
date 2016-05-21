Public Class frmMain

#Region "检查并提示更新"
    Private Sub bgwCheckUpdate_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwCheckUpdate.DoWork
        Try '下载更新信息
            If My.Computer.Network.IsAvailable Then '判断是否已连网
                Dim UpdateInfoXML As New Xml.XmlDocument '声明一个用于暂时存储更新信息的XmlDocument类实例
                UpdateInfoXML.Load("http://n0099.cf/updateinfo.xml") '下载更新信息
                e.Result = UpdateInfoXML.GetElementsByTagName("SC4AutoInstaller")(0) '返回已下载更新信息的XmlDocument类实例
WebError:   Else : e.Result = "WebError" '如果无法连接到更新服务器则弹出提示框并返回WebError字符串
            End If
        Catch : GoTo WebError '如果下载更新信息途中发生异常则跳转至WebError行
        End Try
    End Sub

    Private Sub bgwCheckUpdate_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwCheckUpdate.RunWorkerCompleted
        Try '检查是否有新版本可用
            If e.Result Is "WebError" Then Exit Sub '如果下载更新信息失败则停止检查更新
            Dim AutoInstallerNode As Xml.XmlNode = CType(e.Result, Xml.XmlNode)
            Dim LatestVersion As String = AutoInstallerNode("LatestVersion").InnerText
            With My.Application
                If (LatestVersion.Split(".")(0) > .Info.Version.Major) OrElse (LatestVersion.Split(".")(1) > .Info.Version.Minor) OrElse (LatestVersion.Split(".")(2) > .Info.Version.Revision) Then '判断最新版本号是否大于当前版本号
                    MessageBox.Show("检测到有新版本可用，请到n0099.cf上下载最新版本" & vbCrLf & "当前版本：" & .Info.Version.Major & "." & .Info.Version.Minor & "." & .Info.Version.Revision & vbCrLf &
                                    "更新说明：" & AutoInstallerNode("UpdateDetails").InnerText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) '提示用户有更新可用
                End If
            End With
        Catch
        End Try
    End Sub
#End Region

    Private Sub bgwVerifySC4Version_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwVerifySC4Version.DoWork
        With ModuleDeclare.InstalledModules
            Dim MD5CSP As New Security.Cryptography.MD5CryptoServiceProvider, FilesMD5(4) As String
            Dim FilesName() As String = { .SC4InstallDir & "\Apps\SimCity 4.exe",
                                          .SC4InstallDir & "\SimCity_1.dat", .SC4InstallDir & "\SimCity_2.dat",
                                          .SC4InstallDir & "\SimCity_3.dat", .SC4InstallDir & "\SimCity_4.dat"}
            '将FilesStream数组里各文件的MD5值存储到FilesMD5字符串数组里
            For i As Integer = 0 To 4
                Using FileStram As New IO.FileStream(FilesName(i), IO.FileMode.Open)
                    FilesMD5(i) = BitConverter.ToString(MD5CSP.ComputeHash(FileStram)).Replace("-", "")
                End Using
            Next
#Region "判断FilesStream数组里各文件的MD5值以确定已安装的模拟城市4的版本"
            Select Case FilesMD5(0) 'Apps\SimCity 4.exe
                Case "9ACB71D6D2302158CA614B21A9B187E4", "2F2BD7D9A76E85320A26D7BD7530DCAE" '638/638 4GB
                    If FilesMD5(1) = "A9E238946A8C8C479DD368EC4581B77A" AndAlso FilesMD5(2) = "2CFD520899786AEF47C728B123EBCF05" AndAlso
                        FilesMD5(3) = "7FE6E6678FBBA581092473C5F4C35331" AndAlso FilesMD5(4) = "CB2C26A9C4BC9B8E53709380B64B805C" Then .Is638PatchInstalled = True
                Case "D4796905AAFF2B2DE44C2B59D103F5EA", "1C18B7DC760EDADD2C2EFAF33F60F150" '640/640 4GB
                    .Is638PatchInstalled = True : .Is640PatchInstalled = True
                Case "53D2AE4FA9114B88AD91ECF32A7F16A4", "1414E70EB5CE22DB37D22CB99439D012" '641/641 4GB
                    .Is638PatchInstalled = True : .Is640PatchInstalled = True : .Is641PatchInstalled = True
                Case "427BE3767B1B20866F42D6197EA67AF0", "78202C3EF76988BD2BF05F8D223BE7A3" '610/610 4GB
                    If FilesMD5(1) = "C05406B02449540328DBB4B741E0A81D" AndAlso FilesMD5(2) = "E2976161D7EC772893D273FF753D08F6" AndAlso
                        FilesMD5(3) = "3E660755D70543D2222BD46B5A6F22C4" AndAlso FilesMD5(4) = "6DB4F1F9F1A1EC45B22E35827073FBA2" Then
                        .Is638PatchInstalled = False : .Is640PatchInstalled = False : .Is641PatchInstalled = False
                    End If
                Case "B57B5B03C4854C194CE8BEBD173F3483", "AADC5464919FBDC0F8E315FA51582126" 'NoCD/NoCD 4GB
                    .IsNoCDPatchInstalled = True : .Is638PatchInstalled = False : .Is640PatchInstalled = False : .Is641PatchInstalled = False
            End Select
            Select Case FilesMD5(0) 'Apps\SimCity 4.exe
                Case "2F2BD7D9A76E85320A26D7BD7530DCAE", "1C18B7DC760EDADD2C2EFAF33F60F150", "1414E70EB5CE22DB37D22CB99439D012",
                     "78202C3EF76988BD2BF05F8D223BE7A3", "AADC5464919FBDC0F8E315FA51582126" '638/640/641/610/NoCD 4GB
                    .Is4GBPatchInstalled = True
            End Select
#End Region
            If My.Computer.FileSystem.FileExists(.SC4InstallDir & "\SC4Launcher.exe") Then .IsSC4LauncherInstalled = True '判断是否已经安装模拟城市4 启动器
        End With
    End Sub

    Private Sub bgwVerifySC4Version_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwVerifySC4Version.RunWorkerCompleted
        btnChangeModule.Enabled = True : btnUninstall.Enabled = True : Cursor = Cursors.Default '使更改和卸载按钮可用并将指针图标恢复正常
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'If e.CloseReason <> CloseReason.ApplicationExitCall AndAlso 
        If MessageBox.Show("确定要退出安装程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then e.Cancel = True
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        bgwCheckUpdate.RunWorkerAsync() '开始异步检查更新
        '声明一个用于存储模拟城市4安装目录的字符串变量，如果程序目录下存在游戏文件则值为程序目录，否则为注册表所存储的安装目录
        Dim SC4InstallDir As String = If(My.Computer.FileSystem.FileExists("Apps\SimCity 4.exe"), Windows.Forms.Application.StartupPath,
                                         If(Environment.Is64BitOperatingSystem, My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Install Dir", Nothing),
                                            My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Install Dir", Nothing)))
        With My.Computer.FileSystem
            If SC4InstallDir IsNot Nothing AndAlso (.FileExists(SC4InstallDir & "\Apps\SimCity 4.exe") AndAlso .FileExists(SC4InstallDir & "\SimCity_1.dat") AndAlso .FileExists(SC4InstallDir & "\SimCity_2.dat") AndAlso
                .FileExists(SC4InstallDir & "\SimCity_3.dat") AndAlso .FileExists(SC4InstallDir & "\SimCity_4.dat") AndAlso .FileExists(SC4InstallDir & "\SimCity_5.dat")) Then '如果注册表里的安装目录项存在且安装目录下存在游戏文件则判断已安装的组件
                SC4InstallDir = IO.Path.GetFullPath(SC4InstallDir) '将短路径转换为长路径
                With ModuleDeclare.InstalledModules
                    .SC4InstallDir = If(SC4InstallDir.EndsWith(": \"), SC4InstallDir.Trim, SC4InstallDir.TrimEnd("\").Trim) '如果目录路径不是分区根路径则去掉结尾的\
                    bgwVerifySC4Version.RunWorkerAsync() : Cursor = Cursors.WaitCursor '开始异步判断已安装的组件并将指针图标改为等待图标
                    '声明一个用于存储模拟城市4语言设置的注册表键名的字符串变量
                    Dim LanguageRegKeyName As String = If(Environment.Is64BitOperatingSystem, "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0", "HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0")
                    Select Case My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) '判断已安装的语言补丁
                        Case "18" : .LanguagePatchOption = If(My.Computer.FileSystem.DirectoryExists(SC4InstallDir & "\TChinese"), SC4Language.TraditionalChinese, Nothing)
                        Case "17" : .LanguagePatchOption = If(My.Computer.FileSystem.DirectoryExists(SC4InstallDir & "\SChinese"), SC4Language.SimplifiedChinese, Nothing)
                        Case "1", "English US", Nothing : .LanguagePatchOption = If(My.Computer.FileSystem.DirectoryExists(SC4InstallDir & "\English"), SC4Language.English, Nothing)
                    End Select
                End With
                btnQuickInstall.Visible = False : btnCustomInstall.Visible = False : btnChangeModule.Visible = True : btnUninstall.Visible = True '隐藏快速和自定义安装按钮，显示更改和卸载按钮
                ModuleDeclare.InstallOptions = Nothing
            Else
                If .DirectoryExists("Data\SC4\CD") = False Then '如果不存在Data\SC4\CD文件夹则隐藏自定义安装按钮并移动关于和退出按钮的位置
                    btnCustomInstall.Visible = False : btnAbout.Location = New Point(263, 250) : btnExit.Location = New Point(263, 298)
                End If
                btnChangeModule.Visible = False : btnUninstall.Visible = False '隐藏更改和卸载按钮
                ModuleDeclare.InstalledModules = Nothing : ModuleDeclare.ChangeOptions = Nothing
            End If
        End With
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

    Private Sub btnInstall_Click(sender As Button, e As EventArgs) Handles btnQuickInstall.Click, btnCustomInstall.Click
        If sender Is btnQuickInstall Then
            With ModuleDeclare.InstallOptions '设置快速安装选项
                .IsQuickInstall = True
                .SC4InstallType = InstallOptions.SC4Type.NoInstall
                fbdSC4InstallDir.ShowDialog()
                Do Until fbdSC4InstallDir.SelectedPath <> "" : fbdSC4InstallDir.ShowDialog() : Loop
                .SC4InstallDir = fbdSC4InstallDir.SelectedPath
                .IsInstall638Patch = True : .IsInstall640Patch = True : .IsInstall641Patch = True
                .IsInstall4GBPatch = If(Environment.Is64BitOperatingSystem, True, False) : .IsInstallSC4Launcher = True
                .LanguagePatchOption = SC4Language.TraditionalChinese
                .IsAddDesktopIcon = True : .IsAddStartMenuItem = True
            End With
        End If
        frmLicenses.Show()
        Dispose() '直接释放窗口以避免触发FormClosing事件
    End Sub

    Private Sub btnChangeModule_Click(sender As Object, e As EventArgs) Handles btnChangeModule.Click
        If My.Computer.FileSystem.DirectoryExists("Data") = False Then
            MessageBox.Show("Data文件夹不存在" & vbCrLf & "请使用完整安装程序以安装或卸载组件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
        End If
        frmLicenses.Show()
        Dispose() '直接释放窗口以避免触发FormClosing事件
    End Sub

    Private Sub btnUninstall_Click(sender As Object, e As EventArgs) Handles btnUninstall.Click
        If MessageBox.Show("确定要卸载模拟城市4 豪华版？" & vbCrLf & "卸载目录：" & ModuleDeclare.InstalledModules.SC4InstallDir, "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            frmUninstalling.Show()
            Dispose() '直接释放窗口以避免触发FormClosing事件
        End If
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        RemoveHandler Me.FormClosing, AddressOf frmMain_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
        Application.Exit()
    End Sub

End Class