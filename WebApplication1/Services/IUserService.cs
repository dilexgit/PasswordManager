using PasswordManagerMVC.Models;
using static PasswordManagerMVC.Services.UserService;

namespace PasswordManagerMVC.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Yeni kullan�c� kayd� olu�turur
        /// </summary>
        /// <param name="model">Kullan�c� kay�t bilgileri</param>
        /// <returns>Kay�t ba�ar�l�ysa true, email zaten varsa false</returns>
        bool Register(Register model);

        /// <summary>
        /// Kullan�c� giri�i do�rular
        /// </summary>
        /// <param name="model">Giri� bilgileri</param>
        /// <returns>Do�rulama ba�ar�l�ysa User nesnesi, de�ilse null</returns>
        User Login(Login model);
        AuthenticationResult Authenticate(string username, string password);

        /// <summary>
        /// ID'ye g�re kullan�c� getirir
        /// </summary>
        /// <param name="id">Kullan�c� ID'si</param>
        /// <returns>Bulunan User nesnesi veya null</returns>
        User GetUserById(int id);
        User GetUserByName(string name);

        /// <summary>
        /// Email'e g�re kullan�c� var m� kontrol eder
        /// </summary>
        bool EmailExists(string email);

        /// <summary>
        /// �ifre s�f�rlama token� olu�turur
        /// </summary>
        string GeneratePasswordResetToken(string email);

        /// <summary>
        /// �ifreyi g�nceller
        /// </summary>
        bool ResetPassword(string email, string token, string newPassword);
    }
}
