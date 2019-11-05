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

        private SqlTransaction _transaction;

        private readonly Dictionary<IDataReader, SqlConnection> _readerConnections = new Dictionary<IDataReader, SqlConnection>();

        #endregion

        #region Ctor

        public MsSqlDatabase(string connectionString)
        {
            _connectionString = connectionString;

            QueryFactory = new MsSqlQueryFactory(this);
        }

        #endregion

        #region IDatabase implementation

        public IQueryFactory QueryFactory { get; }

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

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
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

        public IDataAdapter GetDataAdapter(IQueryBuilderSelect queryBuilder)
        {
            return GetDataAdapter(queryBuilder.ToString());
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

        public bool ExecuteQuery(IQueryBuilderInsert queryBuilder)
        {
            return ExecuteQuery(queryBuilder.GetCommand());
        }

        public bool ExecuteQuery(IQueryBuilderUpdate queryBuilder)
        {
            return ExecuteQuery(queryBuilder.GetCommand());
        }

        public bool ExecuteQuery(IQueryBuilderDelete queryBuilder)
        {
            return ExecuteQuery(queryBuilder.GetCommand());
        }

        public bool ExecuteNonQuery(IQueryBuilderInsert queryBuilder)
        {
            return ExecuteNonQuery(queryBuilder.GetCommand());
        }

        public bool ExecuteNonQuery(IQueryBuilderUpdate queryBuilder)
        {
            return ExecuteNonQuery(queryBuilder.GetCommand());
        }

        public bool ExecuteNonQuery(IQueryBuilderDelete queryBuilder)
        {
            return ExecuteNonQuery(queryBuilder.GetCommand());
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
