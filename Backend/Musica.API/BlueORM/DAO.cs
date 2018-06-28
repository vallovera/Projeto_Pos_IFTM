using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Web;
using System.Reflection;

namespace BlueORM
{
    public class DAO
    {
        public static DbConnection getConnection()
        {
            ConnectionString connString = new ConnectionString();

            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connString.ProviderName);

            DbConnection dbConn = dbFactory.CreateConnection();
            dbConn.ConnectionString = connString.ConnString;
            dbConn.Open();

            return dbConn;
        }

        public static DbConnection getConnection(ConnectionString connString)
        {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connString.ProviderName);

            DbConnection dbConn = dbFactory.CreateConnection();
            dbConn.ConnectionString = connString.ConnString;
            dbConn.Open();

            return dbConn;
        }

        public static DbCommand getCommand()
        {
            ConnectionString connString = new ConnectionString();

            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connString.ProviderName);

            DbCommand dbCmd = dbFactory.CreateCommand();
            dbCmd.Connection = getConnection(connString);
            dbCmd.CommandType = CommandType.Text;
            dbCmd.CommandTimeout = 1800;

            return dbCmd;
        }

        public static DbCommand getCommand(ConnectionString connString)
        {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connString.ProviderName);

            DbCommand dbCmd = dbFactory.CreateCommand();
            dbCmd.Connection = getConnection(connString);
            dbCmd.CommandType = CommandType.Text;
            dbCmd.CommandTimeout = 1800;

            return dbCmd;
        }

        public static DbCommand getCommand(string query)
        {
            DbCommand dbCmd = getCommand();
            dbCmd.CommandText = query;

            return dbCmd;
        }

        public static DbCommand getCommand(string query, ConnectionString connString)
        {
            DbCommand dbCmd = getCommand(connString);
            dbCmd.CommandText = query;

            return dbCmd;
        }

        public static DbParameter getParameter(string parameterName)
        {
            ConnectionString connString = new ConnectionString();

            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connString.ProviderName);

            DbParameter dbParam = dbFactory.CreateParameter();

            dbParam.ParameterName = parameterName;

            return dbParam;
        }

        public static DbParameter getParameter(string paramName, object paramValue)
        {
            Type type;

            if (paramValue != null)
                type = paramValue.GetType();
            else
                type = typeof(string);

            return getParameter(paramName, paramValue, type);
        }

        public static DbParameter getParameter(string paramName, object paramValue, Type type)
        {
            DbParameter dbParam = getParameter(paramName);

            if (type == typeof(Int32) || type == typeof(Int32?))
            {
                dbParam.DbType = DbType.Int32;
                if (paramValue != null)
                    dbParam.Value = Convert.ToInt32(paramValue);
                else
                    dbParam.Value = DBNull.Value;
            }
            else if (type == typeof(string))
            {
                dbParam.DbType = DbType.String;
                if (paramValue != null)
                    dbParam.Value = paramValue.ToString();
                else
                    dbParam.Value = DBNull.Value;
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                dbParam.DbType = DbType.Boolean;
                if (paramValue != null)
                    dbParam.Value = Convert.ToBoolean(paramValue);
                else
                    dbParam.Value = DBNull.Value;
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                dbParam.DbType = DbType.DateTime;
                if (paramValue != null)
                    dbParam.Value = Convert.ToDateTime(paramValue);
                else
                    dbParam.Value = DBNull.Value;
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                dbParam.DbType = DbType.Decimal;
                if (paramValue != null)
                    dbParam.Value = Convert.ToDecimal(paramValue);
                else
                    dbParam.Value = DBNull.Value;
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                dbParam.DbType = DbType.Double;
                if (paramValue != null)
                    dbParam.Value = Convert.ToDouble(paramValue);
                else
                    dbParam.Value = DBNull.Value;
            }
            else if (type == typeof(byte[]))
            {
                dbParam.DbType = DbType.Binary;
                if (paramValue != null)
                    dbParam.Value = (byte[])paramValue;
                else
                    dbParam.Value = DBNull.Value;
            }
            else if (type.IsClass)
            {
                dbParam.DbType = DbType.Int32;

                if (Attributes.getTableAttribute(type) != null && paramValue != null)
                {
                    object obj2 = paramValue;

                    foreach (PropertyInfo prop2 in type.GetProperties())
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

            return dbParam;
        }

        public static DataTable getData(DbCommand dbCmd)
        {
            DataTable dt = new DataTable();

            string sqlExec = dbCmd.CommandText.TrimStart().ToString();

            dt.Load(dbCmd.ExecuteReader());

            dbCmd.Connection.Close();

            return dt;
        }

        public static DataTable getData(string query)
        {
            DbCommand dbCmd = getCommand(query);

            return getData(dbCmd);
        }

        public static DataTable getData(string query, ConnectionString connString)
        {
            DbCommand dbCmd = getCommand(query, connString);

            return getData(dbCmd);
        }

        public static int Execute(string query)
        {
            return Execute(getCommand(query));
        }

        public static int Execute(string query, ConnectionString connString)
        {
            return Execute(getCommand(query, connString));
        }

        public static int Execute(DbCommand dbCmd)
        {
            int rowsAffected;

            try
            {
                rowsAffected = dbCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                rowsAffected = 0;
            }
            finally
            {
                dbCmd.Connection.Close();
            }

            return rowsAffected;
        }

        public static Exception ExecuteEx(DbCommand dbCmd)
        {
            Exception Ex = null;

            try
            {
                dbCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Ex = ex;
            }
            finally
            {
                dbCmd.Connection.Close();
            }

            return Ex;
        }

        public static object ExecuteScalar(DbCommand dbCmd)
        {

            object obj;

            try
            {
                obj = dbCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                obj = ex;
            }
            finally
            {
                dbCmd.Connection.Close();
            }

            return obj;
        }

        public static object ExecuteScalar(string query)
        {
            return ExecuteScalar(getCommand(query));
        }

        public static object ExecuteScalar(string query, ConnectionString connString)
        {
            return ExecuteScalar(getCommand(query, connString));
        }
    }
}
