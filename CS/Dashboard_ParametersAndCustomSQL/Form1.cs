using DevExpress.DashboardCommon;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraEditors;

namespace Dashboard_ParametersAndCustomSQL {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();

            Dashboard dashboard = new Dashboard();

            // Creates a new dashboard parameter.
            DashboardParameter parameter1 =
                new DashboardParameter("Parameter1", typeof(string), "Beverages",
                    "Type in the category name:", true, null);
            dashboard.Parameters.Add(parameter1);

            // Creates a connection to the Northwind database and selects required data 
            // according to the parameter's value.
            Access97ConnectionParameters nwParams = 
                new Access97ConnectionParameters(@"..\..\Data\nwind.mdb","","");
            SqlDataProvider sqlProvider = new SqlDataProvider("NW", nwParams, 
                    "select * from SalesPerson where CategoryName = @Parameter1");
            // Creates a data source and adds it to the dashboard data sources' collection.
            DataSource dataSource = new DataSource("Data Source 1", sqlProvider);
            dashboard.DataSources.Add(dataSource);

            // Creates a Grid dashboard item with three columns.
            GridDashboardItem grid = new GridDashboardItem();
            grid.Columns.Add(new GridDimensionColumn(new Dimension("CategoryName")));
            grid.Columns.Add(new GridDimensionColumn(new Dimension("ProductName")));
            grid.Columns.Add(new GridMeasureColumn(new Measure("Extended Price")));
            
            dashboard.Items.Add(grid); grid.DataSource = dataSource;
            dashboardViewer1.Dashboard = dashboard;
        }
    }
}
