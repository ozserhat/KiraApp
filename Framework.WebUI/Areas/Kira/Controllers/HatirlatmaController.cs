using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using Framework.WebUI.App_Helpers;
using Framework.WebUI.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Framework.WebUI.Areas.Kira.Controllers
{
    [CustomAuthorize(Roles = "Kira")]
    public class HatirlatmaController : Controller
    {
        #region Constructor
        private readonly ITahakkukService _tahakkukService;

        public HatirlatmaController(ITahakkukService tahakkukService)
        {
            _tahakkukService = tahakkukService;
        }
        #endregion

        // GET: Kira/Hatirlatma
        #region Listeleme
        public ActionResult Index()
        {
            var model = new HatirlatmaListesiVM();

            var odemesiGecikenler = _tahakkukService.GetirOdemesiGecikenTahakkuklar();
            var odemesiGelenler = _tahakkukService.GetirOdemesiGelenTahakkuklar();
            var artisiGelenler = _tahakkukService.GetirArtisiGelenTahakkuklar();
            var sozlesmesiBitenler = _tahakkukService.GetirSozlemesiBitenBeyanlar();
            
            model.PageNumber = 1;
            model.PageSize = 10;

            model.OdemesiGecikenler = new StaticPagedList<Tahakkuk>(odemesiGecikenler, model.PageNumber, model.PageSize, odemesiGecikenler.Count());
            model.OdemesiGelenler = new StaticPagedList<Tahakkuk>(odemesiGelenler, model.PageNumber, model.PageSize, odemesiGelenler.Count());
            model.ArtisiGelenler = new StaticPagedList<Tahakkuk>(artisiGelenler, model.PageNumber, model.PageSize, artisiGelenler.Count());
            model.SozlesmesiBitenler = new StaticPagedList<Tahakkuk>(sozlesmesiBitenler, model.PageNumber, model.PageSize, sozlesmesiBitenler.Count());

            return View(model);
        } 
        #endregion
    }
}