Public Class DateTimePicker

    ''' <summary>
    ''' The data.
    ''' </summary>
    Private DT As SuperDateTimePicked

    ''' <summary>
    ''' Data to be inserted.
    ''' </summary>
    Private TemporaryData As Date

    ''' <summary>
    ''' Gets or sets the value of the input date and time.
    ''' </summary>
    ''' <returns>
    ''' (Date)
    ''' The date and time set by this window.
    ''' </returns>
    Public Property PickedDateAndTime As Date
        Get
            Return DT.DateAndTime
        End Get
        Set(value As Date)
            DT.SetDate(value)
        End Set
    End Property

    ''' <summary>
    ''' Initialization
    ''' </summary>
    ''' <param name="value">
    ''' (Date)
    ''' The input value of the date.
    ''' </param>
    Public Sub New(Optional value As Date = #1/1/1 0:00:00#)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        DT = CType(FindResource("SuperDateTimeSet"), SuperDateTimePicked)

        If value > #1/1/1 0:00:00# Then
            TemporaryData = value
        End If
    End Sub

    Private Sub SuperExit_Click(sender As Object, e As RoutedEventArgs) Handles SuperExit.Click
        DialogResult = True
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        If TemporaryData > #1/1/1 0:00:00# Then
            DT.SetDate(TemporaryData)
        End If
    End Sub
End Class
