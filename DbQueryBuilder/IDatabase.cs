using System.Data;

namespace DbQueryBuilder
{
    /// <summary>
    /// Database query executer
    /// </summary>
    public interface IDatabase
    {
        void Connect();
        void Disconnect();

        IDataReader ExecuteResultQuery(IQueryBuilderSelect queryBuilder);
        void CloseDataReader(IDataReader dataReader);

        IDataAdapter GetDataAdapter(IQueryBuilderSelect queryBuilder);

        IDbCommand CreateCommand();
        IDataParameter CreateParameter();
    }
}
