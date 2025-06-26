using System.ComponentModel.DataAnnotations;

namespace PasswordManagerMVC.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Kullan�c� ad� gereklidir")]
        [Display(Name = "Kullan�c� Ad�")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "�ifre gereklidir")]
        [DataType(DataType.Password)]
        [Display(Name = "�ifre")]
        public string Password { get; set; }
    }
}