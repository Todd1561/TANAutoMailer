Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Runtime.InteropServices
Imports CredentialManagement

Class MonitorAndMail

    Private pSMTPServer As String, pSMTPPort As Integer, pUseSSL As Boolean, pFromAddr As String, pToAddrs As String,
        pUseCreds As Boolean, pProfile As String, pTray As NotifyIcon

    Sub Start(Profile As String, WatchPath As String, SMTPServer As String, SMTPPort As Integer, UseSSL As Boolean,
              FromAddr As String, ToAddrs As String, UseCreds As Boolean, FileType As String,
              MyThread As Dictionary(Of String, System.Threading.Thread), Tray As NotifyIcon)
        Try

            pProfile = Profile
            pSMTPPort = SMTPPort
            pSMTPServer = SMTPServer
            pUseSSL = UseSSL
            pFromAddr = FromAddr
            pToAddrs = ToAddrs
            pUseCreds = UseCreds
            pTray = Tray

            'send files already in folder at startup
            Dim di As New DirectoryInfo(WatchPath)

            For Each f In di.GetFiles("*." & FileType)
                EmailFile(f.FullName)
            Next

            'register folder watcher
            Dim watcher As New FileSystemWatcher(WatchPath, "*." & FileType)

            AddHandler watcher.Created, AddressOf watcher_FileCreated

            watcher.EnableRaisingEvents = True

            'keep checking if this thread is still active and sleep
            Do While MyThread.ContainsKey(Profile)
                System.Threading.Thread.Sleep(500) 'System.Threading.Timeout.Infinite
            Loop

            'thread has been killed, stop watching and end sub
            RemoveHandler watcher.Created, AddressOf watcher_FileCreated
            watcher = Nothing

        Catch ex As Exception
            pTray.ShowBalloonTip(5000, "TAN Auto Mailer Error", ex.Message, ToolTipIcon.Error)
        End Try

    End Sub

    Sub watcher_FileCreated(sender As Object, e As FileSystemEventArgs)
        Debug.WriteLine(e.FullPath)
        EmailFile(e.FullPath)

    End Sub

    Sub EmailFile(strFile As String)

        'sleep for 10 seconds to wait for file to be unlocked. found it unreliable to try to detect if file locked and looping to wait
        Threading.Thread.Sleep(10000)

        'check to see if file exists.  found issue with Chrome saving as PDF where it creates 2 files erroneously 
        If File.Exists(strFile) Then

            Dim fAttach As Attachment = Nothing, timeout As Integer = 0

            Try
                Dim smtp As New SmtpClient(pSMTPServer, pSMTPPort) With {
                .EnableSsl = pUseSSL,
                .UseDefaultCredentials = False
            }

                If pUseCreds Then
                    Dim cm As New Credential
                    cm.Target = "TANAutoMailer:" & pProfile

                    If cm.Exists Then
                        cm.Load()
                        smtp.Credentials = New Net.NetworkCredential(cm.Username, cm.Password)
                    End If

                End If

                Dim mm As New MailMessage

                'keep looping until file lock is released, until timeout expires
                'Do Until IsFileOpen(strFile) = False Or timeout >= 30
                'Threading.Thread.Sleep(1000)
                'timeout += 1
                'Loop

                'If timeout >= 30 Then
                'pTray.ShowBalloonTip(5000, "TAN Auto Mailer Error", "Skipping file, locked after 30 sec. timeout: " & strFile, ToolTipIcon.Error)
                'smtp.Dispose()
                'mm.Dispose()
                ' Sub
                'End If

                fAttach = New Attachment(strFile)

                mm.Attachments.Add(fAttach)
                mm.To.Add(pToAddrs)
                mm.From = New MailAddress(pFromAddr)

                smtp.Send(mm)
                fAttach.Dispose()
                File.Delete(strFile)

            Catch ex As Exception
                If fAttach IsNot Nothing Then fAttach.Dispose()
                pTray.ShowBalloonTip(5000, "TAN Auto Mailer Error", ex.Message, ToolTipIcon.Error)
            End Try
        End If

    End Sub

    Function IsFileOpen(strFile As String)

        Dim stream As FileStream = Nothing

        Try
            stream = File.Open(strFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            stream.Close()
        Catch ex As Exception
            If TypeOf ex Is IOException Then Return True
        End Try

        Return False

    End Function

    Private Function IsFileLocked(exception As Exception) As Boolean
        Dim errorCode As Integer = Marshal.GetHRForException(exception) And ((1 << 16) - 1)
        Return errorCode = 32 OrElse errorCode = 33
    End Function

End Class
