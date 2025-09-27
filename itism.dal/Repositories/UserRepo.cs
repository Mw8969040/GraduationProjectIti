using itism.dal.Models;
using itism.dal.Models.Data;
using itism.dal.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.dal.Repositories
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        private readonly AppDbContext _context;
        public UserRepo(AppDbContext context) : base(context) => _context = context;

        public bool IsEmailUnique(string email) => !_context.Users.Any(u => u.Email == email);
        public IEnumerable<SelectListItem> GetUsersSelectList(UserRole? role = null)
        {
            var users = _context.Users.AsQueryable();

            if (role.HasValue)
                users = users.Where(u => u.Role == role.Value);

            return users.Select(u => new SelectListItem(u.Name, u.Id.ToString()));
        }

        //public IEnumerable<User> GetUsersByRole(UserRole? role)//review
        //{
        //    if (!role.HasValue) return _context.Users;
        //    return _context.Users.Where(u => u.Role == role.Value);
        //}
    }
}
