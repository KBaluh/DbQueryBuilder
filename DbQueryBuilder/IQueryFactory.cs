namespace DbQueryBuilder
{
    public interface IQueryFactory
    {
        IQueryBuilderSelect CreateSelect();

        IQueryBuilderInsert CreateInsert();

        IQueryBuilderUpdate CreateUpdate();

        IQueryBuilderDelete CreateDelete();

        IQueryBuilderWhere CreateWhere();

        IJoinBuilder CreateJoin();

        ILimitBuilder CreateLimit();

        IQueryQuotes CreateQueryQuotes();
    }
}
