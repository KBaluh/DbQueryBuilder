using DbQueryBuilder.Databases;

namespace DbQueryBuilder.Queries
{
    public class QueryBuilderSelect
    {
        private IDatabase Database { get; }

        public QueryBuilderSelect(IDatabase database)
        {
            Database = database;
        }
    }
}
