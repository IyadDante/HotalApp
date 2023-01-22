using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HotalAppLibrary.Databases
{   
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public List<T> LoadData<T, U>(string sqlStatement,
                                      U parameter,
                                      string connectionStringName,
                                      bool isStoredProcedure = false)
        {
            string? connectionString = _config.GetConnectionString(connectionStringName);
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlStatement, parameter, commandType: commandType).ToList();
                return rows;
            }

        }

        public List<T> SelectData<T, U>(string sqlStatement,
                              string connectionStringName)
        {
            string? connectionString = _config.GetConnectionString(connectionStringName);
            CommandType commandType = CommandType.Text;

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlStatement, commandType: commandType).ToList();
                return rows;
            }

        }

        ////  **********************        Yevhen Answer       **********************  ////

        //public List<T> LoadData<T>(string sqlStatement,
        //                      object parameter,
        //                      string connectionStringName,
        //                      bool isStoredProcedure = false)
        //{
        //    string? connectionString = _config.GetConnectionString(connectionStringName);
        //    CommandType commandType = CommandType.Text;

        //    if (isStoredProcedure == true)
        //    {
        //        commandType = CommandType.StoredProcedure;
        //    }

        //    using (IDbConnection connection = new SqlConnection(connectionString))
        //    {
        //        List<T> rows = connection.Query<T>(sqlStatement, parameter, commandType: commandType).ToList();
        //        return rows;
        //    }
        //}

        public void SaveData<T>(string sqlStatement,
                                      T parameter,
                                      string connectionStringName,
                                      bool isStoredProcedure = false)
        {
            string? connectionString = _config.GetConnectionString(connectionStringName);
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameter, commandType: commandType);
            }
        }

    }
}
