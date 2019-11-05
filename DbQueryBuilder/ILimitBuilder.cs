namespace DbQueryBuilder
{
    public interface ILimitBuilder
    {
        void Limit(int limit);
        void Limit(int from, int to);
        string BuildLimit();
    }
}
