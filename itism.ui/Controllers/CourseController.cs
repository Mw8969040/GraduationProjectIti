using itism.bll.Services.Interface;
using itism.bll.ViewModels;
using itism.dal.Models;
using itism.ui.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.ui.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IOptions<PageSettings> options;

        public CourseController(ICourseService courseService, IOptions<PageSettings> options)
        {
            this.courseService = courseService;
            this.options = options;
        }

        public IActionResult Index(string searchName, Category? category, int page = 1, int pageSize = 0)
        {
            pageSize = pageSize > 0 ? pageSize : options.Value.DefaultPageSize;
            ViewData["Category"] = category;
            ViewData["SearchName"] = searchName;

            var result = courseService.GetAllPaged(searchName, category, page, pageSize);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Instructors = courseService.GetInstructorsSelectList();
            return View(new CreateCourseVM());
        }

        [HttpPost]
        public IActionResult Create(CreateCourseVM model)
        {
            // Debug: Log the model data
            Console.WriteLine($"Creating course: Name={model.Name}, Category={model.Category}, InstructorId={model.InstructorId}");
            
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model state is invalid:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                ViewBag.Instructors = courseService.GetInstructorsSelectList();
                return View(model);
            }
            
            try
            {
                courseService.Create(model);
                Console.WriteLine("Course created successfully, redirecting to index");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in Create: {ex.Message}");
                ModelState.AddModelError("", $"Error creating course: {ex.Message}");
                ViewBag.Instructors = courseService.GetInstructorsSelectList();
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vm = courseService.GetForUpdate(id);
            if (vm == null) return NotFound();
            ViewBag.Instructors = courseService.GetInstructorsSelectList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(UpdateCourseVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Instructors = courseService.GetInstructorsSelectList();
                return View(model);
            }
            courseService.Update(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            courseService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var course = courseService.GetAll().FirstOrDefault(c => c.Id == id);
            if (course == null) return NotFound();
            return View(course);
        }

        public IActionResult IsNameUnique(string name, int? id)
        {
            var isUnique = courseService.IsNameUnique(name, id);
            if (!isUnique)
                return Json($"Course name already exists");
            return Json(true);
        }
    }
}



