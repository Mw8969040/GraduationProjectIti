using itism.dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.dal.Repositories.Interfaces
{
    public interface ICourseRepo : IGenericRepo<Course>
    {
        IEnumerable<Course> GetAllWithInstructor();
        Course? GetByIdWithInstructor(int id);
        bool IsNameUnique(string name, int? excludeId = null);
    }
}
