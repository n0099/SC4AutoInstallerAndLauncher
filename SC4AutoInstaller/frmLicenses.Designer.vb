<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLicenses
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLicenses))
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnDisagree = New System.Windows.Forms.Button()
        Me.btnAgree = New System.Windows.Forms.Button()
        Me.rtxLinence = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(152, 27)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "请阅读下列协议"
        '
        'btnDisagree
        '
        Me.btnDisagree.Location = New System.Drawing.Point(366, 407)
        Me.btnDisagree.Name = "btnDisagree"
        Me.btnDisagree.Size = New System.Drawing.Size(120, 23)
        Me.btnDisagree.TabIndex = 2
        Me.btnDisagree.Text = "我不同意此协议(D)"
        Me.btnDisagree.UseVisualStyleBackColor = True
        '
        'btnAgree
        '
        Me.btnAgree.Location = New System.Drawing.Point(492, 407)
        Me.btnAgree.Name = "btnAgree"
        Me.btnAgree.Size = New System.Drawing.Size(120, 23)
        Me.btnAgree.TabIndex = 3
        Me.btnAgree.Text = "我同意此协议(&A)"
        Me.btnAgree.UseVisualStyleBackColor = True
        '
        'rtxLinence
        '
        Me.rtxLinence.BackColor = System.Drawing.Color.White
        Me.rtxLinence.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.rtxLinence.Location = New System.Drawing.Point(12, 49)
        Me.rtxLinence.Name = "rtxLinence"
        Me.rtxLinence.ReadOnly = True
        Me.rtxLinence.Size = New System.Drawing.Size(600, 343)
        Me.rtxLinence.TabIndex = 1
        Me.rtxLinence.Text = ""
        '
        'frmLicenses
        '
        Me.AcceptButton = Me.btnDisagree
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 442)
        Me.Controls.Add(Me.rtxLinence)
        Me.Controls.Add(Me.btnAgree)
        Me.Controls.Add(Me.btnDisagree)
        Me.Controls.Add(Me.lblTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmLicenses"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "模拟城市4 豪华版 自动安装程序"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnDisagree As System.Windows.Forms.Button
    Friend WithEvents btnAgree As System.Windows.Forms.Button
    Friend WithEvents rtxLinence As System.Windows.Forms.RichTextBox
End Class
