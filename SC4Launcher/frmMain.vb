Public Class frmMain

#Region "检查并下载更新"
    Private Sub bgwCheckUpdate_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwCheckUpdate.DoWork
        Try '下载更新信息
            If My.Computer.Network.IsAvailable AndAlso My.Computer.Network.Ping("n0099.coding.io") Then '确认能否连接到更新服务器
                Dim UpdateInfoXML As New Xml.XmlDocument '声明一个用于暂时存储更新信息的XmlDocument类实例
                UpdateInfoXML.Load("http://n0099.coding.io/updateinfo.xml") '下载更新信息
                e.Result = UpdateInfoXML.GetElementsByTagName("SC4Launcher")(0) '返回已下载更新信息的XmlDocument类实例
            Else '如果无法连接到更新服务器则弹出提示框并返回WebError字符串
WebError:       MessageBox.Show("无法连接更新服务器" & vbCrLf & "请检查网络连接后重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : e.Result = "WebError"
            End If
        Catch : GoTo WebError '如果下载更新信息途中发生异常则跳转至WebError行
        End Try
    End Sub

    Private Sub bgwCheckUpdate_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwCheckUpdate.RunWorkerCompleted
        Try '检查是否有新版本可用
            If e.Result Is "WebError" Then Exit Sub '如果下载更新信息失败则停止检查更新
            Dim LauncherNode As Xml.XmlNode = CType(e.Result, Xml.XmlNode)
            Dim LatestVersion As String = LauncherNode("LatestVersion").InnerText
            With My.Application
                If (LatestVersion.Split(".")(0) > .Info.Version.Major) OrElse (LatestVersion.Split(".")(1) > .Info.Version.Minor) OrElse (LatestVersion.Split(".")(2) > .Info.Version.Revision) Then '判断最新版本号是否大于当前版本号
                    If MessageBox.Show("检测到有新版本可用，是否下载更新程序？" & vbCrLf & "当前版本：" & .Info.Version.Major & "." & .Info.Version.Minor & "." & .Info.Version.Revision & vbCrLf &
                                       "更新说明：" & LauncherNode("UpdateDetails").InnerText, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then '询问用户是否更新
                        Me.Hide() : frmSettings.Hide() '隐藏其他窗口
                        My.Computer.Network.DownloadFile(LauncherNode("UpdaterURL").InnerText, Application.StartupPath & "\LauncherUpdate.exe", "", "", True, 6000000, True) '从指定的下载地址下载更新程序
                        If My.Computer.FileSystem.FileExists("LauncherUpdate.exe") Then '如果存在更新程序则以管理员权限启动更新程序并强制退出程序
                            Process.Start(New ProcessStartInfo With {.FileName = "LauncherUpdate.exe", .Verb = "runas"}) : Environment.Exit(0)
                        End If
                    End If
                End If
            End With
        Catch : Environment.Exit(0) '如果下载更新程序途中发生异常则强制退出程序
        End Try
    End Sub
#End Region

    Private Sub btnLaunch_Click(sender As Object, e As EventArgs) Handles btnLaunch.Click
        Process.Start(My.Settings.SC4InstallDir & "\Apps\SimCity 4.exe", My.Settings.Argument) '将启动参数作为命令行参数来启动模拟城市4
        If My.Settings.IsExirLauncherAfterLaunch Then Application.Exit()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        bgwCheckUpdate.RunWorkerAsync() '开始异步检查更新
        With My.Settings
            '声明一个用于存储模拟城市4安装目录的注册表键值的字符串变量
            Dim SC4InstallDir As String = If(Environment.Is64BitOperatingSystem, My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Install Dir", Nothing),
                                             My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Install Dir", Nothing))
            If .IsFirstRun AndAlso SC4InstallDir IsNot Nothing Then
                SC4InstallDir = IO.Path.GetFullPath(SC4InstallDir) '将短路径转换为长路径
                .SC4InstallDir = If(SC4InstallDir.EndsWith(":\"), SC4InstallDir.Trim, SC4InstallDir.TrimEnd("\").Trim) '如果目录路径不是分区根路径则去掉结尾的\
            ElseIf .IsFirstRun AndAlso SC4InstallDir Is Nothing Then
                If MessageBox.Show("未检测到模拟城市4 安装目录" & vbCrLf & "请手动选择安装目录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) = DialogResult.OK Then
                    fbdSC4InstallDir.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) : fbdSC4InstallDir.ShowDialog()
                    Do Until fbdSC4InstallDir.SelectedPath IsNot Nothing : fbdSC4InstallDir.ShowDialog() : Loop
                    .SC4InstallDir = fbdSC4InstallDir.SelectedPath
                Else : Environment.Exit(0)
                End If
            End If
            Do Until My.Computer.FileSystem.FileExists(.SC4InstallDir & "\Apps\SimCity 4.exe")
                If MessageBox.Show("模拟城市4 安装目录无效" & vbCrLf & "请重新选择模拟城市4 安装目录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) = DialogResult.OK Then
                    fbdSC4InstallDir.SelectedPath = .SC4InstallDir : fbdSC4InstallDir.ShowDialog()
                    Do Until fbdSC4InstallDir.SelectedPath IsNot Nothing : fbdSC4InstallDir.ShowDialog() : Loop
                    .SC4InstallDir = fbdSC4InstallDir.SelectedPath
                Else : Environment.Exit(0)
                End If
            Loop
            .IsFirstRun = False : .Save()
        End With
        BackgroundImage = CType(My.Resources.ResourceManager.GetObject("SC4_" & New Random().Next(1, 11)), Image) '将主窗口的背景图片设置为资源文件里名为SC4_随机数（介于1到10之间）的图片
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

    Private Sub btnSetting_Click(sender As Object, e As EventArgs) Handles btnSetting.Click
        frmSettings.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

End Class