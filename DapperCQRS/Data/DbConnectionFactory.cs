using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperCQRS.Data
{
    public class DbConnectionFactory
    {
        public readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection( _connectionString );
        }
    }
}
