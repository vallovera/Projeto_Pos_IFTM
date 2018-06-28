using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace BlueORM
{
    public class Factory
    {
        public static DbCommand getInsertCommand(object obj)
        {
            return getInsertCommand(obj, true);
        }

        public static DbCommand getInsertCommand(object obj, bool returnMax)
        {
            Type type = obj.GetType();

            DbCommand dbCmd = DAO.getCommand();

            TableAttribute tableAttribute = Attributes.getTableAttribute(type);

            StringBuilder strQuery = new StringBuilder("insert into {0} ({1}) values ({2});");
            StringBuilder strColumns = new StringBuilder();
            StringBuilder strParams = new StringBuilder();

            bool identity = false;

            foreach (PropertyInfo prop in type.GetProperties())
            {
                ColumnAttribute columnAttribute = Attributes.getColumnAttribute(prop);
                if (columnAttribute != null)
                {
                    if (!columnAttribute.Identity && !columnAttribute.Ignore)
                    {
                        strColumns.Append(columnAttribute.ColumnName + ", ");
                        strParams.Append("@" + columnAttribute.ColumnName + ", ");

                        DbParameter dbParam = DAO.getParameter("@" + columnAttribute.ColumnName);

                        if (prop.GetValue(obj, null) != null)
                        {
                            if (prop.PropertyType == typeof(Int32) || prop.PropertyType == typeof(Int32?))
                            {
                                dbParam.DbType = DbType.Int32;
                                dbParam.Value = Convert.ToInt32(prop.GetValue(obj, null));
                            }
                            else if (prop.PropertyType == typeof(string))
                            {
                                dbParam.DbType = DbType.String;
                                dbParam.Value = prop.GetValue(obj, null).ToString();
                            }
                            else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                            {
                                dbParam.DbType = DbType.Boolean;
                                dbParam.Value = Convert.ToBoolean(prop.GetValue(obj, null));
                            }
                            else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                            {
                                dbParam.DbType = DbType.DateTime;
                                dbParam.Value = Convert.ToDateTime(prop.GetValue(obj, null));
                            }
                            else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
                            {
                                dbParam.DbType = DbType.Decimal;
                                dbParam.Value = Convert.ToDecimal(prop.GetValue(obj, null));
                            }
                            else if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
                            {
                                dbParam.DbType = DbType.Double;
                                dbParam.Value = Convert.ToDouble(prop.GetValue(obj, null));
                            }
                            else if (prop.PropertyType == typeof(byte[]))
                            {
                                dbParam.DbType = DbType.Binary;
                                dbParam.Value = (byte[])prop.GetValue(obj, null);
                            }
                            else if (prop.PropertyType.IsClass)
                            {
                                if (Attributes.getTableAttribute(prop.PropertyType) != null)
                                {
                                    dbParam.DbType = DbType.Int32;

                                    object obj2 = prop.GetValue(obj, null);

                                    foreach (PropertyInfo prop2 in prop.PropertyType.GetProperties())
                                    {
                                        ColumnAttribute columnAttribute2 = Attributes.getColumnAttribute(prop2);
                                        if (columnAttribute2 != null)
                                        {
                                            if (columnAttribute2.Identity)
                                            {
                                                dbParam.Value = Convert.ToInt32(prop2.GetValue(obj2, null));
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            dbParam.DbType = DbType.String;
                            dbParam.Value = DBNull.Value;
                        }

                        dbCmd.Parameters.Add(dbParam);
                    }
                    else
                    {
                        if (columnAttribute.Identity)
                            identity = true;
                    }
                }
            }

            strColumns.Remove(strColumns.Length - 2, 2);
            strParams.Remove(strParams.Length - 2, 2);

            strQuery.Replace("{0}", tableAttribute.TableName);
            strQuery.Replace("{1}", strColumns.ToString());
            strQuery.Replace("{2}", strParams.ToString());

            if (tableAttribute.KeyColumn != "" && returnMax)
            {
                if (identity)
                {
                    strQuery.Append("select max({3}) from {4}");
                    strQuery.Replace("{3}", tableAttribute.KeyColumn);
                    strQuery.Replace("{4}", tableAttribute.TableName);
                }
            }

            dbCmd.CommandText = strQuery.ToString();

            return dbCmd;
        }

        public static DbCommand getUpdateCommand(object obj)
        {
            Type type = obj.GetType();

            DbCommand dbCmd = DAO.getCommand();

            TableAttribute tableAttribute = Attributes.getTableAttribute(type);

            StringBuilder strQuery = new StringBuilder("update {0} set {1} where {2} = {3}");
            StringBuilder strColumns = new StringBuilder();

            foreach (PropertyInfo prop in type.GetProperties())
            {
                ColumnAttribute columnAttribute = Attributes.getColumnAttribute(prop);
                if (columnAttribute != null)
                {
                    if (!columnAttribute.Identity && !columnAttribute.Ignore)
                    {
                        strColumns.Append(columnAttribute.ColumnName + " = @" + columnAttribute.ColumnName + ", ");
                    }

                    DbParameter dbParam = DAO.getParameter("@" + columnAttribute.ColumnName);

                    if (prop.PropertyType == typeof(Int32) || prop.PropertyType == typeof(Int32?))
                    {
                        dbParam.DbType = DbType.Int32;
                        if (prop.GetValue(obj, null) != null)
                            dbParam.Value = Convert.ToInt32(prop.GetValue(obj, null));
                        else
                            dbParam.Value = DBNull.Value;
                    }
                    else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                    {
                        dbParam.DbType = DbType.Int32;
                        if (prop.GetValue(obj, null) != null)
                            dbParam.Value = Convert.ToInt32(prop.GetValue(obj, null));
                        else
                            dbParam.Value = DBNull.Value;
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        dbParam.DbType = DbType.String;
                        if (prop.GetValue(obj, null) != null)
                        { dbParam.Value = prop.GetValue(obj, null).ToString(); }
                        else { dbParam.Value = DBNull.Value; }
                    }
                    else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                    {
                        dbParam.DbType = DbType.Boolean;
                        if (prop.GetValue(obj, null) != null)
                        { dbParam.Value = Convert.ToBoolean(prop.GetValue(obj, null)); }
                        else { dbParam.Value = DBNull.Value; }
                    }
                    else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                    {
                        dbParam.DbType = DbType.DateTime;
                        if (prop.GetValue(obj, null) != null)
                        { dbParam.Value = Convert.ToDateTime(prop.GetValue(obj, null)); }
                        else { dbParam.Value = DBNull.Value; }
                    }
                    else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
                    {
                        dbParam.DbType = DbType.Decimal;
                        if (prop.GetValue(obj, null) != null)
                        { dbParam.Value = Convert.ToDecimal(prop.GetValue(obj, null)); }
                        else { dbParam.Value = DBNull.Value; }
                    }
                    else if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
                    {
                        dbParam.DbType = DbType.Double;
                        if (prop.GetValue(obj, null) != null)
                        { dbParam.Value = Convert.ToDouble(prop.GetValue(obj, null)); }
                        else { dbParam.Value = DBNull.Value; }
                    }
                    else if (prop.PropertyType == typeof(byte[]))
                    {
                        dbParam.DbType = DbType.Binary;
                        dbParam.Value = (byte[])prop.GetValue(obj, null);
                    }
                    else if (prop.PropertyType.IsClass)
                    {
                        if (Attributes.getTableAttribute(prop.PropertyType) != null)
                        {
                            dbParam.DbType = DbType.Int32;

                            object obj2 = prop.GetValue(obj, null);

                            if (obj2 != null)
                            {
                                foreach (PropertyInfo prop2 in prop.PropertyType.GetProperties())
                                {
                                    ColumnAttribute columnAttribute2 = Attributes.getColumnAttribute(prop2);
                                    if (columnAttribute2 != null)
                                    {
                                        if (columnAttribute2.Identity)
                                        {
                                            dbParam.Value = Convert.ToInt32(prop2.GetValue(obj2, null));
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                dbParam.Value = DBNull.Value;
                            }
                        }
                    }

                    dbCmd.Parameters.Add(dbParam);
                }
            }

            strColumns.Remove(strColumns.Length - 2, 2);

            strQuery.Replace("{0}", tableAttribute.TableName);
            strQuery.Replace("{1}", strColumns.ToString());
            strQuery.Replace("{2}", tableAttribute.KeyColumn);
            strQuery.Replace("{3}", "@" + tableAttribute.KeyColumn);

            dbCmd.CommandText = strQuery.ToString();

            return dbCmd;
        }

        public static DbCommand getDeleteCommand(int id, Type type)
        {
            TableAttribute tableAttribute = Attributes.getTableAttribute(type);

            StringBuilder strQuery = new StringBuilder("delete from {0} where {1} = {2}");

            strQuery.Replace("{0}", tableAttribute.TableName);
            strQuery.Replace("{1}", tableAttribute.KeyColumn);
            strQuery.Replace("{2}", id.ToString());

            return DAO.getCommand(strQuery.ToString());
        }

        public static DbCommand GetSelectCommand(object obj)
        {
            Type type = obj.GetType();

            DbCommand dbCmd = DAO.getCommand();

            TableAttribute ta = Attributes.getTableAttribute(type);

            StringBuilder query = new StringBuilder("select * from {0} where {1}");
            StringBuilder cols = new StringBuilder();

            foreach (PropertyInfo prop in type.GetProperties())
            {
                object value = prop.GetValue(obj, null);
                object defaultValue = Attributes.GetDefaultValue(prop);

                if (value != null)
                {
                    if (!value.Equals(defaultValue))
                    {
                        ColumnAttribute ca = Attributes.getColumnAttribute(prop);
                        if (ca != null)
                        {
                            DbParameter dbParam = DAO.getParameter("@" + ca.ColumnName);

                            if (prop.PropertyType == typeof(Int32) || prop.PropertyType == typeof(Int32?) || prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                            {
                                cols.Append(ca.ColumnName + " = @" + ca.ColumnName + " and ");
                                dbParam.DbType = DbType.Int32;
                                dbParam.Value = Convert.ToInt32(value);
                            }
                            else if (prop.PropertyType == typeof(string))
                            {
                                if (value.ToString().Contains("%"))
                                    cols.Append(ca.ColumnName + " like @" + ca.ColumnName + " and ");
                                else
                                    cols.Append(ca.ColumnName + " = @" + ca.ColumnName + " and ");

                                dbParam.DbType = DbType.String;
                                dbParam.Value = value.ToString();
                            }
                            else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                            {
                                cols.Append(ca.ColumnName + " = @" + ca.ColumnName + " and ");
                                dbParam.DbType = DbType.Boolean;
                                dbParam.Value = Convert.ToBoolean(value);
                            }
                            else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
                            {
                                cols.Append(ca.ColumnName + " = @" + ca.ColumnName + " and ");
                                dbParam.DbType = DbType.DateTime;
                                dbParam.Value = Convert.ToDateTime(value);
                            }
                            else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
                            {
                                cols.Append(ca.ColumnName + " = @" + ca.ColumnName + " and ");
                                dbParam.DbType = DbType.Decimal;
                                dbParam.Value = Convert.ToDecimal(value);
                            }
                            else if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
                            {
                                cols.Append(ca.ColumnName + " = @" + ca.ColumnName + " and ");
                                dbParam.DbType = DbType.Double;
                                dbParam.Value = Convert.ToDouble(value);
                            }
                            else if (prop.PropertyType == typeof(byte[]))
                            {
                                cols.Append(ca.ColumnName + " = @" + ca.ColumnName + " and ");
                                dbParam.DbType = DbType.Binary;
                                dbParam.Value = (byte[])value;
                            }
                            else if (prop.PropertyType.IsClass)
                            {
                                if (Attributes.getTableAttribute(prop.PropertyType) != null)
                                {
                                    cols.Append(ca.ColumnName + " = @" + ca.ColumnName + " and ");

                                    dbParam.DbType = DbType.Int32;

                                    foreach (PropertyInfo prop2 in prop.PropertyType.GetProperties())
                                    {
                                        ColumnAttribute columnAttribute2 = Attributes.getColumnAttribute(prop2);
                                        if (columnAttribute2 != null)
                                        {
                                            if (columnAttribute2.Identity)
                                            {
                                                dbParam.Value = Convert.ToInt32(prop2.GetValue(value, null));
                                                break;
                                            }
                                        }
                                    }

                                }
                            }

                            dbCmd.Parameters.Add(dbParam);
                        }
                    }
                }
            }

            cols.Remove(cols.Length - 5, 5);

            query.Replace("{0}", ta.TableName);
            query.Replace("{1}", cols.ToString());

            dbCmd.CommandText = query.ToString();

            return dbCmd;
        }
    }
}
