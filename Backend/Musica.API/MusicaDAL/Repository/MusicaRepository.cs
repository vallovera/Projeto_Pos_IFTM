using MusicaDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicaDAL.Repository
{
    public class MusicaRepository
    {
        public static Musica GetById(int id)
        {
            return BlueORM.Objects.getById<Musica>(id, true);
        }

        public static List<Musica> GetAll()
        {
            string query = "select * from musica";
            BlueORM.Command cmd = new BlueORM.Command(query);
            return cmd.GetAll<Musica>();
        }

        public static void Delete(Musica musica)
        {
            //musica.Deleted = true;
            BlueORM.Session.UpdateNow(musica);
        }

        public static void DeleteNow(Musica musica)
        {
            BlueORM.Session.DeleteNow(musica.CODMUSICA, musica.GetType());
        }

        public static void Update(Musica musica)
        {
            BlueORM.Session.UpdateNow(musica);
        }

        public static int Insert(Musica musica)
        {
            return BlueORM.Session.InsertNow(musica);
        }
    }
}
