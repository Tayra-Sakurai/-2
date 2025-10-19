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
        ' get the local data
        Dim query = db.Table.Local
        SuperDataGrid.ItemsSource = query.ToList()
        SuperDataGrid.Items.SortDescriptions.Add(New SortDescription("Date", ListSortDirection.Descending))
    End Sub


End Class
