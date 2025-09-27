using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.dal.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepo Courses { get; }
        //IInstructorRepo Instructors { get; }
        IUserRepo Users { get; }
        ISessionRepo Sessions { get; }
        IGradeRepo Grades { get; }
        int Complete();
    }
}
