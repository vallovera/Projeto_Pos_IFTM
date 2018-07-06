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
    public class GeneroController : ApiController
    {
        [HttpGet]
        public IEnumerable<Genero> Get()
        {
            return GeneroRepository.GetAll();
        }

        [HttpGet]
        public Genero Get(int id)
        {
            return GeneroRepository.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody]Genero genero)
        {
           GeneroRepository.Insert(genero);
        }

        //[HttpPost]
        //public void Associa([FromBody]MusicaDAL.Models.MusicaGenero relaciona)
        //{
        //    MusicaGeneroRepository.Insert(relaciona);
        //}

        [HttpPut]
        public void Put([FromBody]Genero genero)
        {
            GeneroRepository.Update(genero);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Genero genero = new Genero() { CodGenero = id };
            GeneroRepository.DeleteNow(genero);
        }
    }
}
