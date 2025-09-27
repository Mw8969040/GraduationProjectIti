using itism.dal.Models;
using itism.dal.Models.Data;
using itism.dal.Repositories.Interfaces;

namespace itism.dal.Repositories
{
    public class SessionRepo : GenericRepo<Session>, ISessionRepo
    {
        private readonly AppDbContext context;

        public SessionRepo(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}


