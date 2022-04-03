using Microsoft.AspNetCore.Mvc;
using VacationManagement.Data;
using VacationManagement.Models;

namespace VacationManagement.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly VacationDbContext _context;

        public DepartmentsController(VacationDbContext context)
        {
            _context = context;
        }
        public IActionResult Departments()
        {
            return View(_context.Departments.OrderBy(x=>x.Name).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department model)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Departments");
            }
            return View(model);
        }
        public IActionResult Edit(int? Id)
        {
            return View(_context.Departments.FirstOrDefault(x => x.Id == Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Departments");
            }
            return View(model);
        }

        public IActionResult Delete(int? Id)
        {
            return View(_context.Departments.FirstOrDefault(x => x.Id == Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department model)
        {
            if(model != null)
            {
                _context.Departments.Remove(model);
                _context.SaveChanges();
                return RedirectToAction("Departments");
            }    
            return View(model);
        }
    }
}
