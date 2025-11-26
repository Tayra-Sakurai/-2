Imports System.ComponentModel
Imports Microsoft.EntityFrameworkCore

Class MainWindow
    ''' <summary>
    ''' Database
    ''' </summary>
    Dim db As New MyContext()

    ''' <summary>
    ''' Basic Initializations before loading the window
    ''' </summary>
    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        ' Loading the data
        db.Table.Load()
        ' get the source
        Dim TableViewSource As CollectionViewSource = CType(FindResource("SuperTableContext"), CollectionViewSource)
        TableViewSource.Source = db.Table.Local.ToObservableCollection()
    End Sub

    ''' <summary>
    ''' Sets up the elements in the form.
    ''' </summary>
    ''' <param name="sender">
    ''' (Object)
    ''' The event sender.
    ''' </param>
    ''' <param name="e">
    ''' (RoutedEventArgs)
    ''' The event arguments.
    ''' </param>
    Private Sub Home_Loaded(sender As Object, ByVal e As RoutedEventArgs) Handles SuperWindow.Loaded
        SuperDataGrid.SelectionUnit = DataGridSelectionUnit.FullRow
        SuperDataGrid.Items.SortDescriptions.Add(New SortDescription("Date", ListSortDirection.Descending))
        Calc_Current_Charge()
    End Sub

    ''' <summary>
    ''' Calculates the current charge left.
    ''' </summary>
    Private Sub Calc_Current_Charge()
        ' Charges
        Dim cash As Decimal
        Dim icoca As Decimal
        Dim coop As Decimal
        ' Calculation
        For Each t As Table In db.Table.Local
            cash += t.Cash
            icoca += t.ICOCA
            coop += t.Coop
        Next
        SuperSuperCash.Text = Format(cash, "C")
        SuperSuperICOCA.Text = Format(icoca, "C")
        SuperSuperCoop.Text = Format(coop, "C")
    End Sub

    ''' <summary>
    ''' Removes the item.
    ''' </summary>
    ''' <param name="sender">
    ''' (<see cref="Object"/>)
    ''' The event sender.
    ''' </param>
    ''' <param name="e">
    ''' (<see cref="RoutedEventArgs"/>)
    ''' The event arguments.
    ''' </param>
    Private Sub SuperEditButton_Click(sender As Object, e As RoutedEventArgs)
        db.Table.Local.Remove(SuperDataGrid.SelectedItem)
        db.SaveChanges()
    End Sub

    ''' <summary>
    ''' Finds the largest index of the database.
    ''' </summary>
    ''' <returns>
    ''' The <see cref="Integer"/> of the largest index.
    ''' </returns>
    Private Function Find_Largest_Index() As Integer
        ' The returning value.
        ' Since there is no data with the index of 0,
        ' This has to be changed.
        Dim largesti As Integer = 0
        ' The table data.
        For Each Data As Table In db.Table.Local
            If largesti < Data.Id Then
                ' Update largesti if the index is the largest.
                largesti = Data.Id
            End If
        Next
        Return largesti
    End Function

    ''' <summary>
    ''' The data clear.
    ''' </summary>
    ''' <param name="sender">
    ''' (<see cref="Object"/>)
    ''' The event sender.
    ''' </param>
    ''' <param name="e">
    ''' (<see cref="RoutedEventArgs"/>)
    ''' The event arguments.
    ''' </param>
    Private Sub SuperDeleteButton_Click(sender As Object, e As RoutedEventArgs)
        ' The largest index number
        Dim largesti As Integer = Find_Largest_Index()
        ' New index
        Dim Nindex As Integer = largesti + 1
        ' New item's date
        Dim NDate As Date = Now
        ' The change of cash.
        Dim NCash As Decimal = 0
        ' The ICOCA value change
        Dim NICOCA As Decimal = 0
        ' The new coop change value.
        Dim NCoop As Decimal = 0
        ' Create the data.
        Dim NTable As New Table()
        With NTable
            .Id = Nindex
            .Date = NDate
            .Trade = String.Empty
            .Cash = NCash
            .Coop = NCoop
            .ICOCA = NICOCA
        End With
        db.Table.Local.Add(NTable)
        SuperDataGrid.SelectedItem = NTable
    End Sub

    ''' <summary>
    ''' Create a new row with the entered data.
    ''' </summary>
    ''' <param name="sender">
    ''' (<see cref="Object"/>)
    ''' The event sender.
    ''' </param>
    ''' <param name="e">
    ''' (<see cref="RoutedEventArgs"/>)
    ''' The event Arguments.
    ''' </param>
    Private Sub SuperCreateButton_Click(sender As Object, e As RoutedEventArgs)
        db.SaveChanges()

        ' This forces to display latest data
        SuperDataGrid.Items.Refresh()
        SuperDataGrid.Items.SortDescriptions.Add(New SortDescription("Date", ListSortDirection.Descending))
        ' Recalculate
        Calc_Current_Charge()
    End Sub

    ''' <summary>
    ''' The datepicker click function.
    ''' </summary>
    ''' <param name="sender">
    ''' (<see cref="Object"/>)
    ''' The <see cref="Object"/> which sent the event.
    ''' </param>
    ''' <param name="e">
    ''' The <see cref="RoutedEventArgs"/> of the event.
    ''' </param>
    Sub SuperDate_Click(sender As Object, e As RoutedEventArgs)
        ' The value.
        Dim DVal As Date = SuperDataGrid.SelectedItem.Date
        ' The Dialog box.
        ' This will be the input.
        Dim dialog As New DateTimePicker(DVal)
        ' Check the value.
        If dialog.ShowDialog() OrElse True Then
            ' Change the date and time.
            SuperDataGrid.SelectedItem.Date = dialog.PickedDateAndTime
        End If
    End Sub
End Class
