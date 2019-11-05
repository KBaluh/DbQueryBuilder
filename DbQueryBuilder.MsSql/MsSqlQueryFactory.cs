using DbQueryBuilder.Queries;

namespace DbQueryBuilder.MsSql
{
    internal sealed class MsSqlQueryFactory : IQueryFactory
    {
        private readonly MsSqlDatabase _database;

        public MsSqlQueryFactory(MsSqlDatabase database)
        {
            _database = database;
        }

        public IJoinBuilder CreateJoin()
        {
            return new MsSqlJoinBuilder();
        }

        public ILimitBuilder CreateLimit()
        {
            return new MsSqlLimitBuilder();
        }

        public IQueryQuotes CreateQueryQuotes()
        {
            return new MsSqlQueryQuotes();
        }

        public IQueryBuilderSelect CreateSelect()
        {
            return new MsSqlQueryBuilderSelect(_database);
        }

        public IQueryBuilderWhere CreateWhere()
        {
            var quotes = CreateQueryQuotes();
            return new MsSqlQueryBuilderWhere(quotes);
        }
    }
}
