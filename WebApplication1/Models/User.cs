using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PasswordManagerMVC.Models;

namespace PasswordManagerMVC.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Ge�erli bir email adresi giriniz")]
        [StringLength(100, ErrorMessage = "Email en fazla {1} karakter olabilir")]
        [Display(Name = "Email Adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kullan�c� ad� zorunludur")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullan�c� ad� {2}-{1} karakter aras�nda olmal�d�r")]
        [Display(Name = "Kullan�c� Ad�")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "�ifre zorunludur")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "�ifre {2}-{1} karakter aras�nda olmal�d�r")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "�ifre en az bir b�y�k harf, bir k���k harf, bir say� ve bir �zel karakter i�ermelidir")]
        [Display(Name = "�ifre")]
        public string PasswordHash { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Olu�turulma Tarihi")]
        public DateTime CreatedDate { get; set; }

        [StringLength(88, ErrorMessage = "Reset token en fazla {1} karakter olabilir")]
        [Display(Name = "Reset Token")]
        public string ResetToken { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Token Son Kullanma")]
        public DateTime? ResetTokenExpiry { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "Ge�erli bir IP adresi giriniz")]
        [Display(Name = "IP Adresi")]
        public string IPAddress { get; set; }
    }
}