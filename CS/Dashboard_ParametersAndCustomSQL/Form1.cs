using DevExpress.DashboardCommon;
using DevExpress.XtraEditors;
using DevExpress.DataAccess;
using DevExpress.DataAccess.Sql;

namespace Dashboard_ParametersAndCustomSQL {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();

            Dashboard dashboard = new Dashboard();
            dashboard.LoadFromXml(@"..\..\Data\Dashboard.xml");

            // Creates a new dashboard parameter.
            StaticListLookUpSettings staticSettings = new StaticListLookUpSettings();
            staticSettings.Values = new string[] { "2014", "2015", "2016" };
            DashboardParameter yearParameter = new DashboardParameter("yearParameter", 
                typeof(string), "2015", "Select year:", true, staticSettings);
            dashboard.Parameters.Add(yearParameter);

            DashboardSqlDataSource dataSource = (DashboardSqlDataSource)dashboard.DataSources[0];
            CustomSqlQuery salesPersonQuery = (CustomSqlQuery)dataSource.Queries[0];
            salesPersonQuery.Parameters.Add(new QueryParameter("startDate", typeof(Expression), 
                new Expression("[Parameters.yearParameter] + '/01/01'")));
            salesPersonQuery.Parameters.Add(new QueryParameter("endDate", typeof(Expression), 
                new Expression("[Parameters.yearParameter] + '/12/31'")));
            salesPersonQuery.Sql = 
                "select * from SalesPerson where OrderDate between @startDate and @endDate";

            dashboardViewer1.Dashboard = dashboard;
        }
    }
}
