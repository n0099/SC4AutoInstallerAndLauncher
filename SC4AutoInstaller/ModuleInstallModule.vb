Module ModuleInstallModule

    Public Function InstallDAEMONTools() As InstallResult.Result
        Process.Start("Data\DAEMON Tools Lite 5.0.exe", "/S /nogadget /path """ & ModuleMain.InstallOptions.DAEMONInstallDir & """").WaitForExit()
        If My.Computer.FileSystem.FileExists(ModuleMain.InstallOptions.DAEMONInstallDir & "\DTLite.exe") = True Then
            Return InstallResult.Result.Success
        Else : On Error Resume Next
            Return InstallResult.Result.Fail
        End If
    End Function

    Public Function InstallSC4(ByVal InstallType As InstallOptions.SC4InstallType) As InstallResult.Result
        With ModuleMain.InstallOptions
            If InstallType = InstallOptions.SC4InstallType.ISO Then
                If ModuleMain.InstallResult.DAEMONToolsInstallResult = InstallResult.Result.Success Then
                    Do Until My.Computer.FileSystem.FileExists("X:\AutoRun.exe")
                        Process.Start(.DAEMONInstallDir & "\DTLite.exe", "-mount dt, X, """ & Environment.CurrentDirectory & "\Data\CD\SC4DELUXE CD1.mdf""")
                    Loop
                    Do Until My.Computer.FileSystem.FileExists("Y:\RunGame.exe")
                        Process.Start(.DAEMONInstallDir & "\DTLite.exe", "-mount dt, Y, """ & Environment.CurrentDirectory & "\Data\CD\SC4DELUXE CD2.mdf""")
                    Loop

                    Dim AutoRunProcess() As Process = Process.GetProcessesByName("AutoRun")
                    Dim SC4CodeProcess() As Process = Process.GetProcessesByName("SimCity 4 Deluxe_Code")
                    Dim SC4eRegProcess() As Process = Process.GetProcessesByName("SimCity 4 Deluxe_eReg")
                    If AutoRunProcess.Length = 0 = False Then AutoRunProcess(0).Kill()
                    If SC4CodeProcess.Length = 0 = False Then SC4CodeProcess(0).Kill()
                    If SC4eRegProcess.Length = 0 = False Then SC4eRegProcess(0).Kill()

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
                    Threading.Thread.Sleep(100)
                    Do Until FindWindow("#32770", "SimCity 4 Deluxe") <> Nothing : Loop
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

                    Dim RARProcess As Process = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\SC4.rar -o+ ""Graphics Rules.sgr"" """ & .SC4InstallDir & "\""") : RARProcess.WaitForExit()
                    If RARProcess.ExitCode = 0 Or My.Computer.FileSystem.FileExists(.SC4InstallDir & "\Apps\SimCity 4.exe") = True Then
                        Return InstallResult.Result.Success
                    Else : On Error Resume Next
                        Return InstallResult.Result.Fail
                    End If
                Else
                    Return InstallResult.Result.Fail
                End If
            ElseIf InstallType = InstallOptions.SC4InstallType.NoInstall Then
                Dim RARProcess As Process = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\SC4.rar *.* -o+ """ & .SC4InstallDir & "\""") : RARProcess.WaitForExit()
                If RARProcess.ExitCode = 0 Then
                    Return InstallResult.Result.Success
                Else : On Error Resume Next
                    Return InstallResult.Result.Fail
                End If
            End If
        End With
    End Function

    Public Function Install638Patch() As InstallResult.Result
        With ModuleMain.InstallOptions
            Dim RARProcess As Process = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\Patch\638\638.rar *.* -o+ """ & .SC4InstallDir & "\""") : RARProcess.WaitForExit()
            If RARProcess.ExitCode = 0 Then
                Return InstallResult.Result.Success
            Else : On Error Resume Next
                Return InstallResult.Result.Fail
            End If
        End With
    End Function

    Public Function Install640Patch() As InstallResult.Result
        With ModuleMain.InstallOptions
            Dim RARProcess As Process = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\Patch\640\640.rar *.* -o+ """ & .SC4InstallDir & "\""") : RARProcess.WaitForExit()
            If RARProcess.ExitCode = 0 Then
                Return InstallResult.Result.Success
            Else : On Error Resume Next
                Return InstallResult.Result.Fail
            End If
        End With
    End Function

    Public Function Install4GBPatch() As InstallResult.Result
        Dim _4GBProcess As Process = Process.Start("Data\Patch\4GB.exe", """" & ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe""") : _4GBProcess.WaitForExit()
        If _4GBProcess.ExitCode = 0 Then
            Return InstallResult.Result.Success
        Else : On Error Resume Next
            Return InstallResult.Result.Fail
        End If
    End Function

    Public Function InstallNoCDPatch() As InstallResult.Result
        My.Computer.FileSystem.CopyFile("Data\Patch\No-CD\SimCity 4.exe", ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe", True)
        If My.Computer.FileSystem.GetFileInfo(ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe").Length = 7524352 Then
            Return InstallResult.Result.Success
        Else : On Error Resume Next
            Return InstallResult.Result.Fail
        End If
    End Function

    Public Function InstallSC4Launcher() As InstallResult.Result
        My.Computer.FileSystem.CopyFile("Data\SC4Launcher.exe", ModuleMain.InstallOptions.SC4InstallDir)
        If My.Computer.FileSystem.FileExists(ModuleMain.InstallOptions.SC4InstallDir & "\SC4Launcher.exe") = True Then
            Return InstallResult.Result.Success
        Else : On Error Resume Next
            Return InstallResult.Result.Fail
        End If
    End Function

    Public Function InstallLanguagePatch(ByVal LanguagePatch As InstallOptions.Language) As InstallResult.Result
        With My.Computer.Registry
            Dim LanguageRegKeyName As String = Nothing
            If Environment.Is64BitOperatingSystem = True Then LanguageRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0"
            If Environment.Is64BitOperatingSystem = False Then LanguageRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0"
            Select Case LanguagePatch
                Case InstallOptions.Language.TraditionalChinese
                    My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\TChinese", ModuleMain.InstallOptions.SC4InstallDir, True)
                    .SetValue(LanguageRegKeyName, "Language", 18, Microsoft.Win32.RegistryValueKind.DWord)
                    .SetValue(LanguageRegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                    .SetValue(LanguageRegKeyName, "LanguageName", "Chinese (Traditional)", Microsoft.Win32.RegistryValueKind.String)
                    If My.Computer.FileSystem.DirectoryExists(ModuleMain.InstallOptions.SC4InstallDir & "TChinese") = True And _
                        My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) = 18 Then
                        Return InstallResult.Result.Success
                    Else : On Error Resume Next
                        Return InstallResult.Result.Fail
                    End If
                Case InstallOptions.Language.SimplifiedChinese
                    My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\SChinese", ModuleMain.InstallOptions.SC4InstallDir, True)
                    .SetValue(LanguageRegKeyName, "Language", 17, Microsoft.Win32.RegistryValueKind.DWord)
                    .SetValue(LanguageRegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                    .SetValue(LanguageRegKeyName, "LanguageName", "Chinese (Simplified)", Microsoft.Win32.RegistryValueKind.String)
                    If My.Computer.FileSystem.DirectoryExists(ModuleMain.InstallOptions.SC4InstallDir & "SChinese") = True And _
                        My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) = 17 Then
                        Return InstallResult.Result.Success
                    Else : On Error Resume Next
                        Return InstallResult.Result.Fail
                    End If
                Case InstallOptions.Language.English
                    My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\English", ModuleMain.InstallOptions.SC4InstallDir, True)
                    .SetValue(LanguageRegKeyName, "Language", 1, Microsoft.Win32.RegistryValueKind.DWord)
                    .SetValue(LanguageRegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                    .SetValue(LanguageRegKeyName, "LanguageName", "English US", Microsoft.Win32.RegistryValueKind.String)
            End Select
        End With
    End Function

    Public Sub AddDestopIcon(ByVal IsInstallSC4Launcher As Boolean)
        With ModuleMain.InstallOptions
            Dim wshshell As New IWshRuntimeLibrary.WshShell
            Dim shortcut As IWshRuntimeLibrary.IWshShortcut = wshshell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "\模拟城市4 豪华版.lnk")
            shortcut.WindowStyle = 1 : shortcut.Description = "运行模拟城市4 豪华版"
            shortcut.IconLocation = .SC4InstallDir
            If My.Computer.FileSystem.FileExists(.SC4InstallDir & "\SC4Launcher.exe") = True Then
                shortcut.TargetPath = .SC4InstallDir & "\SC4Launcher.exe" : shortcut.Save()
            Else
                shortcut.TargetPath = .SC4InstallDir & "\Apps\SimCity4.exe" : shortcut.Save()
            End If
        End With
    End Sub

    Public Sub AddStartMenuItem(ByVal IsInstallSC4Launcher As Boolean)
        With ModuleMain.InstallOptions
            Dim wshshell As New IWshRuntimeLibrary.WshShell
            My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "模拟城市4 豪华版")
            Dim shortcut As IWshRuntimeLibrary.IWshShortcut = wshshell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "模拟城市4 豪华版" & "\模拟城市4 豪华版.lnk")
            shortcut.WindowStyle = 1 : shortcut.Description = "运行模拟城市4 豪华版"
            shortcut.IconLocation = .SC4InstallDir
            If My.Computer.FileSystem.FileExists(ModuleMain.InstallOptions.SC4InstallDir & "\SC4Launcher.exe") = True Then
                shortcut.TargetPath = .SC4InstallDir & "\SC4Launcher.exe" : shortcut.Save()
            Else
                shortcut.TargetPath = .SC4InstallDir & "\Apps\SimCity4.exe" : shortcut.Save()
            End If
        End With
    End Sub

    Public Sub SetNoInstallSC4RegistryValue()
        Dim SC4RegKeyName As String = Nothing
        If Environment.Is64BitOperatingSystem = True Then SC4RegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0"
        If Environment.Is64BitOperatingSystem = False Then SC4RegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0"
        With My.Computer.Registry
            If Environment.Is64BitOperatingSystem = True Then
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", "(Default)", "CX9H498AMHSS8QXDTXJB", Microsoft.Win32.RegistryValueKind.String)
            Else
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", "(Default)", "CX9H498AMHSS8QXDTXJB", Microsoft.Win32.RegistryValueKind.String)
            End If
            .SetValue(SC4RegKeyName, "CacheSize", 1196879, Microsoft.Win32.RegistryValueKind.DWord)
            .SetValue(SC4RegKeyName, "CD Drive", ".\\", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(SC4RegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(SC4RegKeyName, "Install Dir", ModuleMain.InstallOptions.SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
            .SetValue(SC4RegKeyName, "Installed From", ".\\", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(SC4RegKeyName, "IsDeluxe", 0, Microsoft.Win32.RegistryValueKind.DWord)
            .SetValue(SC4RegKeyName, "Language", "English US", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(SC4RegKeyName, "Locale", "en-us", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(SC4RegKeyName, "Patch URL", "http://simcity.ea.com/update/", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(SC4RegKeyName, "Product GUID", "{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(SC4RegKeyName, "Region", "NA", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(SC4RegKeyName, "Registration", "SOFTWARE\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(SC4RegKeyName, "SwapSize", 0, Microsoft.Win32.RegistryValueKind.DWord)
            .SetValue(SC4RegKeyName, "Folder", Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "模拟城市4 豪华版", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(SC4RegKeyName & "\EP1", "(Default)", "5ZH4HSUIYKHTPFPN7Q30", Microsoft.Win32.RegistryValueKind.String)
            .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "(Default)", ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe", Microsoft.Win32.RegistryValueKind.String)
            .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "Path", ModuleMain.InstallOptions.SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
            .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "Restart", 0, Microsoft.Win32.RegistryValueKind.DWord)
            .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "Game Registry", "SOFTWARE\Maxis\SimCity 4", Microsoft.Win32.RegistryValueKind.String)
            .SetValue("HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap", "UNCAsIntranet", 0, Microsoft.Win32.RegistryValueKind.DWord)
            .SetValue("HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap", "AutoDetect", 1, Microsoft.Win32.RegistryValueKind.DWord)
            .CurrentUser.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\ProxyBypass")
            .CurrentUser.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\IntranetName")
            .LocalMachine.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\ProxyBypass")
            .LocalMachine.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\IntranetName")
        End With
    End Sub

End Module