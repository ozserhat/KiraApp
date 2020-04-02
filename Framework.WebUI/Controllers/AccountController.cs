using Framework.Business.Abstract;
using Framework.Core.CrossCuttingConcerns.Security.Web;
using Framework.WebUI.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Security.Claims;
using System.Web;
using Framework.Core.Aspects.Postsharp.LogAspects;
using Framework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Framework.WebUI.Helpers;

namespace Framework.WebUI.Controllers
{

    [LogAspect(typeof(DatabaseLogger))]
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            //if (User.Identity.IsAuthenticated)
            //    return RedirectToAction("Navigate");

            return View();
        }

        public ActionResult Navigate()
        {
            if (User.Identity.IsAuthenticated)
            {
                var UserId = User.GetUserPropertyValue("UserId");

                if (UserId != null)
                {
                    var kullanici = _userService.GetById(int.Parse(UserId));

                    if (kullanici.UserRoles.Count > 1)
                    {
                        return RedirectToAction("RolSec");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Anasayfa", new { area = kullanici.UserRoles.FirstOrDefault().Roles.Name });
                    }

                    //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                }

            }
            return RedirectToAction("Login");
        }

        public ActionResult RolSec()
        {
            var UserId = User.GetUserPropertyValue("UserId");

            var kullanici = _userService.GetById(int.Parse(UserId));

            var model = new RolVM();

            if (kullanici.UserRoles.Count > 0)
            {
                model.Roller = kullanici.UserRoles.Select(x => new RolSecItem()
                {
                    Aciklama = x.Roles.Name,
                    ActionName = "Index",
                    ControllerName = "Anasayfa",
                    AreaName = x.Roles.Description
                }).ToList();
            }
            else
            {
                model.Errors.Add("Atanmış rol bulunamadı");
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Login(LoginVm loginVm)
        {

            if (!_userService.UserExists(loginVm.UserName))
                loginVm.Errors.Add("Kullanıcı Bilgisi Bulunamadı!!!!!!");

            var user = _userService.GetUsers(loginVm.UserName, loginVm.Password);


            if (user != null)
            {
                loginVm.Id = user.UserId;

                //AuthenticationHelper.CreateAuthCookie(
                //new Guid(),
                //  user.User.UserName,
                //  user.User.Email,
                //DateTime.Now.AddDays(15),
                //_userService.GetUserRoles(user.User).Select(u => u.RoleName).ToArray(),
                //false,
                //user.User.FirstName,
                //user.User.LastName);

                var identity = new ClaimsIdentity(new[] {
                               new Claim(ClaimTypes.Name, user.User.FirstName+" "+user.User.LastName)},
                               DefaultAuthenticationTypes.ApplicationCookie);

                identity.AddClaim(new Claim("UserId", user.UserId.ToString()));
                identity.AddClaim(new Claim("UserName", user.User.UserName));

                _userService.GetUserRoles(user.User).ForEach(x => identity.AddClaim(new Claim(ClaimTypes.Role, x.RoleName)));

                if (user.UserRoles.Count > 1)
                    identity.AddClaim(new Claim("CokluRol", "true"));
                else
                    identity.AddClaim(new Claim("CokluRol", "false"));


                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, identity);

                return RedirectToAction("Navigate");
            }



            loginVm.Errors.Add("Kullanıcı Bilgilerini Kontrol Ediniz!!!");

            return View(loginVm);
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Login", "Account");
        }
    }
}