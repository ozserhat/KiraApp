using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Areas.SosyalYardim.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: SosyalYardim/Anasayfa
        public ActionResult Index()
        {
            return View();
        }
    }
}