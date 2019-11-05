using System.Data;

namespace DbQueryBuilder
{
    public interface IQueryBuilderInsert
    {
        /// <summary>
        /// Build block INTO
        /// </summary>
        /// <param name="tableName"></param>
        void Into(string tableName);

        /// <summary>
        /// Adding into field value and encapsulate it in parameter
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        void Add(string field, object value);

        /// <summary>
        /// Command for insert specific for concrete database
        /// </summary>
        /// <returns></returns>
        IDbCommand GetCommand();
    }
}
