using System.Collections.Generic;
using System.Linq;
using PasswordManagerMVC.Data;
using PasswordManagerMVC.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System;
using Microsoft.Ajax.Utilities;
namespace PasswordManagerMVC.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly IAesCryptoService _cryptoService;



        public PasswordService(AppDbContext context, IUserService userService, IAesCryptoService cryptoService)
        {
            _context = context;
            _userService = userService;
            _cryptoService = cryptoService;
        }

        public List<Password> GetPasswordsByUserId(int userId)
        {
            if (_cryptoService == null)
            {
                throw new ArgumentNullException(nameof(_cryptoService), "Crypto service cannot be null");
            }

            var passwords = _context.Passwords
                .Include(p => p.Category)
                .Where(p => p.User.Id == userId)
                .AsEnumerable()
                .Select(p => new Password
                {
                    Id = p.Id,
                    Description = p.Description,
                    URL = p.URL,
                    Username = p.Username,
                    //PasswordField = _cryptoService.Decrypt(p.EncryptedPassword),
                    EncryptedPassword = _cryptoService.Decrypt(p.EncryptedPassword),
                    UserId = p.UserId,
                    CategoryId = p.CategoryId,
                    Category = p.Category,
                    User = p.User
                })
                .ToList();

            return passwords;
        }

        public Tuple<bool, string> AddPassword(Password model)
        {
            if (model.CategoryId == 0)
            {
                var existingCategory = _context.Categories
                    .FirstOrDefault(c => c.Name.ToLower() == model.Category.Name.ToLower());

                if (existingCategory != null)
                {
                    model.CategoryId = existingCategory.Id;
                }
                else
                {
                    var newCategory = new Category { Name = model.Category.Name };
                    _context.Categories.Add(newCategory);
                    _context.SaveChanges();
                    model.Category = newCategory;
                }

            }
            else
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == model.CategoryId);
                model.Category = category;
            }

            _context.Passwords.Add(model);
            _context.SaveChanges();

            return Tuple.Create(true, "İşlem Tamamlandı");
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public List<Password> SearchPasswords(int userId, string searchTerm)
        {
            return _context.Passwords
                .Include(p => p.Category)
                .Where(p => p.User.Id == userId &&
                          (p.Description.Contains(searchTerm) ||
                           p.URL.Contains(searchTerm) ||
                           p.Category.Name.Contains(searchTerm) ||
                           p.Username.Contains(searchTerm)))
                .ToList();
        }
    }
}
