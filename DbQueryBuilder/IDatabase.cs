using System.Data;

namespace DbQueryBuilder
{
    /// <summary>
    /// Database query executer
    /// </summary>
    public interface IDatabase
    {
        IQueryFactory QueryFactory { get; }

        void Connect();
        void Disconnect();

        IDataReader ExecuteResultQuery(IQueryBuilderSelect queryBuilder);
        void CloseDataReader(IDataReader dataReader);

        IDataAdapter GetDataAdapter(IQueryBuilderSelect queryBuilder);

        IDbCommand CreateCommand();
        IDataParameter CreateParameter();

        void BeginTransaction();

        void CommitTransaction();

        void Rollback();
    }
}
