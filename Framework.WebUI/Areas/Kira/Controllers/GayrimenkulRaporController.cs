using System;
using PagedList;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using Framework.WebUI.Models;
using Framework.WebUI.Helpers;
using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using System.Collections.Generic;
using Framework.WebUI.App_Helpers;
using Framework.WebUI.Models.ViewModels;
using Framework.DataAccess.Abstract;
using System.IO;

namespace Framework.WebUI.Areas.Kira.Controllers
{
    public class GayrimenkulRaporController : Controller
    {
        private readonly IGayrimenkulService _gayrimenkulservice;

        public GayrimenkulRaporController(IGayrimenkulService gayrimenkulservice)
        {
            _gayrimenkulservice = gayrimenkulservice;
        }
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var gayrimenkul = _gayrimenkulservice.GetirListe();

            var model = new GayrimenkulVM
            {
                PageNumber = page ?? 1,
                PageSize = pageSize
            };

            if (gayrimenkul != null)
            {
                model.Gayrimenkuller = new StaticPagedList<Gayrimenkul>(gayrimenkul, model.PageNumber, model.PageSize, gayrimenkul.Count());
                model.TotalRecordCount = gayrimenkul.Count();
            }

            return View(model);
        }
    }
}