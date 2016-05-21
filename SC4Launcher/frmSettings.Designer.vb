<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnUserDir = New System.Windows.Forms.Button()
        Me.lblUserDir = New System.Windows.Forms.Label()
        Me.chkRestartAfterException = New System.Windows.Forms.CheckBox()
        Me.chkWriteLog = New System.Windows.Forms.CheckBox()
        Me.chkExceptionHandling = New System.Windows.Forms.CheckBox()
        Me.chkContinueGameBackground = New System.Windows.Forms.CheckBox()
        Me.chkCustomIME = New System.Windows.Forms.CheckBox()
        Me.chkCustomCursors = New System.Windows.Forms.CheckBox()
        Me.cmbBitDepth = New System.Windows.Forms.ComboBox()
        Me.lblBitDepth = New System.Windows.Forms.Label()
        Me.grpCPU = New System.Windows.Forms.GroupBox()
        Me.cmbCPUPriority = New System.Windows.Forms.ComboBox()
        Me.nudCPUCount = New System.Windows.Forms.NumericUpDown()
        Me.lblCPUCount = New System.Windows.Forms.Label()
        Me.lblCPUPriority = New System.Windows.Forms.Label()
        Me.grpResolution = New System.Windows.Forms.GroupBox()
        Me.rdoDefaultResolution = New System.Windows.Forms.RadioButton()
        Me.mtxCustomResolution = New System.Windows.Forms.MaskedTextBox()
        Me.cmbFixedResolution = New System.Windows.Forms.ComboBox()
        Me.rdoCustomResolution = New System.Windows.Forms.RadioButton()
        Me.rdoFixedResolution = New System.Windows.Forms.RadioButton()
        Me.grpDisplayMode = New System.Windows.Forms.GroupBox()
        Me.rdoDisplayModeFullScreen = New System.Windows.Forms.RadioButton()
        Me.rdoDisplayModeWindow = New System.Windows.Forms.RadioButton()
        Me.chkLoadModelBackground = New System.Windows.Forms.CheckBox()
        Me.chkIntro = New System.Windows.Forms.CheckBox()
        Me.chkAllowMultipleInstances = New System.Windows.Forms.CheckBox()
        Me.chkShowMissingModel = New System.Windows.Forms.CheckBox()
        Me.chkAudio = New System.Windows.Forms.CheckBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblArgument = New System.Windows.Forms.Label()
        Me.txtArgument = New System.Windows.Forms.TextBox()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.txtUserDir = New System.Windows.Forms.TextBox()
        Me.lblRenderMode = New System.Windows.Forms.Label()
        Me.cmbRenderMode = New System.Windows.Forms.ComboBox()
        Me.tipMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkUserDir = New System.Windows.Forms.CheckBox()
        Me.btnDeleteSC4cfgFile = New System.Windows.Forms.Button()
        Me.btnResetSetting = New System.Windows.Forms.Button()
        Me.chkExitLauncherAfterLaunch = New System.Windows.Forms.CheckBox()
        Me.lblSC4InstallDir = New System.Windows.Forms.Label()
        Me.btnSC4InstallDir = New System.Windows.Forms.Button()
        Me.txtSC4InstallDir = New System.Windows.Forms.TextBox()
        Me.fbdUserDir = New System.Windows.Forms.FolderBrowserDialog()
        Me.fbdSC4InstallDir = New System.Windows.Forms.FolderBrowserDialog()
        Me.tabSettings = New System.Windows.Forms.TabControl()
        Me.tabpLessSettings = New System.Windows.Forms.TabPage()
        Me.tabpMoreSettings = New System.Windows.Forms.TabPage()
        Me.grpCPU.SuspendLayout()
        CType(Me.nudCPUCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpResolution.SuspendLayout()
        Me.grpDisplayMode.SuspendLayout()
        Me.tabSettings.SuspendLayout()
        Me.tabpLessSettings.SuspendLayout()
        Me.tabpMoreSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnUserDir
        '
        Me.btnUserDir.Enabled = False
        Me.btnUserDir.Location = New System.Drawing.Point(354, 91)
        Me.btnUserDir.Name = "btnUserDir"
        Me.btnUserDir.Size = New System.Drawing.Size(75, 21)
        Me.btnUserDir.TabIndex = 13
        Me.btnUserDir.Text = "浏览(&B)..."
        Me.tipMain.SetToolTip(Me.btnUserDir, "设置用户文件目录" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "用户文件目录主要用于存储游戏存档、插件、截图等文件" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "默认目录为库文档（我的文档）\SimCity 4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "建议不更改默认的用户文件目录")
        Me.btnUserDir.UseVisualStyleBackColor = True
        '
        'lblUserDir
        '
        Me.lblUserDir.AutoSize = True
        Me.lblUserDir.Enabled = False
        Me.lblUserDir.Location = New System.Drawing.Point(1, 95)
        Me.lblUserDir.Name = "lblUserDir"
        Me.lblUserDir.Size = New System.Drawing.Size(89, 12)
        Me.lblUserDir.TabIndex = 11
        Me.lblUserDir.Text = "用户文件目录："
        Me.tipMain.SetToolTip(Me.lblUserDir, "设置用户文件目录" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "用户文件目录主要用于存储游戏存档、插件、截图等文件" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "默认目录为库文档（我的文档）\SimCity 4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "建议不更改默认的用户文件目录")
        '
        'chkRestartAfterException
        '
        Me.chkRestartAfterException.AutoSize = True
        Me.chkRestartAfterException.Location = New System.Drawing.Point(105, 3)
        Me.chkRestartAfterException.Name = "chkRestartAfterException"
        Me.chkRestartAfterException.Size = New System.Drawing.Size(108, 16)
        Me.chkRestartAfterException.TabIndex = 16
        Me.chkRestartAfterException.Text = "崩溃后重启游戏"
        Me.tipMain.SetToolTip(Me.chkRestartAfterException, "设置是否在游戏崩溃（跳出）后自动重启游戏，默认为否，建议设为否")
        Me.chkRestartAfterException.UseVisualStyleBackColor = True
        '
        'chkWriteLog
        '
        Me.chkWriteLog.AutoSize = True
        Me.chkWriteLog.Location = New System.Drawing.Point(3, 47)
        Me.chkWriteLog.Name = "chkWriteLog"
        Me.chkWriteLog.Size = New System.Drawing.Size(96, 16)
        Me.chkWriteLog.TabIndex = 13
        Me.chkWriteLog.Text = "记录用户信息"
        Me.tipMain.SetToolTip(Me.chkWriteLog, "设置是否在游戏安装目录\Apps下生成系统信息文件，默认为是，建议设为否")
        Me.chkWriteLog.UseVisualStyleBackColor = True
        '
        'chkExceptionHandling
        '
        Me.chkExceptionHandling.AutoSize = True
        Me.chkExceptionHandling.Checked = True
        Me.chkExceptionHandling.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkExceptionHandling.Location = New System.Drawing.Point(105, 25)
        Me.chkExceptionHandling.Name = "chkExceptionHandling"
        Me.chkExceptionHandling.Size = New System.Drawing.Size(144, 16)
        Me.chkExceptionHandling.TabIndex = 17
        Me.chkExceptionHandling.Text = "在崩溃后生成错误报告"
        Me.chkExceptionHandling.ThreeState = True
        Me.tipMain.SetToolTip(Me.chkExceptionHandling, "设置在游戏崩溃（跳出）后是否在用户文件目录\Exception Reports下生成错误报告，默认为是，建议设为否")
        Me.chkExceptionHandling.UseVisualStyleBackColor = True
        '
        'chkContinueGameBackground
        '
        Me.chkContinueGameBackground.AutoSize = True
        Me.chkContinueGameBackground.Location = New System.Drawing.Point(105, 47)
        Me.chkContinueGameBackground.Name = "chkContinueGameBackground"
        Me.chkContinueGameBackground.Size = New System.Drawing.Size(168, 16)
        Me.chkContinueGameBackground.TabIndex = 18
        Me.chkContinueGameBackground.Text = "在后台运行时继续进行游戏"
        Me.tipMain.SetToolTip(Me.chkContinueGameBackground, "设置后台运行游戏时游戏是否继续，默认为是，建议设为是")
        Me.chkContinueGameBackground.UseVisualStyleBackColor = True
        '
        'chkCustomIME
        '
        Me.chkCustomIME.AutoSize = True
        Me.chkCustomIME.Checked = True
        Me.chkCustomIME.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkCustomIME.Location = New System.Drawing.Point(255, 25)
        Me.chkCustomIME.Name = "chkCustomIME"
        Me.chkCustomIME.Size = New System.Drawing.Size(132, 16)
        Me.chkCustomIME.TabIndex = 15
        Me.chkCustomIME.Text = "使用游戏自带输入法"
        Me.chkCustomIME.ThreeState = True
        Me.tipMain.SetToolTip(Me.chkCustomIME, "设置是否使用游戏自带的输入法，全屏显示后只能使用游戏自带输入法，默认为是，建议设为是")
        Me.chkCustomIME.UseVisualStyleBackColor = True
        '
        'chkCustomCursors
        '
        Me.chkCustomCursors.AutoSize = True
        Me.chkCustomCursors.Checked = True
        Me.chkCustomCursors.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkCustomCursors.Location = New System.Drawing.Point(255, 3)
        Me.chkCustomCursors.Name = "chkCustomCursors"
        Me.chkCustomCursors.Size = New System.Drawing.Size(132, 16)
        Me.chkCustomCursors.TabIndex = 14
        Me.chkCustomCursors.Text = "使用游戏的鼠标指针"
        Me.chkCustomCursors.ThreeState = True
        Me.tipMain.SetToolTip(Me.chkCustomCursors, "设置是否使用游戏的鼠标指针图标，默认为是，建议设为是")
        Me.chkCustomCursors.UseVisualStyleBackColor = True
        '
        'cmbBitDepth
        '
        Me.cmbBitDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBitDepth.FormattingEnabled = True
        Me.cmbBitDepth.Items.AddRange(New Object() {"自动", "黑白", "16位色", "256位色", "真彩色"})
        Me.cmbBitDepth.Location = New System.Drawing.Point(145, 101)
        Me.cmbBitDepth.Name = "cmbBitDepth"
        Me.cmbBitDepth.Size = New System.Drawing.Size(93, 20)
        Me.cmbBitDepth.TabIndex = 7
        Me.tipMain.SetToolTip(Me.cmbBitDepth, "设置游戏画面颜色位深，默认为自动，建议设为真彩色")
        '
        'lblBitDepth
        '
        Me.lblBitDepth.AutoSize = True
        Me.lblBitDepth.Location = New System.Drawing.Point(86, 104)
        Me.lblBitDepth.Name = "lblBitDepth"
        Me.lblBitDepth.Size = New System.Drawing.Size(65, 12)
        Me.lblBitDepth.TabIndex = 6
        Me.lblBitDepth.Text = "颜色位深："
        Me.tipMain.SetToolTip(Me.lblBitDepth, "设置游戏画面颜色位深，默认为自动，建议设为真彩色")
        '
        'grpCPU
        '
        Me.grpCPU.Controls.Add(Me.cmbCPUPriority)
        Me.grpCPU.Controls.Add(Me.nudCPUCount)
        Me.grpCPU.Controls.Add(Me.lblCPUCount)
        Me.grpCPU.Controls.Add(Me.lblCPUPriority)
        Me.grpCPU.Location = New System.Drawing.Point(289, 6)
        Me.grpCPU.Name = "grpCPU"
        Me.grpCPU.Size = New System.Drawing.Size(132, 70)
        Me.grpCPU.TabIndex = 2
        Me.grpCPU.TabStop = False
        Me.grpCPU.Text = "CPU"
        '
        'cmbCPUPriority
        '
        Me.cmbCPUPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCPUPriority.FormattingEnabled = True
        Me.cmbCPUPriority.Items.AddRange(New Object() {"自动", "低", "正常", "高"})
        Me.cmbCPUPriority.Location = New System.Drawing.Point(69, 14)
        Me.cmbCPUPriority.Name = "cmbCPUPriority"
        Me.cmbCPUPriority.Size = New System.Drawing.Size(48, 20)
        Me.cmbCPUPriority.TabIndex = 1
        Me.tipMain.SetToolTip(Me.cmbCPUPriority, "设置游戏运行时能够占用的CPU时间优先级，默认为自动，建议设为自动")
        '
        'nudCPUCount
        '
        Me.nudCPUCount.Location = New System.Drawing.Point(69, 42)
        Me.nudCPUCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudCPUCount.Name = "nudCPUCount"
        Me.nudCPUCount.Size = New System.Drawing.Size(48, 21)
        Me.nudCPUCount.TabIndex = 3
        Me.tipMain.SetToolTip(Me.nudCPUCount, "设置游戏运行时能够使用的CPU核心数" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "设置为1可以大大降低游戏跳出（崩溃）几率")
        Me.nudCPUCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblCPUCount
        '
        Me.lblCPUCount.AutoSize = True
        Me.lblCPUCount.Location = New System.Drawing.Point(6, 44)
        Me.lblCPUCount.Name = "lblCPUCount"
        Me.lblCPUCount.Size = New System.Drawing.Size(71, 12)
        Me.lblCPUCount.TabIndex = 2
        Me.lblCPUCount.Text = "CPU核心数："
        Me.tipMain.SetToolTip(Me.lblCPUCount, "设置游戏运行时能够使用的CPU核心数" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "设置为1可以大大降低游戏跳出（崩溃）几率")
        '
        'lblCPUPriority
        '
        Me.lblCPUPriority.AutoSize = True
        Me.lblCPUPriority.Location = New System.Drawing.Point(6, 17)
        Me.lblCPUPriority.Name = "lblCPUPriority"
        Me.lblCPUPriority.Size = New System.Drawing.Size(71, 12)
        Me.lblCPUPriority.TabIndex = 0
        Me.lblCPUPriority.Text = "CPU优先级："
        Me.tipMain.SetToolTip(Me.lblCPUPriority, "设置游戏运行时能够占用的CPU时间优先级，默认为自动，建议设为自动")
        '
        'grpResolution
        '
        Me.grpResolution.Controls.Add(Me.rdoDefaultResolution)
        Me.grpResolution.Controls.Add(Me.mtxCustomResolution)
        Me.grpResolution.Controls.Add(Me.cmbFixedResolution)
        Me.grpResolution.Controls.Add(Me.rdoCustomResolution)
        Me.grpResolution.Controls.Add(Me.rdoFixedResolution)
        Me.grpResolution.Location = New System.Drawing.Point(86, 6)
        Me.grpResolution.Name = "grpResolution"
        Me.grpResolution.Size = New System.Drawing.Size(197, 88)
        Me.grpResolution.TabIndex = 1
        Me.grpResolution.TabStop = False
        Me.grpResolution.Text = "分辨率"
        '
        'rdoDefaultResolution
        '
        Me.rdoDefaultResolution.AutoSize = True
        Me.rdoDefaultResolution.Checked = True
        Me.rdoDefaultResolution.Location = New System.Drawing.Point(6, 64)
        Me.rdoDefaultResolution.Name = "rdoDefaultResolution"
        Me.rdoDefaultResolution.Size = New System.Drawing.Size(47, 16)
        Me.rdoDefaultResolution.TabIndex = 4
        Me.rdoDefaultResolution.TabStop = True
        Me.rdoDefaultResolution.Text = "自动"
        Me.tipMain.SetToolTip(Me.rdoDefaultResolution, "让游戏自动选择分辨率")
        Me.rdoDefaultResolution.UseVisualStyleBackColor = True
        '
        'mtxCustomResolution
        '
        Me.mtxCustomResolution.Location = New System.Drawing.Point(104, 41)
        Me.mtxCustomResolution.Mask = "0000x0000"
        Me.mtxCustomResolution.Name = "mtxCustomResolution"
        Me.mtxCustomResolution.Size = New System.Drawing.Size(86, 21)
        Me.mtxCustomResolution.TabIndex = 3
        Me.tipMain.SetToolTip(Me.mtxCustomResolution, "自定义游戏画面分辨率" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "如果显示方式为窗口化，则分辨率为游戏窗口的大小；" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "如果显示方式为全屏，且设置的分辨率小于屏幕分辨率则会在画面边缘出现黑边" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "默认为自动，" &
        "建议设为1024x768")
        '
        'cmbFixedResolution
        '
        Me.cmbFixedResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFixedResolution.FormattingEnabled = True
        Me.cmbFixedResolution.Items.AddRange(New Object() {"800x600", "1024x768", "1280x1024"})
        Me.cmbFixedResolution.Location = New System.Drawing.Point(95, 19)
        Me.cmbFixedResolution.Name = "cmbFixedResolution"
        Me.cmbFixedResolution.Size = New System.Drawing.Size(95, 20)
        Me.cmbFixedResolution.TabIndex = 1
        Me.tipMain.SetToolTip(Me.cmbFixedResolution, "选择游戏画面分辨率" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "如果显示方式为窗口化，则分辨率为游戏窗口的大小；" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "如果显示方式为全屏，且设置的分辨率小于屏幕分辨率则会在画面边缘出现黑边" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "默认为自动，建" &
        "议设为1024x768")
        '
        'rdoCustomResolution
        '
        Me.rdoCustomResolution.AutoSize = True
        Me.rdoCustomResolution.Location = New System.Drawing.Point(6, 42)
        Me.rdoCustomResolution.Name = "rdoCustomResolution"
        Me.rdoCustomResolution.Size = New System.Drawing.Size(107, 16)
        Me.rdoCustomResolution.TabIndex = 2
        Me.rdoCustomResolution.Text = "自定义分辨率："
        Me.tipMain.SetToolTip(Me.rdoCustomResolution, "自定义游戏画面分辨率" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "如果显示方式为窗口化，则分辨率为游戏窗口的大小；" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "如果显示方式为全屏，且设置的分辨率小于屏幕分辨率则会在画面边缘出现黑边" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "默认为自动，" &
        "建议设为1024x768")
        Me.rdoCustomResolution.UseVisualStyleBackColor = True
        '
        'rdoFixedResolution
        '
        Me.rdoFixedResolution.AutoSize = True
        Me.rdoFixedResolution.Location = New System.Drawing.Point(6, 20)
        Me.rdoFixedResolution.Name = "rdoFixedResolution"
        Me.rdoFixedResolution.Size = New System.Drawing.Size(95, 16)
        Me.rdoFixedResolution.TabIndex = 0
        Me.rdoFixedResolution.Text = "固定分辨率："
        Me.tipMain.SetToolTip(Me.rdoFixedResolution, "选择游戏画面分辨率" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "如果显示方式为窗口化，则分辨率为游戏窗口的大小；" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "如果显示方式为全屏，且设置的分辨率小于屏幕分辨率则会在画面边缘出现黑边" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "默认为自动，建" &
        "议设为1024x768")
        Me.rdoFixedResolution.UseVisualStyleBackColor = True
        '
        'grpDisplayMode
        '
        Me.grpDisplayMode.Controls.Add(Me.rdoDisplayModeFullScreen)
        Me.grpDisplayMode.Controls.Add(Me.rdoDisplayModeWindow)
        Me.grpDisplayMode.Location = New System.Drawing.Point(6, 6)
        Me.grpDisplayMode.Name = "grpDisplayMode"
        Me.grpDisplayMode.Size = New System.Drawing.Size(74, 70)
        Me.grpDisplayMode.TabIndex = 0
        Me.grpDisplayMode.TabStop = False
        Me.grpDisplayMode.Text = "显示方式"
        '
        'rdoDisplayModeFullScreen
        '
        Me.rdoDisplayModeFullScreen.AutoSize = True
        Me.rdoDisplayModeFullScreen.Checked = True
        Me.rdoDisplayModeFullScreen.Location = New System.Drawing.Point(6, 42)
        Me.rdoDisplayModeFullScreen.Name = "rdoDisplayModeFullScreen"
        Me.rdoDisplayModeFullScreen.Size = New System.Drawing.Size(47, 16)
        Me.rdoDisplayModeFullScreen.TabIndex = 1
        Me.rdoDisplayModeFullScreen.TabStop = True
        Me.rdoDisplayModeFullScreen.Text = "全屏"
        Me.tipMain.SetToolTip(Me.rdoDisplayModeFullScreen, "游戏会全屏显示画面，默认为全屏显示，建议设为窗口显示")
        Me.rdoDisplayModeFullScreen.UseVisualStyleBackColor = True
        '
        'rdoDisplayModeWindow
        '
        Me.rdoDisplayModeWindow.AutoSize = True
        Me.rdoDisplayModeWindow.Location = New System.Drawing.Point(6, 20)
        Me.rdoDisplayModeWindow.Name = "rdoDisplayModeWindow"
        Me.rdoDisplayModeWindow.Size = New System.Drawing.Size(59, 16)
        Me.rdoDisplayModeWindow.TabIndex = 0
        Me.rdoDisplayModeWindow.Text = "窗口化"
        Me.tipMain.SetToolTip(Me.rdoDisplayModeWindow, "游戏会以窗口显示画面，默认为全屏显示，建议设为窗口显示")
        Me.rdoDisplayModeWindow.UseVisualStyleBackColor = True
        '
        'chkLoadModelBackground
        '
        Me.chkLoadModelBackground.AutoSize = True
        Me.chkLoadModelBackground.Location = New System.Drawing.Point(3, 25)
        Me.chkLoadModelBackground.Name = "chkLoadModelBackground"
        Me.chkLoadModelBackground.Size = New System.Drawing.Size(84, 16)
        Me.chkLoadModelBackground.TabIndex = 12
        Me.chkLoadModelBackground.Text = "预加载模型"
        Me.tipMain.SetToolTip(Me.chkLoadModelBackground, "设置是否预先加载未显示的模型，默认为是，建议设为是")
        Me.chkLoadModelBackground.UseVisualStyleBackColor = True
        '
        'chkIntro
        '
        Me.chkIntro.AutoSize = True
        Me.chkIntro.Checked = True
        Me.chkIntro.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIntro.Location = New System.Drawing.Point(8, 82)
        Me.chkIntro.Name = "chkIntro"
        Me.chkIntro.Size = New System.Drawing.Size(72, 16)
        Me.chkIntro.TabIndex = 2
        Me.chkIntro.Text = "开场动画"
        Me.tipMain.SetToolTip(Me.chkIntro, "是否显示游戏启动时的开场动画，默认显示，建议设为不显示")
        Me.chkIntro.UseVisualStyleBackColor = True
        '
        'chkAllowMultipleInstances
        '
        Me.chkAllowMultipleInstances.AutoSize = True
        Me.chkAllowMultipleInstances.Location = New System.Drawing.Point(8, 126)
        Me.chkAllowMultipleInstances.Name = "chkAllowMultipleInstances"
        Me.chkAllowMultipleInstances.Size = New System.Drawing.Size(72, 16)
        Me.chkAllowMultipleInstances.TabIndex = 5
        Me.chkAllowMultipleInstances.Text = "允许多开"
        Me.tipMain.SetToolTip(Me.chkAllowMultipleInstances, "是否允许打开多个游戏，默认不允许，建议设为不允许")
        Me.chkAllowMultipleInstances.UseVisualStyleBackColor = True
        '
        'chkShowMissingModel
        '
        Me.chkShowMissingModel.AutoSize = True
        Me.chkShowMissingModel.Checked = True
        Me.chkShowMissingModel.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkShowMissingModel.Location = New System.Drawing.Point(3, 3)
        Me.chkShowMissingModel.Name = "chkShowMissingModel"
        Me.chkShowMissingModel.Size = New System.Drawing.Size(72, 16)
        Me.chkShowMissingModel.TabIndex = 0
        Me.chkShowMissingModel.Text = "显示箱子"
        Me.chkShowMissingModel.ThreeState = True
        Me.tipMain.SetToolTip(Me.chkShowMissingModel, "设置是否将不存在的模型显示为一个箱子，默认为是，建议设为是")
        Me.chkShowMissingModel.UseVisualStyleBackColor = True
        '
        'chkAudio
        '
        Me.chkAudio.AutoSize = True
        Me.chkAudio.Checked = True
        Me.chkAudio.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAudio.Location = New System.Drawing.Point(8, 104)
        Me.chkAudio.Name = "chkAudio"
        Me.chkAudio.Size = New System.Drawing.Size(72, 16)
        Me.chkAudio.TabIndex = 4
        Me.chkAudio.Text = "开启声音"
        Me.tipMain.SetToolTip(Me.chkAudio, "设置是否开启所有游戏声音（包括音乐），默认开启，建议设为开启")
        Me.chkAudio.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(215, 247)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 29
        Me.btnOK.Text = "确定(&O)"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(377, 247)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 31
        Me.btnCancel.Text = "取消(&C)"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblArgument
        '
        Me.lblArgument.AutoSize = True
        Me.lblArgument.Location = New System.Drawing.Point(12, 223)
        Me.lblArgument.Name = "lblArgument"
        Me.lblArgument.Size = New System.Drawing.Size(65, 12)
        Me.lblArgument.TabIndex = 17
        Me.lblArgument.Text = "启动参数："
        Me.tipMain.SetToolTip(Me.lblArgument, "启动游戏时的附加参数")
        '
        'txtArgument
        '
        Me.txtArgument.Location = New System.Drawing.Point(71, 220)
        Me.txtArgument.Name = "txtArgument"
        Me.txtArgument.ReadOnly = True
        Me.txtArgument.Size = New System.Drawing.Size(381, 21)
        Me.txtArgument.TabIndex = 18
        Me.tipMain.SetToolTip(Me.txtArgument, "启动游戏时的附加参数")
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(296, 247)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 30
        Me.btnApply.Text = "应用(&A)"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'txtUserDir
        '
        Me.txtUserDir.Enabled = False
        Me.txtUserDir.Location = New System.Drawing.Point(83, 91)
        Me.txtUserDir.Name = "txtUserDir"
        Me.txtUserDir.Size = New System.Drawing.Size(265, 21)
        Me.txtUserDir.TabIndex = 12
        Me.tipMain.SetToolTip(Me.txtUserDir, "设置用户文件目录" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "用户文件目录主要用于存储游戏存档、插件、截图等文件" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "默认目录为库文档（我的文档）\SimCity 4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "建议不更改默认的用户文件目录")
        '
        'lblRenderMode
        '
        Me.lblRenderMode.AutoSize = True
        Me.lblRenderMode.Location = New System.Drawing.Point(86, 127)
        Me.lblRenderMode.Name = "lblRenderMode"
        Me.lblRenderMode.Size = New System.Drawing.Size(65, 12)
        Me.lblRenderMode.TabIndex = 8
        Me.lblRenderMode.Text = "渲染模式："
        Me.tipMain.SetToolTip(Me.lblRenderMode, "设置游戏画面渲染方式，默认为自动，建议设为自动")
        '
        'cmbRenderMode
        '
        Me.cmbRenderMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRenderMode.FormattingEnabled = True
        Me.cmbRenderMode.Items.AddRange(New Object() {"自动", "DirectX", "OpenGL", "软件渲染"})
        Me.cmbRenderMode.Location = New System.Drawing.Point(145, 124)
        Me.cmbRenderMode.Name = "cmbRenderMode"
        Me.cmbRenderMode.Size = New System.Drawing.Size(93, 20)
        Me.cmbRenderMode.TabIndex = 9
        Me.tipMain.SetToolTip(Me.cmbRenderMode, "设置游戏画面渲染方式，默认为自动，建议设为自动")
        '
        'tipMain
        '
        Me.tipMain.AutoPopDelay = 10000
        Me.tipMain.InitialDelay = 100
        Me.tipMain.ReshowDelay = 100
        '
        'chkUserDir
        '
        Me.chkUserDir.AutoSize = True
        Me.chkUserDir.Location = New System.Drawing.Point(3, 69)
        Me.chkUserDir.Name = "chkUserDir"
        Me.chkUserDir.Size = New System.Drawing.Size(132, 16)
        Me.chkUserDir.TabIndex = 10
        Me.chkUserDir.Text = "自定义用户文件目录"
        Me.tipMain.SetToolTip(Me.chkUserDir, "是否自定义用户文件目录" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "用户文件目录是游戏存储存档、插件和截图等文件的文件夹" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "默认目录为库文档（我的文档）\SimCity 4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "不建议更改默认的用户文件目录" &
        "")
        Me.chkUserDir.UseVisualStyleBackColor = True
        '
        'btnDeleteSC4cfgFile
        '
        Me.btnDeleteSC4cfgFile.Location = New System.Drawing.Point(3, 145)
        Me.btnDeleteSC4cfgFile.Name = "btnDeleteSC4cfgFile"
        Me.btnDeleteSC4cfgFile.Size = New System.Drawing.Size(180, 23)
        Me.btnDeleteSC4cfgFile.TabIndex = 19
        Me.btnDeleteSC4cfgFile.Text = "删除SimCity 4.cfg文件(&D)"
        Me.tipMain.SetToolTip(Me.btnDeleteSC4cfgFile, "如果部分设置无效，请尝试SimCity 4.cfg文件以重置SC4设置" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SimCity 4.cfg文件存储在用户文件目录下")
        Me.btnDeleteSC4cfgFile.UseVisualStyleBackColor = True
        '
        'btnResetSetting
        '
        Me.btnResetSetting.Location = New System.Drawing.Point(249, 145)
        Me.btnResetSetting.Name = "btnResetSetting"
        Me.btnResetSetting.Size = New System.Drawing.Size(180, 23)
        Me.btnResetSetting.TabIndex = 20
        Me.btnResetSetting.Text = "重置为默认设置(&R)"
        Me.tipMain.SetToolTip(Me.btnResetSetting, "清空启动参数并重置选项")
        Me.btnResetSetting.UseVisualStyleBackColor = True
        '
        'chkExitLauncherAfterLaunch
        '
        Me.chkExitLauncherAfterLaunch.AutoSize = True
        Me.chkExitLauncherAfterLaunch.Checked = True
        Me.chkExitLauncherAfterLaunch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkExitLauncherAfterLaunch.Location = New System.Drawing.Point(8, 148)
        Me.chkExitLauncherAfterLaunch.Name = "chkExitLauncherAfterLaunch"
        Me.chkExitLauncherAfterLaunch.Size = New System.Drawing.Size(144, 16)
        Me.chkExitLauncherAfterLaunch.TabIndex = 10
        Me.chkExitLauncherAfterLaunch.Text = "启动游戏后退出启动器"
        Me.tipMain.SetToolTip(Me.chkExitLauncherAfterLaunch, "启动游戏后是否退出启动器，默认为是，建议设为是")
        Me.chkExitLauncherAfterLaunch.UseVisualStyleBackColor = True
        '
        'lblSC4InstallDir
        '
        Me.lblSC4InstallDir.AutoSize = True
        Me.lblSC4InstallDir.Location = New System.Drawing.Point(1, 122)
        Me.lblSC4InstallDir.Name = "lblSC4InstallDir"
        Me.lblSC4InstallDir.Size = New System.Drawing.Size(89, 12)
        Me.lblSC4InstallDir.TabIndex = 14
        Me.lblSC4InstallDir.Text = "游戏安装目录："
        '
        'btnSC4InstallDir
        '
        Me.btnSC4InstallDir.Location = New System.Drawing.Point(354, 118)
        Me.btnSC4InstallDir.Name = "btnSC4InstallDir"
        Me.btnSC4InstallDir.Size = New System.Drawing.Size(75, 21)
        Me.btnSC4InstallDir.TabIndex = 16
        Me.btnSC4InstallDir.Text = "浏览(&B)..."
        Me.btnSC4InstallDir.UseVisualStyleBackColor = True
        '
        'txtSC4InstallDir
        '
        Me.txtSC4InstallDir.Location = New System.Drawing.Point(83, 118)
        Me.txtSC4InstallDir.Name = "txtSC4InstallDir"
        Me.txtSC4InstallDir.Size = New System.Drawing.Size(265, 21)
        Me.txtSC4InstallDir.TabIndex = 15
        '
        'fbdUserDir
        '
        Me.fbdUserDir.Description = "请选择用户文件目录"
        Me.fbdUserDir.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'fbdSC4InstallDir
        '
        Me.fbdSC4InstallDir.Description = "请选择模拟城市4 戏安装目录"
        Me.fbdSC4InstallDir.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'tabSettings
        '
        Me.tabSettings.Controls.Add(Me.tabpLessSettings)
        Me.tabSettings.Controls.Add(Me.tabpMoreSettings)
        Me.tabSettings.Location = New System.Drawing.Point(12, 12)
        Me.tabSettings.Name = "tabSettings"
        Me.tabSettings.SelectedIndex = 0
        Me.tabSettings.Size = New System.Drawing.Size(440, 202)
        Me.tabSettings.TabIndex = 32
        '
        'tabpLessSettings
        '
        Me.tabpLessSettings.Controls.Add(Me.chkExitLauncherAfterLaunch)
        Me.tabpLessSettings.Controls.Add(Me.grpCPU)
        Me.tabpLessSettings.Controls.Add(Me.cmbRenderMode)
        Me.tabpLessSettings.Controls.Add(Me.chkAllowMultipleInstances)
        Me.tabpLessSettings.Controls.Add(Me.cmbBitDepth)
        Me.tabpLessSettings.Controls.Add(Me.grpDisplayMode)
        Me.tabpLessSettings.Controls.Add(Me.lblBitDepth)
        Me.tabpLessSettings.Controls.Add(Me.lblRenderMode)
        Me.tabpLessSettings.Controls.Add(Me.grpResolution)
        Me.tabpLessSettings.Controls.Add(Me.chkAudio)
        Me.tabpLessSettings.Controls.Add(Me.chkIntro)
        Me.tabpLessSettings.Location = New System.Drawing.Point(4, 22)
        Me.tabpLessSettings.Name = "tabpLessSettings"
        Me.tabpLessSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tabpLessSettings.Size = New System.Drawing.Size(432, 176)
        Me.tabpLessSettings.TabIndex = 0
        Me.tabpLessSettings.Text = "常规"
        Me.tabpLessSettings.UseVisualStyleBackColor = True
        '
        'tabpMoreSettings
        '
        Me.tabpMoreSettings.Controls.Add(Me.btnResetSetting)
        Me.tabpMoreSettings.Controls.Add(Me.txtSC4InstallDir)
        Me.tabpMoreSettings.Controls.Add(Me.btnSC4InstallDir)
        Me.tabpMoreSettings.Controls.Add(Me.chkShowMissingModel)
        Me.tabpMoreSettings.Controls.Add(Me.btnDeleteSC4cfgFile)
        Me.tabpMoreSettings.Controls.Add(Me.lblSC4InstallDir)
        Me.tabpMoreSettings.Controls.Add(Me.chkLoadModelBackground)
        Me.tabpMoreSettings.Controls.Add(Me.chkUserDir)
        Me.tabpMoreSettings.Controls.Add(Me.txtUserDir)
        Me.tabpMoreSettings.Controls.Add(Me.chkCustomCursors)
        Me.tabpMoreSettings.Controls.Add(Me.btnUserDir)
        Me.tabpMoreSettings.Controls.Add(Me.chkCustomIME)
        Me.tabpMoreSettings.Controls.Add(Me.chkContinueGameBackground)
        Me.tabpMoreSettings.Controls.Add(Me.lblUserDir)
        Me.tabpMoreSettings.Controls.Add(Me.chkExceptionHandling)
        Me.tabpMoreSettings.Controls.Add(Me.chkWriteLog)
        Me.tabpMoreSettings.Controls.Add(Me.chkRestartAfterException)
        Me.tabpMoreSettings.Location = New System.Drawing.Point(4, 22)
        Me.tabpMoreSettings.Name = "tabpMoreSettings"
        Me.tabpMoreSettings.Size = New System.Drawing.Size(432, 176)
        Me.tabpMoreSettings.TabIndex = 2
        Me.tabpMoreSettings.Text = "高级"
        Me.tabpMoreSettings.UseVisualStyleBackColor = True
        '
        'frmSettings
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(464, 282)
        Me.Controls.Add(Me.tabSettings)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.txtArgument)
        Me.Controls.Add(Me.lblArgument)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "设置"
        Me.grpCPU.ResumeLayout(False)
        Me.grpCPU.PerformLayout()
        CType(Me.nudCPUCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpResolution.ResumeLayout(False)
        Me.grpResolution.PerformLayout()
        Me.grpDisplayMode.ResumeLayout(False)
        Me.grpDisplayMode.PerformLayout()
        Me.tabSettings.ResumeLayout(False)
        Me.tabpLessSettings.ResumeLayout(False)
        Me.tabpLessSettings.PerformLayout()
        Me.tabpMoreSettings.ResumeLayout(False)
        Me.tabpMoreSettings.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblArgument As System.Windows.Forms.Label
    Friend WithEvents txtArgument As System.Windows.Forms.TextBox
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents grpResolution As System.Windows.Forms.GroupBox
    Friend WithEvents grpDisplayMode As System.Windows.Forms.GroupBox
    Friend WithEvents rdoDisplayModeFullScreen As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDisplayModeWindow As System.Windows.Forms.RadioButton
    Friend WithEvents chkLoadModelBackground As System.Windows.Forms.CheckBox
    Friend WithEvents chkIntro As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllowMultipleInstances As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowMissingModel As System.Windows.Forms.CheckBox
    Friend WithEvents chkAudio As System.Windows.Forms.CheckBox
    Friend WithEvents lblBitDepth As System.Windows.Forms.Label
    Friend WithEvents cmbBitDepth As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFixedResolution As System.Windows.Forms.ComboBox
    Friend WithEvents rdoCustomResolution As System.Windows.Forms.RadioButton
    Friend WithEvents rdoFixedResolution As System.Windows.Forms.RadioButton
    Friend WithEvents lblUserDir As System.Windows.Forms.Label
    Friend WithEvents chkRestartAfterException As System.Windows.Forms.CheckBox
    Friend WithEvents chkWriteLog As System.Windows.Forms.CheckBox
    Friend WithEvents chkExceptionHandling As System.Windows.Forms.CheckBox
    Friend WithEvents chkContinueGameBackground As System.Windows.Forms.CheckBox
    Friend WithEvents chkCustomIME As System.Windows.Forms.CheckBox
    Friend WithEvents chkCustomCursors As System.Windows.Forms.CheckBox
    Friend WithEvents grpCPU As System.Windows.Forms.GroupBox
    Friend WithEvents nudCPUCount As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblCPUCount As System.Windows.Forms.Label
    Friend WithEvents lblCPUPriority As System.Windows.Forms.Label
    Friend WithEvents btnUserDir As System.Windows.Forms.Button
    Friend WithEvents mtxCustomResolution As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtUserDir As System.Windows.Forms.TextBox
    Friend WithEvents lblRenderMode As System.Windows.Forms.Label
    Friend WithEvents cmbRenderMode As System.Windows.Forms.ComboBox
    Friend WithEvents tipMain As System.Windows.Forms.ToolTip
    Friend WithEvents chkUserDir As System.Windows.Forms.CheckBox
    Friend WithEvents fbdUserDir As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents fbdSC4InstallDir As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents rdoDefaultResolution As System.Windows.Forms.RadioButton
    Friend WithEvents btnDeleteSC4cfgFile As System.Windows.Forms.Button
    Friend WithEvents cmbCPUPriority As System.Windows.Forms.ComboBox
    Friend WithEvents lblSC4InstallDir As System.Windows.Forms.Label
    Friend WithEvents btnSC4InstallDir As System.Windows.Forms.Button
    Friend WithEvents txtSC4InstallDir As System.Windows.Forms.TextBox
    Friend WithEvents tabSettings As TabControl
    Friend WithEvents tabpLessSettings As TabPage
    Friend WithEvents tabpMoreSettings As TabPage
    Friend WithEvents btnResetSetting As Button
    Friend WithEvents chkExitLauncherAfterLaunch As CheckBox
End Class
