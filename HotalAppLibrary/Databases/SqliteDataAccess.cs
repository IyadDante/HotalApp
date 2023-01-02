using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;

namespace HotalAppLibrary.Databases
{
    public class SqliteDataAccess : ISqliteDataAccess
    {
        private readonly IConfiguration _config;

        public SqliteDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public List<T> LoadDate<T, U>(string sqlStatment, U parameters, string connectionStringName)
        {
            string? connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqliteConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlStatment, parameters).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string sqlStatment, T parameter, string connectionStringName)
        {
            string? connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqliteConnection(connectionString))
            {
                connection.Execute(sqlStatment, parameter);
            }
        }

    }
}
