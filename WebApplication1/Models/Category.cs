using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasswordManagerMVC.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Password> Passwords { get; set; }
    }
}