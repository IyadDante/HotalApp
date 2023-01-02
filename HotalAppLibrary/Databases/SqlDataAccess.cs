using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

// THIS IS ONE OF THE MAIN STEPS IN THE APPLICATION WHERE WE SETUP DAPPER TO READ AND WRITE FROM SQL
namespace HotalAppLibrary.Databases
{   // This is the class  where we talk to SQL database directly 
    public class SqlDataAccess : ISqlDataAccess
    {
        // IConfiguration comes from Microsoft
        // Here we create a Private IConfigaration 
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        // Lets create a method (Retunes Data) that is going to **Load** data using generics  
        public List<T> LoadData<T, U>(string sqlStatement,
                                      U parameter,
                                      string connectionStringName,
                                      bool isStoredProcedure = false)
        {
            // This will read the Connection String from our **appsettings.json**
            string? connectionString = _config.GetConnectionString(connectionStringName);
            // This will specifiy that the sqlStatement is a SQL select statment
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
            {
                //This will specifiy that the sqlStatement is a SQL Stored Procedure
                commandType = CommandType.StoredProcedure;
            }

            // Here we creates the new SQL instance using the IDbConnection Interface to pass in SqlConnection = connectionStringName
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                // Query in Dapper means we are running sqlStatement and retunning data
                List<T> rows = connection.Query<T>(sqlStatement, parameter, commandType: commandType).ToList();
                return rows;
            }

        }

        // Lets create a method (No Retunes Data) that is going to **Save** data using generics 
        public void SaveData<T>(string sqlStatement,
                                      T parameter,
                                      string connectionStringName,
                                      bool isStoredProcedure = false)
        {
            string? connectionString = _config.GetConnectionString(connectionStringName);
            //This will specifiy that the sqlStatement is a SQL select statment
            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
            {
                //This will specifiy that the sqlStatement is a SQL Stored Procedure
                commandType = CommandType.StoredProcedure;
            }

            // Here we creates the new SQL instance using the IDbConnection Interface
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //Execute in Dapper means we are running sqlStatement ONLY
                connection.Execute(sqlStatement, parameter, commandType: commandType);
            }

        }

    }
}
