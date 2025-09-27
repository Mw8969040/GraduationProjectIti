using itism.dal.Models.Data;
using itism.dal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.dal.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ICourseRepo? _courses;
        //private IInstructorRepo? _instructors;
        private IUserRepo? _users;
        private ISessionRepo? _sessions;
        private IGradeRepo? _grades;



        public UnitOfWork(AppDbContext context) => _context = context;

        public ICourseRepo Courses => _courses ??= new CourseRepo(_context);
        //public IInstructorRepo Instructors => _instructors ??= new InstructorRepo(_context);
        public IUserRepo Users => _users ??= new UserRepo(_context);
        public ISessionRepo Sessions => _sessions ??= new SessionRepo(_context);
        public IGradeRepo Grades => _grades ??= new GradeRepo(_context);

        public int Complete() => _context.SaveChanges();
    }
}
