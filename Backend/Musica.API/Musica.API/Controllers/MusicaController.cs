using MusicaDAL.Models;
using MusicaDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Musica.API.Controllers
{
    public class MusicaController : ApiController
    {
        [HttpGet]
        public IEnumerable<MusicaDAL.Models.Musica> Get()
        {
            var todasMusicas = MusicaRepository.GetAll();

            foreach (var musica in todasMusicas)
            {
                musica.generos = GeneroRepository.GetAllByMusic(musica.CODMUSICA);
                foreach (var genero in musica.generos)
                {
                    musica.GenerosTexto += genero.NOME + ",";
                }
               
            }

            return todasMusicas;
        }

        public MusicaDAL.Models.Musica Get(int id)
        {
            return MusicaRepository.GetById(id);
        }

        public void Post([FromBody]MusicaDAL.Models.Musica obj)
        {
            MusicaRepository.Insert(obj);
        }

        public void Put([FromBody]MusicaDAL.Models.Musica obj)
        {
            MusicaRepository.Update(obj);
        }

        public void Delete(int id)
        {
            MusicaDAL.Models.Musica obj = new MusicaDAL.Models.Musica() { CODMUSICA = id };
            MusicaRepository.DeleteNow(obj);
        }
    }
}
