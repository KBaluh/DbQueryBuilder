namespace DbQueryBuilder.MsSql
{
    public sealed class MsSqlQueryQuotes : IQueryQuotes
    {
        public string FieldQuote => "'";
        public string InsertFieldQuote => "";
    }
}
