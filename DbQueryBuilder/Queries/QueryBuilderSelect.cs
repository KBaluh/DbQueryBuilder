using System.Collections.Generic;

using DbQueryBuilder.Databases;

namespace DbQueryBuilder.Queries
{
    public class QueryBuilderSelect
    {
        #region Database fields
        private IDatabase Database { get; }
        private IQueryQuotes QueryQuotes => Database.GetQueryQuotes();
        #endregion

        #region Where

        private string _customWhere = "";

        private readonly QueryBuilderWhere _builderWhere;

        private readonly List<QueryBuilderWhere> _builderWheres = new List<QueryBuilderWhere>();

        #endregion

        #region Ctor

        public QueryBuilderSelect(IDatabase database)
        {
            Database = database ?? throw new System.ArgumentNullException(nameof(database));
            _builderWhere = new QueryBuilderWhere(QueryQuotes);
            _builderWhere.And(_builderWhere);
        }

        #endregion
    }
}
