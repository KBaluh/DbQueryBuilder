namespace DbQueryBuilder.Queries
{
    public interface IJoinBuilder
    {
        void Join(SelectJoinType joinType, string tableName, string tableField, string joinField);
    }
}
