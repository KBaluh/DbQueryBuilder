using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DbQueryBuilder.MsSql
{
    public class MsSqlDatabase : IDatabase
    {
        #region Private fields
        private SqlConnection _connection;
        private readonly string _connectionString;

        private readonly Dictionary<IDataReader, SqlConnection> _readerConnections = new Dictionary<IDataReader, SqlConnection>();

        internal IQueryFactory QueryFactory { get; }
        #endregion

        #region Ctor

        public MsSqlDatabase(string connectionString)
        {
            _connectionString = connectionString;

            QueryFactory = new MsSqlQueryFactory(this);
        }

        #endregion

        #region IDatabase implementation

        public void Connect()
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

        public IDataReader ExecuteResultQuery(IQueryBuilderSelect queryBuilder)
        {
            return ExecuteResultQuery(queryBuilder.ToString());
        }

        public void CloseDataReader(IDataReader dataReader)
        {
            _readerConnections.TryGetValue(dataReader, out SqlConnection connection);
            if (connection != null)
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            _readerConnections.Remove(dataReader);
            dataReader.Close();
            dataReader.Dispose();
        }

        public IDataAdapter GetDataAdapter(IQueryBuilderSelect queryBuilder)
        {
            return GetDataAdapter(queryBuilder.ToString());
        }

        public IDbCommand CreateCommand()
        {
            IDbCommand command = new SqlCommand();
            return command;
        }

        public IDataParameter CreateParameter()
        {
            IDataParameter parameter = new SqlParameter();
            return parameter;
        }

        #endregion

        #region Private methods

        private IDataReader ExecuteResultQuery(string query)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            IDataReader dataReader = command.ExecuteReader();
            _readerConnections.Add(dataReader, connection);
            return dataReader;
        }

        private bool ExecuteQuery(IDbCommand command)
        {
            command.Connection = _connection;
            SqlTransaction transaction = _connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (SqlException)
            {
                transaction.Rollback();
                throw;
            }
        }

        private bool ExecuteNonQuery(IDbCommand command)
        {
            command.Connection = _connection;
            command.ExecuteNonQuery();
            return true;
        }

        private IDataAdapter GetDataAdapter(string query)
        {
            if (_connection == null)
            {
                Connect();
            }
            IDataAdapter dataAdapter = new SqlDataAdapter(query, _connection);
            return dataAdapter;
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

        #endregion
    }
}
