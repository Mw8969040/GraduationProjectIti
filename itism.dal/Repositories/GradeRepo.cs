using itism.dal.Models;
using itism.dal.Models.Data;
using itism.dal.Repositories.Interfaces;

namespace itism.dal.Repositories
{
    public class GradeRepo : GenericRepo<Grade>, IGradeRepo
    {
        private readonly AppDbContext context;

        public GradeRepo(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}


