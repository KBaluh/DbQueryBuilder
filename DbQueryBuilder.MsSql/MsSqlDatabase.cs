using System.Data;
using System.Data.SqlClient;

using DbQueryBuilder.Databases;
using DbQueryBuilder.Queries;

namespace DbQueryBuilder.MsSql
{
    public class MsSqlDatabase : Database
    {
        private SqlConnection _connection;
        private readonly string _connectionString;
        private readonly IQueryQuotes _queryQuotes;

        public MsSqlDatabase(string connectionString) 
            : base(Databases.DbType.MsSql)
        {
            _connectionString = connectionString;
            _queryQuotes = new MsSqlQueryQuotes();
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

        public override IQueryQuotes GetQueryQuotes()
        {
            return _queryQuotes;
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
