using System;
using System.Collections.Generic;
using System.Linq;

namespace DbQueryBuilder
{
    /// <summary>
    /// Query builder for block WHERE in SQL
    /// </summary>
    internal sealed class MsSqlQueryBuilderWhere : IQueryBuilderWhere
    {
        #region Private fields
        private readonly IQueryQuotes _quotes;

        private string _whereField;
        private object _whereValue;
        private string _whereOperand;

        private readonly List<string> _whereStrings = new List<string>();
        #endregion

        #region Ctor
        public MsSqlQueryBuilderWhere(IQueryQuotes quotes)
        {
            _quotes = quotes ?? throw new ArgumentNullException("quotes");
        }
        #endregion

        #region Public methods

        public IQueryBuilderWhere Where(string whereField, object whereValue)
        {
            _whereField = whereField;
            _whereValue = whereValue;
            _whereOperand = "=";
            return this;
        }

        public IQueryBuilderWhere Where(string whereField, string operand, object whereValue)
        {
            _whereField = whereField;
            _whereOperand = operand;
            _whereValue = whereValue;
            return this;
        }

        public IQueryBuilderWhere And(string whereField, object whereValue)
        {
            AppendString(whereField, "=", whereValue, "AND");
            return this;
        }

        public IQueryBuilderWhere And(string whereField, string operand, object whereValue)
        {
            AppendString(whereField, operand, whereValue, "AND");
            return this;
        }

        public IQueryBuilderWhere And(IQueryBuilderWhere where)
        {
            AppendWhere(where, "AND");
            return this;
        }

        public IQueryBuilderWhere Or(string whereField, object whereValue)
        {
            AppendString(whereField, "=", whereValue, "OR");
            return this;
        }

        public IQueryBuilderWhere Or(string whereField, string operand, object whereValue)
        {
            AppendString(whereField, operand, whereValue, "OR");
            return this;
        }

        public IQueryBuilderWhere Or(IQueryBuilderWhere where)
        {
            AppendWhere(where, "OR");
            return this;
        }

        public IQueryBuilderWhere Like(string whereField, object whereValue)
        {
            AppendLike(whereField, whereValue, string.Empty);
            return this;
        }

        public IQueryBuilderWhere LikeAnd(string whereField, object whereValue)
        {
            AppendLike(whereField, whereValue, "AND");
            return this;
        }

        public IQueryBuilderWhere LikeOr(string whereField, object whereValue)
        {
            AppendLike(whereField, whereValue, "OR");
            return this;
        }

        public IQueryBuilderWhere NotLike(string whereField, object whereValue)
        {
            AppendNotLike(whereField, whereValue, string.Empty);
            return this;
        }

        public IQueryBuilderWhere NotLikeAnd(string whereField, object whereValue)
        {
            AppendNotLike(whereField, whereValue, "AND");
            return this;
        }

        public IQueryBuilderWhere NotLikeOr(string whereField, object whereValue)
        {
            AppendNotLike(whereField, whereValue, "OR");
            return this;
        }

        public override string ToString()
        {
            string baseWhere = string.Empty;
            if (!string.IsNullOrEmpty(_whereField))
            {
                if (_whereValue == DBNull.Value)
                {
                    baseWhere = string.Format(" (ISNULL({0}))",
                                              _whereField);
                }
                else
                {
                    baseWhere = string.Format(" ({0} {1} {3}{2}{3})",
                                              _whereField,
                                              _whereOperand,
                                              _whereValue,
                                              _quotes.FieldQuote);
                }
            }

            string whereJoins = _whereStrings.Aggregate(string.Empty,
                (current, whereString) => current + whereString);

            return string.Format("{0} {1}", baseWhere, whereJoins);
        }

        #endregion

        #region Private methods

        private void AppendString(string whereField, string operand,
            object whereValue, string joinOperation)
        {
            if (string.IsNullOrEmpty(whereField))
            {
                return;
            }
            string where;
            if (whereValue == DBNull.Value)
            {
                where = string.Format(" {0} (ISNULL({1}))",
                                      joinOperation, whereField);
            }
            else
            {
                where = string.Format(" {0} ({1} {2} {4}{3}{4})",
                              joinOperation, whereField, operand, whereValue, _quotes.FieldQuote);
            }
            _whereStrings.Add(where);
        }

        private void AppendWhere(IQueryBuilderWhere whereQuery, string joinOperation)
        {
            if (string.IsNullOrWhiteSpace(whereQuery.ToString()))
            {
                return;
            }

            string where = string.Format(" {0} ({1})", joinOperation, whereQuery);
            _whereStrings.Add(where);
        }

        private void AppendLike(string whereField, object whereValue, string joinOperation, string likeCondition = "LIKE")
        {
            string formatValue = string.Format("'%{0}%'", whereValue);
            string like = string.Format(" {0} ({1} {2} {3}) ",
                joinOperation, whereField, likeCondition, formatValue);
            _whereStrings.Add(like);
        }

        private void AppendNotLike(string whereField, object whereValue, string joinOperation)
        {
            AppendLike(whereField, whereValue, joinOperation, "NOT LIKE");
        }

        #endregion
    }
}
