using itism.bll.Services.Interface;
using itism.dal.Models;
using Microsoft.AspNetCore.Mvc;

namespace itism.ui.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService gradeService;

        public GradeController(IGradeService gradeService)
        {
            this.gradeService = gradeService;
        }

        public IActionResult Index()
        {
            var grades = gradeService.GetAll();
            return View(grades);
        }

        public IActionResult Details(int id)
        {
            var grade = gradeService.GetById(id);
            if (grade == null) return NotFound();
            return View(grade);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Grade());
        }

        [HttpPost]
        public IActionResult Create(Grade grade)
        {
            if (!ModelState.IsValid) return View(grade);
            gradeService.Create(grade);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var grade = gradeService.GetById(id);
            if (grade == null) return NotFound();
            return View(grade);
        }

        [HttpPost]
        public IActionResult Edit(Grade grade)
        {
            if (!ModelState.IsValid) return View(grade);
            gradeService.Update(grade);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            gradeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}


