using System;
using System.Collections.Generic;
using System.Text;

namespace BlueORM
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        private string tableName;
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private string keyColumn;
        public string KeyColumn
        {
            get { return keyColumn; }
            set { keyColumn = value; }
        }

        public TableAttribute(string tableName, string keyColumn)
        {
            this.tableName = tableName;
            this.keyColumn = keyColumn;
        }
    }
}
