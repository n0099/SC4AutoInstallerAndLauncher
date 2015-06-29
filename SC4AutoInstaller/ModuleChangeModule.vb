Module ModuleChangeModule

    Public Function Change638Patch() As InstallResult.Result
        With ModuleMain.InstallOptions
            Dim RARProcess As Process
            If .IsInstall638Patch = True Then
                RARProcess = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\Patch\638\638.rar *.* -o+ """ & .SC4InstallDir & "\""") : RARProcess.WaitForExit()
            Else
                RARProcess = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\Patch\SC4.rar ""Apps\SimCity 4.exe"" SimCity_*.dat -o+ """ & .SC4InstallDir & "\""") : RARProcess.WaitForExit()
            End If
            If RARProcess.ExitCode = 0 Then
                Return InstallResult.Result.Success
            Else : On Error Resume Next
                Return InstallResult.Result.Fail
            End If
        End With
    End Function

    Public Function Change640Patch() As InstallResult.Result
        With ModuleMain.InstallOptions
            Dim RARProcess As Process
            If .IsInstall640Patch = True Then
                RARProcess = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\Patch\640\640.rar *.* -o+ """ & .SC4InstallDir & "\""") : RARProcess.WaitForExit()
            Else
                RARProcess = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\Patch\638\638.rar *.* -o+ """ & .SC4InstallDir & "\""") : RARProcess.WaitForExit()
            End If
            If RARProcess.ExitCode = 0 Then
                Return InstallResult.Result.Success
            Else : On Error Resume Next
                Return InstallResult.Result.Fail
            End If
        End With
    End Function

    Public Function Change4GBPatch() As InstallResult.Result
        With ModuleMain.InstallOptions
            Dim Process As Process = Nothing
            If .IsInstall4GBPatch = True Then
                Process = Process.Start("Data\Patch\4GB.exe", """" & .SC4InstallDir & "\Apps\SimCity 4.exe""") : Process.WaitForExit()
            Else
                If .IsInstall638Patch = True Or .IsInstall640Patch = True Then
                    If .IsInstall638Patch = True Then Process = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\Patch\638\638.rar ""Apps\SimCity 4.exe"" -o+ """ & .SC4InstallDir & "\""") : Process.WaitForExit()
                    If .IsInstall640Patch = True Then Process = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\Patch\640\640.rar ""Apps\SimCity 4.exe"" -o+ """ & .SC4InstallDir & "\""") : Process.WaitForExit()
                    If Process.ExitCode = 0 Then
                        Return InstallResult.Result.Success
                    Else : On Error Resume Next
                        Return InstallResult.Result.Fail
                    End If
                ElseIf .IsInstallNoCDPatch = True Then
                    My.Computer.FileSystem.CopyFile("Data\Patch\NoCD\SimCity 4.exe", ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe", True)
                    If My.Computer.FileSystem.GetFileInfo(ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe").Length = 7524352 Then
                        Return InstallResult.Result.Success
                    Else : On Error Resume Next
                        Return InstallResult.Result.Fail
                    End If
                End If
            End If
        End With
    End Function

    Public Function ChangeNoCDPatch() As InstallResult.Result
        With ModuleMain.InstallOptions
            If .IsInstallNoCDPatch = True Then
                My.Computer.FileSystem.CopyFile("Data\Patch\NoCD\SimCity 4.exe", .SC4InstallDir & "\Apps\SimCity 4.exe", True)
                If My.Computer.FileSystem.GetFileInfo(ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe").Length = 7524352 Then
                    Return InstallResult.Result.Success
                Else : On Error Resume Next
                    Return InstallResult.Result.Fail
                End If
            Else
                Dim RARProcess As Process = Process.Start("Data\rar.exe", "x " & Application.StartupPath & "\Data\Patch\SC4.rar ""Apps\SimCity 4.exe"" SimCity_*.dat -o+ """ & .SC4InstallDir & "\""") : RARProcess.WaitForExit()
                If RARProcess.ExitCode = 0 Then
                    Return InstallResult.Result.Success
                Else : On Error Resume Next
                    Return InstallResult.Result.Fail
                End If
            End If
        End With
    End Function

    Public Function ChangeSC4Launcher() As InstallResult.Result
        With ModuleMain.InstallOptions
            If .IsInstallSC4Launcher = True Then
                My.Computer.FileSystem.CopyFile("Data\SC4Launcher.exe", .SC4InstallDir, True)
                If My.Computer.FileSystem.FileExists(.SC4InstallDir & "\SC4Launcher.exe") = True Then
                    Return InstallResult.Result.Success
                Else : On Error Resume Next
                    Return InstallResult.Result.Fail
                End If
            Else
                My.Computer.FileSystem.DeleteFile(.SC4InstallDir & "\SC4Launcher.exe")
                If My.Computer.FileSystem.FileExists(.SC4InstallDir & "\SC4Launcher.exe") = True Then
                    Return InstallResult.Result.Success
                Else : On Error Resume Next
                    Return InstallResult.Result.Fail
                End If
            End If
        End With
    End Function

    Public Function ChangeLaunagePatch(ByVal LanguagePatch As InstallOptions.SC4InstallType) As InstallResult.Result
        With ModuleMain.InstallOptions
            If .LanguagePatch <> ModuleMain.InstalledModule.LanguagePatch Then
                Dim LanguageRegKeyName As String = Nothing
                If Environment.Is64BitOperatingSystem = True Then LanguageRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0"
                If Environment.Is64BitOperatingSystem = False Then LanguageRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0"
                Select Case LanguagePatch
                    Case InstallOptions.Language.TraditionalChinese
                        My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\TChinese", .SC4InstallDir, True)
                        If My.Computer.FileSystem.DirectoryExists(ModuleMain.InstallOptions.SC4InstallDir & "TChinese") = True And _
                            My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) = 18 Then
                            Return InstallResult.Result.Success
                        Else : On Error Resume Next
                            Return InstallResult.Result.Fail
                        End If
                    Case InstallOptions.Language.SimplifiedChinese
                        My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\SChinese", .SC4InstallDir, True)
                        If My.Computer.FileSystem.DirectoryExists(ModuleMain.InstallOptions.SC4InstallDir & "SChinese") = True And _
                            My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) = 17 Then
                            Return InstallResult.Result.Success
                        Else : On Error Resume Next
                            Return InstallResult.Result.Fail
                        End If
                    Case InstallOptions.Language.English
                        My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\English", .SC4InstallDir, True)
                        'If My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) = 1 Then
                        '    Return InstallResult.Result.Success
                        'Else : On Error Resume Next
                        '    Return InstallResult.Result.Fail
                        'End If
                End Select
            End If
        End With
    End Function

End Module