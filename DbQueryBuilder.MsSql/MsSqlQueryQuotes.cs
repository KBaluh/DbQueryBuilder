namespace DbQueryBuilder.MsSql
{
    internal sealed class MsSqlQueryQuotes : IQueryQuotes
    {
        public string FieldQuote => "'";
        public string InsertFieldQuote => "";
    }
}
