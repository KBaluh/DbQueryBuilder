namespace DbQueryBuilder
{
    /// <summary>
    /// Database query executer
    /// </summary>
    public interface IDatabase
    {
        IQueryFactory QueryFactory { get; }

        void Connnect();
        void Disconnect();
    }
}
