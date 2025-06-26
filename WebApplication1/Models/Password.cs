using System.ComponentModel.DataAnnotations;
using PasswordManagerMVC.Models;

namespace PasswordManagerMVC.Models
{
    public class Password
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Taným alaný zorunludur")]
        [StringLength(100, ErrorMessage = "Taným en fazla {1} karakter olabilir")]
        [Display(Name = "Taným")]
        public string Description { get; set; }

        [Required(ErrorMessage = "URL alaný zorunludur")]
        [Url(ErrorMessage = "Geçerli bir URL giriniz (http:// veya https:// ile baþlamalý)")]
        [StringLength(500, ErrorMessage = "URL en fazla 500 karakter olabilir")]
        [Display(Name = "Web Adresi")]
        public string URL { get; set; }

        [Required(ErrorMessage = "Kullanýcý adý zorunludur")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullanýcý adý {2}-{1} karakter aralýðýnda olmalýdýr")]
        [Display(Name = "Kullanýcý Adý")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Þifre zorunludur")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Þifre en az {2} karakter olmalýdýr")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Þifre en az bir büyük harf, bir küçük harf, bir sayý ve bir özel karakter içermelidir")]
        [Display(Name = "Þifre")]
        //public string PasswordField { get; set; }

        public string EncryptedPassword { get; set; }

        [Required(ErrorMessage = "Kullanýcý seçimi zorunludur")]
        [Display(Name = "Kullanýcý")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Kategori seçimi zorunludur")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public virtual User User { get; set; }

        [Display(Name = "Kategori")]
        public virtual Category Category { get; set; }
    }
}