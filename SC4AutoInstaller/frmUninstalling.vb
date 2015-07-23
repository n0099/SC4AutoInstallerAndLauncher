Public Class frmUninstalling

    Private Sub bgwUninstall_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUninstall.DoWork
        With My.Computer.FileSystem
            Try
                .DeleteDirectory(ModuleMain.InstalledModule.SC4InstallDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception
            End Try
            prgUninstall.Value += 1
            If .DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "\Maxis\SimCity 4 Deluxe") = True Then
                .DeleteDirectory(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "\Maxis\SimCity 4 Deluxe", FileIO.DeleteDirectoryOption.DeleteAllContents) : prgUninstall.Maximum += 1 : prgUninstall.Value += 1
            End If
            If .DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SimCity 4") = True AndAlso _
                MessageBox.Show("是否删除用户文件目录（游戏存档、插件等）？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                .DeleteDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SimCity 4", FileIO.DeleteDirectoryOption.DeleteAllContents) : prgUninstall.Maximum += 1 : prgUninstall.Value += 1
            End If
        End With
        With My.Computer.Registry.LocalMachine
            If Environment.Is64BitOperatingSystem = True Then
                .DeleteSubKeyTree("SOFTWARE\Wow6432Node\Maxis\SimCity 4", False) : prgUninstall.Value += 1
                .DeleteSubKeyTree("SOFTWARE\Wow6432Node\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", False) : prgUninstall.Value += 1
                .DeleteSubKeyTree("SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", False) : prgUninstall.Value += 1
                .DeleteSubKeyTree("SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SC4AutoInstaller", False) : prgUninstall.Value += 1
            Else
                .DeleteSubKeyTree("SOFTWARE\Maxis\SimCity 4", False) : prgUninstall.Value += 1
                .DeleteSubKeyTree("SOFTWARE\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", False) : prgUninstall.Value += 1
                .DeleteSubKeyTree("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", False) : prgUninstall.Value += 1
                .DeleteSubKeyTree("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\SC4AutoInstaller", False) : prgUninstall.Value += 1
            End If
            .DeleteSubKeyTree("SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", False) : prgUninstall.Value += 1
        End With
    End Sub

    Private Sub bgwUninstall_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUninstall.RunWorkerCompleted
        MessageBox.Show("模拟城市4 豪华版 卸载完成。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Application.Exit()
    End Sub

    Private Sub frmUninstalling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MessageBox.Show("确定要卸载模拟城市4 豪华版？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then bgwUninstall.RunWorkerAsync() Else Application.Exit()
        Control.CheckForIllegalCrossThreadCalls = False
        Dim ControlBoxHandle As Integer = GetSystemMenu(Me.Handle, 0)
        Dim ControlBoxCount As Integer = GetMenuItemCount(ControlBoxHandle)
        RemoveMenu(ControlBoxHandle, ControlBoxCount - 1, MF_DISABLED Or MF_BYPOSITION)
        DrawMenuBar(Me.Handle)
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099"
    End Sub

End Class