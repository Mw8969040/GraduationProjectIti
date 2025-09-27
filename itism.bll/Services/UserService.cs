using itism.bll.Services.Interface;
using itism.bll.ViewModels;
using itism.dal.Models;
using itism.dal.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.bll.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Create(CreateUserVM vm)
        {
            var user = new User
            {
                Name = vm.Name ?? string.Empty,
                Email = vm.Email ?? string.Empty,
                Role = vm.Role!.Value
            };
            unitOfWork.Users.Add(user);
            unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            unitOfWork.Users.Delete(id);
            unitOfWork.Complete();
        }

        public IEnumerable<UserVM> GetAll()
        {
            return unitOfWork.Users.GetAll().Select(u => new UserVM
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            });
        }

        public PageResults<UserVM> GetAllPaged(string? searchName, UserRole? role, int page, int pageSize)
        {
            var users = GetAll();

            if (!string.IsNullOrEmpty(searchName))
                users = users.Where(u => u.Name.Contains(searchName, System.StringComparison.OrdinalIgnoreCase));

            if (role.HasValue)
                users = users.Where(u => u.Role == role.Value);

            var paged = users.Skip((page - 1) * pageSize).Take(pageSize);

            return new PageResults<UserVM>
            {
                Items = paged,
                Page = page,
                PageSize = pageSize,
                TotalCount = users.Count()
            };
        }

        public UpdateUserVM? GetUserForUpdate(int id)
        {
            var user = unitOfWork.Users.GetById(id);
            if (user == null) return null;

            return new UpdateUserVM
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        public bool IsEmailUnique(string email)
        {
            return !unitOfWork.Users.GetAll().Any(u => u.Email == email);
        }

        public void Update(UpdateUserVM vm)
        {
            var user = new User
            {
                Id = vm.Id,
                Name = vm.Name ?? string.Empty,
                Email = vm.Email ?? string.Empty,
                Role = vm.Role!.Value
            };
            unitOfWork.Users.Update(user);
            unitOfWork.Complete();
        }

        public IEnumerable<SelectListItem> GetUsersForSelectList(UserRole? role = null)
        {
            var users = unitOfWork.Users.GetAll();

            if (role.HasValue)
                users = users.Where(u => u.Role == role.Value);

            return users.Select(u => new SelectListItem(u.Name, u.Id.ToString()));
        }
    }
}
