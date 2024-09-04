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
    /// <summary>
    /// Provides utility methods for database operations.
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// Creates and returns a new database connection using configuration settings.
        /// </summary>
        /// <returns>An open database connection.</returns>
        /// <remarks>
        /// This method reads the database provider and connection string from the configuration file
        /// to create and return an instance of <see cref="IDbConnection"/>.
        /// </remarks>
        public static IDbConnection GetConnection()
        { 
            // Retrieve the database provider from the configuration file
            string dbProvider = ConfigurationManager.ConnectionStrings["default"].ProviderName;

            // Register the database provider factory
            DbProviderFactories.RegisterFactory(dbProvider, SqlClientFactory.Instance);

            // Get the database provider factory
            DbProviderFactory factory = DbProviderFactories.GetFactory(dbProvider);

            // Create and configure the database connection
            IDbConnection conn = factory.CreateConnection();
            string connStr = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            conn.ConnectionString = connStr;

            return conn;
        }
    }
}
