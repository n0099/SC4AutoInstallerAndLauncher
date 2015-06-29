Public Class frmUninstalling

    Private Sub frmUninstalling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099"
    End Sub

End Class