using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;

namespace BlueORM
{
    public class Query
    {
        private StringBuilder strQuery;
        private StringBuilder strBasic;
        private StringBuilder strRelations;
        private StringBuilder strFilters;
        private StringBuilder strOrder;
        private DataTable dtClasses;
        private int index;

        public enum FilterTypes
        {
            Equal,
            NotEqual,
            GreaterOrEqual,
            Greater,
            LessOrEqual,
            Less
        }


        public Query(Type type)
        {
            dtClasses = new DataTable("Classes");
            dtClasses.Columns.Add(new DataColumn("ClassName", typeof(String)));
            dtClasses.Columns.Add(new DataColumn("TableName", typeof(String)));
            dtClasses.Columns.Add(new DataColumn("TableAlias", typeof(String)));
            dtClasses.Columns.Add(new DataColumn("KeyColumn", typeof(String)));
            dtClasses.Columns.Add(new DataColumn("ClassType", typeof(Type)));

            index = 1;

            strQuery = new StringBuilder();
            strBasic = new StringBuilder();
            strRelations = new StringBuilder();
            strFilters = new StringBuilder();
            strOrder = new StringBuilder();

            AddClass(type);

            strBasic.AppendLine("select t1.* from " + getTableName(type.Name));

        }

        private void AddClass(Type type)
        {
            TableAttribute ta = Attributes.getTableAttribute(type);

            DataRow dr = dtClasses.NewRow();
            dr["ClassName"] = type.Name;
            dr["TableName"] = ta.TableName;
            dr["TableAlias"] = "t" + index.ToString();
            dr["KeyColumn"] = ta.KeyColumn;
            dr["ClassType"] = type;
            dtClasses.Rows.Add(dr);

            index++;
        }

        private string getTableName(string className)
        {
            DataRow[] dr = dtClasses.Select("ClassName = '" + className + "'");

            if (dr.Length > 0)
                return dr[0]["TableName"].ToString();
            else
                return null;
        }

        private string getTableAlias(string className)
        {
            DataRow[] dr = dtClasses.Select("ClassName = '" + className + "'");

            if (dr.Length > 0)
                return dr[0]["TableAlias"].ToString();
            else
                return null;
        }

        private string getKeyColumn(string className)
        {
            DataRow[] dr = dtClasses.Select("ClassName = '" + className + "'");

            if (dr.Length > 0)
                return dr[0]["KeyColumn"].ToString();
            else
                return null;
        }

        private Type getClassType(string className)
        {
            DataRow[] dr = dtClasses.Select("ClassName = '" + className + "'");

            if (dr.Length > 0)
                return (Type)dr[0]["ClassType"];
            else
                return null;
        }

        private string getColumn(string property)
        {
            int indexOfDot = property.IndexOf(".");

            string className = property.Substring(0, indexOfDot);
            string propertyName = property.Substring(indexOfDot + 1, property.Length - indexOfDot - 1);

            string tableAlias = getTableAlias(className);

            Type type = getClassType(className);

            ColumnAttribute ca = Attributes.getColumnAttribute(type.GetProperty(propertyName));

            return tableAlias + "." + ca.ColumnName;
        }

        public void Join<T>(string relatedProperty)
        {
            Type type = typeof(T);

            AddClass(type);

            strRelations.AppendLine("join " + getTableName(type.Name) + " as " + getTableAlias(type.Name));
            strRelations.AppendLine("   on " + getTableAlias(type.Name) + "." + getKeyColumn(type.Name) + " = " + getColumn(relatedProperty));

        }

        public void Filter(FilterTypes filterType, string valueOrProp, string valueOrProp2)
        {
            string signal = "";

            switch (filterType)
            {
                case FilterTypes.Equal:
                    signal = " = ";
                    break;
                case FilterTypes.NotEqual:
                    signal = " != ";
                    break;
                case FilterTypes.GreaterOrEqual:
                    signal = " >= ";
                    break;
                case FilterTypes.Greater:
                    signal = " > ";
                    break;
                case FilterTypes.LessOrEqual:
                    signal = " <= ";
                    break;
                case FilterTypes.Less:
                    signal = " < ";
                    break;
            }

            if (valueOrProp.IndexOf("'") == -1)
            { valueOrProp = getColumn(valueOrProp); }

            if (valueOrProp2.IndexOf("'") == -1)
            { valueOrProp2 = getColumn(valueOrProp2); }
            
            if (strFilters.Length == 0)
                strFilters.AppendLine("where " + valueOrProp + signal + valueOrProp2);
            else
                strFilters.AppendLine("and " + valueOrProp + signal + valueOrProp2);
        }

        public void FilterIn(string property, string[] arrValues, bool negative)
        {
            string signal = "";

            if (negative)
                signal = " not in ";
            else
                signal = " in ";
            
            StringBuilder strValues = new StringBuilder();
            
            foreach (string str in arrValues)
            {
                strValues.Append("'" + str + "', ");
            }

            strValues.Remove(strValues.Length - 2, 2);

            if (strFilters.Length == 0)
                strFilters.AppendLine("where " + getColumn(property) + signal + "(" + strValues.ToString() + ")");
            else
                strFilters.AppendLine("and " + getColumn(property) + signal + "(" + strValues.ToString() + ")");
        }

        public void FilterIn(string property, int[] arrValues, bool negative)
        {
            string signal = "";

            if (negative)
                signal = " not in ";
            else
                signal = " in ";

            StringBuilder strValues = new StringBuilder();

            foreach (int num in arrValues)
            {
                strValues.Append(num.ToString() + ", ");
            }

            strValues.Remove(strValues.Length - 2, 2);

            if (strFilters.Length == 0)
                strFilters.AppendLine("where " + getColumn(property) + signal + "(" + strValues.ToString() + ")");
            else
                strFilters.AppendLine("and " + getColumn(property) + signal + "(" + strValues.ToString() + ")");
        }

        public void FilterBetween(string property, int valueIni, int valueFim)
        {
            if (strFilters.Length == 0)
                strFilters.AppendLine("where " + getColumn(property) + " between " + valueIni.ToString() + " and " + valueFim.ToString());
            else
                strFilters.AppendLine("and " + getColumn(property) + " between " + valueIni.ToString() + " and " + valueFim.ToString());
        }

        public void FilterBetween(string property, double valueIni, double valueFim)
        {
            string strValueIni = valueIni.ToString().Replace(",", ".");
            string strValueFim = valueFim.ToString().Replace(",", ".");
            
            if (strFilters.Length == 0)
                strFilters.AppendLine("where " + getColumn(property) + " between " + strValueIni + " and " + strValueFim);
            else
                strFilters.AppendLine("and " + getColumn(property) + " between " + strValueIni + " and " + strValueFim);
        }

        public void FilterBetween(string property, DateTime valueIni, DateTime valueFim)
        {
            string strValueIni = valueIni.ToString("yyyyMMdd HH:mm:ss");
            string strValueFim = valueFim.ToString("yyyyMMdd HH:mm:ss");

            if (strFilters.Length == 0)
                strFilters.AppendLine("where " + getColumn(property) + " between '" + strValueIni + "' and '" + strValueFim + "'");
            else
                strFilters.AppendLine("and " + getColumn(property) + " between '" + strValueIni + "' and '" + strValueFim + "'");
        }

        public void OrderBy(string property, string order)
        {
            if (strOrder.Length == 0)
                strOrder.Append("order by " + getColumn(property) + " " + order.ToLower());
            else
                strOrder.Append(", " + getColumn(property) + " " + order.ToLower());
        }

        public string ToString()
        {
            return strBasic.ToString() + strRelations.ToString() + strFilters.ToString() + strOrder.ToString();
        }

    }
}
