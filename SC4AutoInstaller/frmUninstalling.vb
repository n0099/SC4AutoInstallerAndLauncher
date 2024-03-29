﻿Public Class frmUninstalling

    ''' <summary>递归返回某个文件夹内所有的文件和文件夹的数量</summary>
    ''' <param name="path">要查询的文件夹的路径</param>
    ''' <returns>返回文件夹内所有的文件和文件夹的数量</returns>
    Private Function GetDirectoryContentCount(ByVal path As String) As Long
        Dim count As Long
        count += New IO.DirectoryInfo(path).GetFiles.Count
        For Each i As IO.DirectoryInfo In New IO.DirectoryInfo(path).GetDirectories
            count += GetDirectoryContentCount(i.FullName) '递归返回子文件夹的大小
        Next
        Return count
    End Function

    ''' <summary>递归删除指定文件夹内的所有子目录和文件</summary>
    ''' <param name="path">要删除的文件夹的路径</param>
    Private Sub DeleteFolderAndContents(ByVal path As String)
        For Each i As IO.FileInfo In New IO.DirectoryInfo(path).GetFiles()
            Try
                IO.File.SetAttributes(i.FullName, IO.FileAttributes.Normal) '将该文件设置为正常属性
                If i.FullName <> Application.ExecutablePath Then i.Delete() : bgwUninstall.ReportProgress(Nothing)
            Catch
                bgwUninstall.ReportProgress(Nothing) : Continue For '如果遇到异常则忽略该异常继续删除文件
            End Try
        Next
        For Each i As IO.DirectoryInfo In New IO.DirectoryInfo(path).GetDirectories()
            DeleteFolderAndContents(i.FullName) '递归删除子目录
        Next
        Try
            My.Computer.FileSystem.DeleteDirectory(path, FileIO.DeleteDirectoryOption.DeleteAllContents) '删除空目录
        Catch ex As UnauthorizedAccessException
            MessageBox.Show("游戏安装目录或用户文件目录下的部分文件无法删除" & vbCrLf & "您可以随后手动删除", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As IO.IOException
            MessageBox.Show("游戏安装目录或用户文件目录下的部分文件无法删除" & vbCrLf & "您可以随后手动删除", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub bgwUninstall_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUninstall.DoWork
        With My.Computer.FileSystem '删除文件
            DeleteFolderAndContents(ModuleDeclare.InstalledModules.SC4InstallDir) '删除游戏安装目录
            Dim StartMenuPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) & "\Programs\Maxis\SimCity 4 Deluxe"
            If .DirectoryExists(StartMenuPath) Then '如果开始菜单文件夹下存在Maxis\SimCity 4 Deluxe文件夹则删除该文件夹
                bgwUninstall.ReportProgress(prgUninstall.Maximum + GetDirectoryContentCount(StartMenuPath), "ChangeProgressBarMaximumValue") '更改卸载进度条的最大值
                DeleteFolderAndContents(StartMenuPath) '删除开始菜单\Maxis\SimCity 4 Deluxe文件夹
            End If
            Dim UserDirPath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SimCity 4"
            If .DirectoryExists(UserDirPath) AndAlso MessageBox.Show("是否删除用户文件目录（用于存储游戏存档、插件等）？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                bgwUninstall.ReportProgress(prgUninstall.Maximum + GetDirectoryContentCount(UserDirPath), "ChangeProgressBarMaximumValue") '更改卸载进度条的最大值
                DeleteFolderAndContents(UserDirPath) '删除用户文件目录
            End If
        End With
        With My.Computer.Registry.LocalMachine '删除注册表项
            If Environment.Is64BitOperatingSystem Then '删除游戏所产生的的注册表项及控制面板的卸载或更改程序项
                .DeleteSubKeyTree("SOFTWARE\Wow6432Node\Maxis\SimCity 4", False) : bgwUninstall.ReportProgress(Nothing)
                .DeleteSubKeyTree("SOFTWARE\Wow6432Node\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", False) : bgwUninstall.ReportProgress(Nothing)
                '删除控制面板的卸载或更改程序里的SimCity 4 Deluxe和模拟城市4 豪华版 自动安装程序项
                .DeleteSubKeyTree("SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", False) : bgwUninstall.ReportProgress(Nothing)
                .DeleteSubKeyTree("SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SC4AutoInstaller", False) : bgwUninstall.ReportProgress(Nothing)
            Else
                .DeleteSubKeyTree("SOFTWARE\Maxis\SimCity 4", False) : bgwUninstall.ReportProgress(Nothing)
                .DeleteSubKeyTree("SOFTWARE\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", False) : bgwUninstall.ReportProgress(Nothing)
                '删除控制面板的卸载或更改程序里的SimCity 4 Deluxe和模拟城市4 豪华版 自动安装程序项
                .DeleteSubKeyTree("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", False) : bgwUninstall.ReportProgress(Nothing)
                .DeleteSubKeyTree("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\SC4AutoInstaller", False) : bgwUninstall.ReportProgress(Nothing)
            End If
            .DeleteSubKeyTree("SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", False) : bgwUninstall.ReportProgress(Nothing)
        End With
    End Sub

    Private Sub bgwUninstall_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwUninstall.ProgressChanged
        If e.UserState = "ChangeProgressBarMaximumValue" Then prgUninstall.Maximum = e.ProgressPercentage Else prgUninstall.Value += 1
    End Sub

    Private Sub bgwUninstall_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUninstall.RunWorkerCompleted
        MessageBox.Show("模拟城市4 豪华版 卸载完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        If ModuleDeclare.InstalledModules.SC4InstallDir = Application.StartupPath Then
            Dim bat As String = ":del" & vbCrLf & "del %~dp0\%1" & vbCrLf & "if exist %~dp0\%1 goto del" & vbCrLf & "del %0" '声明一个用于删除安装程序的批处理文件内容的字符串变量
            My.Computer.FileSystem.WriteAllText("del.bat", bat, False, System.Text.Encoding.ASCII) '新建一个内容为bat字符串变量的批处理文件
            Process.Start(New ProcessStartInfo With {.FileName = "del.bat", .Arguments = Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf("\") + 1), .Verb = "runas"})
        End If
        Environment.Exit(0) '强制退出程序以避免触发frmMain的FormClosing事件
    End Sub

    Private Sub frmUninstalling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        prgUninstall.Maximum = GetDirectoryContentCount(ModuleDeclare.InstalledModules.SC4InstallDir) + 5 '初始化卸载进度条的最大值
        '禁用标题栏上的关闭按钮
        Dim ControlBoxHandle As Integer = GetSystemMenu(Me.Handle, 0)
        Dim ControlBoxCount As Integer = GetMenuItemCount(ControlBoxHandle)
        RemoveMenu(ControlBoxHandle, ControlBoxCount - 1, MF_DISABLED Or MF_BYPOSITION)
        DrawMenuBar(Me.Handle)
        bgwUninstall.RunWorkerAsync() '开始异步卸载
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

End Class