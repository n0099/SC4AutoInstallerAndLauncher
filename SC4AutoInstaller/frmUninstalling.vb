Public Class frmUninstalling

    Private Sub bgwUninstall_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUninstall.DoWork
        With My.Computer.FileSystem
            Try
                .DeleteDirectory(ModuleMain.InstalledModule.SC4InstallDir, FileIO.DeleteDirectoryOption.DeleteAllContents) '删除游戏安装目录
            Catch ex As IO.IOException
                MessageBox.Show("游戏安装目录下的部分文件无法删除，您可以随后手动删除。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
            prgUninstall.Value += 1
            If .DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "\Maxis\SimCity 4 Deluxe") = True Then '如果开始菜单文件夹下存在Maxis\SimCity 4 Deluxe文件夹则删除该文件夹
                .DeleteDirectory(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "\Maxis\SimCity 4 Deluxe", FileIO.DeleteDirectoryOption.DeleteAllContents) : prgUninstall.Maximum += 1 : prgUninstall.Value += 1
            End If
            If .DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SimCity 4") = True AndAlso _
                MessageBox.Show("是否删除用户文件目录（游戏存档、插件等）？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                .DeleteDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SimCity 4", FileIO.DeleteDirectoryOption.DeleteAllContents) : prgUninstall.Maximum += 1 : prgUninstall.Value += 1
            End If
        End With
        With My.Computer.Registry.LocalMachine
            If Environment.Is64BitOperatingSystem = True Then
                .DeleteSubKeyTree("SOFTWARE\Wow6432Node\Maxis\SimCity 4", False) : prgUninstall.Value += 1 '删除游戏所产生的的注册表项
                .DeleteSubKeyTree("SOFTWARE\Wow6432Node\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", False) : prgUninstall.Value += 1 '删除游戏所产生的的注册表项
                .DeleteSubKeyTree("SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", False) : prgUninstall.Value += 1 '删除控制面板的卸载或更改程序里的SimCity 4 Deluxe项
                .DeleteSubKeyTree("SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SC4AutoInstaller", False) : prgUninstall.Value += 1 '删除控制面板的卸载或更改程序里的模拟城市4 豪华版 自动安装程序项
            Else
                .DeleteSubKeyTree("SOFTWARE\Maxis\SimCity 4", False) : prgUninstall.Value += 1 '删除游戏所产生的的注册表项
                .DeleteSubKeyTree("SOFTWARE\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", False) : prgUninstall.Value += 1 '删除游戏所产生的的注册表项
                .DeleteSubKeyTree("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", False) : prgUninstall.Value += 1 '删除控制面板的卸载或更改程序里的SimCity 4 Deluxe项
                .DeleteSubKeyTree("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\SC4AutoInstaller", False) : prgUninstall.Value += 1 '删除控制面板的卸载或更改程序里的模拟城市4 豪华版 自动安装程序项
            End If
            .DeleteSubKeyTree("SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", False) : prgUninstall.Value += 1 '删除游戏所产生的的注册表项
        End With
    End Sub

    Private Sub bgwUninstall_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUninstall.RunWorkerCompleted
        MessageBox.Show("模拟城市4 豪华版 卸载完成。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Application.Exit()
    End Sub

    Private Sub frmUninstalling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MessageBox.Show("确定要卸载模拟城市4 豪华版？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then bgwUninstall.RunWorkerAsync() Else Application.Exit()
        Control.CheckForIllegalCrossThreadCalls = False '设置不捕捉对错误线程（跨线程）的调用的异常
        Dim ControlBoxHandle As Integer = GetSystemMenu(Me.Handle, 0) '标题栏右上角的菜单的句柄
        Dim ControlBoxCount As Integer = GetMenuItemCount(ControlBoxHandle) '标题栏右上角的菜单项的数量
        RemoveMenu(ControlBoxHandle, ControlBoxCount - 1, MF_DISABLED Or MF_BYPOSITION) '禁用标题栏右上角的X按钮
        DrawMenuBar(Me.Handle) '立即重绘标题栏的右上角菜单
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

End Class