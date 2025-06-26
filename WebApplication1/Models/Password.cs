using System.ComponentModel.DataAnnotations;
using PasswordManagerMVC.Models;

namespace PasswordManagerMVC.Models
{
    public class Password
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tan�m alan� zorunludur")]
        [StringLength(100, ErrorMessage = "Tan�m en fazla {1} karakter olabilir")]
        [Display(Name = "Tan�m")]
        public string Description { get; set; }

        [Required(ErrorMessage = "URL alan� zorunludur")]
        [Url(ErrorMessage = "Ge�erli bir URL giriniz (http:// veya https:// ile ba�lamal�)")]
        [StringLength(500, ErrorMessage = "URL en fazla 500 karakter olabilir")]
        [Display(Name = "Web Adresi")]
        public string URL { get; set; }

        [Required(ErrorMessage = "Kullan�c� ad� zorunludur")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullan�c� ad� {2}-{1} karakter aral���nda olmal�d�r")]
        [Display(Name = "Kullan�c� Ad�")]
        public string Username { get; set; }

        [Required(ErrorMessage = "�ifre zorunludur")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "�ifre en az {2} karakter olmal�d�r")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "�ifre en az bir b�y�k harf, bir k���k harf, bir say� ve bir �zel karakter i�ermelidir")]
        [Display(Name = "�ifre")]
        //public string PasswordField { get; set; }

        public string EncryptedPassword { get; set; }

        [Required(ErrorMessage = "Kullan�c� se�imi zorunludur")]
        [Display(Name = "Kullan�c�")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Kategori se�imi zorunludur")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public virtual User User { get; set; }

        [Display(Name = "Kategori")]
        public virtual Category Category { get; set; }
    }
}