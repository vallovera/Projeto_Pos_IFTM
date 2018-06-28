using MusicaDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicaDAL.Repository
{
    public class GeneroRepository
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public static Genero GetById(int id)
        {
            return BlueORM.Objects.getById<Genero>(id, true);
        }

        public static List<Genero> GetAll()
        {
            string query = "select * from genero";
            BlueORM.Command cmd = new BlueORM.Command(query);
            return cmd.GetAll<Genero>();
        }
        public static List<Genero> GetAllByMusic(int codMusica)
        {
            string query = @"select g.* from genero g
                                inner join musica_genero mg on g.codGenero = mg.codGenero
                                where mg.codMusica = " + codMusica;
            BlueORM.Command cmd = new BlueORM.Command(query);
            return cmd.GetAll<Genero>();
        }


       
        public static void DeleteNow(Genero obj)
        {
            BlueORM.Session.DeleteNow(obj.CodGenero, obj.GetType());
        }

        public static void Update(Genero obj)
        {
            BlueORM.Session.UpdateNow(obj);
        }

        public static int Insert(Genero obj)
        {
            return BlueORM.Session.InsertNow(obj);
        }
    }
}
