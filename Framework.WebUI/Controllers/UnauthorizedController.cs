using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Framework.WebUI.Controllers
{
    public class UnauthorizedController : Controller
    {
        [HttpPost]
        public ActionResult UyariVer()
        {
            return PartialView("~/Views/Shared/_Unauthorized.cshtml");
        }


        [HttpGet]
        public ActionResult Error()
        {
            Response.StatusCode = 404;
            return View();
        }

        [HttpGet]
        public ActionResult Http404()
        {
            Response.StatusCode = 404;
            return View();
        }

        [HttpGet]
        public ActionResult Http500()
        {
            Response.StatusCode = 500;
            return View();
        }

        private Exception GetError()
        {
            return this.Session["lastError"] as Exception;
        }
        public ActionResult CustomError()
        {
            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return this.View("InternalServer", this.GetError());
        }

        public ActionResult PageNotFound()
        {
            this.Response.StatusCode = (int)HttpStatusCode.NotFound;

            return this.View(this.GetError());
        }

        public ActionResult Forbidden()
        {
            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return this.View(this.GetError());
        }
    }
}