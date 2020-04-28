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
//using Framework.Core.Aspects.Postsharp.LogAspects;
using Framework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Framework.WebUI.Helpers;
using Framework.WebUI.App_Helpers;
using System.Security.Cryptography;
using System.Text;
using Framework.Core.Aspects.Postsharp.LogAspects;
using Framework.Core.CrossCuttingConcerns.Logging.Log4Net;

namespace Framework.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IDuyuru_BildirimService _bildirimService;
        private IUserPermissionsService _userPermissions;

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(IUserService userService, IUserPermissionsService userPermissions, IDuyuru_BildirimService bildirimService)
        {
            _userService = userService;
            _bildirimService = bildirimService;
            _userPermissions = userPermissions;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            //if (User.Identity.IsAuthenticated)
            //    return RedirectToAction("Navigate");
            //throw new ExecutionEngineException("Hata:");

            ModelState.AddModelError("LogMessage", "Login Sayfasına Giriş Yapıldı.");
            return View();
        }

        public ActionResult Navigate()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var UserId = User.GetUserPropertyValue("UserId");


                    if (UserId != null)
                    {
                        var kullanici = _userService.GetById(int.Parse(UserId));

                        if (kullanici.User_Roles.Count > 1)
                        {
                            return RedirectToAction("RolSec");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Anasayfa", new { area = kullanici.User_Roles.FirstOrDefault().Roles.Name });
                        }

                        //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    }

                }
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException();
            }
            return RedirectToAction("Login");
        }

        public ActionResult RolSec()
        {
            var UserId = User.GetUserPropertyValue("UserId");

            var kullanici = _userService.GetById(int.Parse(UserId));

            var model = new RolVM();

            if (kullanici != null && kullanici.User_Roles.Count > 0)
            {
                model.Roller = kullanici.User_Roles.Where(a => a.IsDeleted == false).Select(x => new RolSecItem()
                {
                    Description = x.Roles.Description,
                    ActionName = "Index",
                    ControllerName = "Anasayfa",
                    AreaName = x.Roles.Name
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
            string hashedPassword = "";

            var userExists = _userService.GetByPasswordExists(loginVm.UserName, loginVm.Password);

            if (userExists != null)
            {
                hashedPassword = userExists.Password;

                var user = _userService.GetUsers(loginVm.UserName, hashedPassword);

                if (user != null)
                {
                    #region AddClaims
                    loginVm.Id = user.UserId;

                    var identity = new ClaimsIdentity(new[] {
                               new Claim(ClaimTypes.Name, user.User.FirstName+" "+user.User.LastName)},
                                   DefaultAuthenticationTypes.ApplicationCookie);

                    identity.AddClaim(new Claim("UserId", user.UserId.ToString()));
                    identity.AddClaim(new Claim("UserName", user.User.UserName));
                    _userService.GetUserRoles(user.User).ForEach(x => identity.AddClaim(new Claim(ClaimTypes.Role, x.RoleName)));

                    var permissionList = _userPermissions.GetUserByPermissions(user.UserId);

                    foreach (var permission in permissionList)
                    {
                        identity.AddClaim(new Claim("ControllerActionId", permission.ControllerAction_Id.ToString()));
                        identity.AddClaim(new Claim("IsAuthorize", permission.YetkiliMi.ToString()));
                    }


                    var userPrincipal = new ClaimsPrincipal(new[] { identity });

                    if (user.UserRoles.Count > 1)
                        identity.AddClaim(new Claim("CokluRol", "true"));
                    else
                        identity.AddClaim(new Claim("CokluRol", "false"));


                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, identity);
                    #endregion

                    TempData["MesajSayisi"] = _bildirimService.OkunmamisMesajSayisi(user.UserId);

                    ModelState.AddModelError("LogMessage", "Kullanıcı Bilgileri Doğrulandı Girişi Yapıldı.");

                    return RedirectToAction("Navigate");
                }
            }

            ModelState.AddModelError("LogMessage", "Kullanıcı Bulunamadı!!!");
            loginVm.Errors.Add("Kullanıcı Bilgileri Doğrulanamadı,Tekrar Kontrol Ediniz!!!");
            return View(loginVm);
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Login", "Account");
        }
    }
}