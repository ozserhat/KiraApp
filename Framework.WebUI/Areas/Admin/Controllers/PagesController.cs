using Framework.Business.Abstract;
using Framework.Entities.ComplexTypes;
using Framework.Entities.Concrete;
using Framework.WebUI.App_Helpers;
using Framework.WebUI.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        private ControllerHelper _helper;
        private IControllerActionService _service;

        public PagesController(IControllerActionService service, ControllerHelper helper)
        {
            _helper = helper;
            _service = service;
        }

        public ActionResult Index(int? page, int pageSize = 15)
        {
            var list = _service.GetAll();

            var model = new ControllerActionsVm();

            model.PageNumber = page ?? 1;
            model.PageSize = pageSize;

            if (list != null)
            {
                model.ControllerActionList = new StaticPagedList<ControllerAction>(list, model.PageNumber, model.PageSize, list.Count());
                model.TotalRecordCount = list.Count();
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult UpdatePages()
        {
            var list = _helper.GetAll();

            List<ControllerAction> controllerAction = new List<ControllerAction>();

            foreach (var item in list)
            {
                controllerAction.Add(new ControllerAction()
                {
                    Guid=Guid.NewGuid(),
                    Action = item.Action,
                    Controller = item.Controller,
                    Attributes = item.Attributes,
                    ReturnType = item.ReturnType,
                    IsDeleted=false
                });

            }

            var result = _service.GetExistsAndSave(controllerAction);

            return Json(new { Message = "Sayfalar Başarıyla Güncellendi.", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}