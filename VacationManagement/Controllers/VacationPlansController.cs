using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationManagement.Data;
using VacationManagement.Models;

namespace VacationManagement.Controllers
{
    public class VacationPlansController : Controller
    {
        private readonly VacationDbContext _context;

        public VacationPlansController(VacationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.RequestVacations
                .Include(x=>x.Employee)
                .Include(x=>x.VacationType)
                .OrderByDescending(x=>x.RequestDate)
                .ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete(int? Id)
        {
            return View(_context.RequestVacations
                .Include(x => x.Employee)
                .Include(x => x.VacationType)
                .Include(x => x.VacationPlanList)
                .FirstOrDefault(x=>x.Id == Id));
        }

        [HttpPost]
        public IActionResult Delete(RequestVacation model)
        {
            if(model != null)
            {
                _context.RequestVacations.Remove(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
