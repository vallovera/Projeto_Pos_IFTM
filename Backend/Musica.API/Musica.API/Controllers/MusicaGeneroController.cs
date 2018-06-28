using MusicaDAL.Repository;
using MusicaDAL.Models;
using MusicaDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicaGenero.API.Controllers
{
    public class MusicaGeneroController : ApiController
    {
        [HttpGet]
        public IEnumerable<MusicaDAL.Models.MusicaGenero> Get()
        {
            return MusicaGeneroRepository.GetAll();
        }

        public MusicaDAL.Models.MusicaGenero Get(int id)
        {
            return MusicaGeneroRepository.GetById(id);
        }

        public void Post([FromBody]MusicaDAL.Models.MusicaGenero obj)
        {
            MusicaGeneroRepository.Insert(obj);
        }

        public void Put([FromBody]MusicaDAL.Models.MusicaGenero obj)
        {
            MusicaGeneroRepository.Update(obj);
        }

        public void Delete(int id)
        {
            MusicaDAL.Models.MusicaGenero obj = new MusicaDAL.Models.MusicaGenero() { CODMUSICA = id };
            MusicaGeneroRepository.DeleteNow(obj);
        }
    }
}
