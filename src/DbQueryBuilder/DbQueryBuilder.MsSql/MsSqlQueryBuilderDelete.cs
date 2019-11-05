using System.Data;

namespace DbQueryBuilder.MsSql
{
    internal sealed class MsSqlQueryBuilderDelete : IQueryBuilderDelete
    {
        #region Fields

        private const string WhereParameterName = "@Val";

        private readonly MsSqlDatabase _dataBase;

        private string _tableName;
        private string _whereField;
        private string _where;

        private readonly IDbCommand _command;

        #endregion

        internal MsSqlQueryBuilderDelete(MsSqlDatabase dataBase)
        {
            _dataBase = dataBase;
            _command = dataBase.CreateCommand();
        }

        public void Where(string tableName, string whereField, object whereValue)
        {
            _tableName = tableName;
            _whereField = whereField;
            _command.Parameters.Add(CreateParameter("Val", whereValue));
        }

        public void Where(string tableName, string where)
        {
            _tableName = tableName;
            _where = where;
        }

        public IDbCommand GetCommand()
        {
            string whereString = _tableName + "." + _whereField + " = " + WhereParameterName;

            if (string.IsNullOrEmpty(_where))
            {
                // Формирование полного запроса
                _command.CommandText = string.Format("DELETE FROM {0} WHERE {1}", _tableName, whereString);
            }
            else
            {
                _command.CommandText = string.Format("DELETE FROM {0} WHERE {1}", _tableName, _where);
            }

            return _command;
        }

        private IDataParameter CreateParameter(string parameterName, object value)
        {
            IDataParameter parameter = _dataBase.CreateParameter();
            parameter.ParameterName = "@" + parameterName;
            parameter.Value = value;
            return parameter;
        }
    }
}
