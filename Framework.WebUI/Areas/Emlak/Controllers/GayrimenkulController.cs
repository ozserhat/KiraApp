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

namespace Framework.WebUI.Areas.Emlak.Controllers
{
    [CustomAuthorize(Roles = "Emlak")]
    public class GayrimenkulController : Controller
    {
        #region Constructor

        private IGayrimenkulService _gayrimenkulservice;
        private IGayrimenkulTurService _turService;

        public GayrimenkulController(IGayrimenkulService gayrimenkulservice, IGayrimenkulTurService turService)
        {
            _turService = turService;
            _gayrimenkulservice = gayrimenkulservice;
        }
        #endregion
        // GET: Emlak/Gayrimenkul
        #region Listeleme
        public ActionResult Index(int? page, int pageSize = 15)
        {
            var gayrimenkul = _gayrimenkulservice.GetirListe();

            var model = new GayrimenkulVM();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (gayrimenkul != null)
            {
                model.Gayrimenkuller= new StaticPagedList<Gayrimenkul>(gayrimenkul, model.PageNumber, model.PageSize, gayrimenkul.Count());
                model.TotalRecordCount = gayrimenkul.Count();
            }

            return View(model);
        }

        #endregion
    }
}