Public Class frmMain

    Private Sub bgwComputeMD5_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwComputeMD5.DoWork
        '声明一个用于计算MD5值的System.Security.Cryptography.MD5CryptoServiceProvider类实例和用于存储游戏文件的MD5值的字符串数组
        Dim MD5CSP As New Security.Cryptography.MD5CryptoServiceProvider, MD5(4) As String
        Dim SC4FilesMD5() As String = {"C05406B02449540328DBB4B741E0A81D", "E2976161D7EC772893D273FF753D08F6", "3E660755D70543D2222BD46B5A6F22C4", "6DB4F1F9F1A1EC45B22E35827073FBA2"}
        Dim _638FilesMD5() As String = {"A9E238946A8C8C479DD368EC4581B77A", "2CFD520899786AEF47C728B123EBCF05", "7FE6E6678FBBA581092473C5F4C35331", "CB2C26A9C4BC9B8E53709380B64B805C"}
        With ModuleMain.InstalledModule
            '声明一个用于计算文件MD5值的System.IO.FileStream类实例的文件流数组
            Dim FilesStream() As IO.FileStream = {New IO.FileStream(.SC4InstallDir & "\Apps\SimCity 4.exe", IO.FileMode.Open) _
                                                 , New IO.FileStream(.SC4InstallDir & "\SimCity_1.dat", IO.FileMode.Open) _
                                                 , New IO.FileStream(.SC4InstallDir & "\SimCity_2.dat", IO.FileMode.Open) _
                                                 , New IO.FileStream(.SC4InstallDir & "\SimCity_3.dat", IO.FileMode.Open) _
                                                 , New IO.FileStream(.SC4InstallDir & "\SimCity_4.dat", IO.FileMode.Open)}
            For i As Integer = 0 To 4
                If e.Cancel = True Then '如果请求取消判断则循环关闭文件使用
                    For Each j As IO.FileStream In FilesStream
                        j.Close()
                    Next
                    Exit Sub
                End If
                MD5(i) = BitConverter.ToString(MD5CSP.ComputeHash(FilesStream(i))).Replace("-", "") : FilesStream(i).Close() '将文件流的MD5值存储到数组里并关闭该文件的使用
            Next
            For i As Integer = 1 To 4 '判断是否已经安装638补丁
                If MD5(i) = _638FilesMD5(i - 1) Then .Is638PatchInstalled = True Else .Is638PatchInstalled = False
            Next
            If MD5(1) = "6159A4036F451BEA1740DDB05C32494A" Then .Is640PatchInstalled = True '判断是否已经安装640补丁
            Select Case MD5(0) '判断Apps\SimCity 4.exe文件的MD5值
                Case "6159A4036F451BEA1740DDB05C32494A" : .Is640PatchInstalled = True
                Case "53D2AE4FA9114B88AD91ECF32A7F16A4" : .Is641PatchInstalled = True
                Case "78202C3EF76988BD2BF05F8D223BE7A3" : .Is4GBPatchInstalled = True : .Is638PatchInstalled = False : .Is640PatchInstalled = False : .Is641PatchInstalled = False
                Case "2F2BD7D9A76E85320A26D7BD7530DCAE" : .Is4GBPatchInstalled = True : .Is638PatchInstalled = True
                Case "1C18B7DC760EDADD2C2EFAF33F60F150" : .Is4GBPatchInstalled = True : .Is640PatchInstalled = True
                Case "1414E70EB5CE22DB37D22CB99439D012" : .Is4GBPatchInstalled = True : .Is641PatchInstalled = True
                Case "AADC5464919FBDC0F8E315FA51582126" : .Is4GBPatchInstalled = True : .IsNoCDPatchInstalled = True
                Case "B57B5B03C4854C194CE8BEBD173F3483" : .IsNoCDPatchInstalled = True
            End Select
            If My.Computer.FileSystem.FileExists(.SC4InstallDir & "\SC4Launcher.exe") = True Then .IsSC4LauncherInstalled = True '判断是否已经安装模拟城市4 启动器
        End With
    End Sub

    Private Sub bgwComputeMD5_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwComputeMD5.RunWorkerCompleted
        If e.Cancelled = False Then btnChangeModule.Enabled = True : Cursor = Cursors.Default '使更改按钮可用并将指针图标恢复正常
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("确定要退出安装程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then e.Cancel = True
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With My.Computer.FileSystem
            Dim SC4InstallDir As String = Nothing '声明一个用于存储HKEY_LOCAL_MACHINE\SOFTWARE\（Wow6432Node）\Maxis\SimCity 4\Install Dir项值的字符串变量
            If Environment.Is64BitOperatingSystem = True Then SC4InstallDir = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Install Dir", Nothing)
            If Environment.Is64BitOperatingSystem = False Then SC4InstallDir = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Install Dir", Nothing)
            If SC4InstallDir <> Nothing Then SC4InstallDir = IO.Path.GetFullPath(SC4InstallDir) '将短路径转换为长路径
            If SC4InstallDir <> Nothing And .FileExists(SC4InstallDir & "\Apps\SimCity 4.exe") = True And .FileExists(SC4InstallDir & "\SimCity_1.dat") = True And
                .FileExists(SC4InstallDir & "\SimCity_2.dat") = True And .FileExists(SC4InstallDir & "\SimCity_3.dat") And .FileExists(SC4InstallDir & "\SimCity_4.dat") = True Then '如果注册表里的安装路径项值存在且安装路径下存在游戏文件则判断已安装的组件
                With ModuleMain.InstalledModule
                    If SC4InstallDir.EndsWith("\") = True Then .SC4InstallDir = SC4InstallDir.Substring(0, SC4InstallDir.Length - 1) Else .SC4InstallDir = SC4InstallDir '如果安装目录路径以\结尾则去掉结尾的\
                    bgwComputeMD5.RunWorkerAsync() : Cursor = Cursors.WaitCursor '开始异步判断已安装的组件并将指针图标改为等待图标
                    Dim LanguageRegKeyName As String = Nothing '声明一个用于存储模拟城市4的语言设置的注册表键值的字符串变量
                    If Environment.Is64BitOperatingSystem = True Then LanguageRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0"
                    If Environment.Is64BitOperatingSystem = False Then LanguageRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0"
                    Select Case My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) '判断已安装的语言补丁
                        Case "18" : .LanguagePatch = InstalledModule.Language.TraditionalChinese
                        Case "17" : .LanguagePatch = InstalledModule.Language.SimplifiedChinese
                        Case "1", "English US", Nothing : .LanguagePatch = InstalledModule.Language.English
                    End Select
                    btnInstall.Visible = False : btnChangeModule.Visible = True : btnUninstall.Visible = True '隐藏安装按钮，显示更改和卸载按钮
                    btnAbout.Location = New Point(270, 285) : btnExit.Location = New Point(270, 330) '移动关于和退出按钮的位置
                End With
            Else : ModuleMain.InstalledModule = Nothing
            End If
        End With
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

    Private Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        With My.Computer.FileSystem
            If .DirectoryExists("Data\SC4\CD") = False And .FileExists("Data\SC4\NoInstall.7z") = False Then MessageBox.Show("Data\SC4\CD 文件夹和Data\SC4\NoInstall.7z 文件不存在！" & vbCrLf & "请使用原始安装程序以安装模拟城市4", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
            If .DirectoryExists("Data\SC4\CD") = False Then MessageBox.Show("Data\SC4\CD 文件夹不存在！" & vbCrLf & "请使用原始安装程序以安装模拟城市4", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
            If .FileExists("Data\SC4\NoInstall.7z") = False Then MessageBox.Show("Data\SC4\NoInstall.7z 文件不存在！" & vbCrLf & "请使用原始安装程序以安装模拟城市4", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
        End With
        frmLicenses.Show()
        RemoveHandler Me.FormClosing, AddressOf frmMain_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
        Close()
    End Sub

    Private Sub btnChangeModule_Click(sender As Object, e As EventArgs) Handles btnChangeModule.Click
        With My.Computer.FileSystem
            If .DirectoryExists("Data") = True Then
                If .DirectoryExists("Data\Patch") = False Then MessageBox.Show("Data\Patch 文件夹不存在！" & vbCrLf & "请使用原始安装程序以添加或删除组件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
                If .FileExists("Data\SC4\NoInstall.7z") = False Then MessageBox.Show("Data\SC4\NoInstall.7z 文件不存在！" & vbCrLf & "请使用原始安装程序以添加或删除组件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
                frmChangeModuleOptions.Show()
                RemoveHandler Me.FormClosing, AddressOf frmMain_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
                Close()
            Else
                MessageBox.Show("Data文件夹不存在！" & vbCrLf & "请使用原始安装程序以添加或删除组件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End With
    End Sub

    Private Sub btnUninstall_Click(sender As Object, e As EventArgs) Handles btnUninstall.Click
        frmUninstalling.Show()
        RemoveHandler Me.FormClosing, AddressOf frmMain_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
        bgwComputeMD5.CancelAsync() '取消异步判断已安装的组件
        Close()
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        RemoveHandler Me.FormClosing, AddressOf frmMain_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
        Application.Exit()
    End Sub

End Class