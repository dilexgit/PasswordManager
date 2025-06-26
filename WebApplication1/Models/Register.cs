using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;
namespace PasswordManagerMVC.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Kullan�c� ad� zorunludur")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullan�c� ad� 3-50 karakter aras�nda olmal�d�r")]
        [Display(Name = "Kullan�c� Ad�")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Ge�erli bir email adresi giriniz")]
        [Display(Name = "Email Adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "�ifre zorunludur")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "�ifre en az 8 karakter olmal�d�r")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "�ifre en az bir b�y�k harf, bir k���k harf ve bir say� i�ermelidir")]
        [Display(Name = "�ifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "�ifre tekrar� zorunludur")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "�ifreler uyu�muyor")]
        [Display(Name = "�ifre Tekrar")]
        public string ConfirmPassword { get; set; }
    }
}