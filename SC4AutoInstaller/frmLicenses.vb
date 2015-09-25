Public Class frmLicenses

    Dim i As Integer
    ''' <summary>一个用于存储协议文件路径的字符串数组</summary>
    Dim Licenses() As String = {"Data\Licenses\CC BY-NC-SA.rtf", "Data\Licenses\CC BY-NC-SA 3.0 法律文本.rtf" _
                               , "Data\Licenses\CC BY-NC-SA 4.0 法律文本.rtf", "Data\Licenses\EA EULA.txt", "Data\Licenses\DAEMON Tools 隐私政策.rtf"}

    Private Sub btnAgree_Click(sender As Object, e As EventArgs) Handles btnAgree.Click
        Select Case i
            Case 3 : rtxLinence.Clear() : rtxLinence.LoadFile(Licenses(i), RichTextBoxStreamType.PlainText) '以纯文本方式加载Data\Licenses\EA EULA.txt文件的内容
            Case 5
                If IsNothing(ModuleMain.InstalledModule) = True Then frmInstallOptions.Show() Else frmChangeModuleOptions.Show()
                RemoveHandler Me.FormClosing, AddressOf frmLicenses_FormClosing '移除关闭窗口过程和关闭窗口事件的关联 
                Close()
            Case Else : rtxLinence.Clear() : rtxLinence.LoadFile(Licenses(i)) '清空富文本框的内容以便将光标移到最上面
        End Select
        i += 1
    End Sub

    Private Sub btnDisagree_Click(sender As Object, e As EventArgs) Handles btnDisagree.Click
        Application.Exit()
    End Sub

    Private Sub frmLicenses_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("确定要退出安装程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then e.Cancel = True
    End Sub

    Private Sub frmLicenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rtxLinence.LoadFile(Licenses(i)) : i += 1 '以富文本模式加载文件
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099" '初始化窗口标题
    End Sub

End Class