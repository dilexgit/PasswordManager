using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using BCrypt.Net;
using PasswordManagerMVC.Data;
using PasswordManagerMVC.Models;

namespace PasswordManagerMVC.Services
{
        public class UserService : IUserService
        {
            private readonly AppDbContext _context;

            public UserService(AppDbContext context)
            {
                _context = context;
            }

            public bool Register(Register model)
            {
                if (_context.Users.Any(u => u.UserName == model.UserName))
                    return false;

                var user = new User
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    CreatedDate = DateTime.UtcNow
                };

                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }

            public User Login(Login model)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == model.UserName);
                return user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash) ? user : null;
            }
        public AuthenticationResult Authenticate(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null)
                return AuthenticationResult.Fail("Kullanýcý bulunamadý");

            if(!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return AuthenticationResult.Fail("Kullanýcý þifresi yanlýþ");
            return AuthenticationResult.Success(user);
        }
        public class AuthenticationResult
        {
            public bool IsAuthenticated { get; }
            public User User { get; }
            public string ErrorMessage { get; }

            private AuthenticationResult(bool isAuthenticated, User user, string errorMessage)
            {
                IsAuthenticated = isAuthenticated;
                User = user;
                ErrorMessage = errorMessage;
            }

            public static AuthenticationResult Success(User user)
                => new AuthenticationResult(true, user, null);

            public static AuthenticationResult Fail(string errorMessage)
                => new AuthenticationResult(false, null, errorMessage);
        }
        public User GetUserByName(string name)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == name);
           
            return user != null ?  user  : null;
        }
        public User GetUserById(int id) => _context.Users.Find(id);
          
        public bool EmailExists(string email) => _context.Users.Any(u => u.Email == email);

            public string GeneratePasswordResetToken(string email)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == email);
                if (user == null) return null;

                var token = Guid.NewGuid().ToString();
                user.ResetToken = token;
                user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);
                _context.SaveChanges();
                return token;
            }

            public bool ResetPassword(string username, string token, string newPassword)
            {
                var user = _context.Users.FirstOrDefault(u =>
                    u.UserName == username &&
                    u.ResetToken == token &&
                    u.ResetTokenExpiry > DateTime.UtcNow);

                if (user == null) return false;

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                user.ResetToken = null;
                user.ResetTokenExpiry = null;
                _context.SaveChanges();
                return true;
            }
        }
}
