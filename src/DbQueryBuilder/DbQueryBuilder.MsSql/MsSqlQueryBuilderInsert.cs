using System.Collections.Generic;
using System.Data;

namespace DbQueryBuilder.MsSql
{
    public sealed class MsSqlQueryBuilderInsert : IQueryBuilderInsert
    {
        private string _tableName;

        private readonly MsSqlDatabase _dataBase;
        private readonly IDbCommand _command;

        private readonly Dictionary<string, object> _fieldValues = new Dictionary<string, object>();

        internal MsSqlQueryBuilderInsert(MsSqlDatabase dataBase)
        {
            _dataBase = dataBase;
            _command = dataBase.CreateCommand();
        }

        internal MsSqlQueryBuilderInsert(MsSqlDatabase dataBase, string tableName)
        {
            _dataBase = dataBase;
            _command = dataBase.CreateCommand();
            _tableName = tableName;
        }

        public void Add(string field, object value)
        {
            _command.Parameters.Add(CreateParameter(field, value));
            _fieldValues.Add(field, value);
        }

        public IDbCommand GetCommand()
        {
            string fieldString = "";
            string parameterString = "";

            string insertFieldQuote = _dataBase.QueryFactory.CreateQueryQuotes().InsertFieldQuote;

            // Формируем строки полей и параметров
            foreach (KeyValuePair<string, object> kvp in _fieldValues)
            {
                string field = kvp.Key;

                if (fieldString.Length > 1)
                {
                    fieldString += ", " + insertFieldQuote + field + insertFieldQuote;
                    parameterString += ", @" + field;
                }
                else
                {
                    fieldString += insertFieldQuote + field + insertFieldQuote;
                    parameterString += "@" + field;
                }
            }

            // Формирование полного запроса
            _command.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", _tableName, fieldString, parameterString);

            return _command;
        }

        public void Into(string tableName)
        {
            _tableName = tableName;
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
