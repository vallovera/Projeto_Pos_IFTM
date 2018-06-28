using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Linq;

namespace BlueORM
{
    public class Objects
    {
        public static List<T> getAll<T>(DbCommand dbCmd, bool recursive)
        {
            List<T> list = new List<T>();

            List<object> list2 = getAll(typeof(T), DAO.getData(dbCmd), recursive);

            foreach (object obj in list2)
            {
                list.Add((T)obj);
            }

            return list;
        }

        public static List<T> getAll<T>(DbCommand dbCmd)
        {
            return getAll<T>(dbCmd, true);
        }

        public static List<T> getAll<T>(string query, bool recursive)
        {
            DataTable dt = DAO.getData(query);

            List<T> list = new List<T>();

            List<object> list2 = getAll(typeof(T), dt, recursive);

            foreach (object obj in list2)
            {
                list.Add((T)obj);
            }

            return list;
        }

        public static List<T> getAll<T>(string query)
        {
            return getAll<T>(query, true);
        }

        public static List<T> getAll<T>()
        {
            TableAttribute tableAttribute = Attributes.getTableAttribute(typeof(T));

            string query = "select * from " + tableAttribute.TableName;

            DataTable dt = DAO.getData(query);

            List<T> list = new List<T>();

            List<object> list2 = getAll(typeof(T), dt, true);

            foreach (object obj in list2)
            {
                list.Add((T)obj);
            }

            return list;
        }

        public static List<object> getAll(Type type, DataTable dt, bool recursive)
        {
            List<object> list = new List<object>();

            foreach (DataRow dr in dt.Rows)
            {
                object obj = Activator.CreateInstance(type);

                foreach (PropertyInfo prop in type.GetProperties())
                {
                    ColumnAttribute columnAttribute = Attributes.getColumnAttribute(prop);
                    if (columnAttribute != null)
                    {
                        if (dt.Columns.Contains(columnAttribute.ColumnName))
                        {
                            if (dr[columnAttribute.ColumnName] != DBNull.Value)
                            {
                                if (prop.PropertyType == typeof(Int32) || prop.PropertyType == typeof(Int32?))
                                    prop.SetValue(obj, Convert.ToInt32(dr[columnAttribute.ColumnName]), null);
                                else if (prop.PropertyType == typeof(string))
                                    prop.SetValue(obj, dr[columnAttribute.ColumnName].ToString(), null);
                                else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                                    prop.SetValue(obj, Convert.ToDateTime(dr[columnAttribute.ColumnName]), null);
                                else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                                    prop.SetValue(obj, Convert.ToBoolean(dr[columnAttribute.ColumnName]), null);
                                else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
                                    prop.SetValue(obj, Convert.ToDecimal(dr[columnAttribute.ColumnName]), null);
                                else if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
                                    prop.SetValue(obj, Convert.ToDouble(dr[columnAttribute.ColumnName]), null);
                                else if (prop.PropertyType == typeof(byte[]))
                                    prop.SetValue(obj, (byte[])dr[columnAttribute.ColumnName], null);
                                else if (prop.PropertyType.IsClass)
                                {
                                    if (recursive)
                                    {
                                        if (Attributes.getTableAttribute(prop.PropertyType) != null)
                                        {
                                            prop.SetValue(obj, getById(prop.PropertyType, Convert.ToInt32(dr[columnAttribute.ColumnName]), recursive), null);
                                        }
                                    }
                                    else
                                    {
                                        object objFK = Activator.CreateInstance(prop.PropertyType);
                                        TableAttribute tableAttribute = Attributes.getTableAttribute(prop.PropertyType);

                                        PropertyInfo propPK = prop.PropertyType.GetProperties().Where(x => Attributes.getColumnAttribute(x).ColumnName == tableAttribute.KeyColumn).FirstOrDefault();
                                        if (propPK != null)
                                        {
                                            propPK.SetValue(objFK, Convert.ToInt32(dr[columnAttribute.ColumnName]), null);
                                            prop.SetValue(obj, objFK, null);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public static T get<T>(DbCommand dbCmd, bool recursive)
        {
            List<T> list = new List<T>();

            List<object> list2 = getAll(typeof(T), DAO.getData(dbCmd), recursive);

            foreach (object obj in list2)
            {
                list.Add((T)obj);
            }

            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return default(T);
            }
        }

        private static object getById(Type type, int id, bool recursive)
        {
            TableAttribute tableAttribute = Attributes.getTableAttribute(type);

            DataTable dt = DAO.getData("select * from " + tableAttribute.TableName + " where " + tableAttribute.KeyColumn + " = " + id.ToString());

            if (dt.Rows.Count > 0)
            {
                object obj = Activator.CreateInstance(type);

                foreach (PropertyInfo prop in type.GetProperties())
                {
                    ColumnAttribute columnAttribute = Attributes.getColumnAttribute(prop);
                    if (columnAttribute != null)
                    {
                        if (dt.Columns.Contains(columnAttribute.ColumnName))
                        {
                            if (dt.Rows[0][columnAttribute.ColumnName] != DBNull.Value)
                            {
                                if (prop.PropertyType == typeof(Int32) || prop.PropertyType == typeof(Int32?))
                                    prop.SetValue(obj, Convert.ToInt32(dt.Rows[0][columnAttribute.ColumnName]), null);
                                else if (prop.PropertyType == typeof(string))
                                    prop.SetValue(obj, dt.Rows[0][columnAttribute.ColumnName].ToString(), null);
                                else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                                    prop.SetValue(obj, Convert.ToDateTime(dt.Rows[0][columnAttribute.ColumnName]), null);
                                else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                                    prop.SetValue(obj, Convert.ToBoolean(dt.Rows[0][columnAttribute.ColumnName]), null);
                                else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
                                    prop.SetValue(obj, Convert.ToDecimal(dt.Rows[0][columnAttribute.ColumnName]), null);
                                else if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
                                    prop.SetValue(obj, Convert.ToDouble(dt.Rows[0][columnAttribute.ColumnName]), null);
                                else if (prop.PropertyType == typeof(byte[]))
                                    prop.SetValue(obj, (byte[])dt.Rows[0][columnAttribute.ColumnName], null);
                                else if (prop.PropertyType.IsClass && recursive)
                                {
                                    if (Attributes.getTableAttribute(prop.PropertyType) != null)
                                    {
                                        prop.SetValue(obj, getById(prop.PropertyType, Convert.ToInt32(dt.Rows[0][columnAttribute.ColumnName]), recursive), null);
                                    }
                                }
                            }
                        }
                    }
                }

                return obj;
            }
            else
            {
                return null;
            }
        }

        public static T getById<T>(int id, bool recursive)
        {
            object obj = getById(typeof(T), id, recursive);

            if (obj != null)
                return (T)obj;
            else
                return default(T);
        }

        public static List<T> GetByObj<T>(object obj)
        {
            return getAll<T>(Factory.GetSelectCommand(obj), false);
        }
    }
}
