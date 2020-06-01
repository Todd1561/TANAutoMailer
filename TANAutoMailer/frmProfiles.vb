Imports Microsoft.Win32
Imports CredentialManagement
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.IO

Public Class frmProfiles

    Public MyThreads As New Dictionary(Of String, System.Threading.Thread), gTray As NotifyIcon

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim err As String = ""

        txtFileType.Text = Regex.Replace(txtFileType.Text.ToLower(), "[^a-z0-9]", "")
        txtToAddrs.Text = Regex.Replace(txtToAddrs.Text, " +", "").Replace(",", ";")
        txtProfName.Text = txtProfName.Text.Trim
        txtSMTPServer.Text = txtSMTPServer.Text.Trim
        txtWatchFolder.Text = txtWatchFolder.Text.Trim

        If txtProfName.Text = "" Then err += "No profile name supplied" & vbCrLf
        If Not Directory.Exists(txtWatchFolder.Text) Then err += "Watch path invalid" & vbCrLf
        If txtSMTPServer.Text = "" Then err += "SMTP server invalid" & vbCrLf
        If Not Integer.TryParse(txtSMTPPort.Text.Trim, Nothing) Then err += "SMTP port invalid" & vbCrLf
        If Not Regex.IsMatch(txtFrom.Text, "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") Then err += "From address invalid" & vbCrLf

        For Each addr As String In txtToAddrs.Text.Split(";")
            If Not Regex.IsMatch(addr, "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") Then
                err += "To address invalid" & vbCrLf
                Exit For
            End If
        Next

        Dim rk As RegistryKey
        rk = Registry.CurrentUser.OpenSubKey("Software\Nelonic\TANAutoMailer", False)

        'look to see if this folder is already being monitored
        If rk IsNot Nothing Then
            For Each k As String In rk.GetSubKeyNames
                If k <> txtProfName.Text Then
                    Dim sk As RegistryKey
                sk = Registry.CurrentUser.OpenSubKey("Software\Nelonic\TANAutoMailer\" & k, False)

                    If sk IsNot Nothing Then If sk.GetValue("Path") = txtWatchFolder.Text Then err += "This folder is being watched in '" & k & "'"
                End If
            Next
        End If

        If err = "" Then

            Try
                Dim regKey As RegistryKey

                regKey = Registry.CurrentUser.OpenSubKey("Software", True)
                regKey.CreateSubKey("Nelonic")
                regKey = Registry.CurrentUser.OpenSubKey("Software\Nelonic", True)
                regKey.CreateSubKey("TANAutoMailer")
                regKey = Registry.CurrentUser.OpenSubKey("Software\Nelonic\TANAutoMailer", True)
                regKey.CreateSubKey(txtProfName.Text)
                regKey = Registry.CurrentUser.OpenSubKey("Software\Nelonic\TANAutoMailer\" & txtProfName.Text, True)
                regKey.SetValue("Path", txtWatchFolder.Text)
                regKey.SetValue("SMTPServer", txtSMTPServer.Text)
                regKey.SetValue("SMTPPort", txtSMTPPort.Text)
                regKey.SetValue("UseSSL", chkUseSSL.Checked)
                regKey.SetValue("FromAddr", txtFrom.Text)
                regKey.SetValue("ToAddrs", txtToAddrs.Text)
                regKey.SetValue("UseCreds", chkUseCreds.Checked)
                regKey.SetValue("FileType", txtFileType.Text)

                regKey.Close()

                If chkUseCreds.Checked Then

                    Dim cm As New Credential
                    cm.Target = "TANAutoMailer:" & txtProfName.Text
                    cm.PersistanceType = PersistanceType.LocalComputer
                    cm.Username = txtUsername.Text
                    cm.Password = txtPW.Text
                    cm.Save()
                Else
                    Dim cm As New Credential
                    cm.Target = "TANAutoMailer:" & txtProfName.Text

                    If cm.Exists Then
                        cm.Delete()
                    End If
                End If

            Catch ex As Exception
                gTray.ShowBalloonTip(5000, "TAN Auto Mailer Error", ex.Message, ToolTipIcon.Error)
            End Try

            BuildProfileList()

            cboProfiles.SelectedItem = txtProfName.Text

            MyThreads.Remove(txtProfName.Text)

            Threading.Thread.Sleep(1500) 'sleep to wait for old thread to realize it should kill itself

            'Start the folder monitoring in new thread
            Dim t As New Thread(Sub()
                                    Dim mm As New MonitorAndMail
                                    mm.Start(txtProfName.Text, txtWatchFolder.Text, txtSMTPServer.Text, txtSMTPPort.Text, chkUseSSL.Checked, txtFrom.Text, txtToAddrs.Text, chkUseCreds.Checked, txtFileType.Text, MyThreads, gTray)
                                End Sub)
            t.IsBackground = True
            t.Start()

            MyThreads.Add(txtProfName.Text, t)

        Else
            MsgBox("1 or more error were found: " & vbCrLf & vbCrLf & err)
        End If

    End Sub

    Private Sub cboProfiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProfiles.SelectedIndexChanged

        btnDelete.Enabled = True

        Try
            Dim regKey As RegistryKey

            regKey = Registry.CurrentUser.OpenSubKey("Software\Nelonic\TANAutoMailer\" & cboProfiles.SelectedItem, False)

            txtProfName.Text = cboProfiles.SelectedItem
            txtWatchFolder.Text = regKey.GetValue("Path")
            txtSMTPServer.Text = regKey.GetValue("SMTPServer")
            txtSMTPPort.Text = regKey.GetValue("SMTPPort")
            chkUseSSL.Checked = CBool(regKey.GetValue("UseSSL"))
            txtFrom.Text = regKey.GetValue("FromAddr")
            txtToAddrs.Text = regKey.GetValue("ToAddrs")
            txtFileType.Text = regKey.GetValue("FileType")

            If CBool(regKey.GetValue("UseCreds")) Then
                chkUseCreds.Checked = True

                Dim cm As New Credential
                cm.Target = "TANAutoMailer:" & cboProfiles.SelectedItem

                If cm.Exists Then
                    cm.Load()
                    txtUsername.Text = cm.Username
                    txtPW.Text = cm.Password
                End If
            Else
                chkUseCreds.Checked = False
                txtUsername.Text = ""
                txtPW.Text = ""
            End If

            regKey.Close()
        Catch ex As Exception
            gTray.ShowBalloonTip(5000, "TAN Auto Mailer Error", ex.Message, ToolTipIcon.Error)
        End Try

    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click

        If FolderBrowserDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            txtWatchFolder.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim result As Integer = MessageBox.Show("Are you sure you want to delete this profile?", "Delete Profile?", MessageBoxButtons.YesNo)

        If result = System.Windows.Forms.DialogResult.Yes Then

            Registry.CurrentUser.DeleteSubKey("Software\Nelonic\TANAutoMailer\" & cboProfiles.SelectedItem)

            txtProfName.Text = ""
            txtWatchFolder.Text = ""
            txtSMTPServer.Text = ""
            txtSMTPPort.Text = ""
            txtFrom.Text = ""
            txtToAddrs.Text = ""
            txtFileType.Text = ""
            chkUseSSL.Checked = False
            chkUseCreds.Checked = False
            btnDelete.Enabled = False

            Dim cm As New Credential
            cm.Target = "TANAutoMailer:" & cboProfiles.SelectedItem

            If cm.Exists Then
                cm.Delete()
            End If

            'remove this thread from the collection, it should die 500 ms later
            MyThreads.Remove(cboProfiles.SelectedItem)

            cboProfiles.Items.Remove(cboProfiles.SelectedItem)

        End If

    End Sub

    Private Sub chkUseCreds_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseCreds.CheckedChanged

        If chkUseCreds.Checked Then
            txtUsername.Enabled = True
            txtPW.Enabled = True
            lblUsername.Enabled = True
            lblPW.Enabled = True
        Else
            txtUsername.Enabled = False
            txtPW.Enabled = False
            lblUsername.Enabled = False
            lblPW.Enabled = False
        End If

    End Sub

    Private Sub frmProfiles_Load(sender As Object, e As EventArgs) Handles Me.Load

        txtUsername.Enabled = False
        txtPW.Enabled = False
        lblUsername.Enabled = False
        lblPW.Enabled = False
        btnDelete.Enabled = False

        BuildProfileList()

    End Sub

    Sub BuildProfileList()

        cboProfiles.Items.Clear()

        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey("Software\Nelonic\TANAutoMailer", False)

        If regKey IsNot Nothing Then
            For Each k As String In regKey.GetSubKeyNames
                cboProfiles.Items.Add(k)
            Next

            regKey.Close()
        End If

    End Sub

End Class