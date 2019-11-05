namespace DbQueryBuilder.Queries
{
    public interface IQueryQuotes
    {
        /// <summary>
        /// Each database has own quotes for insert e.g. " or '
        /// </summary>
        /// <returns></returns>
        string GetInsertFieldQuote();

        /// <summary>
        /// Each database has own quetes for field incapsulations e.g. " or '
        /// </summary>
        /// <returns></returns>
        string GetFieldQuote();
    }
}
