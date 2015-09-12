<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVerifyFilesMD5
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVerifyFilesMD5))
        Me.prgVerifyFilesMD5 = New System.Windows.Forms.ProgressBar()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblMain = New System.Windows.Forms.Label()
        Me.bgwComputeMD5 = New System.ComponentModel.BackgroundWorker()
        Me.tlpBorder = New System.Windows.Forms.TableLayoutPanel()
        Me.tlpMain.SuspendLayout()
        Me.tlpBorder.SuspendLayout()
        Me.SuspendLayout()
        '
        'prgVerifyFilesMD5
        '
        Me.prgVerifyFilesMD5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.prgVerifyFilesMD5.Location = New System.Drawing.Point(3, 52)
        Me.prgVerifyFilesMD5.Name = "prgVerifyFilesMD5"
        Me.prgVerifyFilesMD5.Size = New System.Drawing.Size(264, 14)
        Me.prgVerifyFilesMD5.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prgVerifyFilesMD5.TabIndex = 2
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.btnCancel, 0, 3)
        Me.tlpMain.Controls.Add(Me.prgVerifyFilesMD5, 0, 2)
        Me.tlpMain.Controls.Add(Me.lblProgress, 0, 1)
        Me.tlpMain.Controls.Add(Me.lblMain, 0, 0)
        Me.tlpMain.Location = New System.Drawing.Point(5, 5)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 4
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.tlpMain.Size = New System.Drawing.Size(270, 95)
        Me.tlpMain.TabIndex = 0
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(97, 72)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "取消(&C)"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblProgress.Location = New System.Drawing.Point(103, 29)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(63, 17)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "100% 0/0"
        '
        'lblMain
        '
        Me.lblMain.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblMain.AutoSize = True
        Me.lblMain.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblMain.Location = New System.Drawing.Point(67, 3)
        Me.lblMain.Name = "lblMain"
        Me.lblMain.Size = New System.Drawing.Size(135, 20)
        Me.lblMain.TabIndex = 0
        Me.lblMain.Text = "正在验证文件完整性"
        '
        'bgwComputeMD5
        '
        Me.bgwComputeMD5.WorkerReportsProgress = True
        Me.bgwComputeMD5.WorkerSupportsCancellation = True
        '
        'tlpBorder
        '
        Me.tlpBorder.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.tlpBorder.ColumnCount = 1
        Me.tlpBorder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpBorder.Controls.Add(Me.tlpMain, 0, 0)
        Me.tlpBorder.Location = New System.Drawing.Point(0, 0)
        Me.tlpBorder.Name = "tlpBorder"
        Me.tlpBorder.RowCount = 1
        Me.tlpBorder.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpBorder.Size = New System.Drawing.Size(280, 105)
        Me.tlpBorder.TabIndex = 0
        '
        'frmVerifyFilesMD5
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(279, 104)
        Me.Controls.Add(Me.tlpBorder)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVerifyFilesMD5"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "正在验证文件完整性"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.tlpBorder.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents prgVerifyFilesMD5 As System.Windows.Forms.ProgressBar
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents bgwComputeMD5 As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblMain As System.Windows.Forms.Label
    Friend WithEvents tlpBorder As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
