using System.Data;
using System.Data.SqlClient;

namespace DbQueryBuilder.MsSql
{
    public class MsSqlDatabase : IDatabase
    {
        private SqlConnection _connection;
        private readonly string _connectionString;

        public MsSqlDatabase(string connectionString)
        {
            _connectionString = connectionString;

            QueryFactory = new MsSqlQueryFactory(this);
        }

        public IQueryFactory QueryFactory { get; }

        public void Connnect()
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

        public void Disconnect()
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
