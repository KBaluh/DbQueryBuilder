using System;

namespace DbQueryBuilder.Queries
{
    /// <summary>
    /// Query builder for block WHERE in SQL
    /// </summary>
    public class QueryBuilderWhere
    {
        private readonly IQueryQuotes _quotes;

        public QueryBuilderWhere(IQueryQuotes quotes)
        {
            _quotes = quotes ?? throw new ArgumentNullException("quotes");
        }
    }
}
