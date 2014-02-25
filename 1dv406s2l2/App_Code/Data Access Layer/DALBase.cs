using System.Data.SqlClient;
using System.Web.Configuration;

public class DALBase
{
	//fält som håller anslutningssträngen:
    private static string _connectionString;

    //konstruktor:
    static DALBase()
    {
        //hämtar anslutningssträngen:
        _connectionString = WebConfigurationManager.ConnectionStrings["1dv406_AdventureWorksAssignmentConnectionString"].ConnectionString;
    }

    //metoder:
    protected SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}