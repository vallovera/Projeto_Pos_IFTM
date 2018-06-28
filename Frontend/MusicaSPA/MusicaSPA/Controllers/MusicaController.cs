using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicaSPA.Controllers
{
    public class MusicaController : Controller
    {
        // GET: Musica
        public ActionResult Index()
        {
            return View();
        }
    }
}