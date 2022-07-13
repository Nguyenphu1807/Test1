using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAPI1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Route("Home/Alo/{testid?}")]
        public ActionResult Alo(int? testid)
        {
            return View();
        }
    }
}