using DbQueryBuilder.Queries;

namespace DbQueryBuilder.MsSql
{
    public sealed class MsSqlQueryQuotes : IQueryQuotes
    {
        public string GetFieldQuote() => "'";
        public string GetInsertFieldQuote() => "";
    }
}
