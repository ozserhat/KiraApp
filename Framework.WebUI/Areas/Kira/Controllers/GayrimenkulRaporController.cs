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
        private readonly IIlceService _ilceService;
        private readonly IMahalleService _mahalleService;
        private readonly IGayrimenkulTurService _gayrimenkulTurService;


        public GayrimenkulRaporController(IGayrimenkulService gayrimenkulservice, IIlceService ilceService, IMahalleService mahalleService, IGayrimenkulTurService gayrimenkulTurService)
        {
            _gayrimenkulservice = gayrimenkulservice;
            _ilceService = ilceService;
            _mahalleService = mahalleService;
            _gayrimenkulTurService = gayrimenkulTurService;
        }
  
        public ActionResult Index(GayrimenkulBeyanRequest request, int? page, int pageSize = 15)
        {
            var gayrimenkul = _gayrimenkulservice.GetirSorguListeGayrimenkul(request);

            var model = new GayrimenkulVM
            {
                PageNumber = page ?? 1,
                PageSize = pageSize
            };

            if (gayrimenkul != null)
            {
                model.IlceSelectList = IlceSelectList();
                model.GayrimenkulTuruSelectList = GayrimenkulTuruSelectList();

                if (gayrimenkul != null)
                {
                    model.TotalRecordCount = gayrimenkul.Count();

                    gayrimenkul = gayrimenkul.ToPagedList(model.PageNumber, model.PageSize);

                    model.Gayrimenkuller = new StaticPagedList<Gayrimenkul>(gayrimenkul, model.PageNumber, model.PageSize, model.TotalRecordCount);
                }
            }
            return View(model);
        }

        public SelectList IlceSelectList()
        {
            var ilceler = _ilceService.GetirListe().Where(a => a.Il_Id == 6).Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(ilceler, "Id", "Ad");
        }
        public SelectList GayrimenkulTuruSelectList()
        {
            var gayrimenkulTuru = _gayrimenkulTurService.GetirListe().Select(x => new { x.Id, x.Ad }).ToList();

            return new SelectList(gayrimenkulTuru, "Id", "Ad");
        }
        [HttpPost]
        public JsonResult MahalleSelectList(int ilceId)
        {
            var mahalleler = _mahalleService.GetirListe().Where(a => a.Ilce_Id == ilceId).Select(x => new { x.Id, x.Ad }).ToList();

            return Json(new { Data = mahalleler, success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}