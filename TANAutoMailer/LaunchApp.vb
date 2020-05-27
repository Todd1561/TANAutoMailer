Imports System.Threading

Module LaunchApp

    Public Sub Main()

        Dim owned As Boolean

        'prevent program from running more than once
        Using mutex As New Mutex(True, "Global\isrSDFInzxVXClo", owned)
            If (owned) Then
                Application.EnableVisualStyles()
                Application.Run(New AppContext)
                mutex.ReleaseMutex()

            Else
                MsgBox("Another instance is already running.  Quiting...")
                Application.Exit()
            End If
        End Using


    End Sub

End Module
