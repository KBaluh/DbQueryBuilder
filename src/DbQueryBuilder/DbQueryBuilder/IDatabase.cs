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

        void BeginTransaction();
        void CommitTransaction();
        void Rollback();

        IDbCommand CreateCommand();
        IDataParameter CreateParameter();

        IDataAdapter GetDataAdapter(IQueryBuilderSelect queryBuilder);
        IDataReader ExecuteResultQuery(IQueryBuilderSelect queryBuilder);
        void CloseDataReader(IDataReader dataReader);

        bool ExecuteQuery(IQueryBuilderInsert queryBuilder);
        bool ExecuteQuery(IQueryBuilderUpdate queryBuilder);
        bool ExecuteQuery(IQueryBuilderDelete queryBuilder);

        bool ExecuteNonQuery(IQueryBuilderInsert queryBuilder);
        bool ExecuteNonQuery(IQueryBuilderUpdate queryBuilder);
        bool ExecuteNonQuery(IQueryBuilderDelete queryBuilder);
    }
}
