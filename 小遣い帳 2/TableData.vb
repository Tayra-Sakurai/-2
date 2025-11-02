Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports Microsoft.EntityFrameworkCore

Public Class TableData
    Implements INotifyPropertyChanged

    Private dateValue As Date
    Private idValue As Integer
    Private tradeValue As String
    Private cashValue As Decimal
    Private coopValue As Decimal
    Private icocaValue As Decimal

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    ''' <summary>
    ''' Property change notifications
    ''' </summary>
    ''' <param name="propertyName">
    ''' (String)
    ''' The changed property name.
    ''' </param>
    Private Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    ''' <summary>
    ''' Initialization
    ''' </summary>
    Public Sub New()
        ' Load the database.
        Dim db As New MyContext()
        db.Table.Load()

        ' The Table
        Dim tbl As List(Of Table) = db.Table.Local.ToList()
        ' The Ids
        Dim IdList As IEnumerable(Of Integer) = From t As Table In tbl
                                                Select t.Id
        ' The max id
        Dim IdMax As Integer = IdList.Max()

        idValue = IdMax + 1
        cashValue = 0D
        coopValue = 0D
        icocaValue = 0D

        ' The date and time of now
        Dim theDate As Date = Date.Now
        Me.Date = theDate
    End Sub

    Public Property [Date] As Date
        Get
            Return dateValue
        End Get
        Set(value As Date)
            dateValue = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property Id As Integer
        Get
            Return idValue
        End Get
        Set(value As Integer)
            idValue = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property Cash As Decimal
        Get
            Return cashValue
        End Get
        Set(value As Decimal)
            cashValue = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property Trade As String
        Get
            Return tradeValue
        End Get
        Set(value As String)
            tradeValue = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property ICOCA As Decimal
        Get
            Return icocaValue
        End Get
        Set(value As Decimal)
            icocaValue = value
            NotifyPropertyChanged()
        End Set
    End Property

    Public Property Coop As Decimal
        Get
            Return coopValue
        End Get
        Set(value As Decimal)
            coopValue = value
            NotifyPropertyChanged()
        End Set
    End Property
End Class
