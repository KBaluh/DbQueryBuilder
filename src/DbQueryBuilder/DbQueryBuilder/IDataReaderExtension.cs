using System;
using System.Data;

namespace DbQueryBuilder
{
    public static class IDataReaderExtension
    {
        public static Guid GetGuid(this IDataReader dataReader, string name)
        {
            object value = dataReader[name];
            if (value == DBNull.Value)
            {
                return Guid.Empty;
            }

            string strValue = value.ToString();
            if (string.IsNullOrEmpty(strValue))
            {
                return Guid.Empty;
            }

            return new Guid(strValue);
        }

        public static DateTime? GetDateTime(this IDataReader dataReader, string name)
        {
            object value = dataReader[name];
            if (value == DBNull.Value)
            {
                return null;
            }
            return (DateTime)value;
        }

        public static int GetInt32(this IDataReader dataReader, string name)
        {
            object value = dataReader[name];
            if (value == DBNull.Value)
            {
                return 0;
            }
            int.TryParse(value.ToString(), out int iValue);
            return iValue;
        }

        public static long GetInt64(this IDataReader dataReader, string name)
        {
            object value = dataReader[name];
            if (value == DBNull.Value)
            {
                return 0L;
            }
            long.TryParse(value.ToString(), out long lValue);
            return lValue;
        }

        public static float GetFloat(this IDataReader dataReader, string name)
        {
            object value = dataReader[name];
            if (value == DBNull.Value)
            {
                return 0f;
            }
            float.TryParse(value.ToString(), out float fValue);
            return fValue;
        }

        public static decimal GetDecimal(this IDataReader dataReader, string name)
        {
            object value = dataReader[name];
            if (value == DBNull.Value)
            {
                return 0m;
            }
            decimal.TryParse(value.ToString(), out decimal dValue);
            return dValue;
        }

        public static string GetString(this IDataReader dataReader, string name)
        {
            string value = Convert.ToString(dataReader[name]);
            return value;
        }

        public static bool GetBoolean(this IDataReader dataReader, string name)
        {
            object value = dataReader[name];
            if (value == DBNull.Value)
            {
                return false;
            }
            return Convert.ToBoolean(value);
        }

        public static bool IsDBNull(this IDataReader dataReader, string name)
        {
            object value = dataReader[name];
            return value == null || value == DBNull.Value;
        }
    }
}
