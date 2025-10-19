Imports Microsoft.EntityFrameworkCore

Public Class MyContext
    Inherits DbContext
    ''' <summary>
    ''' Data
    ''' </summary>
    ''' <returns></returns>
    Public Property Table() As DbSet(Of Table)

    Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
        optionsBuilder.UseSqlServer(My.Settings.DatabaseConnectionString)
    End Sub

    ''' <summary>
    ''' Model creator
    ''' </summary>
    ''' <param name="modelBuilder">
    ''' (ModelBuilder)
    ''' ModelBuilder instance.
    ''' </param>
    Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
        modelBuilder.Entity(Of Table)()
    End Sub
End Class

Public Class Table
    Public Property Id As Integer
    Public Property [Date] As Date
    Public Property Trade As String
    Public Property Cash As Decimal
    Public Property ICOCA As Decimal
    Public Property Coop As Decimal
End Class