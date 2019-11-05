namespace DbQueryBuilder.Databases
{
    /// <summary>
    /// Database query executer
    /// </summary>
    public abstract class Database : IDatabase
    {
        #region Properties

        public DbType DatabaseType { get; }

        #endregion

        #region Ctor

        protected Database(DbType dbType)
        {
            DatabaseType = dbType;
        }

        #endregion

        #region Implementation of IDatabase

        public abstract void Connnect();

        public abstract void Disconnect();

        #endregion
    }
}
