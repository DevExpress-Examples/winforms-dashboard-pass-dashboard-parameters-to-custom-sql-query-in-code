Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.XtraEditors

Namespace Dashboard_ParametersAndCustomSQL

    Public Partial Class Form1
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
            Dim dashboard As Dashboard = New Dashboard()
            ' Creates a new dashboard parameter.
            Dim parameter1 As DashboardParameter = New DashboardParameter("Parameter1", GetType(String), "Beverages", "Type in the category name:", True, Nothing)
            dashboard.Parameters.Add(parameter1)
            ' Creates a connection to the Northwind database and selects required data 
            ' according to the parameter's value.
            Dim nwParams As Access97ConnectionParameters = New Access97ConnectionParameters("..\..\Data\nwind.mdb", "", "")
            Dim sqlProvider As SqlDataProvider = New SqlDataProvider("NW", nwParams, "select * from SalesPerson where CategoryName = @Parameter1")
            ' Creates a data source and adds it to the dashboard data sources' collection.
            Dim dataSource As DataSource = New DataSource("Data Source 1", sqlProvider)
            dashboard.DataSources.Add(dataSource)
            ' Creates a Grid dashboard item with three columns.
            Dim grid As GridDashboardItem = New GridDashboardItem()
            grid.Columns.Add(New GridDimensionColumn(New Dimension("CategoryName")))
            grid.Columns.Add(New GridDimensionColumn(New Dimension("ProductName")))
            grid.Columns.Add(New GridMeasureColumn(New Measure("Extended Price")))
            dashboard.Items.Add(grid)
            grid.DataSource = dataSource
            dashboardViewer1.Dashboard = dashboard
        End Sub
    End Class
End Namespace
