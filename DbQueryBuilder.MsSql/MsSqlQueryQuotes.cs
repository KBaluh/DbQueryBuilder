using DbQueryBuilder.Queries;

namespace DbQueryBuilder.MsSql
{
    public class MsSqlQueryQuotes : IQueryQuotes
    {
        public string GetFieldQuote() => "'";
        public string GetInsertFieldQuote() => "";
    }
}
