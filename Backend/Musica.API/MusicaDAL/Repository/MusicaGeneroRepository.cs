using MusicaDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicaDAL.Repository
{
    public class MusicaGeneroRepository
    {
        public static MusicaGenero GetById(int id)
        {
            return BlueORM.Objects.getById<MusicaGenero>(id, true);
        }

        public static List<MusicaGenero> GetAll()
        {
            string query = "select * from musica_genero";
            BlueORM.Command cmd = new BlueORM.Command(query);
            return cmd.GetAll<MusicaGenero>();
        }

        public static void Delete(MusicaGenero musicaGenero)
        {
            //musicaGenero.Deleted = true;
            BlueORM.Session.UpdateNow(musicaGenero);
        }

        public static void DeleteNow(MusicaGenero musicaGenero)
        {
            BlueORM.Session.DeleteNow(musicaGenero.CODMUSICAGENERO, musicaGenero.GetType());
        }

        public static void Update(MusicaGenero musicaGenero)
        {
            BlueORM.Session.UpdateNow(musicaGenero);
        }

        public static int Insert(MusicaGenero musicaGenero)
        {
            return BlueORM.Session.InsertNow(musicaGenero);
        }
    }
}
