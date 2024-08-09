using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.DataAccessLayer.utils
{
    public class DbHelper
    {
        public static IDbConnection GetConnection()
        {
            string dbProvider = ConfigurationManager.ConnectionStrings["default"].ProviderName;
            DbProviderFactories.RegisterFactory(dbProvider, SqlClientFactory.Instance);
            DbProviderFactory factory = DbProviderFactories.GetFactory(dbProvider);

            IDbConnection conn = factory.CreateConnection();
            string connStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            conn.ConnectionString = connStr;

            return conn;
        }
    }
}
