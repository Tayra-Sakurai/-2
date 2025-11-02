Imports System.ComponentModel
Imports Microsoft.EntityFrameworkCore

Class MainWindow
    ''' <summary>
    ''' Database
    ''' </summary>
    Dim db As New MyContext()

    Private Data As TableData

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
        Data = CType(FindResource("SuperTableData"), TableData)
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

    Private Sub SuperEditButton_Click(sender As Object, e As RoutedEventArgs) Handles SuperEditButton.Click
        If SuperDataGrid.SelectedIndex >= 0 Then
            ' The updated row.
            Dim TriData As Table = Data.GenerateRow()
            With SuperDataGrid.SelectedItem
                .Id = TriData.Id
                .Trade = TriData.Trade
                .Cash = TriData.Cash
                .Date = TriData.Date
                .Coop = TriData.Coop
                .ICOCA = TriData.ICOCA
            End With
            ' Update the database
            db.SaveChanges()
            Calc_Current_Charge()
        End If
    End Sub

    ''' <summary>
    ''' Displays new data.
    ''' </summary>
    ''' <param name="sender">
    ''' (Object)
    ''' The sender of the event.
    ''' </param>
    ''' <param name="e">
    ''' (SelectionChangedEventArgs)
    ''' The event arguments.
    ''' </param>
    Private Sub SuperDataGrid_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles SuperDataGrid.SelectionChanged
        If (SuperDataGrid.SelectedItem IsNot Nothing) AndAlso (Data IsNot Nothing) Then
            ' The selected row.
            ' Insert to the border.
            Data.Insert_Table(SuperDataGrid.SelectedItem)
        End If
    End Sub

    ''' <summary>
    ''' Switches to new row mode.
    ''' </summary>
    ''' <param name="sender">
    ''' (Object)
    ''' The event sender.
    ''' </param>
    ''' <param name="e">
    ''' (RoutedEventArgs)
    ''' The event arguments.
    ''' </param>
    Private Sub SuperDeleteButton_Click(sender As Object, e As RoutedEventArgs) Handles SuperDeleteButton.Click
        ' Remove the selection
        SuperDataGrid.SelectedIndex = -1
        Data.Clear()
    End Sub

    ''' <summary>
    ''' Adds new data.
    ''' This is the only entry sub.
    ''' </summary>
    ''' <param name="sender">
    ''' (Object)
    ''' The sender of this event.
    ''' </param>
    ''' <param name="e">
    ''' (RoutedEventArgs)
    ''' The event arguments.
    ''' </param>
    Private Sub SuperCreateButtton_Click(sender As Object, e As RoutedEventArgs) Handles SuperCreateButtton.Click
        ' The new row
        Dim NewRow As Table = Data.GenerateRow()
        ' Add the data
        db.Table.Local.Add(NewRow)
        db.SaveChanges()
        Calc_Current_Charge()
    End Sub
End Class
