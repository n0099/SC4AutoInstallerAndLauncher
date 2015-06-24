Public Class frmMain

    Private Sub btnLaunch_Click(sender As Object, e As EventArgs) Handles btnLaunch.Click
        Process.Start(My.Settings.SC4InstallDir & "\Apps\SimCity 4.exe", My.Settings.Argument)
        Application.Exit()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim RegSC4InstallDir As String = Nothing
        If Environment.Is64BitOperatingSystem = True Then RegSC4InstallDir = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4 Deluxe", "Install Dir", Nothing)
        If Environment.Is64BitOperatingSystem = False Then RegSC4InstallDir = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4 Deluxe", "Install Dir", Nothing)
        If My.Settings.IsFirstRun = True And RegSC4InstallDir <> Nothing Then
            If RegSC4InstallDir.EndsWith("\") = True Then My.Settings.SC4InstallDir = RegSC4InstallDir.Substring(0, RegSC4InstallDir.Length - 1) Else My.Settings.SC4InstallDir = RegSC4InstallDir
        ElseIf RegSC4InstallDir = Nothing Then
            If MessageBox.Show("未检测到模拟城市4安装目录，是否手动选择安装目录？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
                fbdSC4InstallDir.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) : fbdSC4InstallDir.ShowDialog()
                Do Until fbdSC4InstallDir.SelectedPath <> Nothing
                    fbdSC4InstallDir.ShowDialog()
                Loop
                My.Settings.SC4InstallDir = fbdSC4InstallDir.SelectedPath
            Else
                Close()
            End If
        End If
        Do Until My.Computer.FileSystem.FileExists(My.Settings.SC4InstallDir & "\Apps\SimCity 4.exe")
            MessageBox.Show("模拟城市4安装目录里没有游戏程序！" & vbCrLf & "请重新选择模拟城市4安装目录！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            fbdSC4InstallDir.SelectedPath = My.Settings.SC4InstallDir : fbdSC4InstallDir.ShowDialog()
            Do Until fbdSC4InstallDir.SelectedPath <> Nothing
                fbdSC4InstallDir.ShowDialog()
            Loop
            My.Settings.SC4InstallDir = fbdSC4InstallDir.SelectedPath
        Loop
        My.Settings.IsFirstRun = False : My.Settings.Save()
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099"
    End Sub

    Private Sub btnSetting_Click(sender As Object, e As EventArgs) Handles btnSetting.Click
        frmSetting.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub

End Class