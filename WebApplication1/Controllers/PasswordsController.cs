using System;
using System.Diagnostics.SymbolStore;
using System.Web.Mvc;
using System.Web.Services.Description;
using PasswordManagerMVC.Models;
using PasswordManagerMVC.Services;
using PasswordManagerMVC.Utilities;

namespace PasswordManagerMVC.Controllers
{
    [Authorize]
    public class PasswordsController : Controller
    {
        private readonly IPasswordService _passwordService;
        private readonly IUserService _userService;
        private readonly IAesCryptoService _aesCryptoService;

        public PasswordsController(IPasswordService passwordService, IUserService userService, IAesCryptoService aesCryptoService)
        {
            _passwordService = passwordService;
            _userService = userService;
            _aesCryptoService = aesCryptoService;
        }

        public ActionResult Index(string searchTerm)
        {
            string username = HttpContext.User.Identity.Name; 
            var user = _userService.GetUserByName(username);
            var passwords = string.IsNullOrEmpty(searchTerm)
                ? _passwordService.GetPasswordsByUserId(user.Id)
                : _passwordService.SearchPasswords(user.Id, searchTerm);

            return View(passwords);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = _passwordService.GetCategories();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Password model)
        {
            if (ModelState.IsValid)
            {
                // PasswordGenerator.GenerateAndPrintKeys();
                model.EncryptedPassword = _aesCryptoService.Encrypt(model.EncryptedPassword);
                //model.PasswordField= _aesCryptoService.Decrypt(model.EncryptedPassword);

                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName)) 
                    throw new ArgumentNullException(nameof(_userService), "Kullanýcý bulunamadý.");
                
                var user = _userService.GetUserByName(userName);
                model.UserId = user.Id;
                
                _passwordService.AddPassword(model);

                return RedirectToAction("Index");
            }

            ViewBag.Categories = _passwordService.GetCategories();
            return View(model);
        }

        public ActionResult GeneratePassword(int length = 12)
        {
            return Content(PasswordGenerator.Generate(length));
        }

    }
}