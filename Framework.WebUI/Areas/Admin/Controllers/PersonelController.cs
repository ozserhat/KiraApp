using Framework.Business.Abstract;
using Framework.Entities.Concrete;
using Framework.WebUI.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Areas.Admin.Controllers
{
    public class PersonelController : Controller
    {
        private IPersonelService _personelService;
        

        // GET: Admin/Personel
        public PersonelController(IPersonelService personelService)
        {
            _personelService = personelService;
        }
        public ActionResult Index(int? page, int pageSize = 1)
        {
            var roller = _personelService.GetirPersonelList();

            //var model = new PersonelVm();

            //model.PageNumber = page ?? 1;
            //model.PageSize = pageSize;

            //if (roller != null)
            //{
            //    model.Personeller = new StaticPagedList<Personel>(roller, model.PageNumber, model.PageSize, roller.Count());
            //    model.TotalRecordCount = roller.Count();
            //}

            return View();
        }
   
        public ActionResult Ekle()
        {
            //var model = new PersonelEkleVM();

            return View();
        }
    }
}