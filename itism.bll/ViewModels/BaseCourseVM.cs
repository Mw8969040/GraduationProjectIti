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
    public class BaseCourseVM
    {
        [Required, MinLength(3), MaxLength(50)]
        [NoNumber]
        [Remote(action: "IsNameUnique", controller: "Course")]
        public string? Name { get; set; }

        [Required]
        public Category? Category { get; set; }

        [Display(Name = "Instructor Name")]
        public int? InstructorId { get; set; }
    }
}


