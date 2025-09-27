using itism.dal.Models;
using itism.dal.Models.Data;
using itism.dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.dal.Repositories
{
    public class CourseRepo : GenericRepo<Course>, ICourseRepo
    {
        private readonly AppDbContext _context;
        public CourseRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAllWithInstructor()
        {
            return _context.Courses.Include(c => c.Instructor);
        }

        public Course? GetByIdWithInstructor(int id)
        {
            return _context.Courses.Include(c => c.Instructor).FirstOrDefault(c => c.Id == id);
        }

        public bool IsNameUnique(string name, int? excludeId = null)
        {
            var query = _context.Courses.AsQueryable();
            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);
            return !query.Any(c => c.Name == name);
        }
    }
}

