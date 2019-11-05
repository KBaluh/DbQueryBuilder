namespace DbQueryBuilder
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

        IQueryBuilderWhere Like(string whereField, object whereValue);
        IQueryBuilderWhere LikeAnd(string whereField, object whereValue);
        IQueryBuilderWhere LikeOr(string whereField, object whereValue);

        IQueryBuilderWhere NotLike(string whereField, object whereValue);
        IQueryBuilderWhere NotLikeAnd(string whereField, object whereValue);
        IQueryBuilderWhere NotLikeOr(string whereField, object whereValue);

        string ToString();
    }
}
