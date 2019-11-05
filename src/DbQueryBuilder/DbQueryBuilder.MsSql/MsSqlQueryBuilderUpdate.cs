using System.Collections.Generic;
using System.Data;

namespace DbQueryBuilder.MsSql
{
    internal class MsSqlQueryBuilderUpdate : IQueryBuilderUpdate
    {
        #region Fields

        private const string WhereParameterName = "@Val";

        private readonly MsSqlDatabase _dataBase;

        private string _tableName;
        private string _whereField;
        private string _customWhere;

        private readonly IDbCommand _command;

        private readonly Dictionary<string, object> _fieldValues = new Dictionary<string, object>();

        #endregion

        public MsSqlQueryBuilderUpdate(MsSqlDatabase dataBase)
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

        public void Where(string tableName, string customWhere)
        {
            _tableName = tableName;
            _customWhere = customWhere;
        }

        public void Add(string field, object value)
        {
            _command.Parameters.Add(CreateParameter(field, value));
            _fieldValues.Add(field, value);
        }

        public IDbCommand GetCommand()
        {
            string setString = "";

            foreach (KeyValuePair<string, object> kvp in _fieldValues)
            {
                string field = kvp.Key;

                if (setString.Length > 1)
                {
                    setString += ", " + field + " = @" + field;
                }
                else
                {
                    setString += field + " = @" + field;
                }
            }

            string whereString;
            if (string.IsNullOrEmpty(_customWhere))
            {
                whereString = _tableName + "." + _whereField + " = " + WhereParameterName;
            }
            else
            {
                whereString = _customWhere;
            }

            _command.CommandText = string.Format($"UPDATE {_tableName} SET {setString} WHERE {whereString}");

            return _command;
        }

        /// <summary>
        /// Создание параметра базы данных
        /// </summary>
        /// <param name="parameterName">Имя параметра. Вводить без знака @</param>
        /// <param name="value">Значение параметра</param>
        /// <returns></returns>
        private IDataParameter CreateParameter(string parameterName, object value)
        {
            IDataParameter parameter = _dataBase.CreateParameter();
            parameter.ParameterName = "@" + parameterName;
            parameter.Value = value;
            return parameter;
        }
    }
}
