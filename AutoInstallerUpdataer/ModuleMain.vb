Module ModuleMain

    Sub Main()
        With My.Computer.FileSystem
            Console.Write("正在安装更新")
            '1.5.30
            If .DirectoryExists("Data\Image") = True Then .RenameDirectory("Data\Image", "CD")
            If .DirectoryExists("Data\No-CD") = True Then .RenameDirectory("Data\No-CD", "NoCD")
            If .FileExists("Data\4GB\4gb_patch.exe") = True Then .MoveFile("Data\4GB\4gb_patch.exe", "Data\4GB.exe")
            If .FileExists("Data\638.EXE") = True Then .MoveFile("Data\638.EXE", "Data\638.EXE")
            If .FileExists("Data\638.rar") = True Then .MoveFile("Data\638.EXE", "Data\638.rar")
            If .FileExists("Data\640.exe") = True Then .MoveFile("Data\638.EXE", "Data\640.exe")
            If .FileExists("Data\640.rar") = True Then .MoveFile("Data\638.EXE", "Data\640.rar")
            Console.Write(vbCrLf) : Console.Write("更新安装完成")
            Dim bat As String = ":del" & vbCrLf & "del %1" & vbCrLf & "if exist %1 goto del" & vbCrLf & "del %0"
            My.Computer.FileSystem.WriteAllText("DeleteUpdater.bat", bat, False, Text.Encoding.ASCII)
            Process.Start("DeleteUpdater.bat", """" & Process.GetCurrentProcess.MainModule.FileName & """")
            Process.Start("Setup.exe")
        End With
    End Sub

End Module