using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using Framework.WebUI.Helpers;
using Framework.WebUI.Models.ViewModels;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace Framework.WebUI.Controllers
{
    public class HomeController : Controller
    {
        #region Constructor

        private readonly IDuyuruService _service;

        private readonly IDuyuru_BildirimService _bildirimService;

        public IDuyuruService Service => _service;

        public HomeController(IDuyuruService service,
            IDuyuru_BildirimService bildirimService)
        {
            _service = service;
            _bildirimService = bildirimService;
        }

        #endregion
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BildirimListesi()
        {
            int kullaniciId = 0;

            if (User.GetUserPropertyValue("UserId") != null)
                kullaniciId = int.Parse(User.GetUserPropertyValue("UserId"));

            var bildirimListe = _bildirimService.GetirKullaniciMesajlari(kullaniciId);

            DuyuruBildirimVM model = new DuyuruBildirimVM
            {
                PageNumber = 1,
                PageSize = 15
            };

            if (bildirimListe != null)
            {
                model.MesajSayisi = _bildirimService.OkunmamisMesajSayisi(kullaniciId);
                model.DuyuruBildirimleri = new StaticPagedList<Duyuru_Bildirim>(bildirimListe, model.PageNumber, model.PageSize, bildirimListe.Count());
                model.TotalRecordCount = bildirimListe.Count();
            }

            ModelState.AddModelError("LogMessage", "Duyuru Bildirim Mesajları Getirildi.");
            return PartialView("~/Views/Shared/_bildirimListesi.cshtml", model);
        }
    }
}