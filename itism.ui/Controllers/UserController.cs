using itism.bll.Services.Interface;
using itism.bll.ViewModels;
using itism.dal.Models;
using itism.ui.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace itism.ui.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IOptions<PageSettings> options;

        public UserController(IUserService userService, IOptions<PageSettings> options)
        {
            this.userService = userService;
            this.options = options;
        }

        public IActionResult GetAllUsers(string searchName, UserRole? role, int page = 1, int pageSize = 0)
        {
            pageSize = pageSize > 0 ? pageSize : options.Value.DefaultPageSize;
            ViewData["Role"] = role;
            ViewData["SearchName"] = searchName;

            var result = userService.GetAllPaged(searchName, role, page, pageSize);
            return View(result);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserVM model)
        {
            if (!ModelState.IsValid) return View(model);

            userService.Create(model);
            return RedirectToAction(nameof(GetAllUsers));
        }

        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            var updateVM = userService.GetUserForUpdate(id);
            if (updateVM == null) return NotFound();
            return View(updateVM);
        }

        [HttpPost]
        public IActionResult UpdateUser(UpdateUserVM model)
        {
            if (!ModelState.IsValid) return View(model);

            userService.Update(model);
            return RedirectToAction(nameof(GetAllUsers));
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            userService.Delete(id);
            return RedirectToAction(nameof(GetAllUsers));
        }
        public IActionResult GetByIdUser(int id)
        {
            var user = userService.GetAll().FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            return View(user);
        }
        public IActionResult CheckIfEmailUnique(string email)
        {
            if (!userService.IsEmailUnique(email))
                return Json($"Email already exists");

            return Json(true);
        }
    }
}
