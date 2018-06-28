using System;
using System.Collections.Generic;
using System.Text;

namespace BlueORM
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        private string columnName;
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        private bool identity;
        public bool Identity
        {
            get { return identity; }
            set { identity = value; }
        }

        private bool ignore;
        public bool Ignore
        {
            get { return ignore; }
            set { ignore = value; }
        }

        public ColumnAttribute(string columnName, bool identity)
        {
            this.columnName = columnName;
            this.identity = identity;
            this.ignore = false;
        }

        public ColumnAttribute(string columnName, bool identity, bool ignore)
        {
            this.columnName = columnName;
            this.identity = identity;
            this.ignore = ignore;
        }
    }
}
