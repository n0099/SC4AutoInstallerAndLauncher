Public Class frmVerifyFilesMD5

    Dim MD5CSP As New Security.Cryptography.MD5CryptoServiceProvider
    Dim DataFilesMD5() As String = {"Data\DAEMON Tools Lite 5.0.exe", "E4D2A05D4A5C22C6D4BC20D6B502CE6B", "Data\EA EULA.txt", "4A263CEC16B302BE4E080A85614A90F9" _
                                , "Data\rar.exe", "863B5C17C3A02095DFAE098CBCC09A6E", "Data\SC4Launcher.exe", "4E40154B59FA04764617ED1B052D2180" _
                                , "Data\SC4.rar", "2ACDA5FEEE321943722FA49C70D47DF1" _
                                , "Data\CD\SC4DELUXE CD1.mdf", "82A112B441DC90305331ABEFF0E66237", "Data\CD\SC4DELUXE CD1.mds", "CFB13663F10FCAB916C0A4EDD29FC975" _
                                , "Data\CD\SC4DELUXE CD2.mdf", "15AD42821D2CCFAC4ED62CF2E5E153D1", "Data\CD\SC4DELUXE CD2.mds", "F623584CCC7E3206045D97CD12D454C8" _
                                , "Data\Patch\638\638.EXE", "CF95BA3341D0832B532CE176492321D1", "Data\Patch\640\640.exe", "E612D3BF65DFA7BED951CC8D40366BBF" _
                                , "Data\Patch\638\638.rar", "198FC87663DFA08CFACED3600F97C9C6", "Data\Patch\640\640.rar", "70912679404A52B4F0A3FA41C98B2335" _
                                , "Data\Patch\NoCD\SimCity 4.exe", "B57B5B03C4854C194CE8BEBD173F3483", "Data\Patch\4GB.exe", "96490CFDF3C7DD5AE7EF378C689A8734" _
                                , "Data\Patch\Language\TChinese\SimCityLocale.DAT", "3D7163C89D35E7388CF7EBC503BAF47B" _
                                , "Data\Patch\Language\SChinese\SimCityLocale.DAT", "42E66866C5E7C95A29CD153423F4F6FD" _
                                , "Data\Patch\Language\English\SimCityLocale.DAT", "196A1F3CD9CF58E84E0B0F31E9F81171"}

    Private Sub bgwComputeMD5_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwComputeMD5.DoWork
        For i As Integer = 0 To DataFilesMD5.Length - 1 Step 2
Retry:      If bgwComputeMD5.CancellationPending = True Then e.Cancel = True : Exit For
            If My.Computer.FileSystem.FileExists(DataFilesMD5(i)) = False Then MessageBox.Show(DataFilesMD5(i) & " 文件不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error) : Application.Exit()
            Dim File As New IO.FileStream(DataFilesMD5(i), IO.FileMode.Open)
            If i = 0 Then bgwComputeMD5.ReportProgress(1) Else bgwComputeMD5.ReportProgress(i / 2)
            If BitConverter.ToString(MD5CSP.ComputeHash(File)).Replace("-", "") <> DataFilesMD5(i + 1) Then
Ignore:         Select Case MessageBox.Show("文件 " & DataFilesMD5(i) & " 不完整！", "错误", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                    Case Windows.Forms.DialogResult.Abort
                        Environment.Exit(0)
                    Case Windows.Forms.DialogResult.Retry
                        GoTo Retry
                    Case Windows.Forms.DialogResult.Ignore
                        If MessageBox.Show("确定忽略此错误吗？" & vbCrLf & "文件不完整可能会导致安装失败。", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then Continue For Else GoTo Ignore
                End Select
            End If
            File.Close()
        Next
    End Sub

    Private Sub bgwComputeMD5_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwComputeMD5.ProgressChanged
        lblProgress.Text = Math.Truncate((e.ProgressPercentage / (DataFilesMD5.Length / 2)) * 100) & "% " & e.ProgressPercentage & "/" & DataFilesMD5.Length / 2
        prgProgress.Value = e.ProgressPercentage
    End Sub

    Private Sub bgwComputeMD5_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwComputeMD5.RunWorkerCompleted
        If e.Cancelled = False Then frmMain.Show() : Close()
    End Sub

    Private Sub frmVerifyFiles_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Computer.FileSystem.DirectoryExists("Data") = False Then MessageBox.Show("Data 文件夹不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) : Application.Exit()
        lblProgress.Text = "0% 0/" & DataFilesMD5.Length / 2
        prgProgress.Maximum = DataFilesMD5.Length / 2
        bgwComputeMD5.RunWorkerAsync()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If MessageBox.Show("确定要取消文件验证吗？" & vbCrLf & "如果文件不完整可能会导致安装失败。", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then frmMain.Show() : bgwComputeMD5.CancelAsync() : Close()
    End Sub

End Class