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

            foreach (var item in loglar)
            {
                var log = _service.JsonDeserialize<LogsVM>(item.Detail);

                model.JsonDeserializeLog.Add(log);
            }

            return View(model);
        }

        #endregion

    }
}