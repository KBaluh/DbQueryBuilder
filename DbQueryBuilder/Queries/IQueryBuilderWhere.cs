namespace DbQueryBuilder.Queries
{
    public interface IQueryBuilderWhere
    {
        IQueryBuilderWhere Where(string whereField, object whereValue);
        IQueryBuilderWhere Where(string whereField, string operand, object whereValue);

        IQueryBuilderWhere And(string whereField, object whereValue);
        IQueryBuilderWhere And(string whereField, string operand, object whereValue);
        IQueryBuilderWhere And(IQueryBuilderWhere where);

        IQueryBuilderWhere Or(string whereField, object whereValue);
        IQueryBuilderWhere Or(string whereField, string operand, object whereValue);
        IQueryBuilderWhere Or(IQueryBuilderWhere where);

        string ToString();
    }
}
