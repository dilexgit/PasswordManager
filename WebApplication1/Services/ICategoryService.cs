using System.Collections.Generic;
namespace PasswordManagerMVC.Services
{
    public interface ICategoryService
    {
        IEnumerable<string> GetAll();
    }
}