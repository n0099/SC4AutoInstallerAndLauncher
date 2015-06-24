Public Class frmInstalling

    Private Sub ReportProgress(ByVal result As InstallResult.Result, ByVal item As ListViewItem)
        With ModuleMain.InstallResult
            If result = InstallResult.Result.Success Then
                item.ImageKey = "success"
                Select Case item.Text
                    Case "DAEMON Tools Lite" : .DAEMONToolsInstallResult = InstallResult.Result.Success
                    Case "638补丁" : ._638PatchInstallResult = InstallResult.Result.Success
                    Case "640补丁" : ._640PatchInstallResult = InstallResult.Result.Success
                    Case "免CD补丁" : .NoCDPatchInstallResult = InstallResult.Result.Success
                    Case "4GB补丁" : ._4GBPatchInstallResult = InstallResult.Result.Success
                    Case "繁体中文语言补丁", "简体中文语言补丁" : .LanguagePatchInstallResult = InstallResult.Result.Success
                    Case "添加开始菜单项" : .AddDesktopIconResult = InstallResult.Result.Success
                    Case "添加桌面图标" : .AddStartMenuItemResult = InstallResult.Result.Success
                    Case Else : If item.Text.Contains("模拟城市4 豪华版") = True Then .SC4InstallResult = InstallResult.Result.Success
                End Select
            Else
                item.ImageKey = "fail"
                Select Case item.Text
                    Case "DAEMON Tools Lite" : .DAEMONToolsInstallResult = InstallResult.Result.Fail
                    Case "638补丁" : ._638PatchInstallResult = InstallResult.Result.Fail
                    Case "640补丁" : ._640PatchInstallResult = InstallResult.Result.Fail
                    Case "免CD补丁" : .NoCDPatchInstallResult = InstallResult.Result.Fail
                    Case "4GB补丁" : ._4GBPatchInstallResult = InstallResult.Result.Fail
                    Case "繁体中文语言补丁", "简体中文语言补丁" : .LanguagePatchInstallResult = InstallResult.Result.Fail
                    Case "添加开始菜单项" : .AddDesktopIconResult = InstallResult.Result.Fail
                    Case "添加桌面图标" : .AddStartMenuItemResult = InstallResult.Result.Fail
                    Case Else
                        If item.Text.Contains("模拟城市4 豪华版") = True Then
                            For i As Integer = 0 To lvwTask.Items.Count - 1
                                lvwTask.Items(i).ImageKey = "fail"
                            Next
                            .SC4InstallResult = InstallResult.Result.Fail
                            ._638PatchInstallResult = InstallResult.Result.Fail
                            ._640PatchInstallResult = InstallResult.Result.Fail
                            ._4GBPatchInstallResult = InstallResult.Result.Fail
                            .NoCDPatchInstallResult = InstallResult.Result.Fail
                            .SC4LauncherInstallResult = InstallResult.Result.Fail
                            .LanguagePatchInstallResult = InstallResult.Result.Fail
                            .AddDesktopIconResult = InstallResult.Result.Fail
                            .AddStartMenuItemResult = InstallResult.Result.Fail
                            bgwInstall.CancelAsync()
                        End If
                End Select
            End If
            If item.Index < lvwTask.Items.Count - 1 Then
                lvwTask.Items(item.Index + 1).ImageKey = "installing"
            End If
        End With
    End Sub

#Region "安装过程"
    Private Sub InstallDAEMONTools()
        Process.Start("Data\DAEMON Tools Lite 5.0.exe", "/S /nogadget /path """ & ModuleMain.InstallOptions.DAEMONInstallDir & """").WaitForExit()
        If My.Computer.FileSystem.FileExists(ModuleMain.InstallOptions.DAEMONInstallDir & "\DTLite.exe") = True Then
            ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("DAEMON Tools Lite"))
        Else : On Error Resume Next
            ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("DAEMON Tools Lite"))
        End If
    End Sub

    Private Sub InstallSC4(ByVal InstallType As InstallOptions.SC4InstallType)
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
                        ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("模拟城市4 豪华版 镜像版"))
                    Else : On Error Resume Next
                        ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("模拟城市4 豪华版 镜像版"))
                    End If
                Else
                    ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("模拟城市4 豪华版 镜像版"))
                End If
            ElseIf InstallType = InstallOptions.SC4InstallType.NoInstall Then
                Dim RARProcess As Process = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\SC4.rar *.* -o+ """ & .SC4InstallDir & "\""") : RARProcess.WaitForExit()
                If RARProcess.ExitCode = 0 Then
                    ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("模拟城市4 豪华版 硬盘版"))
                Else : On Error Resume Next
                    ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("模拟城市4 豪华版 硬盘版"))
                End If
            End If
        End With
    End Sub

    Private Sub Install638Patch()
        With ModuleMain.InstallOptions
            Dim RARProcess As Process = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\Patch\638\638.rar *.* -o+ """ & .SC4InstallDir & "\""") : RARProcess.WaitForExit()
            If RARProcess.ExitCode = 0 Then
                ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("638补丁"))
            Else : On Error Resume Next
                ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("638补丁"))
            End If
        End With
    End Sub

    Private Sub Install640Patch()
        With ModuleMain.InstallOptions
            Dim RARProcess As Process = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\Patch\640\640.rar *.* -o+ """ & .SC4InstallDir & "\""") : RARProcess.WaitForExit()
            If RARProcess.ExitCode = 0 Then
                ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("640补丁"))
            Else : On Error Resume Next
                ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("640补丁"))
            End If
        End With
    End Sub

    Private Sub Install4GBPatch()
        If Shell("Data\Patch\4GB.exe """ & ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe" & """", True) = 0 Then
            ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("4GB补丁"))
        Else : On Error Resume Next
            ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("4GB补丁"))
        End If
    End Sub

    Private Sub InstallNoCDPatch()
        My.Computer.FileSystem.CopyFile("Data\Patch\No-CD\SimCity 4.exe", ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe", True)
        If My.Computer.FileSystem.GetFileInfo(ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe").Length = 7524352 Then
            ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("免CD补丁"))
        Else : On Error Resume Next
            ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("免CD补丁"))
        End If
    End Sub

    Private Sub InstallSC4Launcher()
        My.Computer.FileSystem.CopyFile("Data\SC4Launcher.exe", ModuleMain.InstallOptions.SC4InstallDir)
        If My.Computer.FileSystem.FileExists(ModuleMain.InstallOptions.SC4InstallDir & "\SC4Launcher.exe") = True Then
            ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("模拟城市4 启动器"))
        Else : On Error Resume Next
            ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("模拟城市4 启动器"))
        End If
    End Sub

    Private Sub InstallLanguagePatch(ByVal LanguagePatch As InstallOptions.Language)
        With My.Computer.Registry
            Select Case LanguagePatch
                Case InstallOptions.Language.TraditionalChinese
                    My.Computer.FileSystem.CopyDirectory("Data\Patch\TChinese", ModuleMain.InstallOptions.SC4InstallDir, True)
                    If Environment.Is64BitOperatingSystem = True Then
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0", "Language", 18, Microsoft.Win32.RegistryValueKind.DWord)
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0", "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0", "LanguageName", "Chinese (Traditional)", Microsoft.Win32.RegistryValueKind.String)
                    Else
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "Language", 18, Microsoft.Win32.RegistryValueKind.DWord)
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "LanguageName", "Chinese (Traditional)", Microsoft.Win32.RegistryValueKind.String)
                    End If
                    If My.Computer.FileSystem.DirectoryExists(ModuleMain.InstallOptions.SC4InstallDir & "TChinese") = True And _
                        My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "Language", Nothing) = 18 Then
                        ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("繁体中文语言补丁"))
                    Else
                        On Error Resume Next
                        ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("繁体中文语言补丁"))
                    End If
                Case InstallOptions.Language.SimplifiedChinese
                    My.Computer.FileSystem.CopyDirectory("Data\Patch\SChinese", ModuleMain.InstallOptions.SC4InstallDir, True)
                    If Environment.Is64BitOperatingSystem = True Then
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0", "Language", 17, Microsoft.Win32.RegistryValueKind.DWord)
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0", "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0", "LanguageName", "Chinese (Simplified)", Microsoft.Win32.RegistryValueKind.String)
                    Else
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "Language", 17, Microsoft.Win32.RegistryValueKind.DWord)
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "LanguageName", "Chinese (Simplified)", Microsoft.Win32.RegistryValueKind.String)
                    End If
                    If My.Computer.FileSystem.DirectoryExists(ModuleMain.InstallOptions.SC4InstallDir & "SChinese") = True And _
                        My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "Language", Nothing) = 17 Then
                        ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("简体中文语言补丁"))
                    Else
                        On Error Resume Next
                        ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("简体中文语言补丁"))
                    End If
                Case InstallOptions.Language.English
                    My.Computer.FileSystem.CopyDirectory("Data\Patch\English", ModuleMain.InstallOptions.SC4InstallDir, True)
                    If Environment.Is64BitOperatingSystem = True Then
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0", "Language", 1, Microsoft.Win32.RegistryValueKind.DWord)
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0", "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0", "LanguageName", "English US", Microsoft.Win32.RegistryValueKind.String)
                    Else
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "Language", 1, Microsoft.Win32.RegistryValueKind.DWord)
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "LanguageName", "English US", Microsoft.Win32.RegistryValueKind.String)
                    End If
                    If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "Language", Nothing) = 1 Then
                        ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("简体中文语言补丁"))
                    Else
                        On Error Resume Next
                        ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("简体中文语言补丁"))
                    End If
            End Select
        End With
    End Sub

    Private Sub AddDestopIcon(ByVal IsInstallSC4Launcher As Boolean)
        Dim wshshell As New IWshRuntimeLibrary.WshShell
        Dim shortcut As IWshRuntimeLibrary.IWshShortcut = wshshell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "\模拟城市4 豪华版.lnk")
        shortcut.WindowStyle = 1 : shortcut.Description = "运行模拟城市4 豪华版"
        shortcut.IconLocation = ModuleMain.InstallOptions.SC4InstallDir
        If My.Computer.FileSystem.FileExists(ModuleMain.InstallOptions.SC4InstallDir & "\SC4Launcher.exe") = True Then
            shortcut.TargetPath = ModuleMain.InstallOptions.SC4InstallDir & "\SC4Launcher.exe" : shortcut.Save()
        Else
            shortcut.TargetPath = ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity4.exe" : shortcut.Save()
        End If
        If My.Computer.FileSystem.FileExists(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "\模拟城市4 豪华版.lnk") = True Then
            ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("添加开始菜单项"))
        Else : On Error Resume Next
            ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("添加开始菜单项"))
        End If
    End Sub

    Private Sub AddStartMenuItem(ByVal IsInstallSC4Launcher As Boolean)
        Dim wshshell As New IWshRuntimeLibrary.WshShell
        My.Computer.FileSystem.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "模拟城市4 豪华版")
        Dim shortcut As IWshRuntimeLibrary.IWshShortcut = wshshell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "模拟城市4 豪华版" & "\模拟城市4 豪华版.lnk")
        shortcut.WindowStyle = 1 : shortcut.Description = "运行模拟城市4 豪华版"
        shortcut.IconLocation = ModuleMain.InstallOptions.SC4InstallDir
        If My.Computer.FileSystem.FileExists(ModuleMain.InstallOptions.SC4InstallDir & "\SC4Launcher.exe") = True Then
            shortcut.TargetPath = ModuleMain.InstallOptions.SC4InstallDir & "\SC4Launcher.exe" : shortcut.Save()
        Else
            shortcut.TargetPath = ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity4.exe" : shortcut.Save()
        End If
        If My.Computer.FileSystem.DirectoryExists(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "模拟城市4 豪华版") = True Then
            ReportProgress(InstallResult.Result.Success, lvwTask.FindItemWithText("添加桌面图标"))
        Else : On Error Resume Next
            ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("添加桌面图标"))
        End If
    End Sub

    Private Sub SetNoInstallSC4RegistryValue()
        With My.Computer.Registry
            If Environment.Is64BitOperatingSystem = True Then
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", "(Default)", "CX9H498AMHSS8QXDTXJB", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "CacheSize", 1196879, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "CD Drive", ".\\", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Install Dir", ModuleMain.InstallOptions.SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Installed From", ".\\", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "IsDeluxe", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Language", "English US", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Locale", "en-us", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Patch URL", "http://simcity.ea.com/update/", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Product GUID", "{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Region", "NA", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Registration", "SOFTWARE\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "SwapSize", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4", "Folder", Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "模拟城市4 豪华版", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\EP1", "(Default)", "5ZH4HSUIYKHTPFPN7Q30", Microsoft.Win32.RegistryValueKind.String)
            Else
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", "(Default)", "CX9H498AMHSS8QXDTXJB", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "CacheSize", 1196879, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "CD Drive", ".\\", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Install Dir", ModuleMain.InstallOptions.SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Installed From", ".\\", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "IsDeluxe", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Language", "English US", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Locale", "en-us", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Patch URL", "http://simcity.ea.com/update/", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Product GUID", "{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Region", "NA", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Registration", "SOFTWARE\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "SwapSize", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4", "Folder", Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) & "模拟城市4 豪华版", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\EP1", "(Default)", "5ZH4HSUIYKHTPFPN7Q30", Microsoft.Win32.RegistryValueKind.String)
            End If
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
#End Region

    Private Sub bgwInstall_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwInstall.DoWork
        'For i As Integer = 0 To lvwTask.Items.Count - 1
        '    Threading.Thread.Sleep(1000)
        '    Console.WriteLine("1")
        '    ReportProgress(InstallResult.Result.Success, lvwTask.Items(i))
        'Next
        'ReportProgress(InstallResult.Result.Fail, lvwTask.FindItemWithText("模拟城市4 豪华版 镜像版"))
        With ModuleMain.InstallOptions
            If .IsInstallDAEMONTools = True Then InstallDAEMONTools()
            If .SC4Type = InstallOptions.SC4InstallType.ISO Then InstallSC4(InstallOptions.SC4InstallType.ISO)
            If .SC4Type = InstallOptions.SC4InstallType.NoInstall Then InstallSC4(InstallOptions.SC4InstallType.NoInstall) : SetNoInstallSC4RegistryValue()
            If bgwInstall.CancellationPending = True Then Exit Sub
            If .IsInstall638Patch = True Then Install638Patch()
            If .IsInstall640Patch = True Then Install640Patch()
            If .IsInstallNoCDPatch = True Then InstallNoCDPatch()
            If .IsInstall4GBPatch = True Then Install4GBPatch()
            If .IsInstallSC4Launcher = True Then InstallSC4Launcher()
            Select Case .LanguagePatch
                Case InstallOptions.Language.TraditionalChinese
                    InstallLanguagePatch(InstallOptions.Language.TraditionalChinese)
                Case InstallOptions.Language.SimplifiedChinese
                    InstallLanguagePatch(InstallOptions.Language.SimplifiedChinese)
                Case InstallOptions.Language.English
                    InstallLanguagePatch(InstallOptions.Language.English)
            End Select
            If .IsAddDesktopIcon = True Then If .IsInstallSC4Launcher = True Then AddDestopIcon(True) Else AddDestopIcon(False)
            If .IsAddStartMenuItem = True Then If .IsInstallSC4Launcher = True Then AddStartMenuItem(True) Else AddStartMenuItem(False)
            Threading.Thread.Sleep(1000)
        End With
    End Sub

    Private Sub bgwInstall_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwInstall.RunWorkerCompleted
        frmFinish.Show()
        Close()
    End Sub

    Private Sub frmInstalling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With ModuleMain.InstallOptions
            If .SC4Type = InstallOptions.SC4InstallType.ISO Then
                lvwTask.FindItemWithText("模拟城市4 豪华版").Text = "模拟城市4 豪华版 镜像版"
            ElseIf .SC4Type = InstallOptions.SC4InstallType.NoInstall Then
                lvwTask.FindItemWithText("模拟城市4 豪华版").Text = "模拟城市4 豪华版 硬盘版"
            End If
            If .IsInstallDAEMONTools = False Then lvwTask.FindItemWithText("DAEMON Tools Lite").Remove()
            If .IsInstall638Patch = False Then lvwTask.FindItemWithText("638补丁").Remove()
            If .IsInstall640Patch = False Then lvwTask.FindItemWithText("640补丁").Remove()
            If .IsInstallNoCDPatch = False Then lvwTask.FindItemWithText("免CD补丁").Remove()
            If .IsInstall4GBPatch = False Then lvwTask.FindItemWithText("4GB补丁").Remove()
            Select Case .LanguagePatch
                Case InstallOptions.Language.TraditionalChinese
                    lvwTask.FindItemWithText("语言补丁").Text = "繁体中文语言补丁"
                Case InstallOptions.Language.SimplifiedChinese
                    lvwTask.FindItemWithText("语言补丁").Text = "简体中文语言补丁"
                Case InstallOptions.Language.English
                    lvwTask.FindItemWithText("语言补丁").Remove()
            End Select
            If .IsAddDesktopIcon = False Then lvwTask.FindItemWithText("添加桌面图标").Remove()
            If .IsAddStartMenuItem = False Then lvwTask.FindItemWithText("添加开始菜单项").Remove()
        End With
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099"
        Control.CheckForIllegalCrossThreadCalls = False
        Dim ControlBoxHandle As Integer = GetSystemMenu(Me.Handle, 0)
        Dim ControlBoxCount As Integer = GetMenuItemCount(ControlBoxHandle)
        RemoveMenu(ControlBoxHandle, ControlBoxCount - 1, MF_DISABLED Or MF_BYPOSITION)
        DrawMenuBar(Me.Handle)
        bgwInstall.RunWorkerAsync()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        If MessageBox.Show("确定退出安装程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then Application.Exit()
    End Sub

End Class