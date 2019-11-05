namespace DbQueryBuilder
{
    public interface IQueryFactory
    {
        IQueryBuilderSelect CreateSelect();

        IQueryBuilderWhere CreateWhere();

        IJoinBuilder CreateJoin();

        ILimitBuilder CreateLimit();

        IQueryQuotes CreateQueryQuotes();
    }
}
