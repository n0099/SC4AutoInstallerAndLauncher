Public Class frmMain

    Private Sub btnLaunch_Click(sender As Object, e As EventArgs) Handles btnLaunch.Click
        Process.Start(My.Settings.SC4InstallDir & "\Apps\SimCity 4.exe", My.Settings.Argument) '以设置的启动参数作为命令行参数来启动游戏
        Application.Exit()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With My.Settings
            '声明一个用于存储模拟城市4安装目录的注册表键值的字符串变量
            Dim SC4InstallDir As String = If(Environment.Is64BitOperatingSystem, My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Install Dir", Nothing),
                                             My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Install Dir", Nothing))
            If .IsFirstRun AndAlso SC4InstallDir IsNot Nothing Then
                SC4InstallDir = IO.Path.GetFullPath(SC4InstallDir) '将短路径转换为长路径
                .SC4InstallDir = If(SC4InstallDir.EndsWith(":\"), SC4InstallDir.Trim, SC4InstallDir.TrimEnd("\").Trim) '如果目录路径不是分区根路径则去掉结尾的\
            ElseIf .IsFirstRun AndAlso SC4InstallDir Is Nothing Then
                If MessageBox.Show("未检测到模拟城市4安装目录" & vbCrLf & "请手动选择安装目录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) = DialogResult.OK Then
                    fbdSC4InstallDir.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) : fbdSC4InstallDir.ShowDialog()
                    Do Until fbdSC4InstallDir.SelectedPath IsNot Nothing : fbdSC4InstallDir.ShowDialog() : Loop
                    .SC4InstallDir = fbdSC4InstallDir.SelectedPath
                Else : Environment.Exit(0)
                End If
            End If
            Do Until My.Computer.FileSystem.FileExists(.SC4InstallDir & "\Apps\SimCity 4.exe")
                If MessageBox.Show("模拟城市4安装目录无效" & vbCrLf & "请重新选择模拟城市4安装目录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) = DialogResult.OK Then
                    fbdSC4InstallDir.SelectedPath = .SC4InstallDir : fbdSC4InstallDir.ShowDialog()
                    Do Until fbdSC4InstallDir.SelectedPath IsNot Nothing : fbdSC4InstallDir.ShowDialog() : Loop
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