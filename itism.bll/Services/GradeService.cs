using itism.bll.Services.Interface;
using itism.dal.Models;
using itism.dal.Repositories.Interfaces;
using System.Collections.Generic;

namespace itism.bll.Services
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork unitOfWork;

        public GradeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Grade> GetAll() => unitOfWork.Grades.GetAll();
        public Grade? GetById(int id) => unitOfWork.Grades.GetById(id);

        public void Create(Grade grade)
        {
            unitOfWork.Grades.Add(grade);
            unitOfWork.Complete();
        }

        public void Update(Grade grade)
        {
            unitOfWork.Grades.Update(grade);
            unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            unitOfWork.Grades.Delete(id);
            unitOfWork.Complete();
        }
    }
}


