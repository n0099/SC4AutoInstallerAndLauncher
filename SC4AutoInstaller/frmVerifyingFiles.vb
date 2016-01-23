Public Class frmVerifyingFiles

    ''' <summary>一个用于存储要验证的文件的相对路径和MD5值的List泛型类</summary>
    Dim DataFilesMD5 As New List(Of String)({"Data\7za.exe", "EC79CABD55A14379E4D676BB17D9E3DF",
                                            "Data\DAEMON Tools Lite 5.0.exe", "E4D2A05D4A5C22C6D4BC20D6B502CE6B",
                                            "Data\Licenses\CC BY-NC-SA.rtf", "995C9B18CABFBB6DE54A4EE7886D843C",
                                            "Data\Licenses\CC BY-NC-SA 3.0 法律文本.rtf", "473B4BFEDFE91351CE00BB962284DBCC",
                                            "Data\Licenses\CC BY-NC-SA 4.0 法律文本.rtf", "E27D76D2E75DE182B6C10F6EBA0482A4",
                                            "Data\Licenses\DAEMON Tools 隐私政策.rtf", "B772FA3468C7C3879A5A16614DC3613C",
                                            "Data\Licenses\EA EULA.txt", "4A263CEC16B302BE4E080A85614A90F9",
                                            "Data\Patch\4GB.exe", "96490CFDF3C7DD5AE7EF378C689A8734",
                                            "Data\Patch\638 SKU1.EXE", "CF95BA3341D0832B532CE176492321D1",
                                            "Data\Patch\640.exe", "E612D3BF65DFA7BED951CC8D40366BBF",
                                            "Data\Patch\641.7z", "15A5635619A9C8995B11804471A79DA0",
                                            "Data\Patch\Graphics Rules GOG.sgr", "DCF0FA2DE3828BC52991BDA20B7E5735",
                                            "Data\Patch\SC4Launcher.exe", "64B105B51E5DE6F291158CF2D1239898",
                                            "Data\Patch\Language\English\SimCityLocale.DAT", "196A1F3CD9CF58E84E0B0F31E9F81171",
                                            "Data\Patch\Language\SChinese\SimCityLocale.DAT", "42E66866C5E7C95A29CD153423F4F6FD",
                                            "Data\Patch\Language\TChinese\SimCityLocale.DAT", "3D7163C89D35E7388CF7EBC503BAF47B",
                                            "Data\Patch\SimCity 4.exe\SimCity 4 610.exe", "427BE3767B1B20866F42D6197EA67AF0",
                                            "Data\Patch\SimCity 4.exe\SimCity 4 638.exe", "9ACB71D6D2302158CA614B21A9B187E4",
                                            "Data\Patch\SimCity 4.exe\SimCity 4 640.exe", "D4796905AAFF2B2DE44C2B59D103F5EA",
                                            "Data\Patch\SimCity 4.exe\SimCity 4 641.exe", "53D2AE4FA9114B88AD91ECF32A7F16A4",
                                            "Data\Patch\SimCity 4.exe\SimCity 4 NoCD.exe", "B57B5B03C4854C194CE8BEBD173F3483",
                                            "Data\SC4\NoInstall.7z", "96C3021E01A4C34FDABDF3B3EB1F92F2",
                                            "Data\SC4\CD\CD1.mdf", "82A112B441DC90305331ABEFF0E66237",
                                            "Data\SC4\CD\CD1.mds", "CFB13663F10FCAB916C0A4EDD29FC975",
                                            "Data\SC4\CD\CD2.mdf", "15AD42821D2CCFAC4ED62CF2E5E153D1",
                                            "Data\SC4\CD\CD2.mds", "F623584CCC7E3206045D97CD12D454C8"})

    ''' <summary>递归返回某个文件夹内所有的文件和文件夹的大小</summary>
    ''' <param name="path">要查询的文件夹的路径</param>
    ''' <returns>返回文件夹内所有的文件和文件夹的大小</returns>
    Private Function GetFolderSize(ByVal path As String) As Long
        Dim size As Long
        For Each i As IO.FileInfo In New IO.DirectoryInfo(path).GetFiles
            size += i.Length
        Next
        For Each i As IO.DirectoryInfo In New IO.DirectoryInfo(path).GetDirectories
            size += GetFolderSize(i.FullName) '递归返回子文件夹的大小
        Next
        Return size
    End Function

    Private Sub bgwVerifyFilesMD5_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwVerifyFilesMD5.DoWork
        Dim MD5CSP As New Security.Cryptography.MD5CryptoServiceProvider
        For i As Integer = 0 To DataFilesMD5.Count - 1 Step 2
            Try
Retry:          If bgwVerifyFilesMD5.CancellationPending Then e.Cancel = True : Exit Sub '如果请求取消验证则退出验证
                Dim File As IO.FileStream = New IO.FileStream(DataFilesMD5(i), IO.FileMode.Open)
                If My.Computer.FileSystem.FileExists(DataFilesMD5(i)) = False Then MessageBox.Show(DataFilesMD5(i) & "文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Application.Exit()
                If BitConverter.ToString(MD5CSP.ComputeHash(File)).Replace("-", "") <> DataFilesMD5(i + 1) Then
                    Select Case MessageBox.Show("文件" & DataFilesMD5(i) & "不完整", "错误", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                        Case DialogResult.Abort
                            Application.Exit()
                        Case DialogResult.Retry
                            File.Close() : GoTo Retry '关闭文件使用后重新开始验证
                        Case DialogResult.Ignore
                            If MessageBox.Show("确定忽略此错误吗？" & vbCrLf & "文件不完整可能会导致安装失败", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then GoTo Ignore Else GoTo Retry '跳转到下一个循环
                    End Select
                End If
Ignore:         bgwVerifyFilesMD5.ReportProgress(i)
                File.Close() '关闭文件使用
            Catch ex As IO.FileNotFoundException
                MessageBox.Show(DataFilesMD5(i) & "文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Application.Exit()
            Catch ex As IO.DirectoryNotFoundException
                MessageBox.Show(DataFilesMD5(i).Remove(DataFilesMD5(i).LastIndexOf("\")) & "文件夹不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Application.Exit()
            Catch ex As IO.IOException
                Select Case MessageBox.Show("文件" & DataFilesMD5(i) & "已被其他进程占用，无法验证此文件的完整性" & vbCrLf & "您可以尝试稍后再试", "错误", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
                    Case DialogResult.Abort
                        Application.Exit()
                    Case DialogResult.Retry
                        GoTo Retry
                    Case DialogResult.Ignore
                        If MessageBox.Show("确定忽略此错误吗？" & vbCrLf & "文件不完整可能会导致安装失败", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then GoTo Ignore Else GoTo Retry
                End Select
            End Try
        Next
    End Sub

    Private Sub bgwVerifyFilesMD5_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwVerifyFilesMD5.ProgressChanged
        lblProgress.Text = Math.Truncate((prgVerifyFilesMD5.Value / prgVerifyFilesMD5.Maximum) * 100) & "% " & e.ProgressPercentage / 2 & "/" & DataFilesMD5.Count / 2
        prgVerifyFilesMD5.Value += Int(New IO.FileInfo(DataFilesMD5(e.ProgressPercentage)).Length / 1024)
    End Sub

    Private Sub bgwVerifyFilesMD5_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwVerifyFilesMD5.RunWorkerCompleted
        If e.Cancelled = False Then
            Threading.Thread.Sleep(500) '挂起当前线程0.5秒以便让用户看到验证结果
            frmMain.Show()
            Dispose() '直接释放窗口以避免触发FormClosing事件
        End If
    End Sub

    Private Sub frmVerifyingFiles_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            If MessageBox.Show("确定要取消文件验证吗？" & vbCrLf & "如果文件不完整可能会导致安装失败", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                frmMain.Show() : bgwVerifyFilesMD5.CancelAsync() '取消异步验证文件完整性
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmVerifyFiles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblProgress.Text = "0% 0/" & DataFilesMD5.Count / 2 '初始化进度条和进度文本
        prgVerifyFilesMD5.Maximum = Int(GetFolderSize("Data") / 1024)
        bgwVerifyFilesMD5.RunWorkerAsync() '开始异步验证文件完整性
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

End Class