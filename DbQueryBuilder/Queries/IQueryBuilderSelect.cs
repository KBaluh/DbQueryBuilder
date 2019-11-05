namespace DbQueryBuilder.Queries
{
    public interface IQueryBuilderSelect
    {
        /// <summary>
        /// Columns for selecting.
        /// Example without alias: "Customer.Name"
        /// Example with alias: "Customer.Name AS 'CustomerName'"
        /// Example without table name: "Name"
        /// Example without table name with alias: "Name AS 'CustomerName'
        /// If you don't pass any arguments it will select all columns *
        /// </summary>
        /// <param name="columns"></param>
        void Select(string[] columns);

        /// <summary>
        /// Sql block FROM
        /// </summary>
        /// <param name="tableName"></param>
        void From(string tableName);

        /// <summary>
        /// Where with customer filterin condition
        /// </summary>
        /// <param name="customWhere">any SQL filter</param>
        void Where(string customWhere);

        /// <summary>
        /// Adding where builder into inner where filters
        /// </summary>
        /// <param name="where"></param>
        void Where(QueryBuilderWhere where);

        /// <summary>
        /// Adding condition of Where block
        /// </summary>
        /// <param name="whereField">Field for filtering</param>
        /// <param name="whereValue">Value</param>
        QueryBuilderWhere Where(string whereField, object whereValue);

        /// <summary>
        /// Adding condition of Where block with custom operand
        /// </summary>
        /// <param name="whereField">Field for filtering</param>
        /// <param name="operand">Operand</param>
        /// <param name="whereValue">Value</param>
        QueryBuilderWhere Where(string whereField, string operand, object whereValue);
    }
}
