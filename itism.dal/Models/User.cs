using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.dal.Models
{
    public enum UserRole
    {
        Admin,
        Instructor,
        Trainee
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Grade> Grades { get; set; } = new HashSet<Grade>();
    }
}
