using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.dal.Models
{
    public enum Category
    {
        backend,
        ui,
        flutter
    }

    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        public Category Category { get; set; }
  
        public int? InstructorId { get; set; }
        public User? Instructor { get; set; }

        public ICollection<Session> Sessions { get; set; } = new HashSet<Session>();
    }
}
