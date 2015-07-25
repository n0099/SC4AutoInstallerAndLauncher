Imports System.Text.RegularExpressions

Public Class frmSetting

    ''' <summary>临时存储的启动参数</summary>
    Private Argument As String = ""

    ''' <summary>验证指定文本框的文本是否为有效的目录路径</summary>
    ''' <param name="TextBox">要验证的文本框</param>
    ''' <returns>如果文本框的文本为有效的目录路径，则为True；否则为False</returns>
    Private Function IsPathValidated(ByVal TextBox As TextBox) As Boolean
        '声明一个用于存储要验证的安装目录是属于哪个组件的的字符串变量和一个存储要验证的安装目录的路径的字符串变量
        Dim TextBoxName As String = IIf(TextBox.Name = "txtUserDir", "用户文件目录", "模拟城市4安装目录"), Path As String = TextBox.Text.Trim()
        If Path = Nothing Then
            MessageBox.Show(TextBoxName & " 的安装路径不能为空！" & vbCrLf & "您必须输入一个带分区卷标的完整路径！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
        ElseIf System.Text.RegularExpressions.Regex.IsMatch(Path, "[A-Za-z]\:\\") = False Then '使用正则表达式判断路径是否存在分区盘符
            MessageBox.Show(TextBoxName & " 的安装路径格式不正确！" & vbCrLf & "您必须输入一个带分区卷标和安装文件夹名的完整路径！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
        ElseIf System.Text.RegularExpressions.Regex.IsMatch(Path.Remove(0, Path.IndexOf("\") + 1).TrimEnd("\"), "[\\,\/,\:,\*,\?,\"",\<,\>,\|]") = True Then '使用正则表达式判断去除开头的分区盘符和最后的\后的字符串是否存在不允许的字符
            MessageBox.Show(TextBoxName & " 的安装路径不能包含下列任何字符：" & vbCrLf & "\ / : * ? "" < > |", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
        ElseIf My.Computer.FileSystem.GetDriveInfo(Path.Substring(0, 1)).IsReady = False Then '判断安装路径的分区是否可写
            MessageBox.Show(TextBoxName & " 的安装路径的分区不存在或为不可写的分区！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) : Return False
        End If
        Return True
    End Function

    ''' <summary>更新启动参数文本框的文本并激活应用按钮</summary>
    Private Sub ArgumentChanged()
        txtArgument.Text = Argument
        btnApply.Enabled = True
    End Sub

    Private Sub rdoDisplayMode_CheckedChanged(sender As RadioButton, e As EventArgs) Handles rdoDisplayModeWindow.CheckedChanged, rdoDisplayModeFullScreen.CheckedChanged
        Select Case sender.Text
            Case "窗口化" : Argument = Argument.Replace("-fs ", "") : Argument &= "-w " : ArgumentChanged()
            Case "全屏" : Argument = Argument.Replace("-w ", "") : Argument &= "-fs " : ArgumentChanged()
        End Select
    End Sub

    Private Sub rdoResolution_CheckedChanged(sender As RadioButton, e As EventArgs) Handles rdoFixedResolution.CheckedChanged, rdoCustomResolution.CheckedChanged, rdoDefaultResolution.CheckedChanged
        Select Case sender.Text
            Case "固定分辨率："
                cmbFixedResolution.Enabled = True : mtxCustomResolution.Enabled = False
                If Argument.Contains("-CustomResolution") = True Then Argument = Argument.Replace("-CustomResolution ", "")
            Case "自定义分辨率："
                cmbFixedResolution.Enabled = False : mtxCustomResolution.Enabled = True
                If Argument.Contains("-CustomResolution") = False Then Argument &= "-CustomResolution "
            Case "自动"
                cmbFixedResolution.Enabled = False : mtxCustomResolution.Enabled = False
                Argument = Argument.Replace("-CustomResolution ", "")
                Dim tmp As String = Regex.Match(Argument, "-r\d{3,4}x\d{3,4} ").ToString
                If tmp <> Nothing Then Argument = Argument.Replace(tmp, "")
        End Select
        ArgumentChanged()
    End Sub

    Private Sub cmbFixedResolution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFixedResolution.SelectedIndexChanged
        Argument = Argument.Replace("-CustomResolution ", "")
        Dim tmp As String = Regex.Match(Argument, "-r\d{3,4}x\d{3,4} ").ToString
        If tmp <> Nothing Then Argument = Argument.Replace(tmp, "")
        Select Case cmbFixedResolution.SelectedItem
            Case "800x600" : Argument &= "-r800x600 "
            Case "1024x768" : Argument &= "-r1024x768 "
            Case "1280x1024" : Argument &= "-r1280x1024 "
        End Select
        ArgumentChanged()
    End Sub

    Private Sub mtxCustomResolution_TextChanged(sender As Object, e As EventArgs) Handles mtxCustomResolution.TextChanged
        If Argument.Contains("-CustomResolution") = False Then Argument &= "-CustomResolution "
        Dim tmp As String = Regex.Match(Argument, "-r\d{3,4}x\d{3,4} ").ToString
        If tmp <> Nothing Then Argument = Argument.Replace(tmp, "")
        If mtxCustomResolution.Text.Length > 7 Then Argument &= "-r" & mtxCustomResolution.Text.Replace(" ", "") & " " : ArgumentChanged()
    End Sub

    Private Sub cmbCPUPriority_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCPUPriority.SelectedIndexChanged
        Dim tmp As String = Regex.Match(Argument, "-CPUPriority:[a-z]{3,6} ").ToString
        If tmp <> Nothing Then Argument = Argument.Replace(tmp, "")
        Select Case cmbCPUPriority.SelectedItem
            Case "低" : Argument &= "-CPUPriority:low "
            Case "正常" : Argument &= "-CPUPriority:normal "
            Case "高" : Argument &= "-CPUPriority:high "
        End Select
        ArgumentChanged()
    End Sub

    Private Sub nudCPUCount_ValueChanged(sender As Object, e As EventArgs) Handles nudCPUCount.ValueChanged
        Dim tmp As String = Regex.Match(Argument, "-CPUCount:\d{1,3} ").ToString
        If tmp <> Nothing Then Argument = Argument.Replace(tmp, "")
        Argument &= "-CPUCount:" & nudCPUCount.Value & " " : ArgumentChanged()
    End Sub

    Private Sub cmbBitDepth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBitDepth.SelectedIndexChanged
        Dim tmp As String = Regex.Match(Argument, "-Cursors:[a-z,1-9]{2,9} ").ToString
        If tmp <> Nothing Then Argument = Argument.Replace(tmp, "")
        Select Case cmbBitDepth.SelectedItem
            Case "黑白" : Argument &= "-Cursors:bw "
            Case "16位色" : Argument &= "-Cursors:color16 "
            Case "256位色" : Argument &= "-Cursors:color256 "
            Case "真彩色" : Argument &= "-Cursors:fullcolor "
        End Select
        ArgumentChanged()
    End Sub

    Private Sub cmbRenderMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRenderMode.SelectedIndexChanged
        Dim tmp As String = Regex.Match(Argument, "-d:[A-Z,a-z]{6,8} ").ToString
        If tmp <> Nothing Then Argument = Argument.Replace(tmp, "")
        Select Case cmbRenderMode.SelectedItem
            Case "DirectX" : Argument &= "-d:DirectX "
            Case "OpenGL" : Argument &= "-d:OpenGL "
            Case "软件渲染" : Argument &= "-d:Software "
        End Select
        ArgumentChanged()
    End Sub

    Private Sub CheckBoxs_CheckedChanged(sender As CheckBox, e As EventArgs) Handles chkIntro.CheckedChanged, chkAudio.CheckedChanged, chkAllowMultipleInstances.CheckedChanged, _
        chkShowMissingModel.CheckedChanged, chkLoadModelBackground.CheckedChanged, chkWriteLog.CheckedChanged, chkCustomCursors.CheckedChanged, chkCustomIME.CheckedChanged, _
        chkRestartAfterException.CheckedChanged, chkExceptionHandling.CheckedChanged, chkContinueGameBackground.CheckedChanged
        Select Case sender.Name
            Case "chkIntro" : If chkIntro.Checked = True Then Argument = Argument.Replace("-Intro:off ", "") Else Argument &= "-Intro:off "
            Case "chkAudio" : If chkAudio.Checked = True Then Argument = Argument.Replace("-audio:off ", "") Else Argument &= "-audio:off "
            Case "chkAllowMultipleInstances" : If chkAllowMultipleInstances.Checked = False Then Argument = Argument.Replace("-AllowMultipleInstances ", "") Else Argument &= "-AllowMultipleInstances "
            Case "chkShowMissingModel"
                If chkShowMissingModel.Checked = True Then
                    If Argument.Contains("-IgnoreMissingModelDataBugs") = True Then Argument = Argument.Replace("-IgnoreMissingModelDataBugs:on ", "-IgnoreMissingModelDataBugs:off ") Else Argument &= "-IgnoreMissingModelDataBugs:off "
                Else
                    If Argument.Contains("-IgnoreMissingModelDataBugs") = True Then Argument = Argument.Replace("-IgnoreMissingModelDataBugs:off ", "-IgnoreMissingModelDataBugs:on ") Else Argument &= "-IgnoreMissingModelDataBugs:on "
                End If
            Case "chkLoadModelBackground" : If chkLoadModelBackground.Checked = True Then Argument = Argument.Replace("-BackgroundLoader:off ", "") Else Argument &= "-BackgroundLoader:off "
            Case "chkWriteLog" : If chkWriteLog.Checked = True Then Argument = Argument.Replace("-WriteLog:off ", "") Else Argument &= "-WriteLog:off "
            Case "chkCustomCursors"
                If chkCustomCursors.Checked = True Then
                    If Argument.Contains("-CustomCursors") = True Then Argument = Argument.Replace("-CustomCursors:enabled ", "-CustomCursors:disabled ") Else Argument &= "-CustomCursors:disabled "
                Else
                    If Argument.Contains("-CustomCursors") = True Then Argument = Argument.Replace("-CustomCursors:disabled ", "-CustomCursors:enabled ") Else Argument &= "-CustomCursors:enabled "
                End If
            Case "chkCustomIME"
                If chkCustomIME.Checked = True Then
                    If Argument.Contains("-IME") = True Then Argument = Argument.Replace("-IME:enabled ", "-IME:disabled ") Else Argument &= "-IME:disabled "
                Else
                    If Argument.Contains("-IME") = True Then Argument = Argument.Replace("-IME:disabled ", "-IME:enabled ") Else Argument &= "-IME:enabled "
                End If
            Case "chkRestartAfterException" : If chkRestartAfterException.Checked = False Then Argument = Argument.Replace("-Restart ", "") Else Argument &= "-Restart "
            Case "chkExceptionHandling"
                If chkExceptionHandling.Checked = True Then
                    If Argument.Contains("-ExceptionHandling") = True Then Argument = Argument.Replace("-ExceptionHandling:off ", "-ExceptionHandling:on ") Else Argument &= "-ExceptionHandling:on "
                Else
                    If Argument.Contains("-ExceptionHandling") = True Then Argument = Argument.Replace("-ExceptionHandling:on ", "-ExceptionHandling:off ") Else Argument &= "-ExceptionHandling:off "
                End If
            Case "chkContinueGameBackground" : If chkContinueGameBackground.Checked = True Then Argument = Argument.Replace("-gp ", "") Else Argument &= "-gp "
        End Select
        ArgumentChanged()
    End Sub

    Private Sub chkUserDir_CheckedChanged(sender As Object, e As EventArgs) Handles chkUserDir.CheckedChanged
        txtUserDir.Enabled = chkUserDir.Checked : btnUserDir.Enabled = chkUserDir.Checked : lblUserDir.Enabled = chkUserDir.Checked
        With Argument
            If chkUserDir.Checked = True And txtUserDir.Text <> Nothing Then
                If .StartsWith(" ") = True Then Argument &= "-UserDir:""" & txtUserDir.Text & """ " Else Argument &= " -UserDir:""" & txtUserDir.Text & """ "
            Else
                If .Contains("-UserDir") = True Then
                    Argument = .Remove(.IndexOf("-UserDir"), (.LastIndexOf("""") + 2) - .IndexOf("-UserDir")) : txtUserDir.Text = ""
                End If
            End If
        End With
        ArgumentChanged()
    End Sub

    Private Sub btnUserDir_Click(sender As Object, e As EventArgs) Handles btnUserDir.Click
        If fbdUserDir.ShowDialog = Windows.Forms.DialogResult.OK And fbdUserDir.SelectedPath <> Nothing Then
            txtUserDir.Text = fbdUserDir.SelectedPath
            With Argument
                If .Contains("-UserDir") = True Then
                    Argument = .Replace(.Substring(.IndexOf(""""), .LastIndexOf("""") - .IndexOf("""")), """" & txtUserDir.Text)
                Else
                    If Argument = "" OrElse (.StartsWith(" ") = True Or .EndsWith(" ") = True) Then
                        Argument &= "-UserDir:""" & txtUserDir.Text & """ "
                    Else
                        Argument &= " -UserDir:""" & txtUserDir.Text & """ "
                    End If
                End If
            End With
            ArgumentChanged()
        End If
    End Sub

    Private Sub txtUserDir_TextChanged(sender As Object, e As EventArgs) Handles txtUserDir.TextChanged
        With Argument
            If txtUserDir.Text <> Nothing And .Contains("-UserDir") = True Then
                Argument = .Replace(.Substring(.IndexOf(""""), .LastIndexOf("""") - .IndexOf("""")), """" & txtUserDir.Text)
            ElseIf txtUserDir.Text <> Nothing And .Contains("-UserDir") = False Then
                If Argument = "" OrElse (.StartsWith(" ") = True Or .EndsWith(" ") = True) Then
                    Argument &= "-UserDir:""" & txtUserDir.Text & """ "
                Else
                    Argument &= " -UserDir:""" & txtUserDir.Text & """ "
                End If
            End If
        End With
        ArgumentChanged()
    End Sub

    Private Sub btnSC4Installdir_Click(sender As Object, e As EventArgs) Handles btnSC4InstallDir.Click
        fbdSC4InstallDir.SelectedPath = My.Settings.SC4InstallDir
        If fbdSC4InstallDir.ShowDialog = Windows.Forms.DialogResult.OK And fbdSC4InstallDir.SelectedPath <> Nothing Then
            If My.Computer.FileSystem.FileExists(fbdSC4InstallDir.SelectedPath & "\Apps\SimCity 4.exe") = True Then
                If MessageBox.Show("模拟城市4安装目录里没有游戏程序！" & vbCrLf & "是否重新选择模拟城市4安装目录？", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = Windows.Forms.DialogResult.Yes Then
                    fbdSC4InstallDir.ShowDialog()
                Else
                    Exit Sub
                End If
            End If
            txtSC4InstallDir.Text = fbdSC4InstallDir.SelectedPath
            ArgumentChanged()
        End If
    End Sub

    Private Sub txtSC4InstallDir_TextChanged(sender As Object, e As EventArgs)
        txtSC4InstallDir.Text = fbdSC4InstallDir.SelectedPath
        ArgumentChanged()
    End Sub

    Private Sub btnDeleteSC4cfgFile_Click(sender As Object, e As EventArgs) Handles btnDeleteSC4cfgFile.Click
        Dim SC4cfgFilePath As String
        With My.Settings.Argument
            If .Contains("-UserDir") = True Then
                SC4cfgFilePath = .Substring(.IndexOf("""") + 1, .LastIndexOf("""") - 1) & "\SimCity 4.cfg"
            Else
                SC4cfgFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SimCity 4\SimCity 4.cfg"
            End If
        End With
        If My.Computer.FileSystem.FileExists(SC4cfgFilePath) = True Then
            My.Computer.FileSystem.DeleteFile(SC4cfgFilePath)
            MessageBox.Show("已成功删除SimCity 4.cfg文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("SimCity 4.cfg文件不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub frmSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With My.Settings.Argument '读取启动参数里的设置，并同步设置窗口上对应的选项
            rdoDisplayModeWindow.Checked = .Contains("-w")
            rdoDisplayModeFullScreen.Checked = .Contains("-fs")
            If .Contains("-r") = True And .Contains("-CustomResolution") = False Then
                rdoFixedResolution.Checked = True : mtxCustomResolution.Enabled = False
                Select Case Regex.Match(My.Settings.Argument, "-r\d{3,4}x\d{3,4}").ToString.Remove(0, 2)
                    Case "800x600" : cmbFixedResolution.SelectedItem = "800x600"
                    Case "1024x768" : cmbFixedResolution.SelectedItem = "1024x768"
                    Case "1280x1024" : cmbFixedResolution.SelectedItem = "1280x1024"
                End Select
            ElseIf .Contains("-r") = True And .Contains("-CustomResolution") = True Then
                rdoCustomResolution.Checked = True : cmbFixedResolution.Enabled = False
                mtxCustomResolution.Text = Regex.Match(My.Settings.Argument, "-r\d{3,4}").ToString.Remove(0, 2) & "x" & Regex.Match(My.Settings.Argument, "x\d{3,4}").ToString.Remove(0, 1)
            ElseIf .Contains("-r") = False Then
                rdoDefaultResolution.Checked = True : cmbFixedResolution.Enabled = False : mtxCustomResolution.Enabled = False
            End If
            If .Contains("-CPUPriority") = True Then
                Select Case Regex.Match(My.Settings.Argument, "-CPUPriority:[a-z]{3,6}").ToString.Remove(0, 12)
                    Case "low" : cmbCPUPriority.SelectedItem = "低"
                    Case "normal" : cmbCPUPriority.SelectedItem = "正常"
                    Case "high" : cmbCPUPriority.SelectedItem = "高"
                End Select
            Else
                cmbCPUPriority.SelectedItem = "自动"
            End If
            If .Contains("-CPUCount") = True Then nudCPUCount.Value = Regex.Match(My.Settings.Argument, "-CPUCount:\d{1,3}").ToString.Remove(0, 10) Else nudCPUCount.Value = Environment.ProcessorCount
            nudCPUCount.Maximum = Environment.ProcessorCount
            If .Contains("-Cursors") = True Then
                Select Case Regex.Match(My.Settings.Argument, "-Cursors:[a-z,1-9]{2,9}").ToString.Remove(0, 9)
                    Case "bw" : cmbBitDepth.SelectedItem = "黑白"
                    Case "color16" : cmbBitDepth.SelectedItem = "16位色"
                    Case "color256" : cmbBitDepth.SelectedItem = "256位色"
                    Case "fullcolor" : cmbBitDepth.SelectedItem = "真彩色"
                End Select
            Else
                cmbBitDepth.SelectedItem = "自动"
            End If
            If .Contains("-d") = True Then
                Select Case Regex.Match(My.Settings.Argument, "-d:[A-Z,a-z]{1,8}").ToString.Remove(0, 2)
                    Case "DirectX" : cmbRenderMode.SelectedItem = "DirectX"
                    Case "OpenGL" : cmbRenderMode.SelectedItem = "OpenGL"
                    Case "Software" : cmbRenderMode.SelectedItem = "Software"
                End Select
            Else
                cmbRenderMode.SelectedItem = "自动"
            End If

            chkIntro.Checked = Not .Contains("-Intro")
            chkAudio.Checked = Not .Contains("-audio")
            chkAllowMultipleInstances.Checked = .Contains("-AllowMultipleInstances")
            If .Contains("-IgnoreMissingModelDataBugs") = False Then chkShowMissingModel.Checked = True
            If .Contains("-IgnoreMissingModelDataBugs:on") = True Then chkShowMissingModel.Checked = True
            If .Contains("-IgnoreMissingModelDataBugs:off") = True Then chkShowMissingModel.Checked = False
            chkLoadModelBackground.Checked = Not .Contains("-BackgroundLoader")
            chkWriteLog.Checked = Not .Contains("-WriteLog")
            chkCustomCursors.Checked = Not .Contains("-CustomCursors")
            If .Contains("-CustomCursors") = False Then chkCustomCursors.Checked = True
            If .Contains("-CustomCursors:enabled") = True Then chkCustomCursors.Checked = True
            If .Contains("-CustomCursors:disabled") = True Then chkCustomCursors.Checked = False
            If .Contains("-IME") = False Then chkCustomIME.Checked = True
            If .Contains("-IME:enabled") = True Then chkCustomIME.Checked = True
            If .Contains("-IME:disabled") = True Then chkCustomIME.Checked = False
            chkRestartAfterException.Checked = .Contains("-Restart")
            If .Contains("-ExceptionHandling") = False Then chkExceptionHandling.Checked = True
            If .Contains("-ExceptionHandling:on") = True Then chkExceptionHandling.Checked = True
            If .Contains("-ExceptionHandling:off") = True Then chkExceptionHandling.Checked = False
            chkContinueGameBackground.Checked = Not .Contains("-gp")

            If .Contains("-UserDir") = True Then
                chkUserDir.Checked = True
                txtUserDir.Text = .Substring(.IndexOf("""") + 1, (.LastIndexOf("""") - .IndexOf("""")) - 1)
            Else
                txtUserDir.Text = Nothing
                chkUserDir.Checked = False
            End If
            txtSC4InstallDir.Text = My.Settings.SC4InstallDir
            AddHandler txtSC4InstallDir.TextChanged, AddressOf txtSC4InstallDir_TextChanged '添加关闭窗口过程和关闭窗口事件的关联
            txtArgument.Text = My.Settings.Argument
            Argument = My.Settings.Argument
            btnApply.Enabled = False
        End With
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If chkUserDir.Checked = True And txtUserDir.Text <> Nothing Then If IsPathValidated(txtUserDir) = False Then Exit Sub
        If IsPathValidated(txtSC4InstallDir) = False Then Exit Sub
        If My.Computer.FileSystem.FileExists(txtSC4InstallDir.Text & "\Apps\SimCity 4.exe") = False Then MessageBox.Show("模拟城市4安装目录里没有游戏程序！" & vbCrLf & "请重新选择模拟城市4安装目录！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        My.Settings.Argument = Argument
        Close()
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        If chkUserDir.Checked = True And txtUserDir.Text <> Nothing Then If IsPathValidated(txtUserDir) = False Then Exit Sub
        If IsPathValidated(txtSC4InstallDir) = False Then Exit Sub
        If My.Computer.FileSystem.FileExists(txtSC4InstallDir.Text & "\Apps\SimCity 4.exe") = False Then MessageBox.Show("模拟城市4安装目录里没有游戏程序！" & vbCrLf & "请重新选择模拟城市4安装目录！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        My.Settings.Argument = Argument
        btnApply.Enabled = False
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

End Class