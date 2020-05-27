Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports Microsoft.Win32
Imports System.Threading
Imports System.Text.RegularExpressions

Public Class AppContext
    Inherits ApplicationContext

    Private WithEvents Tray As NotifyIcon
    Private WithEvents MainMenu As ContextMenuStrip
    Private WithEvents mnuHelpAbout As ToolStripMenuItem
    Private WithEvents mnuProfiles As ToolStripMenuItem
    Private WithEvents mnuSep1 As ToolStripSeparator
    Private WithEvents mnuExit As ToolStripMenuItem
    Public MyThreads As New Dictionary(Of String, Thread)

    Public Sub New()
        'Initialize the menus

        mnuHelpAbout = New ToolStripMenuItem("About TAN Window Manager")
        mnuProfiles = New ToolStripMenuItem("Manage Profiles")
        mnuSep1 = New ToolStripSeparator()
        mnuExit = New ToolStripMenuItem("Exit")
        MainMenu = New ContextMenuStrip
        MainMenu.Items.AddRange(New ToolStripItem() {mnuProfiles, mnuHelpAbout, mnuSep1, mnuExit})

        'Initialize the tray
        Tray = New NotifyIcon
        Tray.Icon = My.Resources.TrayIcon
        Tray.ContextMenuStrip = MainMenu
        Tray.Text = "TAN Auto Mailer"

        'Display
        Tray.Visible = True

        Try
            'get all profiles from registry
            Dim regKey As RegistryKey
            regKey = Registry.CurrentUser.OpenSubKey("Software\Nelonic\TANAutoMailer", False)

            If regKey IsNot Nothing Then

                If regKey.SubKeyCount = 0 Then
                    regKey.Close()
                    Tray.ShowBalloonTip(5000, "TAN Auto Mailer", "Could not find any profiles.", ToolTipIcon.Error)
                End If

                For Each k As String In regKey.GetSubKeyNames

                    Dim sk As RegistryKey
                    sk = Registry.CurrentUser.OpenSubKey("Software\Nelonic\TANAutoMailer\" & k, False)

                    If sk IsNot Nothing Then
                        Dim Profile = k
                        Dim Path = sk.GetValue("Path")
                        Dim SMTPServer = sk.GetValue("SMTPServer")
                        Dim SMTPPort = sk.GetValue("SMTPPort")
                        Dim UseSSL = sk.GetValue("UseSSL")
                        Dim FromAddr = sk.GetValue("FromAddr")
                        Dim ToAddrs = sk.GetValue("ToAddrs")
                        Dim UseCreds = sk.GetValue("UseCreds")
                        Dim FileType = Regex.Replace(sk.GetValue("FileType").ToString.ToLower(), "[^a-z0-9]", "")

                        'Start the folder monitoring in new thread
                        Dim t As New Thread(Sub()
                                                Dim mm As New MonitorAndMail
                                                mm.Start(Profile, Path, SMTPServer, SMTPPort, UseSSL, FromAddr, ToAddrs, UseCreds, FileType, MyThreads, Tray)
                                            End Sub)
                        t.IsBackground = True
                        t.Start()

                        MyThreads.Add(Profile, t)

                        sk.Close()

                    End If
                Next

                regKey.Close()

            End If

        Catch ex As Exception
            Tray.ShowBalloonTip(5000, "TAN Auto Mailer", "Error loading profiles.", ToolTipIcon.Error)
        End Try

        frmProfiles.MyThreads = MyThreads
        frmProfiles.gTray = Tray

    End Sub

    Private Sub AppContext_ThreadExit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ThreadExit
        'Guarantees that the icon will not linger.
        Tray.Visible = False
    End Sub

    Private Sub mnuProfiles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuProfiles.Click
        If Not frmProfiles.Visible Then frmProfiles.Show() : frmProfiles.Activate()
    End Sub

    Private Sub mnuHelpAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuHelpAbout.Click
        If Not frmAbout.Visible Then frmAbout.Show() : frmAbout.Activate()
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Application.Exit()
    End Sub

    Private Sub Tray_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tray.DoubleClick

        If Not frmAbout.Visible Then frmAbout.Show() : frmAbout.Activate()

    End Sub

End Class