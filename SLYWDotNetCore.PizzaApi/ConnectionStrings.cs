using System.Data.SqlClient;

namespace SLYWDotNetCore.PizzaApi
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".", // Server Name
            InitialCatalog = "DotNetTrainingBatch4", // DataBase Name
            UserID = "sa", //User Name
            Password = "sa123@", // User Password
            TrustServerCertificate = true,
        };
    }
}
