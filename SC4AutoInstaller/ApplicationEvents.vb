Namespace My

    ' 以下事件可用于 MyApplication: 
    ' 
    ' Startup:  应用程序启动时在创建启动窗体之前引发。
    ' Shutdown:  在关闭所有应用程序窗体后引发。  如果应用程序异常终止，则不会引发此事件。
    ' UnhandledException:  在应用程序遇到未经处理的异常时引发。
    ' StartupNextInstance:  在启动单实例应用程序且应用程序已处于活动状态时引发。
    ' NetworkAvailabilityChanged:  在连接或断开网络连接时引发。
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            Dim SC4InstallDir As String = Nothing '声明一个用于存储HKEY_LOCAL_MACHINE\SOFTWARE\（Wow6432Node）\Maxis\SimCity 4\Install Dir项值的字符串变量
            If Environment.Is64BitOperatingSystem = True Then SC4InstallDir = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Install Dir", Nothing)
            If Environment.Is64BitOperatingSystem = False Then SC4InstallDir = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Install Dir", Nothing)
            With My.Computer.FileSystem
                '如果已经安装了模拟城市4且Data文件夹不存在则不验证文件完整性
                If SC4InstallDir <> Nothing And .FileExists(SC4InstallDir & "\Apps\SimCity 4.exe") = True And .FileExists(SC4InstallDir & "\SimCity_1.dat") = True And
                .FileExists(SC4InstallDir & "\SimCity_2.dat") = True And .FileExists(SC4InstallDir & "\SimCity_3.dat") And .FileExists(SC4InstallDir & "\SimCity_4.dat") = True And .DirectoryExists("Data") = False Then My.Application.MainForm = frmMain
            End With
            '检查是否有新版本可用
            Try
                If My.Computer.Network.IsAvailable = True And My.Computer.Network.Ping("n0099.sinaapp.com") = True Then '确定是否能连接到更新服务器
                    Dim UpdataInfoXML As New Xml.XmlDocument, AutoInstallerNode As Xml.XmlNode '获取更新信息
                    UpdataInfoXML.Load("http://n0099.sinaapp.com/updatainfo.xml")
                    AutoInstallerNode = UpdataInfoXML.GetElementsByTagName("AutoInstaller")(0)
                    Dim LatestVersion As String = AutoInstallerNode("LatestVersion").InnerText
                    If LatestVersion.Split(".")(0) > My.Application.Info.Version.Major Or LatestVersion.Split(".")(1) > My.Application.Info.Version.Minor _
                        Or LatestVersion.Split(".")(2) > My.Application.Info.Version.Revision Then '检查是否有新版本可用
                        Dim version As String = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision
                        Dim UpdataDetail As String = AutoInstallerNode("UpdataDetail").InnerText '声明一个用于存储最新版的更新说明的字符串变量
                        If MessageBox.Show("检测到有新版本可用，是否下载更新？" & vbCrLf & "当前版本：" & version & vbCrLf & "更新说明：" & UpdataDetail, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                            Dim DownloadLink As String = AutoInstallerNode("DonwloadLink").InnerText '声明一个用于存储新版本的下载地址的字符串变量
                            My.Computer.Network.DownloadFile(DownloadLink, Windows.Forms.Application.StartupPath & "\Updata.exe", "", "", True, 6000000, True) '从指定的下载地址下载自动更新程序
                            If My.Computer.FileSystem.FileExists("Updata.exe") = True Then '如果存在自动更新程序则以管理员权限启动自动更新程序并退出程序
                                Process.Start(New ProcessStartInfo With {.FileName = "Updata.exe", .Verb = "runas"}) : Environment.Exit(0)
                            End If
                        End If
                    End If
                Else
WebError:           MessageBox.Show("无法连接更新服务器！" & vbCrLf & "请检查网络连接后重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Catch ex As Net.WebException : GoTo WebError
            Catch ex As TimeoutException : GoTo WebError
            Catch ex As Security.SecurityException : GoTo WebError
            Catch ex As Net.NetworkInformation.PingException : GoTo WebError
            Catch
            End Try
        End Sub

        Private Sub MyApplication_StartupNextInstance(sender As Object, e As ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            MessageBox.Show("不能同时运行多个安装程序！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub

    End Class

End Namespace