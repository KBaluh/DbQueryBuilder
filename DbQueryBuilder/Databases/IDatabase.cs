namespace DbQueryBuilder.Databases
{
    /// <summary>
    /// Database query executer
    /// </summary>
    public interface IDatabase
    {
        void Connnect();
        void Disconnect();
    }
}
