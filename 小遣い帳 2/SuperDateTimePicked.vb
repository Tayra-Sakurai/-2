Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class SuperDateTimePicked
    Implements INotifyPropertyChanged

    ''' <summary>
    ''' Input year
    ''' </summary>
    Private _Year As Integer

    ''' <summary>
    ''' Input month number
    ''' </summary>
    Private _Month As Integer

    ''' <summary>
    ''' Input day of the month.
    ''' </summary>
    Private _DayOfMonth As Integer

    ''' <summary>
    ''' The hour part of the time.
    ''' </summary>
    Private _Hour As Integer

    ''' <summary>
    ''' The minute part of the time.
    ''' </summary>
    Private _Minute As Integer

    ''' <summary>
    ''' The second part of the time.
    ''' </summary>
    Private _Second As Integer

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub NotifyPropertyChanged(<CallerMemberName()> Optional propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    ''' <summary>
    ''' Initilizes the class by filling the fields with the current date and time.
    ''' </summary>
    Public Sub New()
        ' The current date and time.
        Dim d As Date = DateTime.Now
        _Year = d.Year
        _Month = d.Month
        _DayOfMonth = d.Day
        _Hour = d.Hour
        _Minute = d.Minute
        _Second = d.Second
    End Sub

    ''' <summary>
    ''' Gets or sets the date.
    ''' </summary>
    ''' <returns>
    ''' (Date)
    ''' The date of the input.
    ''' </returns>
    Public Property [Date] As Date
        Get
            Return New DateTime(_Year, _Month, _DayOfMonth)
        End Get
        Set(value As Date)
            _Year = value.Year
            _Month = value.Month
            _DayOfMonth = value.Day
            NotifyPropertyChanged()
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the hours.
    ''' </summary>
    ''' <returns>
    ''' (Integer)
    ''' The hour.
    ''' </returns>
    Public Property Hour As Integer
        Get
            Return _Hour
        End Get
        Set(value As Integer)
            _Hour = value
            NotifyPropertyChanged()
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the minute of the time.
    ''' </summary>
    ''' <returns>
    ''' (Integer)
    ''' The minute.
    ''' </returns>
    Public Property Minute As Integer
        Get
            Return _Minute
        End Get
        Set(value As Integer)
            _Minute = value
            NotifyPropertyChanged()
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the second of the time.
    ''' </summary>
    ''' <returns>
    ''' (Integer)
    ''' The second of the time.
    ''' </returns>
    Public Property Second As Integer
        Get
            Return _Second
        End Get
        Set(value As Integer)
            _Second = value
            NotifyPropertyChanged()
        End Set
    End Property

    ''' <summary>
    ''' This is the property to check the result.
    ''' </summary>
    ''' <returns>
    ''' (Date)
    ''' The input date and time.
    ''' </returns>
    Public ReadOnly Property [DateAndTime] As Date
        Get
            Return New DateTime(_Year, _Month, _DayOfMonth, _Hour, _Minute, _Second)
        End Get
    End Property

    ''' <summary>
    ''' Set the date.
    ''' This can be only used for setup.
    ''' </summary>
    ''' <param name="value">
    ''' (Date)
    ''' The date and time to be set.
    ''' </param>
    Public Sub SetDate(value As Date)
        _Year = value.Year
        _Month = value.Month
        _DayOfMonth = value.Day
        _Hour = value.Hour
        _Minute = value.Minute
        _Second = value.Second
        NotifyPropertyChanged()
    End Sub
End Class
