<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmProfiles
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProfiles))
        Me.cboProfiles = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkUseSSL = New System.Windows.Forms.CheckBox()
        Me.txtSMTPServer = New System.Windows.Forms.TextBox()
        Me.txtSMTPPort = New System.Windows.Forms.TextBox()
        Me.txtWatchFolder = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtProfName = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtFrom = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtToAddrs = New System.Windows.Forms.TextBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.chkUseCreds = New System.Windows.Forms.CheckBox()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.lblPW = New System.Windows.Forms.Label()
        Me.txtPW = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtFileType = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cboProfiles
        '
        Me.cboProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProfiles.FormattingEnabled = True
        Me.cboProfiles.Location = New System.Drawing.Point(95, 9)
        Me.cboProfiles.Name = "cboProfiles"
        Me.cboProfiles.Size = New System.Drawing.Size(170, 21)
        Me.cboProfiles.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Watch Folder"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 132)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "SMTP Server"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 158)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "SMTP Port"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Profile Name"
        '
        'chkUseSSL
        '
        Me.chkUseSSL.AutoSize = True
        Me.chkUseSSL.Location = New System.Drawing.Point(179, 157)
        Me.chkUseSSL.Name = "chkUseSSL"
        Me.chkUseSSL.Size = New System.Drawing.Size(68, 17)
        Me.chkUseSSL.TabIndex = 7
        Me.chkUseSSL.Text = "Use SSL"
        Me.chkUseSSL.UseVisualStyleBackColor = True
        '
        'txtSMTPServer
        '
        Me.txtSMTPServer.Location = New System.Drawing.Point(95, 129)
        Me.txtSMTPServer.Name = "txtSMTPServer"
        Me.txtSMTPServer.Size = New System.Drawing.Size(170, 20)
        Me.txtSMTPServer.TabIndex = 5
        '
        'txtSMTPPort
        '
        Me.txtSMTPPort.Location = New System.Drawing.Point(95, 155)
        Me.txtSMTPPort.Name = "txtSMTPPort"
        Me.txtSMTPPort.Size = New System.Drawing.Size(65, 20)
        Me.txtSMTPPort.TabIndex = 6
        '
        'txtWatchFolder
        '
        Me.txtWatchFolder.Location = New System.Drawing.Point(95, 77)
        Me.txtWatchFolder.Name = "txtWatchFolder"
        Me.txtWatchFolder.Size = New System.Drawing.Size(111, 20)
        Me.txtWatchFolder.TabIndex = 2
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(213, 75)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(52, 23)
        Me.btnBrowse.TabIndex = 3
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtProfName
        '
        Me.txtProfName.Location = New System.Drawing.Point(95, 51)
        Me.txtProfName.Name = "txtProfName"
        Me.txtProfName.Size = New System.Drawing.Size(170, 20)
        Me.txtProfName.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(95, 318)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 13
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 184)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "'From' Address"
        '
        'txtFrom
        '
        Me.txtFrom.Location = New System.Drawing.Point(95, 181)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(170, 20)
        Me.txtFrom.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 210)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "'To' Addresses"
        '
        'txtToAddrs
        '
        Me.txtToAddrs.Location = New System.Drawing.Point(95, 207)
        Me.txtToAddrs.Name = "txtToAddrs"
        Me.txtToAddrs.Size = New System.Drawing.Size(170, 20)
        Me.txtToAddrs.TabIndex = 9
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(190, 318)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 14
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'chkUseCreds
        '
        Me.chkUseCreds.AutoSize = True
        Me.chkUseCreds.Location = New System.Drawing.Point(13, 236)
        Me.chkUseCreds.Name = "chkUseCreds"
        Me.chkUseCreds.Size = New System.Drawing.Size(100, 17)
        Me.chkUseCreds.TabIndex = 10
        Me.chkUseCreds.Text = "Use Credentials"
        Me.chkUseCreds.UseVisualStyleBackColor = True
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(95, 259)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(170, 20)
        Me.txtUsername.TabIndex = 11
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(10, 262)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(55, 13)
        Me.lblUsername.TabIndex = 19
        Me.lblUsername.Text = "Username"
        '
        'lblPW
        '
        Me.lblPW.AutoSize = True
        Me.lblPW.Location = New System.Drawing.Point(10, 288)
        Me.lblPW.Name = "lblPW"
        Me.lblPW.Size = New System.Drawing.Size(53, 13)
        Me.lblPW.TabIndex = 20
        Me.lblPW.Text = "Password"
        '
        'txtPW
        '
        Me.txtPW.Location = New System.Drawing.Point(95, 285)
        Me.txtPW.Name = "txtPW"
        Me.txtPW.Size = New System.Drawing.Size(170, 20)
        Me.txtPW.TabIndex = 12
        Me.txtPW.UseSystemPasswordChar = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(10, 106)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 13)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "File Type Filter"
        '
        'txtFileType
        '
        Me.txtFileType.Location = New System.Drawing.Point(95, 103)
        Me.txtFileType.Name = "txtFileType"
        Me.txtFileType.Size = New System.Drawing.Size(65, 20)
        Me.txtFileType.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(10, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 13)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "Profile List"
        '
        'frmProfiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(281, 354)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtFileType)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtPW)
        Me.Controls.Add(Me.lblPW)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.chkUseCreds)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.txtToAddrs)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtFrom)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtProfName)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.txtWatchFolder)
        Me.Controls.Add(Me.txtSMTPPort)
        Me.Controls.Add(Me.txtSMTPServer)
        Me.Controls.Add(Me.chkUseSSL)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboProfiles)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProfiles"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage Profiles"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboProfiles As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents chkUseSSL As CheckBox
    Friend WithEvents txtSMTPServer As TextBox
    Friend WithEvents txtSMTPPort As TextBox
    Friend WithEvents txtWatchFolder As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents btnBrowse As Button
    Friend WithEvents txtProfName As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents txtFrom As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtToAddrs As TextBox
    Friend WithEvents btnDelete As Button
    Friend WithEvents chkUseCreds As CheckBox
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents lblUsername As Label
    Friend WithEvents lblPW As Label
    Friend WithEvents txtPW As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtFileType As TextBox
    Friend WithEvents Label8 As Label
End Class
