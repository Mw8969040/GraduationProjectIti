using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using itism.dal.CustomValidators;
namespace itism.dal.Models
{
    public class Session
    {
        public int Id { get; set; }
        //[Required]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        //[Required]
        public DateTime StartDate { get; set; }
       // [Required]
        //[DateGreaterThan("StartDate")]
        public DateTime EndDate { get; set; }

        public ICollection<Grade> Grades { get; set; } = new HashSet<Grade>();
    }
}
