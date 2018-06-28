using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace BlueORM
{
    public class Attributes
    {
        public static TableAttribute getTableAttribute(Type type)
        {
            object[] objArr = type.GetCustomAttributes(typeof(TableAttribute), true);
            if (objArr.Length > 0)
                return (TableAttribute)objArr[0];
            else
                return null;
        }

        public static ColumnAttribute getColumnAttribute(PropertyInfo prop)
        {
            object[] objArr = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
            if (objArr.Length > 0)
                return (ColumnAttribute)objArr[0];
            else
                return null;
        }

        public static object GetDefaultValue(PropertyInfo prop)
        {
            object[] objArr = prop.GetCustomAttributes(typeof(DefaultValueAttribute), true);
            if (objArr.Length > 0)
            {
                DefaultValueAttribute att = (DefaultValueAttribute)objArr[0];
                return att.Value;
            }

            if (prop.PropertyType.IsValueType)
                return Activator.CreateInstance(prop.PropertyType);

            return null;
        }
    }
}
