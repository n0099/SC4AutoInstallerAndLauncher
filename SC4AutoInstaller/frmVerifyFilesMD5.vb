Public Class frmVerifyFilesMD5

    ''' <summary>一个用于存储要验证的文件的相对路径和MD5值的 List 泛型类</summary>
    Dim DataFilesMD5 As New List(Of String)({"Data\DAEMON Tools Lite 5.0.exe", "E4D2A05D4A5C22C6D4BC20D6B502CE6B", "Data\7z.exe", "BB146FFBDB414C2D7FDFABEBDFBF2DE6", "Data\SC4Launcher.exe", "8AC44C08782F6372DAC6886A992A9A28" _
                                            , "Data\Licenses\CC BY-NC-SA.rtf", "995C9B18CABFBB6DE54A4EE7886D843C", "Data\Licenses\CC BY-NC-SA 3.0 法律文本.rtf", "473B4BFEDFE91351CE00BB962284DBCC" _
                                            , "Data\Licenses\CC BY-NC-SA 4.0 法律文本.rtf", "E27D76D2E75DE182B6C10F6EBA0482A4", "Data\Licenses\EA EULA.txt", "4A263CEC16B302BE4E080A85614A90F9", "Data\Licenses\DAEMON Tools 隐私政策.rtf", "B772FA3468C7C3879A5A16614DC3613C" _
                                            , "Data\Patch\638.7z", "29AF195D1AB5F0ECCA63554E4BB69325", "Data\Patch\640.7z", "59CD8A9571880CA378AB0E5523E1D058", "Data\Patch\SimCity 4 641.exe", "53D2AE4FA9114B88AD91ECF32A7F16A4" _
                                            , "Data\Patch\SimCity 4 NoCD.exe", "B57B5B03C4854C194CE8BEBD173F3483", "Data\Patch\4GB.exe", "96490CFDF3C7DD5AE7EF378C689A8734" _
                                            , "Data\Patch\Language\TChinese\SimCityLocale.DAT", "3D7163C89D35E7388CF7EBC503BAF47B", "Data\Patch\Language\SChinese\SimCityLocale.DAT", "42E66866C5E7C95A29CD153423F4F6FD", "Data\Patch\Language\English\SimCityLocale.DAT", "196A1F3CD9CF58E84E0B0F31E9F81171"})

    ''' <summary>递归查询一个文件夹内所有的文件和文件夹的大小</summary>
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

    Private Sub bgwComputeMD5_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwComputeMD5.DoWork
        Dim MD5CSP As New Security.Cryptography.MD5CryptoServiceProvider '声明一个用于计算MD5值的System.Security.Cryptography.MD5CryptoServiceProvider类实例
        For i As Integer = 0 To DataFilesMD5.Count - 1 Step 2
            Try
Retry:          Dim File As New IO.FileStream(DataFilesMD5(i), IO.FileMode.Open) '声明一个用于计算文件MD5值的System.IO.FileStream类实例
                If bgwComputeMD5.CancellationPending = True Then File.Close() : e.Cancel = True : Exit For '如果请求取消验证则退出验证
                If My.Computer.FileSystem.FileExists(DataFilesMD5(i)) = False Then MessageBox.Show(DataFilesMD5(i) & " 文件不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Application.Exit()
                If BitConverter.ToString(MD5CSP.ComputeHash(File)).Replace("-", "") <> DataFilesMD5(i + 1) Then
                    Select Case MessageBox.Show("文件 " & DataFilesMD5(i) & " 不完整！", "错误", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                        Case DialogResult.Abort
                            Application.Exit()
                        Case DialogResult.Retry
                            File.Close() : GoTo Retry '关闭文件使用后重新开始验证
                        Case DialogResult.Ignore
                            If MessageBox.Show("确定忽略此错误吗？" & vbCrLf & "文件不完整可能会导致安装失败", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then GoTo Ignore Else GoTo Retry '跳转到下一个循环
                    End Select
                End If
Ignore:         bgwComputeMD5.ReportProgress(i)
                File.Close() '关闭文件使用
            Catch ex As IO.FileNotFoundException
                MessageBox.Show(DataFilesMD5(i) & " 文件不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Application.Exit()
            Catch ex As IO.DirectoryNotFoundException
                MessageBox.Show(DataFilesMD5(i).Remove(DataFilesMD5(i).LastIndexOf("\")) & " 文件夹不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Application.Exit()
            Catch ex As IO.IOException
                Select Case MessageBox.Show("文件 " & DataFilesMD5(i) & " 正在使用，无法验证此文件的完整性" & vbCrLf & "您可以尝试稍后再试", "错误", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2)
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

    Private Sub bgwComputeMD5_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwComputeMD5.ProgressChanged
        lblProgress.Text = Math.Truncate((prgProgress.Value / prgProgress.Maximum) * 100) & "% " & e.ProgressPercentage / 2 & "/" & DataFilesMD5.Count / 2
        prgProgress.Value += Int(New IO.FileInfo(DataFilesMD5(e.ProgressPercentage)).Length / 1024)
    End Sub

    Private Sub bgwComputeMD5_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwComputeMD5.RunWorkerCompleted
        If e.Cancelled = False Then
            frmMain.Show()
            RemoveHandler Me.FormClosing, AddressOf frmVerifyFilesMD5_FormClosing '移除关闭窗口过程和关闭窗口事件的关联
            Close()
        End If
    End Sub

    Private Sub frmVerifyFilesMD5_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            If MessageBox.Show("确定要取消文件验证吗？" & vbCrLf & "如果文件不完整可能会导致安装失败", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                frmMain.Show() : bgwComputeMD5.CancelAsync() '取消异步验证文件完整性
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmVerifyFiles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With My.Computer.FileSystem
            If .DirectoryExists("Data") = False Then MessageBox.Show("Data 文件夹不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Application.Exit()
            Dim CDSC4FilesMD5() As String = {"Data\SC4\CD\CD1.mdf", "82A112B441DC90305331ABEFF0E66237", "Data\SC4\CD\CD1.mds", "CFB13663F10FCAB916C0A4EDD29FC975" _
                                            , "Data\SC4\CD\CD2.mdf", "15AD42821D2CCFAC4ED62CF2E5E153D1", "Data\SC4\CD\CD2.mds", "F623584CCC7E3206045D97CD12D454C8"}
            Dim NoInstallSC4FileMD5() As String = {"Data\SC4\NoInstall.7z", "7B9E95C32FDF1DB09B2835A70E821DA9"}
            If .DirectoryExists("Data\SC4\CD") = True Then DataFilesMD5.AddRange(CDSC4FilesMD5) '如果存在Data\SC4\CD文件夹则向要验证的文件列表里增加项
            If .FileExists("Data\SC4\NoInstall.7z") = True Then DataFilesMD5.AddRange(NoInstallSC4FileMD5) '如果存在Data\SC4\NoInstall.7z文件则向要验证的文件列表里增加项
        End With
        lblProgress.Text = "0% 0/" & DataFilesMD5.Count / 2 '初始化进度条和进度文本
        prgProgress.Maximum = Int(GetFolderSize("Data") / 1024)
        bgwComputeMD5.RunWorkerAsync() '开始异步验证文件完整性
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

End Class