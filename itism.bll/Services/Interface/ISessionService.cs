using itism.dal.Models;
using System.Collections.Generic;

namespace itism.bll.Services.Interface
{
    public interface ISessionService
    {
        IEnumerable<Session> GetAll();
        Session? GetById(int id);
        void Create(Session session);
        void Update(Session session);
        void Delete(int id);
    }
}


