using System;

namespace DbQueryBuilder.MsSql
{
    public class MsSqlJoinBuilder : IJoinBuilder
    {
        private SelectJoinType _joinType;

        private string _tableName;
        private string _tableField;
        private string _joinField;

        public void Join(SelectJoinType joinType, string tableName, string tableField, string joinField)
        {
            _joinType = joinType;
            _tableName = tableName;
            _tableField = tableField;
            _joinField = joinField;
        }

        public override string ToString()
        {
            string join;
            switch (_joinType)
            {
                case SelectJoinType.LeftOuterJoin:
                    join = "LEFT OUTER JOIN";
                    break;
                case SelectJoinType.InnerJoin:
                    join = "INNER JOIN";
                    break;
                default:
                    throw new Exception("Unknown join type");
            }

            // LEFT OUTER JOINT tbl_TestJoinTable ON tbl_TestFromTable.TestJoinID = tbl_TestJoinTable.ID
            string joinString = string.Format("{0} {1} ON {2} = {3}",
                join, _tableName, _tableField, _joinField);
            return joinString;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return obj.ToString().Equals(ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
