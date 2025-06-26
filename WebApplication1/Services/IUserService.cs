using PasswordManagerMVC.Models;
using static PasswordManagerMVC.Services.UserService;

namespace PasswordManagerMVC.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Yeni kullanýcý kaydý oluþturur
        /// </summary>
        /// <param name="model">Kullanýcý kayýt bilgileri</param>
        /// <returns>Kayýt baþarýlýysa true, email zaten varsa false</returns>
        bool Register(Register model);

        /// <summary>
        /// Kullanýcý giriþi doðrular
        /// </summary>
        /// <param name="model">Giriþ bilgileri</param>
        /// <returns>Doðrulama baþarýlýysa User nesnesi, deðilse null</returns>
        User Login(Login model);
        AuthenticationResult Authenticate(string username, string password);

        /// <summary>
        /// ID'ye göre kullanýcý getirir
        /// </summary>
        /// <param name="id">Kullanýcý ID'si</param>
        /// <returns>Bulunan User nesnesi veya null</returns>
        User GetUserById(int id);
        User GetUserByName(string name);

        /// <summary>
        /// Email'e göre kullanýcý var mý kontrol eder
        /// </summary>
        bool EmailExists(string email);

        /// <summary>
        /// Þifre sýfýrlama tokený oluþturur
        /// </summary>
        string GeneratePasswordResetToken(string email);

        /// <summary>
        /// Þifreyi günceller
        /// </summary>
        bool ResetPassword(string email, string token, string newPassword);
    }
}
