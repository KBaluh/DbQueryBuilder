namespace DbQueryBuilder.Queries
{
    public abstract class LimitBuilder : ILimitBuilder
    {
        #region Public properties

        public int From { get; private set; }
        public int To { get; private set; }
        public bool SingleLimit { get; private set; }
        public LimitDbType LimitType { get; private set; }

        #endregion

        #region Ctor

        protected LimitBuilder(LimitDbType limitDbType)
        {
            LimitType = limitDbType;
        }

        #endregion

        #region Public methods

        public void Limit(int limit)
        {
            From = limit;
            To = 0;
            SingleLimit = true;
        }

        public void Limit(int from, int to)
        {
            From = from;
            To = to;
            SingleLimit = false;
        }

        /// <summary>
        /// Building block LIMIT
        /// </summary>
        /// <returns></returns>
        public string BuildLimit()
        {
            string limit = string.Empty;
            if (From == 0 && To == 0)
            {
                return limit;
            }

            switch (LimitType)
            {
                case LimitDbType.MySql:
                    if (SingleLimit)
                    {
                        limit = string.Format(" LIMIT {0} ", From);
                    }
                    else
                    {
                        limit = string.Format(" LIMIT {0},{1} ", From, To);
                    }
                    break;

                case LimitDbType.MsSql:
                    if (SingleLimit)
                    {
                        limit = string.Format(" TOP {0} ", From);
                    }
                    else
                    {
                        limit = string.Format(" TOP {0},{1} ", From, To);
                    }
                    break;
                case LimitDbType.Firebird:
                    break;
                default:
                    limit = string.Empty;
                    break;
            }
            return limit;
        }

        #endregion
    }
}
