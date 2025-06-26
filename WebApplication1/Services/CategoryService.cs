using System.Collections.Generic;
namespace PasswordManagerMVC.Services
{
    public class CategoryService : ICategoryService
    {
        public IEnumerable<string> GetAll()
        {
            return new List<string> { "Sosyal Medya", "Bankacılık", "İş", "Diğer" };
        }
    }
}