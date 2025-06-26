using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;
namespace PasswordManagerMVC.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Kullanýcý adý zorunludur")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullanýcý adý 3-50 karakter arasýnda olmalýdýr")]
        [Display(Name = "Kullanýcý Adý")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        [Display(Name = "Email Adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Þifre zorunludur")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Þifre en az 8 karakter olmalýdýr")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Þifre en az bir büyük harf, bir küçük harf ve bir sayý içermelidir")]
        [Display(Name = "Þifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Þifre tekrarý zorunludur")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Þifreler uyuþmuyor")]
        [Display(Name = "Þifre Tekrar")]
        public string ConfirmPassword { get; set; }
    }
}