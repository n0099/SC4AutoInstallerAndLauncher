Public Class frmMain

    Private Sub btnLaunch_Click(sender As Object, e As EventArgs) Handles btnLaunch.Click
        Process.Start(My.Settings.SC4InstallDir & "\Apps\SimCity 4.exe", My.Settings.Argument) '以设置的启动参数作为命令行参数来启动游戏
        Application.Exit()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim SC4InstallDir As String = Nothing '声明一个用于存储HKEY_LOCAL_MACHINE\SOFTWARE\（Wow6432Node）\Maxis\SimCity 4\Install Dir项值的字符串变量
        If Environment.Is64BitOperatingSystem = True Then SC4InstallDir = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Install Dir", Nothing)
        If Environment.Is64BitOperatingSystem = False Then SC4InstallDir = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Install Dir", Nothing)
        With My.Settings
            If .IsFirstRun = True And SC4InstallDir <> Nothing Then
                SC4InstallDir = IO.Path.GetFullPath(SC4InstallDir) '将短路径转换为长路径
                If SC4InstallDir.EndsWith("\") = True Then .SC4InstallDir = SC4InstallDir.Substring(0, SC4InstallDir.Length - 1) Else .SC4InstallDir = SC4InstallDir '如果安装目录路径以\结尾则去掉结尾的\
            ElseIf SC4InstallDir = Nothing Then
                If MessageBox.Show("未检测到模拟城市4安装目录，是否手动选择安装目录？", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then
                    fbdSC4InstallDir.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) : fbdSC4InstallDir.ShowDialog()
                    Do Until fbdSC4InstallDir.SelectedPath <> Nothing : fbdSC4InstallDir.ShowDialog() : Loop
                    .SC4InstallDir = fbdSC4InstallDir.SelectedPath
                Else : Environment.Exit(0)
                End If
            End If
            Do Until My.Computer.FileSystem.FileExists(.SC4InstallDir & "\Apps\SimCity 4.exe")
                If MessageBox.Show("模拟城市4安装目录里没有游戏程序！" & vbCrLf & "是否重新选择模拟城市4安装目录？", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then
                    fbdSC4InstallDir.SelectedPath = .SC4InstallDir : fbdSC4InstallDir.ShowDialog()
                    Do Until fbdSC4InstallDir.SelectedPath <> Nothing : fbdSC4InstallDir.ShowDialog() : Loop
                    .SC4InstallDir = fbdSC4InstallDir.SelectedPath
                Else : Environment.Exit(0)
                End If
            Loop
            .IsFirstRun = False : .Save()
        End With
        Dim random As New Random '声明一个用于产生随机数的System.Random类实例
        BackgroundImage = CType(My.Resources.ResourceManager.GetObject("SC4_" & random.Next(1, 10)), Image) '将主窗口的背景图片设置为资源文件里名为SC4_随机数（介于1到7之间）的图片
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

    Private Sub btnSetting_Click(sender As Object, e As EventArgs) Handles btnSetting.Click
        frmSetting.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub

End Class