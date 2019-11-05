using System.Data;

namespace DbQueryBuilder
{
    public interface IQueryBuilderDelete
    {
        void Where(string tableName, string whereField, object whereValue);
        void Where(string tableName, string where);
        IDbCommand GetCommand();
    }
}
