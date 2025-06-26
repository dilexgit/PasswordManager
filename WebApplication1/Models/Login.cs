using System.ComponentModel.DataAnnotations;

namespace PasswordManagerMVC.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Kullanýcý adý gereklidir")]
        [Display(Name = "Kullanýcý Adý")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Þifre gereklidir")]
        [DataType(DataType.Password)]
        [Display(Name = "Þifre")]
        public string Password { get; set; }
    }
}