Imports System.Windows.Threading
Imports NuSelfUpdate
Imports Squirrel

Class MainWindow
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim view = New SplashScreenView1

        Dim objTimer As New DispatcherTimer
        AddHandler objTimer.Tick, AddressOf Timer_Tick
        objTimer.Interval = New TimeSpan(0, 0, 0, 1)
        objTimer.Start()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        Dim objtimer = DirectCast(sender, DispatcherTimer)
        objtimer.Stop()

        Dim appUpdater = CreateAppUpdater()

        If appUpdater.OldVersionExists() Then
            UpgradeDatabase()
            appUpdater.RemoveOldVersionFiles()
        End If

        Dim updateCheck = appUpdater.CheckForUpdate()

        If updateCheck.UpdateAvailable Then
            Dim preparedUpdate = appUpdater.PrepareUpdate(updateCheck.UpdatePackage)
            Dim installedUpdate = appUpdater.ApplyPreparedUpdate(preparedUpdate)

            appUpdater.LaunchInstalledUpdate(installedUpdate)
            Return
        End If

        DoYourApplicationStuff()


    End Sub

    Private Sub DoYourApplicationStuff()
    End Sub

    Private Sub UpgradeDatabase()
    End Sub

    Private Function CreateAppUpdater() As AppUpdater
        Return New AppUpdaterBuilder("lalala").Build()
    End Function

End Class
