Module ModuleMain

    Sub Main()
        With My.Computer.FileSystem
            Console.Write("正在安装更新" & vbCrLf)
            If .FileExists("\Apps\SimCity 4.exe") = False Then
                '1.5.30
                If .DirectoryExists("Data\Image") = True Then .RenameDirectory("Data\Image", "CD")
                If .DirectoryExists("Data\Patch\No-CD") = True Then .RenameDirectory("Data\Patch\No-CD", "NoCD")
                If .FileExists("Data\Patch\4GB\4gb_patch.exe") = True Then .MoveFile("Data\Patch\4GB\4gb_patch.exe", "Data\Patch\4GB.exe", True)
                'If .FileExists("Data\Patch\638.EXE") = True Then .MoveFile("Data\Patch\638.EXE", "Data\Patch\638\638.EXE", True)
                'If .FileExists("Data\Patch\638.rar") = True Then .MoveFile("Data\Patch\638.rar", "Data\Patch\638\638.rar", True)
                'If .FileExists("Data\Patch\640.exe") = True Then .MoveFile("Data\Patch\640.exe", "Data\Patch\640\640.exe", True)
                'If .FileExists("Data\Patch\640.rar") = True Then .MoveFile("Data\Patch\640.rar", "Data\Patch\640\640.rar", True)
                '2.2.19
                If .FileExists("Data\EA EULA.txt") = True Then .DeleteFile("Data\EA EULA.txt")
                '2.2.45
                If .DirectoryExists("Data\Patch\638") = True Then
                    .MoveFile("Data\Patch\638\638.rar", "Data\Patch\638.rar", True)
                    .DeleteDirectory("Data\Patch\638", FileIO.DeleteDirectoryOption.DeleteAllContents)
                End If
                If .DirectoryExists("Data\Patch\640") = True Then
                    .MoveFile("Data\Patch\640\640.rar", "Data\Patch\640.rar", True)
                    .DeleteDirectory("Data\Patch\640", FileIO.DeleteDirectoryOption.DeleteAllContents)
                End If
                If .DirectoryExists("Data\Patch\NoCD") = True Then
                    .MoveFile("Data\Patch\NoCD\SimCity 4.exe", "Data\Patch\SimCity 4 NoCD.exe", True)
                    .DeleteDirectory("Data\Patch\NoCD", FileIO.DeleteDirectoryOption.DeleteAllContents)
                End If
                If .FileExists("Data\Patch\638.EXE") = True Then .DeleteFile("Data\Patch\638.EXE")
                If .FileExists("Data\Patch\640.exe") = True Then .DeleteFile("Data\Patch\640.exe")
                '2.4.10
                If .DirectoryExists("Data\CD") = True Then .MoveDirectory("Data\CD", "Data\SC4\CD")
                If .FileExists("Data\SC4.rar") = True Then .MoveFile("Data\SC4.rar", "Data\SC4\NoInstall.rar")
                If .FileExists("Data\SC4\CD\SC4DELUXE CD1.mdf") = True Then .RenameFile("Data\SC4\CD\SC4DELUXE CD1.mdf", "Data\SC4\CD\CD1.mdf")
                If .FileExists("Data\SC4\CD\SC4DELUXE CD1.mds") = True Then .RenameFile("Data\SC4\CD\SC4DELUXE CD1.mds", "Data\SC4\CD\CD1.mds")
                If .FileExists("Data\SC4\CD\SC4DELUXE CD2.mdf") = True Then .RenameFile("Data\SC4\CD\SC4DELUXE CD2.mdf", "Data\SC4\CD\CD2.mdf")
                If .FileExists("Data\SC4\CD\SC4DELUXE CD2.mds") = True Then .RenameFile("Data\SC4\CD\SC4DELUXE CD2.mds", "Data\SC4\CD\CD2.mds")
                '2.5.21
                If .FileExists("Data\SC4\NoInstall.rar") = True Or .FileExists("Data\Patch\638.rar") = True Or .FileExists("Data\Patch\640.rar") = True Then
                    Console.WriteLine("请到http://pan.baidu.com/s/1bnezR7h重下Data\SC4\NoInstall.7z、638.7z和640.7z文件！" & vbCrLf)
                End If
            Else
                .DeleteDirectory("Data", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
            Console.Write("更新安装完成")
            Dim bat As String = ":del" & vbCrLf & "del %1" & vbCrLf & "if exist %1 goto del" & vbCrLf & "del %0"
            My.Computer.FileSystem.WriteAllText("DeleteUpdater.bat", bat, False, Text.Encoding.ASCII)
            If .FileExists("Setup.exe") = True Then Process.Start("Setup.exe")
            Process.Start(New ProcessStartInfo With {.FileName = "DeleteUpdater.bat", .Arguments = """" & Process.GetCurrentProcess.MainModule.FileName & """", .Verb = "runas", .WindowStyle = ProcessWindowStyle.Hidden})
        End With
    End Sub

End Module