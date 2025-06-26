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
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        [StringLength(100, ErrorMessage = "Email en fazla {1} karakter olabilir")]
        [Display(Name = "Email Adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kullanýcý adý zorunludur")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullanýcý adý {2}-{1} karakter arasýnda olmalýdýr")]
        [Display(Name = "Kullanýcý Adý")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Þifre zorunludur")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Þifre {2}-{1} karakter arasýnda olmalýdýr")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Þifre en az bir büyük harf, bir küçük harf, bir sayý ve bir özel karakter içermelidir")]
        [Display(Name = "Þifre")]
        public string PasswordHash { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Oluþturulma Tarihi")]
        public DateTime CreatedDate { get; set; }

        [StringLength(88, ErrorMessage = "Reset token en fazla {1} karakter olabilir")]
        [Display(Name = "Reset Token")]
        public string ResetToken { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Token Son Kullanma")]
        public DateTime? ResetTokenExpiry { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "Geçerli bir IP adresi giriniz")]
        [Display(Name = "IP Adresi")]
        public string IPAddress { get; set; }
    }
}