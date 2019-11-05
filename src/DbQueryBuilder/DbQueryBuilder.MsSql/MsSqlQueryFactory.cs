using DbQueryBuilder.Queries;

namespace DbQueryBuilder.MsSql
{
    public class MsSqlQueryFactory : IQueryFactory
    {
        private readonly MsSqlDatabase _database;

        public MsSqlQueryFactory(MsSqlDatabase database)
        {
            _database = database;
        }

        public IQueryBuilderSelect CreateSelect()
        {
            return new MsSqlQueryBuilderSelect(_database);
        }

        public IQueryBuilderInsert CreateInsert()
        {
            return new MsSqlQueryBuilderInsert(_database);
        }

        public IQueryBuilderUpdate CreateUpdate()
        {
            return new MsSqlQueryBuilderUpdate(_database);
        }

        public IQueryBuilderDelete CreateDelete()
        {
            return new MsSqlQueryBuilderDelete(_database);
        }

        public IQueryBuilderWhere CreateWhere()
        {
            var quotes = CreateQueryQuotes();
            return new MsSqlQueryBuilderWhere(quotes);
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
    }
}
