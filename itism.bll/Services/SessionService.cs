using itism.bll.Services.Interface;
using itism.dal.Models;
using itism.dal.Repositories.Interfaces;
using System.Collections.Generic;

namespace itism.bll.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Session> GetAll() => unitOfWork.Sessions.GetAll();
        public Session? GetById(int id) => unitOfWork.Sessions.GetById(id);

        public void Create(Session session)
        {
            unitOfWork.Sessions.Add(session);
            unitOfWork.Complete();
        }

        public void Update(Session session)
        {
            unitOfWork.Sessions.Update(session);
            unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            unitOfWork.Sessions.Delete(id);
            unitOfWork.Complete();
        }
    }
}


