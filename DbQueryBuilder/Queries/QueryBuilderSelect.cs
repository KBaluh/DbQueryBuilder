using System.Collections.Generic;

using DbQueryBuilder.Databases;

namespace DbQueryBuilder.Queries
{
    public class QueryBuilderSelect : IQueryBuilderSelect
    {
        #region Database fields
        private IDatabase Database { get; }
        private IQueryQuotes QueryQuotes => Database.GetQueryQuotes();
        #endregion

        #region Select

        private string _select = " * ";

        private string _from = string.Empty;

        #endregion

        #region Where

        private string _customWhere = "";

        private readonly IQueryBuilderWhere _builderWhere;

        private readonly List<IQueryBuilderWhere> _builderWheres = new List<IQueryBuilderWhere>();

        #endregion

        #region Ctor

        public QueryBuilderSelect(IDatabase database)
        {
            Database = database ?? throw new System.ArgumentNullException(nameof(database));
            _builderWhere = new QueryBuilderWhere(QueryQuotes);
            _builderWhere.And(_builderWhere);
        }

        #endregion

        #region Implementation of IQueryBuilderSelect

        public void Select(string[] columns)
        {
            if (columns == null || columns.Length == 0)
            {
                _select = " * ";
            }
            else
            {
                string columnString = string.Join(", ", columns);
                _select = columnString;
            }
        }

        public void From(string tableName)
        {
            _from = tableName;
        }

        public void Where(string customWhere)
        {
            _customWhere = customWhere;
        }
        
        public void Where(IQueryBuilderWhere where)
        {
            int index = _builderWheres.IndexOf(where);
            if (index == -1)
            {
                _builderWheres.Add(where);
            }
            else
            {
                _builderWheres[index] = where;
            }
        }

        public IQueryBuilderWhere Where(string whereField, object whereValue)
        {
            _builderWhere.Where(whereField, whereValue);
            return _builderWhere;
        }

        public IQueryBuilderWhere Where(string whereField, string operand, object whereValue)
        {
            _builderWhere.Where(whereField, operand, whereValue);
            return _builderWhere;
        }

        #endregion
    }
}
