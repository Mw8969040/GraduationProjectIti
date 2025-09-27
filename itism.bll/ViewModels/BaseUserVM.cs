using itism.bll.CustomValidators;
using itism.dal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.bll.ViewModels
{
    public class BaseUserVM
    {
        [Required, MinLength(3), MaxLength(50)]
        [NoNumber]
        public string? Name { get; set; }

        [Required, EmailAddress]
       // [Remote(action: "CheckIfEmailUnique", controller: "User")]
        public virtual string? Email { get; set; }

        [Required]
        public UserRole? Role { get; set; } 
    }
}
