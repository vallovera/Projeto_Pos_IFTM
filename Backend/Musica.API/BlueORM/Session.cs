using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace BlueORM
{
    public class Session
    {
        private List<DbCommand> queue;

        public Session()
        {
            queue = new List<DbCommand>();
        }

        public void InsertOnCommit(object obj)
        {
            queue.Add(Factory.getInsertCommand(obj, false));
        }

        public static int InsertNow(object obj)
        {
            return InsertNow(obj, true);
        }

        public static int InsertNow(object obj, bool returnMax)
        {
            int retorno = 0;

            if (returnMax)
            {
                retorno = Convert.ToInt32(DAO.ExecuteScalar(Factory.getInsertCommand(obj, returnMax)));
            }
            else
            {
                DAO.Execute(Factory.getInsertCommand(obj, returnMax));
            }

            return retorno;
        }

        public void UpdateOnCommit(object obj)
        {
            queue.Add(Factory.getUpdateCommand(obj));
        }

        public static void UpdateNow(object obj)
        {
            DAO.Execute(Factory.getUpdateCommand(obj));
        }

        public void DeleteOnCommit(int id, Type type)
        {
            queue.Add(Factory.getDeleteCommand(id, type));
        }

        public static void DeleteNow(int id, Type type)
        {
            DAO.Execute(Factory.getDeleteCommand(id, type));
        }

        public void Commit()
        {
            foreach (DbCommand dbCmd in queue)
            {
                DAO.Execute(dbCmd);
            }
            queue.Clear();
        }

        public void RollBack()
        {
            queue.Clear();
        }
    }
}
