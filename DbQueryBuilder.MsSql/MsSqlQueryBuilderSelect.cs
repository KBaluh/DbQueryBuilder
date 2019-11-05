using System.Collections.Generic;

using DbQueryBuilder.MsSql;

namespace DbQueryBuilder
{
    public class MsSqlQueryBuilderSelect : IQueryBuilderSelect
    {
        #region Database fields
        private MsSqlDatabase Database { get; }
        #endregion

        #region Select

        private string _select = " * ";

        private string _from = string.Empty;

        private readonly List<IJoinBuilder> _joins = new List<IJoinBuilder>();

        #endregion

        #region Where

        private string _customWhere = string.Empty;

        private readonly IQueryBuilderWhere _builderWhere;

        private readonly List<IQueryBuilderWhere> _builderWheres = new List<IQueryBuilderWhere>();

        #endregion

        #region Order

        private bool _isOrder;
        private string _orderField;
        private OrderType _orderType;

        #endregion

        #region Limit

        private readonly ILimitBuilder _limitBuilder;

        #endregion

        #region Ctor

        public MsSqlQueryBuilderSelect(MsSqlDatabase database)
        {
            Database = database ?? throw new System.ArgumentNullException(nameof(database));

            _builderWhere = database.QueryFactory.CreateWhere();
            _builderWhere.And(_builderWhere);

            _limitBuilder = database.QueryFactory.CreateLimit();
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

        public void Join(SelectJoinType joinType, string tableName, string tableField, string joinField)
        {
            IJoinBuilder builder = Database.QueryFactory.CreateJoin();
            builder.Join(joinType, tableName, tableField, joinField);
            _joins.Add(builder);
        }

        public void Join(IJoinBuilder join)
        {
            if (!_joins.Contains(join))
            {
                _joins.Add(join);
            }
        }

        public IQueryBuilderSelect GetQueryCount()
        {
            IQueryBuilderSelect queryBuilder = new MsSqlQueryBuilderSelect(Database);
            queryBuilder.Select(new[] { "COUNT(*)" });
            queryBuilder.From(_from);
            foreach (IJoinBuilder join in _joins)
            {
                queryBuilder.Join(join);
            }
            queryBuilder.Where(BuildWhere());
            return queryBuilder;
        }

        public override string ToString()
        {
            return BuildQuery();
        }

        #endregion

        #region Private methods

        private string BuildQuery()
        {
            string joinString = BuildJoinTables();

            string whereString = BuildWhere();

            string limit = _limitBuilder.BuildLimit();

            string order = BuildOrder();

            string query = string.Format($"SELECT {limit} {_select} FROM {_from}");
            query += joinString + whereString + order;

            return query;
        }

        private string BuildJoinTables()
        {
            string joinString = string.Empty;
            foreach (IJoinBuilder join in _joins)
            {
                joinString += join + " ";
            }
            return joinString;
        }

        private string BuildOrder()
        {
            string order = string.Empty;
            if (_isOrder)
            {
                order = string.Format(" ORDER BY {0} {1}", _orderField,
                                      _orderType == OrderType.Asc ? "ASC" : "DESC");
            }
            return order;
        }

        private string BuildWhere()
        {
            string where = string.Empty;
            foreach (IQueryBuilderWhere builderWhere in _builderWheres)
            {
                where += builderWhere.ToString();
            }

            string whereString = string.Empty;
            if (!string.IsNullOrWhiteSpace(_customWhere))
            {
                whereString = _customWhere;
            }
            else if (!string.IsNullOrWhiteSpace(where))
            {
                whereString = string.Format("WHERE {0}", where);
            }
            return whereString;
        }

        #endregion
    }
}
