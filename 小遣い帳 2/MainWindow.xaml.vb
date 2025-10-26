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
        Dim TableViewSource As CollectionViewSource = CType(FindResource("TableContext"), CollectionViewSource)
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
    End Sub
End Class
