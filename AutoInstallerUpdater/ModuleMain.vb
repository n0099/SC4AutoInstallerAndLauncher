Module ModuleMain

    Public Sub Main()
        Console.Write("正在安装更新" & vbCrLf)
        With My.Computer.FileSystem
            If .FileExists("Apps\SimCity 4.exe") = False Then
                '1.5.30
                If .DirectoryExists("Data\Image") Then .RenameDirectory("Data\Image", "CD")
                If .DirectoryExists("Data\Patch\No-CD") Then .RenameDirectory("Data\Patch\No-CD", "NoCD")
                If .FileExists("Data\Patch\4GB\4gb_patch.exe") Then .MoveFile("Data\Patch\4GB\4gb_patch.exe", "Data\Patch\4GB.exe", True)
                'If .FileExists("Data\Patch\638.EXE") Then .MoveFile("Data\Patch\638.EXE", "Data\Patch\638\638.EXE", True)
                'If .FileExists("Data\Patch\638.rar") Then .MoveFile("Data\Patch\638.rar", "Data\Patch\638\638.rar", True)
                'If .FileExists("Data\Patch\640.exe") Then .MoveFile("Data\Patch\640.exe", "Data\Patch\640\640.exe", True)
                'If .FileExists("Data\Patch\640.rar") Then .MoveFile("Data\Patch\640.rar", "Data\Patch\640\640.rar", True)
                '2.2.19
                If .FileExists("Data\EA EULA.txt") Then .DeleteFile("Data\EA EULA.txt")
                '2.2.45
                If .DirectoryExists("Data\Patch\638") Then
                    .MoveFile("Data\Patch\638\638.rar", "Data\Patch\638.rar", True)
                    .DeleteDirectory("Data\Patch\638", FileIO.DeleteDirectoryOption.DeleteAllContents)
                End If
                If .DirectoryExists("Data\Patch\640") Then
                    .MoveFile("Data\Patch\640\640.rar", "Data\Patch\640.rar", True)
                    .DeleteDirectory("Data\Patch\640", FileIO.DeleteDirectoryOption.DeleteAllContents)
                End If
                If .DirectoryExists("Data\Patch\NoCD") Then
                    .MoveFile("Data\Patch\NoCD\SimCity 4.exe", "Data\Patch\SimCity 4 NoCD.exe", True)
                    .DeleteDirectory("Data\Patch\NoCD", FileIO.DeleteDirectoryOption.DeleteAllContents)
                End If
                If .FileExists("Data\Patch\638.EXE") Then .DeleteFile("Data\Patch\638.EXE")
                If .FileExists("Data\Patch\640.exe") Then .DeleteFile("Data\Patch\640.exe")
                '2.4.10
                If .DirectoryExists("Data\CD") Then .MoveDirectory("Data\CD", "Data\SC4\CD")
                If .FileExists("Data\SC4.rar") Then .MoveFile("Data\SC4.rar", "Data\SC4\NoInstall.rar")
                If .FileExists("Data\SC4\CD\SC4DELUXE CD1.mdf") Then .RenameFile("Data\SC4\CD\SC4DELUXE CD1.mdf", "Data\SC4\CD\CD1.mdf")
                If .FileExists("Data\SC4\CD\SC4DELUXE CD1.mds") Then .RenameFile("Data\SC4\CD\SC4DELUXE CD1.mds", "Data\SC4\CD\CD1.mds")
                If .FileExists("Data\SC4\CD\SC4DELUXE CD2.mdf") Then .RenameFile("Data\SC4\CD\SC4DELUXE CD2.mdf", "Data\SC4\CD\CD2.mdf")
                If .FileExists("Data\SC4\CD\SC4DELUXE CD2.mds") Then .RenameFile("Data\SC4\CD\SC4DELUXE CD2.mds", "Data\SC4\CD\CD2.mds")
                '2.5.21
                If .FileExists("Data\SC4\NoInstall.rar") Then
                    Console.WriteLine("请到http://pan.baidu.com/s/1bnezR7h重下Data\SC4\NoInstall.7z文件" & vbCrLf)
                    Console.WriteLine("请按任意键继续更新" & vbCrLf)
                    Console.ReadKey(False)
                End If
                '2.6.34
                If .FileExists("Data\7z.exe") Then .DeleteFile("Data\7z.exe")
                '2.7.11
                If .FileExists("Data\7z x64.exe") Then .DeleteFile("Data\7z x64.exe")
                If .FileExists("Data\7z x86.exe") Then .DeleteFile("Data\7z x86.exe")
                '2.8.30
                If .FileExists("Data\SC4Launcher.exe") Then .DeleteFile("Data\SC4Launcher.exe")
                If .FileExists("Data\Patch\638.7z") Then .DeleteFile("Data\Patch\638.7z")
                If .FileExists("Data\Patch\640.7z") Then .DeleteFile("Data\Patch\640.7z")
                If .FileExists("Data\Patch\SimCity 4 641.exe") Then .DeleteFile("Data\Patch\SimCity 4 641.exe")
                If .FileExists("Data\Patch\SimCity 4 NoCD.exe") Then .DeleteFile("Data\Patch\SimCity 4 NoCD.exe")
                If .DirectoryExists("Data\SC4") Then
                    If .DirectoryExists("Data\SC4\CD") = False Then
                        Console.WriteLine("请到http://pan.baidu.com/s/1bnezR7h下载Data\SC4\CD文件夹" & vbCrLf)
                        Console.WriteLine("请按任意键继续更新" & vbCrLf)
                        Console.ReadKey(False)
                    End If
                    If .GetFileInfo("Data\SC4\NoInstall.7z").Length <> 948727882 Then
                        Console.WriteLine("请到http://pan.baidu.com/s/1bnezR7h重下Data\SC4\NoInstall.7z文件" & vbCrLf)
                        Console.WriteLine("请按任意键继续更新" & vbCrLf)
                        Console.ReadKey(False)
                    End If
                Else
                    Console.WriteLine("请到http://pan.baidu.com/s/1bnezR7h重下Data\SC4文件夹" & vbCrLf)
                    Console.WriteLine("请按任意键继续更新" & vbCrLf)
                    Console.ReadKey(False)
                End If
                '2.8.3?
                If .FileExists("Data\Patch\638 SKU1.EXE") = False Then
                    Console.WriteLine("请到http://pan.baidu.com/s/1bnezR7h重下Data\Patch\638 SKU1.EXE文件" & vbCrLf)
                    Console.WriteLine("请按任意键继续更新" & vbCrLf)
                    Console.ReadKey(False)
                End If
                If .FileExists("Data\Patch\640.exe") = False Then
                    Console.WriteLine("请到http://pan.baidu.com/s/1bnezR7h重下Data\Patch\640.exe文件" & vbCrLf)
                    Console.WriteLine("请按任意键继续更新" & vbCrLf)
                    Console.ReadKey(False)
                End If
                If .FileExists("Data\Patch\641.7z") = False Then
                    Console.WriteLine("请到http://pan.baidu.com/s/1bnezR7h重下Data\Patch\641.7z文件" & vbCrLf)
                    Console.WriteLine("请按任意键继续更新" & vbCrLf)
                    Console.ReadKey(False)
                End If
                If .DirectoryExists("Data\Patch\SimCity 4.exe") = False Then
                    Console.WriteLine("请到http://pan.baidu.com/s/1bnezR7h重下Data\Patch\SimCity 4.exe文件夹" & vbCrLf)
                    Console.WriteLine("请按任意键继续更新" & vbCrLf)
                    Console.ReadKey(False)
                End If
            Else
                .DeleteDirectory("Data", FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
        End With
        Console.Write("更新安装完成")
        Dim bat As String = ":del" & vbCrLf & "del %~dp0\%1" & vbCrLf & "if exist %~dp0\%1 goto del" & vbCrLf & "del %0"
        My.Computer.FileSystem.WriteAllText("del.bat", bat, False, Text.Encoding.ASCII) '声明一个用于删除安装程序的批处理文件内容的字符串变量
        If My.Computer.FileSystem.FileExists("Setup.exe") Then Process.Start("Setup.exe") '新建一个内容为bat字符串变量的批处理文件
        Process.Start(New ProcessStartInfo With {.FileName = "del.bat", .Arguments = "AutoInstallerUpdater.exe", .Verb = "runas"})
    End Sub

End Module