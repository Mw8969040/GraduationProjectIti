using itism.bll.ViewModels;
using itism.dal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.bll.Services.Interface
{
    public interface IUserService
    {
        IEnumerable<UserVM> GetAll();
        PageResults<UserVM> GetAllPaged(string? searchName, UserRole? role, int page, int pageSize);
        void Create(CreateUserVM vm);
        void Update(UpdateUserVM vm);
        void Delete(int id);
        bool IsEmailUnique(string email);
        UpdateUserVM? GetUserForUpdate(int id);
        IEnumerable<SelectListItem> GetUsersForSelectList(UserRole? role = null);
    }

}
