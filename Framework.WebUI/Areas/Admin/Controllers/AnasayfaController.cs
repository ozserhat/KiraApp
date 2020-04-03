using Framework.WebUI.App_Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class AnasayfaController : Controller
    {
        // GET: Admin/Anasayfa
        public ActionResult Index()
        {
            return View();
        }
    }
}