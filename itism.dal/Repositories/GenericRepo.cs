using itism.dal.Models.Data;
using itism.dal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.dal.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext context;

        public GenericRepo(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        public T? GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
                context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetPaged(int page, int pageSize)
        {
            return GetAll().Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
