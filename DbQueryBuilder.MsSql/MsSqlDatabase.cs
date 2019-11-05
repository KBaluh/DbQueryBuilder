using System.Data;
using System.Data.SqlClient;

using DbQueryBuilder.Databases;

namespace DbQueryBuilder.MsSql
{
    public class MsSqlDatabase : Database
    {
        private SqlConnection _connection;
        private readonly string _connectionString;

        public MsSqlDatabase(string connectionString) 
            : base(Databases.DbType.MsSql)
        {
            _connectionString = connectionString;
        }

        public override void Connnect()
        {
            if (_connection == null)
            {
                _connection = CreateConnection();
            }

            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public override void Disconnect()
        {
            if (_connection == null)
            {
                return;
            }
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        private SqlConnection CreateConnection()
        {
            string connectionString = GetConnectionString();
            return new SqlConnection(connectionString);
        }

        private string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
