using System;
using System.Collections.Generic;
using PasswordManagerMVC.Models;
namespace PasswordManagerMVC.Services
{
    public interface IPasswordService
    {
        List<Password> GetPasswordsByUserId(int userId);
        Tuple<bool,string> AddPassword(Password model);
        List<Category> GetCategories();
        List<Password> SearchPasswords(int userId, string searchTerm);
    }
}
