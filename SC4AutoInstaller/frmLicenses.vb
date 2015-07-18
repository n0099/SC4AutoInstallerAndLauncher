Public Class frmLicenses

    Dim i As Integer
    Dim Licenses() As String = {"Data\Licenses\CC BY-NC-SA.rtf", "Data\Licenses\CC BY-NC-SA 3.0 法律文本.rtf", _
                                "Data\Licenses\CC BY-NC-SA 4.0 法律文本.rtf", "Data\Licenses\EA EULA.txt", "Data\Licenses\DAEMON Tools 隐私政策.rtf"}

    Private Sub rtxLinence_GotFocus(sender As Object, e As EventArgs) Handles rtxLinence.GotFocus
        'btnDisagree.Focus()
    End Sub

    Private Sub btnAgree_Click(sender As Object, e As EventArgs) Handles btnAgree.Click
        Select Case i
            Case 3 : rtxLinence.Clear() : rtxLinence.LoadFile(Licenses(i), RichTextBoxStreamType.PlainText)
            Case 5 : frmInstallOptions.Show() : Close()
            Case Else : rtxLinence.Clear() : rtxLinence.LoadFile(Licenses(i)) : rtxLinence.DeselectAll()
        End Select
        i += 1
    End Sub

    Private Sub btnDisagree_Click(sender As Object, e As EventArgs) Handles btnDisagree.Click
        Application.Exit()
    End Sub

    Private Sub frmLicenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rtxLinence.LoadFile(Licenses(i)) : i += 1
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099"
    End Sub

End Class