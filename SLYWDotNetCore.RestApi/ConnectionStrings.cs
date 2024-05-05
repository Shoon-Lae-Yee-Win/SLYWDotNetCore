using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLYWDotNetCore.Restapi
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new()
        {
            DataSource = ".", // Server Name
            InitialCatalog = "DotNetTrainingBatch4", // DataBase Name
            UserID = "sa", //User Name
            Password = "sa123@", // User Password
            TrustServerCertificate = true,
        };
    }
}
