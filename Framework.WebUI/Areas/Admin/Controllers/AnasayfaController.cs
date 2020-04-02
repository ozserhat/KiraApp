using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Areas.Admin.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: Admin/Anasayfa
        public ActionResult Index()
        {
            return View();
        }
    }
}