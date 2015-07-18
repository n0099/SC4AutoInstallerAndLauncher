Module ModuleInstallModule

    Private Function IsFileUsing(path As String) As Boolean
        Try
            IO.File.Open(path, IO.FileMode.Open).Close() : Return False
        Catch ex As Exception
            Return True
        End Try
    End Function

    Public Function InstallDAEMONTools() As InstallResult.Result
        Try
            Do Until Not IsFileUsing("Data\DAEMON Tools Lite 5.0.exe") : Loop
            Process.Start("Data\DAEMON Tools Lite 5.0.exe", "/S /nogadget /path """ & ModuleMain.InstallOptions.DAEMONInstallDir & """").WaitForExit()
            Return IIf(My.Computer.FileSystem.FileExists(ModuleMain.InstallOptions.DAEMONInstallDir & "\DTLite.exe") = True, InstallResult.Result.Success, InstallResult.Result.Fail)
        Catch ex As Exception
            Return InstallResult.Result.Fail
        End Try
    End Function

    Public Function InstallSC4(ByVal InstallType As InstallOptions.SC4InstallType) As InstallResult.Result
        Try
            My.Computer.FileSystem.CopyFile(Application.ExecutablePath, ModuleMain.InstallOptions.SC4InstallDir & "\Setup.exe", True)
            If InstallType = InstallOptions.SC4InstallType.ISO Then
                If ModuleMain.InstallResult.DAEMONToolsInstallResult = InstallResult.Result.Success Then
                    Do Until My.Computer.FileSystem.FileExists("X:\AutoRun.exe")
                        Process.Start(ModuleMain.InstallOptions.DAEMONInstallDir & "\DTLite.exe", "-mount dt, X, """ & Environment.CurrentDirectory & "\Data\CD\SC4DELUXE CD1.mdf""")
                    Loop
                    Do Until My.Computer.FileSystem.FileExists("Y:\RunGame.exe")
                        Process.Start(ModuleMain.InstallOptions.DAEMONInstallDir & "\DTLite.exe", "-mount dt, Y, """ & Environment.CurrentDirectory & "\Data\CD\SC4DELUXE CD2.mdf""")
                    Loop

                    Dim AutoRunProcess() As Process = Process.GetProcessesByName("AutoRun")
                    Dim SC4CodeProcess() As Process = Process.GetProcessesByName("SimCity 4 Deluxe_Code")
                    Dim SC4eRegProcess() As Process = Process.GetProcessesByName("SimCity 4 Deluxe_eReg")
                    If AutoRunProcess.Length <> 0 Then AutoRunProcess(0).Kill()
                    If SC4CodeProcess.Length <> 0 Then SC4CodeProcess(0).Kill()
                    If SC4eRegProcess.Length <> 0 Then SC4eRegProcess(0).Kill()

                    Dim tempfolder As String = Environment.GetEnvironmentVariable("TEMP")
                    If My.Computer.FileSystem.FileExists(tempfolder & "\AutoRun.exe") = False Or My.Computer.FileSystem.FileExists(tempfolder & "\AutoRunGUI.dll") = False Then
                        My.Computer.FileSystem.CopyFile("X:\AutoRun.exe", tempfolder & "\AutoRun.exe", FileIO.UIOption.OnlyErrorDialogs, FileIO.UICancelOption.DoNothing)
                        My.Computer.FileSystem.CopyFile("X:\AutoRunGUI.dll", tempfolder & "\AutoRunGUI.dll", FileIO.UIOption.OnlyErrorDialogs, FileIO.UICancelOption.DoNothing)
                        Process.Start("regsvr32.exe", "/s """ & tempfolder & "\AutoRunGUI.dll""").WaitForExit()
                    End If
                    Process.Start(tempfolder & "\AutoRun.exe", "-restart -dir X:\")

                    Do Until FindWindow("#32770", "SimCity 4 Deluxe") <> Nothing : Loop
                    PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "Install"), WM_LBUTTONDOWN, 0, 0)
                    PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "Install"), WM_LBUTTONUP, 0, 0)
                    Threading.Thread.Sleep(100) : Do Until FindWindow("#32770", "SimCity 4 Deluxe") <> Nothing : Loop
                    Dim key() As String = {"C", "X", "9", "H", "4", "9", "8", "A", "M", "H", "S", "S", "8", "Q", "X", "D", "T", "X", "J", "B"}
                    For Each i As String In key
                        SetWindowPos(FindWindow("#32770", "SimCity 4 Deluxe"), HWND_TOP, 0, 0, 0, 0, SWP_NOSIZE + SWP_NOMOVE)
                        SendKeys.SendWait(i)
                    Next
                    PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "&Next>"), WM_LBUTTONDOWN, 0, 0)
                    PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "&Next>"), WM_LBUTTONUP, 0, 0)
                    Threading.Thread.Sleep(100) : Do Until FindWindow("#32770", "SimCity 4 Deluxe") <> Nothing : Loop
                    Dim installdir As String = "G:\SC4"
                    SendMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Edit", ""), WM_SETTEXT, 0, installdir)
                    PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "&Next>"), WM_LBUTTONDOWN, 0, 0)
                    PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "&Next>"), WM_LBUTTONUP, 0, 0)
                    Threading.Thread.Sleep(100) : Do Until FindWindow("#32770", "Electronic Registration") <> Nothing : Loop : Threading.Thread.Sleep(100)
                    PostMessage(FindWindowEx(FindWindow("#32770", "Electronic Registration"), 0, "Button", "Register Later"), WM_LBUTTONDOWN, 0, 0)
                    PostMessage(FindWindowEx(FindWindow("#32770", "Electronic Registration"), 0, "Button", "Register Later"), WM_LBUTTONUP, 0, 0)
                    Threading.Thread.Sleep(100) : Do Until FindWindow("#32770", "") <> Nothing : Loop
                    PostMessage(FindWindowEx(FindWindow("#32770", ""), 0, "Button", "Ok"), WM_LBUTTONDOWN, 0, 0)
                    PostMessage(FindWindowEx(FindWindow("#32770", ""), 0, "Button", "Ok"), WM_LBUTTONUP, 0, 0)

                    Do Until Process.GetProcessesByName("SimCity 4.exe").Length <> 0 : Loop
                    Process.GetProcessesByName("SimCity 4.exe")(0).Kill()

                    Dim RARProcess As Process = Process.Start("Data\rar.exe", "x Data\SC4.rar -o+ ""Graphics Rules.sgr"" """ & installdir & "\""") : RARProcess.WaitForExit()
                    My.Computer.FileSystem.CopyFile(Process.GetCurrentProcess.StartInfo.FileName, installdir & "\SC4AutoInstaller.exe")
                    Return IIf(RARProcess.ExitCode = 0 Or My.Computer.FileSystem.FileExists(installdir & "\Apps\SimCity 4.exe") = True, InstallResult.Result.Success, InstallResult.Result.Fail)
                Else
                    Return InstallResult.Result.Fail
                End If
            ElseIf InstallType = InstallOptions.SC4InstallType.NoInstall Then
                Do Until Not IsFileUsing("Data\SC4.rar") : Loop
                Dim RARProcess As Process = Process.Start("Data\rar.exe", "x Data\SC4.rar *.* -o+ """ & ModuleMain.InstallOptions.SC4InstallDir & "\""") : RARProcess.WaitForExit()
                Return IIf(RARProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
            End If
        Catch ex As Exception
            Return InstallResult.Result.Fail
        End Try
    End Function

    Public Function Install638Patch(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As InstallResult.Result
        Try
            Dim RARProcess As Process
            If IsUninstall = False Then
                Do Until Not IsFileUsing("Data\Patch\638.rar") : Loop
                RARProcess = Process.Start("Data\rar.exe", "x Data\Patch\638.rar *.* -o+ """ & InstallDir & "\""") : RARProcess.WaitForExit()
            Else
                Do Until Not IsFileUsing("Data\SC4.rar") : Loop
                RARProcess = Process.Start("Data\rar.exe", "x Data\SC4.rar ""Apps\SimCity 4.exe"" SimCity_*.dat -o+ """ & InstallDir & "\""") : RARProcess.WaitForExit()
            End If
            Return IIf(RARProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
        Catch ex As Exception
            Return InstallResult.Result.Fail
        End Try
    End Function

    Public Function Install640Patch(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As InstallResult.Result
        Try
            Dim RARProcess As Process
            If IsUninstall = False Then
                Do Until Not IsFileUsing("Data\Patch\640.rar") : Loop
                RARProcess = Process.Start("Data\rar.exe", "x Data\Patch\640.rar *.* -o+ """ & InstallDir & "\""") : RARProcess.WaitForExit()
            Else
                Do Until Not IsFileUsing("Data\Patch\638.rar") : Loop
                RARProcess = Process.Start("Data\rar.exe", "x Data\Patch\638.rar *.* -o+ """ & InstallDir & "\""") : RARProcess.WaitForExit()
            End If
            Return IIf(RARProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
        Catch ex As Exception
            Return InstallResult.Result.Fail
        End Try
    End Function

    Public Function Install641Patch(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As InstallResult.Result
        Try
            If IsUninstall = False Then
                Do Until Not IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") : Loop
                My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4 641.exe", InstallDir & "\Apps\SimCity 4.exe", True)
                Return IIf(My.Computer.FileSystem.GetFileInfo(InstallDir & "\Apps\SimCity 4.exe").Length = 7524352, InstallResult.Result.Success, InstallResult.Result.Fail)
            Else
                Dim RARProcess As Process
                Do Until Not IsFileUsing("Data\Patch\640.rar") : Loop
                RARProcess = Process.Start("Data\rar.exe", "x Data\Patch\640.rar *.* -o+ """ & InstallDir & "\""") : RARProcess.WaitForExit()
                Return IIf(RARProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
            End If
        Catch ex As Exception
            Return InstallResult.Result.Fail
        End Try
    End Function

    Public Function Install4GBPatch(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As InstallResult.Result
        With ModuleMain.InstallOptions
            Try
                If IsUninstall = False Then
                    Do Until Not IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") : Loop
                    Process.Start("Data\Patch\4GB.exe", """" & InstallDir & "\Apps\SimCity 4.exe""").WaitForExit()
                    Dim MD5CSP As New Security.Cryptography.MD5CryptoServiceProvider, MD5 As String
                    MD5 = BitConverter.ToString(MD5CSP.ComputeHash(New IO.FileStream(InstallDir & "\Apps\SimCity 4.exe", IO.FileMode.Open))).Replace("-", "")
                    Return IIf(MD5 = "78202C3EF76988BD2BF05F8D223BE7A3" Or MD5 = "2F2BD7D9A76E85320A26D7BD7530DCAE" Or MD5 = "1C18B7DC760EDADD2C2EFAF33F60F150" _
                               Or MD5 = "1414E70EB5CE22DB37D22CB99439D012" Or MD5 = "AADC5464919FBDC0F8E315FA51582126", InstallResult.Result.Success, InstallResult.Result.Fail)
                Else
                    Dim RARProcess As Process = Nothing
                    If .IsInstall638Patch = True Then
                        Do Until Not IsFileUsing(InstallDir & "Data\Patch\638.rar") : Loop
                        RARProcess = Process.Start("Data\rar.exe", "x Data\Patch\638.rar ""Apps\SimCity 4.exe"" -o+ """ & InstallDir & "\""") : RARProcess.WaitForExit()
                        Return IIf(RARProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
                    ElseIf .IsInstall640Patch = True And .IsInstall638Patch = True Then
                        Do Until Not IsFileUsing(InstallDir & "Data\Patch\640.rar") : Loop
                        RARProcess = Process.Start("Data\rar.exe", "x Data\Patch\640.rar ""Apps\SimCity 4.exe"" -o+ """ & InstallDir & "\""") : RARProcess.WaitForExit()
                        Return IIf(RARProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
                    ElseIf .IsInstall641Patch = True And .IsInstall640Patch = True And .IsInstall638Patch = True Then
                        Do Until Not IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") : Loop
                        My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4 641.exe", InstallDir & "\Apps\SimCity 4.exe", True)
                        Return IIf(My.Computer.FileSystem.GetFileInfo(InstallDir & "\Apps\SimCity 4.exe").Length = 7524352, InstallResult.Result.Success, InstallResult.Result.Fail)
                    ElseIf .IsInstallNoCDPatch = True Then
                        Do Until Not IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") : Loop
                        My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4 NoCD.exe", InstallDir & "\Apps\SimCity 4.exe", True)
                        Return IIf(My.Computer.FileSystem.GetFileInfo(InstallDir & "\Apps\SimCity 4.exe").Length = 7524352, InstallResult.Result.Success, InstallResult.Result.Fail)
                    ElseIf .IsInstall638Patch = False And .IsInstall640Patch = False And .IsInstall641Patch = False And .IsInstallNoCDPatch = False Then
                        Do Until Not IsFileUsing("Data\SC4.rar") : Loop
                        RARProcess = Process.Start("Data\rar.exe", "x Data\SC4.rar ""Apps\SimCity 4.exe"" SimCity_*.dat -o+ """ & InstallDir & "\""") : RARProcess.WaitForExit()
                        Return IIf(RARProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
                    End If
                End If
            Catch ex As Exception
                Return InstallResult.Result.Fail
            End Try
        End With
    End Function

    Public Function InstallNoCDPatch(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As InstallResult.Result
        Try
            If IsUninstall = False Then
                Do Until Not IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") : Loop
                My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4 NoCD.exe", InstallDir & "\Apps\SimCity 4.exe", True)
                Return IIf(My.Computer.FileSystem.GetFileInfo(InstallDir & "\Apps\SimCity 4.exe").Length = 7524352, InstallResult.Result.Success, InstallResult.Result.Fail)
            Else
                Do Until Not IsFileUsing("Data\SC4.rar") : Loop
                Dim RARProcess As Process = Process.Start("Data\rar.exe", "x Data\SC4.rar ""Apps\SimCity 4.exe"" SimCity_*.dat -o+ """ & InstallDir & "\""") : RARProcess.WaitForExit()
                Return IIf(RARProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
            End If
        Catch ex As Exception
            Return InstallResult.Result.Fail
        End Try
    End Function

    Public Function InstallSC4Launcher(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As InstallResult.Result
        Try
            If IsUninstall = False Then
                My.Computer.FileSystem.CopyFile("Data\SC4Launcher.exe", InstallDir & "\SC4Launcher.exe", True)
                Return IIf(My.Computer.FileSystem.FileExists(InstallDir & "\SC4Launcher.exe") = True, InstallResult.Result.Success, InstallResult.Result.Fail)
            Else
                Do Until Not IsFileUsing(InstallDir & "\SC4Launcher.exe") : Loop
                My.Computer.FileSystem.DeleteFile(InstallDir & "\SC4Launcher.exe")
                Return IIf(My.Computer.FileSystem.FileExists(InstallDir & "\SC4Launcher.exe") = False, InstallResult.Result.Success, InstallResult.Result.Fail)
            End If
        Catch ex As Exception
            Return InstallResult.Result.Fail
        End Try
    End Function

    Public Function InstallLanguagePatch(ByVal InstallDir As String, ByVal LanguagePatch As InstallOptions.Language) As InstallResult.Result
        With My.Computer.Registry
            Dim LanguageRegKeyName As String = Nothing
            If Environment.Is64BitOperatingSystem = True Then LanguageRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0"
            If Environment.Is64BitOperatingSystem = False Then LanguageRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0"
            Try
                Select Case LanguagePatch
                    Case InstallOptions.Language.TraditionalChinese
                        My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\TChinese", InstallDir, True)
                        .SetValue(LanguageRegKeyName, "Language", 18, Microsoft.Win32.RegistryValueKind.DWord)
                        .SetValue(LanguageRegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue(LanguageRegKeyName, "LanguageName", "Chinese (Traditional)", Microsoft.Win32.RegistryValueKind.String)
                        Return IIf(My.Computer.FileSystem.DirectoryExists(InstallDir & "\TChinese") = True And _
                            My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) = 18, InstallResult.Result.Success, InstallResult.Result.Fail)
                    Case InstallOptions.Language.SimplifiedChinese
                        My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\SChinese", InstallDir, True)
                        .SetValue(LanguageRegKeyName, "Language", 17, Microsoft.Win32.RegistryValueKind.DWord)
                        .SetValue(LanguageRegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue(LanguageRegKeyName, "LanguageName", "Chinese (Simplified)", Microsoft.Win32.RegistryValueKind.String)
                        Return IIf(My.Computer.FileSystem.DirectoryExists(InstallDir & "\SChinese") = True And _
                            My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) = 17, InstallResult.Result.Success, InstallResult.Result.Fail)
                    Case Else
                        My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\English", InstallDir, True)
                        .SetValue(LanguageRegKeyName, "Language", 1, Microsoft.Win32.RegistryValueKind.DWord)
                        .SetValue(LanguageRegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue(LanguageRegKeyName, "LanguageName", "English US", Microsoft.Win32.RegistryValueKind.String)
                        Return IIf(My.Computer.FileSystem.DirectoryExists(InstallDir & "\English") = True And _
                            My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) = 1, InstallResult.Result.Success, InstallResult.Result.Fail)
                End Select
            Catch ex As Exception
                Return InstallResult.Result.Fail
            End Try
        End With
    End Function

    Public Sub AddDestopIcon()
        Dim wshshell As New IWshRuntimeLibrary.WshShell
        Dim shortcut As IWshRuntimeLibrary.IWshShortcut
        With ModuleMain.InstallOptions
            If My.Computer.FileSystem.FileExists(.SC4InstallDir & "\SC4Launcher.exe") = True Then
                shortcut = wshshell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "\模拟城市4 启动器.lnk")
                shortcut.TargetPath = .SC4InstallDir & "\SC4Launcher.exe" : shortcut.Description = "使用模拟城市4 启动器来运行模拟城市4 豪华版"
            Else
                shortcut = wshshell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "\模拟城市4 豪华版.lnk")
                shortcut.TargetPath = .SC4InstallDir & "\Apps\SimCity 4.exe" : shortcut.Description = "运行模拟城市4 豪华版"
            End If
            shortcut.WindowStyle = 1 : shortcut.IconLocation = .SC4InstallDir & "\SC4.ico" : shortcut.Save()
        End With
    End Sub

    Public Sub AddStartMenuItems()
        Dim wshshell As New IWshRuntimeLibrary.WshShell
        My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "\Maxis\SimCity 4 Deluxe")
        Dim shortcut As IWshRuntimeLibrary.IWshShortcut
        With ModuleMain.InstallOptions
            If My.Computer.FileSystem.FileExists(.SC4InstallDir & "\SC4Launcher.exe") = True Then
                shortcut = wshshell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "\Maxis\SimCity 4 Deluxe\模拟城市4 启动器.lnk")
                shortcut.TargetPath = .SC4InstallDir & "\SC4Launcher.exe" : shortcut.Description = "使用模拟城市4 启动器来运行模拟城市4 豪华版"
            Else
                shortcut = wshshell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "\Maxis\SimCity 4 Deluxe\模拟城市4 豪华版.lnk")
                shortcut.TargetPath = .SC4InstallDir & "\Apps\SimCity 4.exe" : shortcut.Description = "运行模拟城市4 豪华版"
            End If
            shortcut.WindowStyle = 1 : shortcut.IconLocation = .SC4InstallDir & "\SC4.ico" : shortcut.Save()
            shortcut = wshshell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "\Maxis\SimCity 4 Deluxe\卸载或更改模拟城市4 豪华版.lnk")
            shortcut.TargetPath = .SC4InstallDir & "\Setup.exe" : shortcut.Description = "使用模拟城市4 自动安装程序以卸载或更改模拟城市4 豪华版"
            shortcut.WindowStyle = 1 : shortcut.IconLocation = .SC4InstallDir & "\SC4AutoInstaller.exe" : shortcut.Save()
        End With
    End Sub

    Private Function GetFolderSize(ByVal path As String) As Long
        Dim size As Long
        For Each i As IO.FileInfo In My.Computer.FileSystem.GetDirectoryInfo(path).GetFiles
            size += i.Length
        Next
        For Each i As IO.DirectoryInfo In My.Computer.FileSystem.GetDirectoryInfo(path).GetDirectories
            size += GetFolderSize(i.FullName)
        Next
        Return size
    End Function

    Public Sub SetControlPanelProgramItemRegValue()
        Dim ProgramItemRegKeyName As String = Nothing
        If Environment.Is64BitOperatingSystem = True Then ProgramItemRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SC4AutoInstaller"
        If Environment.Is64BitOperatingSystem = False Then ProgramItemRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\SC4AutoInstaller"
        With My.Computer.Registry
            If ModuleMain.InstallOptions.SC4Type = InstallOptions.SC4InstallType.ISO Then .LocalMachine.DeleteSubKeyTree("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", False)
            .SetValue(ProgramItemRegKeyName, "DisplayIcon", ModuleMain.InstallOptions.SC4InstallDir & "\SC4.ico", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "DisplayName", "模拟城市4 豪华版 自动安装程序", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "DisplayVersion", My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision, Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "EstimatedSize", GetFolderSize(ModuleMain.InstallOptions.SC4InstallDir) / 1024, Microsoft.Win32.RegistryValueKind.DWord)
            .SetValue(ProgramItemRegKeyName, "InstallLocation", ModuleMain.InstallOptions.SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "Publisher", "n0099", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "UninstallString", ModuleMain.InstallOptions.SC4InstallDir & "\Setup.exe", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "URLInfoAbout", "http://tieba.baidu.com/p/3802761033", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "URLUpdateInfo", "http://n0099.sinaapp.com", Microsoft.Win32.RegistryValueKind.String)
        End With
    End Sub

    Public Sub SetNoInstallSC4RegValue()
        Try
            Dim SC4RegKeyName As String, ergcRegKeyName As String
            If Environment.Is64BitOperatingSystem = True Then
                SC4RegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0"
                ergcRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc"
            Else
                ergcRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc"
                SC4RegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0"
            End If
            With My.Computer.Registry
                .SetValue(ergcRegKeyName, "(Default)", "CX9H498AMHSS8QXDTXJB", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "CacheSize", 1196879, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue(SC4RegKeyName, "CD Drive", ".\\", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Install Dir", ModuleMain.InstallOptions.SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Installed From", ".\\", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "IsDeluxe", 1, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue(SC4RegKeyName, "Language", 1, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue(SC4RegKeyName, "Locale", "en-us", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Patch URL", "http://simcity.ea.com/update/", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Product GUID", "{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Region", "NA", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Registration", ergcRegKeyName, Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "SwapSize", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue(SC4RegKeyName, "Folder", Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "\模拟城市4 豪华版", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName & "\EP1", "(Default)", "5ZH4HSUIYKHTPFPN7Q30", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "(Default)", ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "Path", ModuleMain.InstallOptions.SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "Restart", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "Game Registry", SC4RegKeyName.Replace("\1.0", ""), Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap", "UNCAsIntranet", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap", "AutoDetect", 1, Microsoft.Win32.RegistryValueKind.DWord)
                .CurrentUser.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\ProxyBypass")
                .CurrentUser.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\IntranetName")
                .LocalMachine.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\ProxyBypass")
                .LocalMachine.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\IntranetName")
            End With
        Catch ex As Exception
        End Try
    End Sub

End Module