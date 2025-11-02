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

    ''' <summary>
    ''' Generates a table row
    ''' </summary>
    ''' <returns>
    ''' (Table)
    ''' The generated row.
    ''' </returns>
    Public Function GenerateRow() As Table
        ' The row
        Dim tbl As New Table()
        With tbl
            .Id = Me.Id
            .Trade = Trade
            .Date = Me.Date
            .Cash = Cash
            .Coop = Coop
            .ICOCA = ICOCA
        End With
        Return tbl
    End Function

    ''' <summary>
    ''' Enter the data into the TableData.
    ''' </summary>
    ''' <param name="table">
    ''' (Table)
    ''' The table to insert.
    ''' </param>
    Public Sub Insert_Table(table As Table)
        Me.Date = table.Date
        Id = table.Id
        Trade = table.Trade
        Cash = table.Cash
        ICOCA = table.ICOCA
        Coop = table.Coop
    End Sub

    ''' <summary>
    ''' Clears the table
    ''' </summary>
    Public Sub Clear()
        Me.Date = Date.Now
        ' Data to read
        Dim db As New MyContext()
        ' Ids
        Dim IdList As IQueryable(Of Integer) = From t As Table In db.Table
                                               Select t.Id
        Id = IdList.ToList().Max() + 1
        Trade = ""
        ICOCA = 0D
        Coop = 0D
        Cash = 0D
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
