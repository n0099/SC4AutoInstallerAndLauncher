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
            With My.Computer.FileSystem
                '声明一个用于存储模拟城市4安装目录的字符串变量，如果程序目录下存在游戏文件则值为程序目录，否则为注册表所存储的安装目录
                Dim SC4InstallDir As String = If(My.Computer.FileSystem.FileExists("Apps\SimCity 4.exe"), Windows.Forms.Application.StartupPath,
                                                 If(Environment.Is64BitOperatingSystem, My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Install Dir", Nothing),
                                                    My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Install Dir", Nothing)))
                '如果已经安装了模拟城市4或程序目录下存在游戏文件且Data文件夹不存在则不验证文件完整性
                If .DirectoryExists("Data") = False AndAlso SC4InstallDir IsNot Nothing AndAlso (.FileExists(SC4InstallDir & "\Apps\SimCity 4.exe") AndAlso .FileExists(SC4InstallDir & "\SimCity_1.dat") AndAlso .FileExists(SC4InstallDir & "\SimCity_2.dat") AndAlso
                    .FileExists(SC4InstallDir & "\SimCity_3.dat") AndAlso .FileExists(SC4InstallDir & "\SimCity_4.dat") AndAlso .FileExists(SC4InstallDir & "\SimCity_5.dat")) Then Application.MainForm = frmMain
            End With
        End Sub

        Private Sub MyApplication_StartupNextInstance(sender As Object, e As ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            MessageBox.Show("不能同时运行多个安装程序", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub

    End Class

End Namespace