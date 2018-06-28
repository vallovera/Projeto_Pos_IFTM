using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Linq;

namespace BlueORM
{
    public class Command
    {
        private DbParameter[] dbParams;
        private int pos;

        private string cmdText;
        public string CommandText
        {
            get { return cmdText; }
            set { cmdText = value; }
        }

        private ConnectionString connString;
        public ConnectionString ConnectionString
        {
            get { return connString; }
            set { connString = value; }
        }

        public Command()
        {
            dbParams = new DbParameter[50];
            pos = 0;
            connString = new ConnectionString();
        }

        public Command(string commandText)
        {
            dbParams = new DbParameter[50];
            pos = 0;
            connString = new ConnectionString();
            cmdText = commandText;
        }

        public void AddParam(string paramName, object paramValue)
        {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connString.ProviderName);
            DbParameter dbParam = dbFactory.CreateParameter();

            dbParam.ParameterName = paramName;

            if (paramValue != null)
            {
                if (paramValue.GetType() == typeof(Int32) || paramValue.GetType() == typeof(Int32?))
                {
                    dbParam.DbType = DbType.Int32;
                    dbParam.Value = paramValue;
                }
                else if (paramValue.GetType() == typeof(string))
                {
                    dbParam.DbType = DbType.String;
                    dbParam.Value = paramValue;
                }
                else if (paramValue.GetType() == typeof(bool) || paramValue.GetType() == typeof(bool?))
                {
                    dbParam.DbType = DbType.Boolean;
                    dbParam.Value = paramValue;
                }
                else if (paramValue.GetType() == typeof(DateTime) || paramValue.GetType() == typeof(DateTime?))
                {
                    dbParam.DbType = DbType.DateTime;
                    dbParam.Value = paramValue;
                }
                else if (paramValue.GetType() == typeof(decimal) || paramValue.GetType() == typeof(decimal?))
                {
                    dbParam.DbType = DbType.Decimal;
                    dbParam.Value = paramValue;
                }
                else if (paramValue.GetType() == typeof(double) || paramValue.GetType() == typeof(double?))
                {
                    dbParam.DbType = DbType.Double;
                    dbParam.Value = paramValue;
                }
                else if (paramValue.GetType() == typeof(byte[]))
                {
                    dbParam.DbType = DbType.Binary;
                    dbParam.Value = paramValue;
                }
                else
                {
                    dbParam.DbType = DbType.String;
                    dbParam.Value = DBNull.Value;
                }
            }
            else
            {
                dbParam.DbType = DbType.String;
                dbParam.Value = DBNull.Value;
            }

            dbParams[pos] = dbParam;

            pos++;
        }

        private DbCommand GetCommand()
        {
            DbCommand dbCmd = DAO.getCommand(connString);
            dbCmd.CommandText = cmdText;

            for (int i = 0; i < pos; i++)
                dbCmd.Parameters.Add(dbParams[i]);

#if DEBUG
            string command = cmdText;
            foreach (var param in dbParams.Where(p => p != null))
            {
                if (param.Value == DBNull.Value)
                    command = command.Replace(param.ParameterName, "NULL");
                else if (param.DbType == DbType.DateTime || param.DbType == DbType.Date)
                    command = command.Replace(param.ParameterName, string.Concat("'", DateTime.Parse(param.Value.ToString()).ToString("yyyy-MM-dd hh:mm:ss"), "'"));
                else if (param.DbType == DbType.Int16 || param.DbType == DbType.Int32 || param.DbType == DbType.Int64 || param.DbType == DbType.Decimal || param.DbType == DbType.Double || param.DbType == DbType.Currency)
                    command = command.Replace(param.ParameterName, param.Value.ToString());
                else
                    command = command.Replace(param.ParameterName, string.Concat("'", param.Value.ToString(), "'"));
            }
            System.Diagnostics.Debug.WriteLine(command);
#endif

            return dbCmd;
        }

        public DataTable GetData()
        {
            return DAO.getData(GetCommand());
        }

        public int Execute()
        {
            return DAO.Execute(GetCommand());
        }

        public Exception ExecuteEx()
        {
            return DAO.ExecuteEx(GetCommand());
        }

        public object ExecuteScalar()
        {
            return DAO.ExecuteScalar(GetCommand());
        }

        public List<T> GetAll<T>()
        {
            return Objects.getAll<T>(GetCommand());
        }

        public List<T> GetAll<T>(bool recursive)
        {
            return Objects.getAll<T>(GetCommand(), recursive);
        }

        public T Get<T>()
        {
            return Objects.get<T>(GetCommand(), true);
        }

        public T Get<T>(bool recursive)
        {
            return Objects.get<T>(GetCommand(), recursive);
        }
    }
}
