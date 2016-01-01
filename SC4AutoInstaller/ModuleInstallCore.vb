Imports System.Runtime.InteropServices
Imports Result = SC4AutoInstaller.InstallResults.InstallResult
Imports ChangeOption = SC4AutoInstaller.ChangeOptions.ChangeOption

''' <summary>ModuleInstallCore模块提供安装各种组件的方法</summary>
Public Module ModuleInstallCore

    ''' <summary>判断某个文件是否已被其他进程占用</summary>
    ''' <param name="path">要判断的文件的路径</param>
    ''' <returns>如果文件已被其他进程占用，则为True；否则为False</returns>
    Public Function IsFileUsing(path As String) As Boolean
        Try
            If My.Computer.FileSystem.FileExists(path) = False Then MessageBox.Show("文件" & path & "不存在" & vbCrLf & "单击[确定]按钮重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Return True
            Using IO.File.Open(path, IO.FileMode.Open)
                Return False
            End Using
        Catch ex As UnauthorizedAccessException
            MessageBox.Show("文件" & path & "已被其他进程占用" & vbCrLf & "单击[确定]按钮重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Return True
        End Try
    End Function

    ''' <summary>在指定的路径安装DAEMON Tools Lite</summary>
    ''' <param name="InstallDir">指定DAEMON Tools Lite安装路径</param>
    ''' <returns>InstallResult.Result的值之一，如果安装成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function InstallDAEMONTools(ByVal InstallDir As String) As Result
        Try
            Do Until IsFileUsing("Data\DAEMON Tools Lite 5.0.exe") = False : Loop '判断需要修改或读取的文件是否已被其他进程占用
            Process.Start(New ProcessStartInfo With {.FileName = "Data\DAEMON Tools Lite 5.0.exe", .Arguments = "/S /nogadget /path """ & InstallDir & """", .Verb = "runas"}).WaitForExit() '安装DAEMON Tools Lite
            Return If(My.Computer.FileSystem.FileExists(InstallDir & "\DTLite.exe"), Result.Success, Result.Fail) '判断是否安装成功
        Catch
            Return Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr : End Function
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Function FindWindowEx(ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr, ByVal lclassName As String, ByVal windowTitle As String) As IntPtr : End Function
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As String) As IntPtr : End Function
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Function PostMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean : End Function
    Private Const WM_LBUTTONDOWN = &H201
    Private Const WM_LBUTTONUP = &H202
    Private Const WM_SETTEXT = &HC

    ''' <summary>在指定的路径安装指定版本的模拟城市4</summary>
    ''' <param name="SC4InstallDir">指定模拟城市4安装路径</param>
    ''' <param name="DAEMONToolsInstallDir">DAEMON Tools Lite的安装路径，用于安装镜像版模拟城市4</param>
    ''' <param name="InstallType">InstallOptions.SC4InstallType的值之一，指定要安装的版本</param>
    ''' <returns>InstallResult.Result的值之一，如果安装成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function InstallSC4(ByVal SC4InstallDir As String, ByVal DAEMONToolsInstallDir As String, ByVal InstallType As InstallOptions.SC4Type) As Result
        Try
            My.Computer.FileSystem.CopyFile(Application.ExecutablePath, SC4InstallDir & "\Setup.exe", True) '将安装程序自身复制到SC4InstallDir下
            If InstallType = InstallOptions.SC4Type.CD Then '安装镜像版模拟城市4
                With My.Computer.FileSystem
                    '判断X盘符和Y盘符是否已被占用
                    Dim XDriveType As IO.DriveType = .GetDriveInfo("X:\").DriveType, YDriveType As IO.DriveType = .GetDriveInfo("Y:\").DriveType
                    If XDriveType <> IO.DriveType.NoRootDirectory AndAlso XDriveType <> IO.DriveType.CDRom Then MessageBox.Show("盘符为X的分区已被占用" & vbCrLf & "请更改该分区的盘符", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    If YDriveType <> IO.DriveType.NoRootDirectory AndAlso YDriveType <> IO.DriveType.CDRom Then MessageBox.Show("盘符为Y的分区已被占用" & vbCrLf & "请更改该分区的盘符", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    If (XDriveType <> IO.DriveType.NoRootDirectory AndAlso XDriveType <> IO.DriveType.CDRom) OrElse (YDriveType <> IO.DriveType.NoRootDirectory AndAlso YDriveType <> IO.DriveType.CDRom) Then Throw New Exception("X盘符或Y盘符已被占用")
                    '加载CD1和CD2虚拟光驱镜像文件
                    Dim StartupPath As String = Application.StartupPath
                    If Application.StartupPath.EndsWith("\") Then StartupPath = Application.StartupPath.Remove(Application.StartupPath.LastIndexOf("\"), 1) '如果程序存储在分区根目录下则去掉结尾的\
                    Do Until .FileExists("X:\AutoRun.exe") '将CD1虚拟光驱镜像文件加载到X盘符上
                        Process.Start(New ProcessStartInfo With {.FileName = DAEMONToolsInstallDir & "\DTLite.exe", .Arguments = "-mount dt, X, """ & StartupPath & "\Data\SC4\CD\CD1.mdf""", .Verb = "runas"}).WaitForExit()
                    Loop
                    Do Until .FileExists("Y:\RunGame.exe") '将CD2虚拟光驱镜像文件加载到Y盘符上
                        Process.Start(New ProcessStartInfo With {.FileName = DAEMONToolsInstallDir & "\DTLite.exe", .Arguments = "-mount dt, Y, """ & StartupPath & "\Data\SC4\CD\CD2.mdf""", .Verb = "runas"}).WaitForExit()
                    Loop
                    '如果安装程序正在运行则结束安装程序的进程
                    If Process.GetProcessesByName("AutoRun").Length <> 0 Then Process.GetProcessesByName("AutoRun")(0).Kill()
                    If Process.GetProcessesByName("SimCity 4 Deluxe_Code").Length <> 0 Then Process.GetProcessesByName("SimCity 4 Deluxe_Code")(0).Kill()
                    If Process.GetProcessesByName("SimCity 4 Deluxe_eReg").Length <> 0 Then Process.GetProcessesByName("SimCity 4 Deluxe_eReg")(0).Kill()
                    '运行安装程序
                    Dim TempFolderPath As String = IO.Path.GetTempPath
                    If .FileExists(TempFolderPath & "\AutoRun.exe") = False Or .FileExists(TempFolderPath & "\AutoRunGUI.dll") = False Then
                        .CopyFile("X:\AutoRun.exe", TempFolderPath & "\AutoRun.exe", True) : .CopyFile("X:\AutoRunGUI.dll", TempFolderPath & "\AutoRunGUI.dll", True)
                        Process.Start(New ProcessStartInfo With {.FileName = "regsvr32.exe", .Arguments = "/s """ & TempFolderPath & "\AutoRunGUI.dll""", .Verb = "runas"}).WaitForExit() '运行regsvr32.exe以注册AutoRunGUI.dll文件
                    End If
                End With
                Dim SetupProcess As Process = Process.Start(New ProcessStartInfo With {.FileName = IO.Path.GetTempPath & "\AutoRun.exe", .Arguments = "-restart -dir X:\", .Verb = "runas"}) '运行临时文件目录下的安装程序
                '安装程序主窗口
                Do Until FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "Install") <> Nothing : Loop
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "Install"), WM_LBUTTONDOWN, 0, 0) '模拟点击安装按钮
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "Install"), WM_LBUTTONUP, 0, 0)
                '输入序列号进程
                Do Until FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Edit", "") <> Nothing : Loop
                Dim CDKey As String() = {"CX9H", "498A", "MHSS", "8QXD", "TXJB"}
                Dim TextBoxsHandles(4) As IntPtr '声明一个用于存储输入序列号窗口的5个序列号文本框句柄的IntPtr结构数组
                For i As Integer = 0 To 4
                    TextBoxsHandles(i) = FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), If(i = 0, 0, CInt(TextBoxsHandles(i - 1))), "Edit", "")
                    SendMessage(TextBoxsHandles(i), WM_SETTEXT, 0, CDKey(i)) '通过SendMessage API向第i个序列号文本框发送WM_SETTEXT消息（lParam附加值为CDKey(i)的值）来模拟输入序列号
                Next
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "&Next>"), WM_LBUTTONDOWN, 0, 0) '模拟点击下一步按钮
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "&Next>"), WM_LBUTTONUP, 0, 0)
                '输入安装路径窗口
                Do Until FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Edit", "") <> Nothing : Loop
                '通过SendMessage向安装路径文本框发送WM_SETTEXT消息（lParam附加值为InstallDir参数值）来模拟输入安装路径
                SendMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Edit", ""), WM_SETTEXT, 0, SC4InstallDir)
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "&Next>"), WM_LBUTTONDOWN, 0, 0) '模拟点击下一步按钮
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "&Next>"), WM_LBUTTONUP, 0, 0)
                '注册EA账号进程
                Do Until FindWindowEx(FindWindow("#32770", "Electronic Registration"), 0, "Button", "Register Later") <> Nothing : Loop
                PostMessage(FindWindowEx(FindWindow("#32770", "Electronic Registration"), 0, "Button", "Register Later"), WM_LBUTTONDOWN, 0, 0) '模拟点击以后注册按钮
                PostMessage(FindWindowEx(FindWindow("#32770", "Electronic Registration"), 0, "Button", "Register Later"), WM_LBUTTONUP, 0, 0)
                Do Until FindWindowEx(FindWindow("#32770", ""), 0, "Button", "Ok") : Loop
                PostMessage(FindWindowEx(FindWindow("#32770", ""), 0, "Button", "Ok"), WM_LBUTTONDOWN, 0, 0) '模拟点击不注册按钮
                PostMessage(FindWindowEx(FindWindow("#32770", ""), 0, "Button", "Ok"), WM_LBUTTONUP, 0, 0)
                SetupProcess.WaitForExit() '等待安装程序完成安装
                If Process.GetProcessesByName("SimCity 4").Length <> 0 Then Process.GetProcessesByName("SimCity 4")(0).Kill() '结束安装完成后自动运行的游戏进程
                Return If(My.Computer.FileSystem.FileExists(SC4InstallDir & "\Apps\SimCity 4.exe"), Result.Success, Result.Fail) '判断是否安装成功
            ElseIf InstallType = InstallOptions.SC4Type.NoInstall Then '安装硬盘版模拟城市4
                Do Until IsFileUsing("Data\SC4\NoInstall.7z") = False : Loop '判断需要修改或读取的文件是否已被其他进程占用
                Dim _7zaProcess As Process = Process.Start(New ProcessStartInfo With {.FileName = "Data\7za.exe", .Arguments = "x Data\SC4\NoInstall.7z -aoa -o""" & SC4InstallDir & """", .Verb = "runas"}) : _7zaProcess.WaitForExit()
                Return If(_7zaProcess.ExitCode = 0, Result.Success, Result.Fail) '判断是否解压成功
            Else : Return Nothing
            End If
        Catch
            Return Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装或卸载638补丁</summary>
    ''' <param name="InstallDir">指定安装或卸载路径</param>
    ''' <param name="IsUninstall">指定是否卸载638补丁</param>
    ''' <returns>InstallResult.Result的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function Change638Patch(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As Result
        Try
            If IsUninstall = False Then '安装638补丁
                '判断需要修改或读取的文件是否已被其他进程占用
                Dim Files As String() = {InstallDir & "\Apps\SimCity 4.exe", InstallDir & "\Graphics Rules.sgr", InstallDir & "\SimCity_1.dat",
                                         InstallDir & "\SimCity_2.dat", InstallDir & "\SimCity_3.dat", InstallDir & "\SimCity_4.dat"}
                For Each i As String In Files : Do Until IsFileUsing(i) = False : Loop : Next
                My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4.exe\SimCity 4 610.exe", InstallDir & "\Apps\SimCity 4.exe", True) '卸载4GB补丁
                '通过模拟点击来自动安装638补丁
                Process.Start("Data\Patch\638 SKU1.EXE").WaitForInputIdle()
                Do Until FindWindowEx(FindWindow("#32770", "Patch"), 0, "Button", "确定") <> Nothing : Loop
                PostMessage(FindWindowEx(FindWindow("#32770", "Patch"), 0, "Button", "确定"), WM_LBUTTONDOWN, 0, 0) '模拟点击确认安装按钮
                PostMessage(FindWindowEx(FindWindow("#32770", "Patch"), 0, "Button", "确定"), WM_LBUTTONUP, 0, 0)
                Do Until FindWindowEx(FindWindow("#32770", "RTPatch Binary Update System"), 0, "Button", "确定") <> Nothing : Loop
                PostMessage(FindWindowEx(FindWindow("#32770", "RTPatch Binary Update System"), 0, "Button", "确定"), WM_LBUTTONDOWN, 0, 0) '模拟点击完成安装按钮
                PostMessage(FindWindowEx(FindWindow("#32770", "RTPatch Binary Update System"), 0, "Button", "确定"), WM_LBUTTONUP, 0, 0)
                Do Until Process.GetProcessesByName("638 SKU1").Length = 0 : Loop '等待补丁安装程序退出
                '询问用户是否使用GOG版本的Graphics Rules.sgr文件
                If MessageBox.Show("是否使用GOG版本的Graphics Rules.sgr文件？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    IO.File.SetAttributes(InstallDir & "\Graphics Rules.sgr", IO.FileAttributes.Normal) '将InstallDir\Graphics Rules.sgr文件设置为正常属性
                    Do Until IsFileUsing(InstallDir & "\Graphics Rules.sgr") = False : Loop
                    My.Computer.FileSystem.CopyFile("Data\Patch\Graphics Rules GOG.sgr", InstallDir & "\Graphics Rules.sgr", True)
                End If
                '通过补丁安装日志内容来判断是否安装成功
                Dim PatchLog As String = My.Computer.FileSystem.ReadAllText("sc4_patchlog.txt")
                My.Computer.FileSystem.DeleteFile("sc4_patchlog.txt") '删除补丁安装日志文件
                Return If(PatchLog.Substring(PatchLog.IndexOf("File Patches Applied ......................... (   ") + 51, 1) = 6, Result.Success, Result.Fail)
            Else '卸载638补丁（回滚至610版本）
                '判断需要修改或读取的文件是否已被其他进程占用
                Dim UsingFiles As String() = {"Data\SC4\NoInstall.7z", InstallDir & "\Apps\SimCity 4.exe", InstallDir & "\SimCity_1.dat", InstallDir & "\SimCity_2.dat",
                                              InstallDir & "\SimCity_3.dat", InstallDir & "\SimCity_4.dat", InstallDir & "\SimCity_5.dat"}
                For Each i As String In UsingFiles : Do Until IsFileUsing(i) = False : Loop : Next
                Dim _7zaProcess As Process = Process.Start(New ProcessStartInfo With {.FileName = "Data\7za.exe", .Arguments = "x Data\SC4\NoInstall.7z ""Apps\SimCity 4.exe"" ""Graphics Rules.sgr"" ""SimCity_*.dat"" -aoa -o""" & InstallDir & """", .Verb = "runas"}) : _7zaProcess.WaitForExit()
                Return If(_7zaProcess.ExitCode = 0, Result.Success, Result.Fail) '判断是否解压成功
            End If
        Catch
            Return Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装或卸载640补丁</summary>
    ''' <param name="InstallDir">指定安装或卸载路径</param>
    ''' <param name="IsUninstall">指定是否卸载640补丁</param>
    ''' <returns>InstallResult.Result的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function Change640Patch(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As Result
        Try
            If IsUninstall = False Then '安装640补丁
                '判断需要修改或读取的文件是否已被其他进程占用
                Dim Files As String() = {InstallDir & "\Apps\SimCity 4.exe", InstallDir & "\SimCity_1.dat"}
                For Each i As String In Files : Do Until IsFileUsing(i) = False : Loop : Next
                My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4.exe\SimCity 4 638.exe", InstallDir & "\Apps\SimCity 4.exe", True) '卸载4GB补丁
                '如果用户同意使用638版本的SimCity_1.dat文件则先备份638版本的SimCity_1.dat文件
                Dim IsUse638File As DialogResult = MessageBox.Show("是否使用638版本的SimCity_1.dat文件？" & vbCrLf & "使用638版本的SimCity_1.dat文件可以避免可能出现的细节缺失显示问题", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If IsUse638File = DialogResult.Yes Then My.Computer.FileSystem.CopyFile(InstallDir & "\SimCity_1.dat", InstallDir & "\SimCity_1 638.dat")
                '通过模拟点击来自动安装640补丁
                Process.Start("Data\Patch\640.exe").WaitForInputIdle()
                Do Until FindWindowEx(FindWindow("#32770", "SimCity 4 EP1 Update 2"), 0, "Button", "确定") <> Nothing : Loop
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 EP1 Update 2"), 0, "Button", "确定"), WM_LBUTTONDOWN, 0, 0) '模拟点击确认安装按钮
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 EP1 Update 2"), 0, "Button", "确定"), WM_LBUTTONUP, 0, 0)
                Do Until FindWindowEx(FindWindow("#32770", "RTPatch Binary Update System"), 0, "Button", "确定") <> Nothing : Loop
                PostMessage(FindWindowEx(FindWindow("#32770", "RTPatch Binary Update System"), 0, "Button", "确定"), WM_LBUTTONDOWN, 0, 0) '模拟点击完成安装按钮
                PostMessage(FindWindowEx(FindWindow("#32770", "RTPatch Binary Update System"), 0, "Button", "确定"), WM_LBUTTONUP, 0, 0)
                Do Until Process.GetProcessesByName("640").Length = 0 : Loop '等待补丁安装程序退出
                '如果用户同意使用638版本的SimCity_1.dat文件则将之前备份的638版本的SimCity_1.dat文件替换原文件
                If IsUse638File = DialogResult.Yes Then My.Computer.FileSystem.MoveFile(InstallDir & "\SimCity_1 638.dat", InstallDir & "\SimCity_1.dat", True)
                '通过安装日志内容来判断是否安装成功
                Dim PatchLog As String = My.Computer.FileSystem.ReadAllText("sc4_patchlog.txt")
                My.Computer.FileSystem.DeleteFile("sc4_patchlog.txt") '删除补丁安装日志文件
                Return If(PatchLog.Substring(PatchLog.IndexOf("File Patches Applied ......................... (   ") + 51, 1) = 2, Result.Success, Result.Fail)
            Else '卸载640补丁（回滚至638版本）
                If Change638Patch(InstallDir, True) = Result.Fail Then Return Result.Fail Else Return Change638Patch(InstallDir, False) '先卸载638补丁以回滚至610版本然后再安装638补丁以回滚至638版本
            End If
        Catch
            Return Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装或卸载641补丁</summary>
    ''' <param name="InstallDir">指定安装或卸载路径</param>
    ''' <param name="IsUninstall">指定是否卸载641补丁</param>
    ''' <returns>InstallResult.Result的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function Change641Patch(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As Result
        Try
            If IsUninstall = False Then '安装641补丁
                '判断需要修改或读取的文件是否已被其他进程占用
                Dim Files As String() = {InstallDir & "\Apps\SimCity 4.exe", InstallDir & "\SimCity_1.dat"}
                For Each i As String In Files : Do Until IsFileUsing(i) = False : Loop : Next
                Dim _7zaProcess As Process = Process.Start(New ProcessStartInfo With {.FileName = "Data\7za.exe", .Arguments = "x Data\Patch\641.7z -aoa -o""" & InstallDir & """", .Verb = "runas"}) : _7zaProcess.WaitForExit()
                Return If(_7zaProcess.ExitCode = 0, Result.Success, Result.Fail) '判断是否解压成功
            Else '卸载641补丁（回滚至640版本）
                If Change640Patch(InstallDir, True) = Result.Fail Then Return Result.Fail Else Return Change640Patch(InstallDir, False) '先卸载640补丁以回滚至638版本然后再安装640补丁以回滚至640版本
            End If
        Catch
            Return Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装或卸载4GB补丁</summary>
    ''' <param name="InstallDir">指定安装或卸载路径</param>
    ''' <param name="IsUninstall">指定是否卸载4GB补丁</param>
    ''' <param name="ChangeOptions">一个InstallOptions类实例，用于判断应回滚至什么版本</param>
    ''' <returns>InstallResult.Result的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function Change4GBPatch(ByVal InstallDir As String, ByVal IsUninstall As Boolean, ByVal ChangeOptions As ChangeOptions) As Result
        Try
            Do Until IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") = False : Loop '判断需要修改或读取的文件是否已被其他进程占用
            Dim MD5CSP As New Security.Cryptography.MD5CryptoServiceProvider, SC4exeFileMD5 As String
            If IsUninstall = False Then '安装4GB补丁
                Process.Start(New ProcessStartInfo With {.FileName = "Data\Patch\4GB.exe", .Arguments = """" & InstallDir & "\Apps\SimCity 4.exe""", .Verb = "runas"}).WaitForExit()
                '通过验证InstallDir\Apps\SimCity 4.exe文件的MD5值来确定是否成功安装4GB补丁
                Using SC4exeFileStream As New IO.FileStream(InstallDir & "\Apps\SimCity 4.exe", IO.FileMode.Open)
                    SC4exeFileMD5 = BitConverter.ToString(MD5CSP.ComputeHash(SC4exeFileStream)).Replace("-", "")
                End Using
                Return If(SC4exeFileMD5 = "78202C3EF76988BD2BF05F8D223BE7A3" OrElse SC4exeFileMD5 = "2F2BD7D9A76E85320A26D7BD7530DCAE" OrElse SC4exeFileMD5 = "1C18B7DC760EDADD2C2EFAF33F60F150" OrElse
                          SC4exeFileMD5 = "1414E70EB5CE22DB37D22CB99439D012" OrElse SC4exeFileMD5 = "AADC5464919FBDC0F8E315FA51582126", Result.Success, Result.Fail)
            Else '卸载4GB补丁
                Dim SC4Version As String '声明一个用于存储SC4当前版本的字符串变量
                With ChangeOptions '判断SC4的版本
                    If (._638PatchOption = ChangeOption.Install OrElse (._638PatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.Is638PatchInstalled)) AndAlso
                        (._640PatchOption = ChangeOption.Uninstall OrElse (._640PatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.Is640PatchInstalled = False)) AndAlso
                        (._641PatchOption = ChangeOption.Uninstall OrElse (._641PatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.Is641PatchInstalled = False)) Then : SC4Version = "638"
                    ElseIf (._638PatchOption = ChangeOption.Install OrElse (._638PatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.Is638PatchInstalled)) AndAlso
                        (._640PatchOption = ChangeOption.Install OrElse (._640PatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.Is640PatchInstalled)) AndAlso
                        (._641PatchOption = ChangeOption.Uninstall OrElse (._641PatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.Is641PatchInstalled = False)) Then : SC4Version = "640"
                    ElseIf (._638PatchOption = ChangeOption.Install OrElse (._638PatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.Is638PatchInstalled)) AndAlso
                        (._640PatchOption = ChangeOption.Install OrElse (._640PatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.Is640PatchInstalled)) AndAlso
                        (._641PatchOption = ChangeOption.Install OrElse (._641PatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.Is641PatchInstalled)) Then : SC4Version = "641"
                    ElseIf (._638PatchOption = ChangeOption.Uninstall OrElse (._638PatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.Is638PatchInstalled = False)) AndAlso
                        (._640PatchOption = ChangeOption.Uninstall OrElse (._640PatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.Is640PatchInstalled = False)) AndAlso
                        (._641PatchOption = ChangeOption.Uninstall OrElse (._641PatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.Is641PatchInstalled = False)) Then : SC4Version = "610"
                    ElseIf .NoCDPatchOption = ChangeOption.Install OrElse (.NoCDPatchOption = ChangeOption.Unchanged AndAlso ModuleDeclare.InstalledModule.IsNoCDPatchInstalled) Then : SC4Version = "NoCD"
                    Else SC4Version = Nothing
                    End If
                End With
                '根据SC4的版本来判断如何卸载4GB补丁
                Select Case SC4Version
                    Case "638" : My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4.exe\SimCity 4 638.exe", InstallDir & "\Apps\SimCity 4.exe", True)
                    Case "640" : My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4.exe\SimCity 4 640.exe", InstallDir & "\Apps\SimCity 4.exe", True)
                    Case "641" : My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4.exe\SimCity 4 641.exe", InstallDir & "\Apps\SimCity 4.exe", True)
                    Case "610" : My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4.exe\SimCity 4 610.exe", InstallDir & "\Apps\SimCity 4.exe", True)
                    Case "NoCD" : Return ChangeNoCDPatch(InstallDir, False) '直接调用安装免CD补丁的方法
                End Select
                '通过验证InstallDir\Apps\SimCity 4.exe文件的MD5值来确定是否成功安装4GB补丁
                Using SC4exeFileStream As New IO.FileStream(InstallDir & "\Apps\SimCity 4.exe", IO.FileMode.Open)
                    SC4exeFileMD5 = BitConverter.ToString(MD5CSP.ComputeHash(SC4exeFileStream)).Replace("-", "")
                End Using
                Select Case SC4Version
                    Case "638" : Return If(SC4exeFileMD5 = "9ACB71D6D2302158CA614B21A9B187E4", Result.Success, Result.Fail)
                    Case "640" : Return If(SC4exeFileMD5 = "D4796905AAFF2B2DE44C2B59D103F5EA", Result.Success, Result.Fail)
                    Case "641" : Return If(SC4exeFileMD5 = "53D2AE4FA9114B88AD91ECF32A7F16A4", Result.Success, Result.Fail)
                    Case "610" : Return If(SC4exeFileMD5 = "427BE3767B1B20866F42D6197EA67AF0", Result.Success, Result.Fail)
                    Case Else : Return Nothing
                End Select
            End If
        Catch
            Return Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装或卸载免CD补丁</summary>
    ''' <param name="InstallDir">指定安装或卸载路径</param>
    ''' <param name="IsUninstall">指定是否卸载免CD补丁</param>
    ''' <returns>InstallResult.Result的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function ChangeNoCDPatch(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As Result
        Try
            If IsUninstall = False Then '安装免CD补丁
                Do Until IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") = False : Loop '判断需要修改或读取的文件是否已被其他进程占用
                My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4.exe\SimCity 4 NoCD.exe", InstallDir & "\Apps\SimCity 4.exe", True)
                '通过验证InstallDir\Apps\SimCity 4.exe文件的MD5值来确定是否成功安装免CD补丁
                Dim MD5CSP As New Security.Cryptography.MD5CryptoServiceProvider, SC4exeFileMD5 As String
                Using SC4exeFileStream As New IO.FileStream(InstallDir & "\Apps\SimCity 4.exe", IO.FileMode.Open)
                    SC4exeFileMD5 = BitConverter.ToString(MD5CSP.ComputeHash(SC4exeFileStream)).Replace("-", "")
                End Using
                Return If(SC4exeFileMD5 = "B57B5B03C4854C194CE8BEBD173F3483", Result.Success, Result.Fail)
            Else '卸载免CD补丁
                Return Change638Patch(InstallDir, True) '直接调用卸载638补丁的方法以回滚至610版本
            End If
        Catch
            Return Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装或卸载模拟城市4 启动器</summary>
    ''' <param name="InstallDir">指定安装或卸载路径</param>
    ''' <param name="IsUninstall">指定是否卸载模拟城市4 启动器</param>
    ''' <returns>InstallResult.Result的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function ChangeSC4Launcher(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As Result
        Try
            If IsUninstall = False Then '安装模拟城市4 启动器
                My.Computer.FileSystem.CopyFile("Data\Patch\SC4Launcher.exe", InstallDir & "\SC4Launcher.exe", True)
                Return If(My.Computer.FileSystem.FileExists(InstallDir & "\SC4Launcher.exe"), Result.Success, Result.Fail)
            Else '卸载模拟城市4 启动器
                Do Until IsFileUsing(InstallDir & "\SC4Launcher.exe") = False : Loop '判断需要修改或读取的文件是否已被其他进程占用
                My.Computer.FileSystem.DeleteFile(InstallDir & "\SC4Launcher.exe")
                Return If(My.Computer.FileSystem.FileExists(InstallDir & "\SC4Launcher.exe") = False, Result.Success, Result.Fail)
            End If
        Catch
            Return Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装指定语言的语言补丁</summary>
    ''' <param name="InstallDir">指定安装路径</param>
    ''' <param name="Language">InstallOptions.Language的值之一，指定要安装的语言补丁的语言</param>
    ''' <returns>InstallResult.Result的值之一，如果安装成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function ChangeLanguage(ByVal InstallDir As String, ByVal Language As ModuleDeclare.SC4Language) As Result
        Try
            With My.Computer.Registry
                '声明一个用于存储模拟城市4语言设置的注册表键名的字符串变量
                Dim LanguageRegKeyName As String = If(Environment.Is64BitOperatingSystem, "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0", "HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0")
                Select Case Language
                    Case SC4Language.TraditionalChinese '安装繁体中文语言补丁
                        My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\TChinese", InstallDir & "\TChinese", True)
                        .SetValue(LanguageRegKeyName, "Language", 18, Microsoft.Win32.RegistryValueKind.DWord) '设置繁体中文语言补丁的注册表值
                        .SetValue(LanguageRegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue(LanguageRegKeyName, "LanguageName", "Chinese (Traditional)", Microsoft.Win32.RegistryValueKind.String)
                        Return If(My.Computer.FileSystem.DirectoryExists(InstallDir & "\TChinese") AndAlso .GetValue(LanguageRegKeyName, "Language", Nothing) = 18, Result.Success, Result.Fail)
                    Case SC4Language.SimplifiedChinese '安装简体中文语言补丁
                        My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\SChinese", InstallDir & "\SChinese", True)
                        .SetValue(LanguageRegKeyName, "Language", 17, Microsoft.Win32.RegistryValueKind.DWord) '设置简体中文语言补丁的注册表值
                        .SetValue(LanguageRegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue(LanguageRegKeyName, "LanguageName", "Chinese (Simplified)", Microsoft.Win32.RegistryValueKind.String)
                        Return If(My.Computer.FileSystem.DirectoryExists(InstallDir & "\SChinese") AndAlso .GetValue(LanguageRegKeyName, "Language", Nothing) = 17, Result.Success, Result.Fail)
                    Case SC4Language.English '安装英语语言补丁
                        My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\English", InstallDir & "\English", True)
                        .SetValue(LanguageRegKeyName, "Language", 1, Microsoft.Win32.RegistryValueKind.DWord) '设置英语语言补丁的注册表值
                        .SetValue(LanguageRegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue(LanguageRegKeyName, "LanguageName", "English US", Microsoft.Win32.RegistryValueKind.String)
                        Return If(My.Computer.FileSystem.DirectoryExists(InstallDir & "\English") AndAlso .GetValue(LanguageRegKeyName, "Language", Nothing) = 1, Result.Success, Result.Fail)
                    Case Else : Return Nothing
                End Select
            End With
        Catch
            Return Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在桌面上添加一个快捷方式</summary>
    ''' <param name="SC4InstallDir">指定模拟城市4的安装路径</param>
    ''' <param name="IsLauncherInstalled">指定是否将快捷方式导向模拟城市4 启动器</param>
    ''' <returns>InstallResult.Result的值之一，如果添加成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function AddDestopIcon(ByVal SC4InstallDir As String, ByVal IsLauncherInstalled As Boolean) As Result
        Try
            '声明一个IWshRuntimeLibrary.WshShell接口的实例和一个IWshRuntimeLibrary.IWshShortcut接口的实例（引用自Windows Script Host Object Model）
            Dim wshshell As New IWshRuntimeLibrary.WshShell, shortcut As IWshRuntimeLibrary.IWshShortcut
            Dim DesktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) '声明一个用于存储公用桌面目录路径的字符串变量
            '设置快捷方式的名称、目标和图标并保存快捷方式
            shortcut = wshshell.CreateShortcut(DesktopPath & If(IsLauncherInstalled, "\模拟城市4 启动器.lnk", "\模拟城市4 豪华版.lnk"))
            shortcut.TargetPath = SC4InstallDir & If(IsLauncherInstalled, "\SC4Launcher.exe", "\Apps\SimCity 4.exe")
            shortcut.IconLocation = SC4InstallDir & "\SC4.ico" : shortcut.Save()
            Return If(IsLauncherInstalled, If(My.Computer.FileSystem.FileExists(DesktopPath & "\模拟城市4 启动器.lnk"), Result.Success, Result.Fail),
                      If(My.Computer.FileSystem.FileExists(DesktopPath & "\模拟城市4 豪华版.lnk"), Result.Success, Result.Fail))
        Catch
            Return Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在开始菜单\Maxis\SimCity 4 Deluxe文件夹内添加快捷方式</summary>
    ''' <param name="SC4InstallDir">指定模拟城市4的安装路径</param>
    ''' <param name="IsLauncherInstalled">指定是否将快捷方式导向模拟城市4 启动器</param>
    ''' <returns>InstallResult.Result的值之一，如果添加成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function AddStartMenuItems(ByVal SC4InstallDir As String, ByVal IsLauncherInstalled As Boolean) As Result
        Try
            '声明一个IWshRuntimeLibrary.WshShell接口的实例和一个IWshRuntimeLibrary.IWshShortcut接口的实例（引用自Windows Script Host Object Model）
            Dim wshshell As New IWshRuntimeLibrary.WshShell, shortcut As IWshRuntimeLibrary.IWshShortcut
            '声明一个用于存储公用开始菜单\程序\Maxis\SimCity 4 Deluxe目录路径的字符串变量
            Dim StartMenuPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) & "\Programs\Maxis\SimCity 4 Deluxe"
            My.Computer.FileSystem.CreateDirectory(StartMenuPath) '创建公用开始菜单\程序\Maxis\SimCity 4 Deluxe文件夹
            '设置快捷方式的名称、目标和图标并保存快捷方式
            shortcut = wshshell.CreateShortcut(StartMenuPath & If(IsLauncherInstalled, "\模拟城市4 启动器.lnk", "\模拟城市4 豪华版.lnk"))
            shortcut.TargetPath = SC4InstallDir & If(IsLauncherInstalled, "\SC4Launcher.exe", "\Apps\SimCity 4.exe")
            shortcut.IconLocation = SC4InstallDir & "\SC4.ico" : shortcut.Save()
            '设置另一个快捷方式的名称、目标和图标并保存快捷方式
            shortcut = wshshell.CreateShortcut(StartMenuPath & "\卸载或更改模拟城市4 豪华版.lnk")
            shortcut.TargetPath = SC4InstallDir & "\Setup.exe" : shortcut.IconLocation = SC4InstallDir & "\Setup.exe" : shortcut.Save()
            If My.Computer.FileSystem.FileExists(StartMenuPath & "\卸载或更改模拟城市4 豪华版.lnk") = False Then Return Result.Fail
            Return If(IsLauncherInstalled, If(My.Computer.FileSystem.FileExists(StartMenuPath & "\模拟城市4 启动器.lnk"), Result.Success, Result.Fail),
                      If(My.Computer.FileSystem.FileExists(StartMenuPath & "\模拟城市4 豪华版.lnk"), Result.Success, Result.Fail))
        Catch
            Return Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>递归返回某个文件夹内所有的文件和文件夹的大小</summary>
    ''' <param name="path">要查询的文件夹的路径</param>
    ''' <returns>返回文件夹内所有的文件和文件夹的大小</returns>
    Private Function GetFolderSize(ByVal path As String) As Long
        Dim size As Long
        For Each i As IO.FileInfo In My.Computer.FileSystem.GetDirectoryInfo(path).GetFiles
            size += i.Length
        Next
        For Each i As IO.DirectoryInfo In My.Computer.FileSystem.GetDirectoryInfo(path).GetDirectories
            size += GetFolderSize(i.FullName) '递归返回子文件夹的大小
        Next
        Return size
    End Function

    ''' <summary>在控制面板的卸载或更改程序里添加模拟城市4 豪华版 自动安装程序项</summary>
    ''' <param name="SC4InstallDir">模拟城市4的安装路径</param>
    Public Sub SetControlPanelProgramItemRegValue(ByVal SC4InstallDir As String)
        '声明一个用于存储控制面板的卸载或更改程序里的模拟城市4 豪华版 自动安装程序项的注册表键名的字符串变量
        Dim ProgramItemRegKeyName As String = If(Environment.Is64BitOperatingSystem, "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SC4AutoInstaller",
                                                 "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\SC4AutoInstaller")
        With My.Computer.Registry
            '在控制面板的卸载或更改程序里添加模拟城市4 豪华版 自动安装程序项
            .SetValue(ProgramItemRegKeyName, "DisplayIcon", SC4InstallDir & "\SC4.ico", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "DisplayName", "模拟城市4 豪华版 自动安装程序", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "DisplayVersion", My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision, Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "EstimatedSize", GetFolderSize(SC4InstallDir) / 1024, Microsoft.Win32.RegistryValueKind.DWord)
            .SetValue(ProgramItemRegKeyName, "InstallLocation", SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "Publisher", "n0099", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "UninstallString", SC4InstallDir & "\Setup.exe", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "URLInfoAbout", "http://tieba.baidu.com/p/3802761033", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "URLUpdateInfo", "http://n0099.sinaapp.com", Microsoft.Win32.RegistryValueKind.String)
        End With
    End Sub

    ''' <summary>导入镜像版模拟城市4安装程序所添加或更改的注册表项和值</summary>
    ''' <param name="SC4InstallDir">模拟城市4的安装路径</param>
    Public Sub SetNoInstallSC4RegValue(ByVal SC4InstallDir As String)
        Try
            '声明两个用于存储模拟城市4所产生的注册表键名的字符串变量
            Dim SC4RegKeyName As String = If(Environment.Is64BitOperatingSystem, "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4")
            Dim ergcRegKeyName As String = If(Environment.Is64BitOperatingSystem, "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", "HKEY_LOCAL_MACHINE\SOFTWARE\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc")
            With My.Computer.Registry '导入镜像版模拟城市4安装程序所添加或更改的注册表项和值
                .SetValue(ergcRegKeyName, "", "CX9H498AMHSS8QXDTXJB", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "CacheSize", 1196879, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue(SC4RegKeyName, "CD Drive", ".\\", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Install Dir", SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Installed From", ".\\", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "IsDeluxe", 1, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue(SC4RegKeyName, "Language", 1, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue(SC4RegKeyName, "Locale", "en-us", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Patch URL", "http://simcity.ea.com/update/", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Product GUID", "{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Region", "NA", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Registration", ergcRegKeyName, Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "SwapSize", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue(SC4RegKeyName, "Folder", Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) & "\Programs\Maxis\SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName & "\EP1", "", "5ZH4HSUIYKHTPFPN7Q30", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "", SC4InstallDir & "\Apps\SimCity 4.exe", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "Path", SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "Restart", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "Game Registry", SC4RegKeyName.Replace("\1.0", ""), Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap", "UNCAsIntranet", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap", "AutoDetect", 1, Microsoft.Win32.RegistryValueKind.DWord)
                .CurrentUser.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\ProxyBypass")
                .CurrentUser.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\IntranetName")
                .LocalMachine.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\ProxyBypass")
                .LocalMachine.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\IntranetName")
            End With
        Catch
        End Try
    End Sub

End Module