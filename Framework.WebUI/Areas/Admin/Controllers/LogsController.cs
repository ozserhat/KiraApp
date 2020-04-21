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
using Newtonsoft.Json;

namespace Framework.WebUI.Areas.Admin.Controllers
{
    public class LogsController : Controller
    {
        #region Constructor

        private ILogService _service;

        public LogsController(ILogService service)
        {
            _service = service;
        }
        #endregion

        #region Listeleme

        public ActionResult Index(int? page, int pageSize = 15)
        {
            var loglar = _service.GetAll();

            var model = new LogsVM();

            model.JsonDeserializeLog = new List<LogsVM>();
            //var lgs = loglar.Select(x => x.Detail.ToString()).ToList();
            

            foreach (var item in loglar)
            {
                var log = _service.JsonDeserialize<LogsVM>(item.Detail);
                log.Id = item.Id;
                model.JsonDeserializeLog.Add(log);
            }

            return View(model);
        }

        #endregion

        #region Detay 
        [HttpPost]
        public JsonResult Detay(int Id)
        {
            try
            {
                var detay = _service.GetById(Id);
                var log = _service.JsonDeserialize<LogsVM>(detay.Detail);
                log.Id = detay.Id;
                ModelState.AddModelError("LogMessage", "Log Detay Bilgisi Görüntülendi.");
                return Json(new { Data = log, success = true, Message = "Log Detay Bilgisi Görüntülendi." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LogMessage", "Log Detay Bilgisi Görüntüleme Esnasında Hata Oluştu!!!");
                return Json(new { success = false, Message = "Log Detay Bilgisi Görüntüleme Esnasında Hata Oluştu!!!" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}