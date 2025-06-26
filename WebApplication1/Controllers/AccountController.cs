using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using PasswordManagerMVC.Models;
using PasswordManagerMVC.Services;

namespace PasswordManagerMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
            {
                return RedirectToAction("Index", "Passwords"); 
            }
            return View(new Login());
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult Verify(Login model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Geçersiz form verisi" });
            }

            var loginResult = _userService.Authenticate(model.UserName, model.Password);

            if (!loginResult.IsAuthenticated)
            {
                return Json(new
                {
                    success = false,
                    message = loginResult.ErrorMessage ?? "Geçersiz kullanıcı adı veya şifre"
                });
            }

            return Json(new { success = true });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginResult = _userService.Authenticate(model.UserName, model.Password);

            if (!loginResult.IsAuthenticated)
            {
                ModelState.AddModelError("", loginResult.ErrorMessage ?? "Geçersiz kullanıcı adı veya şifre.");
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(loginResult.User.UserName, false);

            return RedirectToLocal(returnUrl);
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Passwords");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!_userService.Register(model))
            {
                ModelState.AddModelError("UserName", "Bu kullanıcı adı zaten kullanımda.");
                return View(model);
            }

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}