using System.Data;

namespace DbQueryBuilder
{
    public interface IQueryBuilderUpdate
    {
        void Where(string tableName, string whereField, object whereValue);
        void Where(string tableName, string customWhere);

        void Add(string field, object value);

        IDbCommand GetCommand();
    }
}
