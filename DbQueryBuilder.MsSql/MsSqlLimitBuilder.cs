namespace DbQueryBuilder.Queries
{
    public class MsSqlLimitBuilder : ILimitBuilder
    {
        #region Public properties

        public int From { get; private set; }
        public int To { get; private set; }
        public bool SingleLimit { get; private set; }

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

            if (SingleLimit)
            {
                limit = string.Format(" TOP {0} ", From);
            }
            else
            {
                limit = string.Format(" TOP {0},{1} ", From, To);
            }

            return limit;
        }

        #endregion
    }
}
