using itism.dal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.dal.Repositories.Interfaces
{
    public interface IUserRepo : IGenericRepo<User>
    {
        bool IsEmailUnique(string email);
        IEnumerable<SelectListItem> GetUsersSelectList(UserRole? role = null);
        //IEnumerable<User> GetUsersByRole(UserRole? role); //review
    }
}
