Imports DevExpress.DashboardCommon
Imports DevExpress.XtraEditors
Imports DevExpress.DataAccess
Imports DevExpress.DataAccess.Sql

Namespace Dashboard_ParametersAndCustomSQL
	Partial Public Class Form1
		Inherits XtraForm

		Public Sub New()
			InitializeComponent()

			Dim dashboard As New Dashboard()
			dashboard.LoadFromXml("..\..\Data\Dashboard.xml")

			' Creates a new dashboard parameter.
			Dim staticSettings As New StaticListLookUpSettings()
			staticSettings.Values = New String() { "2014", "2015", "2016" }
			Dim yearParameter As New DashboardParameter("yearParameter", GetType(String), "2015", "Select year:", True, staticSettings)
			dashboard.Parameters.Add(yearParameter)

			Dim dataSource As DashboardSqlDataSource = DirectCast(dashboard.DataSources(0), DashboardSqlDataSource)
			Dim salesPersonQuery As CustomSqlQuery = CType(dataSource.Queries(0), CustomSqlQuery)
			salesPersonQuery.Parameters.Add(New QueryParameter("startDate", GetType(Expression), New Expression("[Parameters.yearParameter] + '/01/01'")))
			salesPersonQuery.Parameters.Add(New QueryParameter("endDate", GetType(Expression), New Expression("[Parameters.yearParameter] + '/12/31'")))
			salesPersonQuery.Sql = "select * from SalesPerson where OrderDate between @startDate and @endDate"

			dashboardViewer1.Dashboard = dashboard
		End Sub
	End Class
End Namespace
