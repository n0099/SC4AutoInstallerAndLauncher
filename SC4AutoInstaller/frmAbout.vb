Public Class frmAbout

    Private Sub llbBlog_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbBlog.LinkClicked
        Process.Start("http://n0099.sinaapp.com")
    End Sub

    Private Sub llbReportBug_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbReportBug.LinkClicked
        Process.Start("http://tieba.baidu.com/p/3802761033")
    End Sub

    Private Sub llbSCB_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSCB.LinkClicked
        Process.Start("http://tieba.baidu.com/f?kw=%C4%A3%C4%E2%B3%C7%CA%D0")
    End Sub

    Private Sub llbSCCN_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSCCN.LinkClicked
        Process.Start("http://www.simcity.cn")
    End Sub

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblVersion.Text = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision '初始化版本号文本
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

End Class