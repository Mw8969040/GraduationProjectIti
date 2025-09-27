using itism.bll.Services.Interface;
using itism.dal.Models;
using Microsoft.AspNetCore.Mvc;

namespace itism.ui.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        public IActionResult Index()
        {
            var sessions = sessionService.GetAll();
            return View(sessions);
        }

        public IActionResult Details(int id)
        {
            var session = sessionService.GetById(id);
            if (session == null) return NotFound();
            return View(session);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Session());
        }

        [HttpPost]
        public IActionResult Create(Session session)
        {
            if (!ModelState.IsValid)
            {
               
                var errors = ModelState.Values
                                       .SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

               
                ViewBag.Errors = errors;

                return View(session);
            }

            sessionService.Create(session);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var session = sessionService.GetById(id);
            if (session == null) return NotFound();
            return View(session);
        }

        [HttpPost]
        public IActionResult Edit(Session session)
        {
            if (!ModelState.IsValid) return View(session);
            sessionService.Update(session);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            sessionService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}


