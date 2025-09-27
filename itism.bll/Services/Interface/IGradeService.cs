using itism.dal.Models;
using System.Collections.Generic;

namespace itism.bll.Services.Interface
{
    public interface IGradeService
    {
        IEnumerable<Grade> GetAll();
        Grade? GetById(int id);
        void Create(Grade grade);
        void Update(Grade grade);
        void Delete(int id);
    }
}


