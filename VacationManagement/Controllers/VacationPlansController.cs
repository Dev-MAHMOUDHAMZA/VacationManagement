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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VacationPlan model, int[] DayOfWeekCheckbox)
        {
            if (ModelState.IsValid) 
            {
                var Result = _context.VacationPlans
                    .Where(x => x.RequestVacation.EmployeeId == model.RequestVacation.EmployeeId
                    && x.VacationDate >= model.RequestVacation.StartDate
                    && x.VacationDate <= model.RequestVacation.EndDate).FirstOrDefault();
                if(Result != null)
                {
                    ViewBag.ErrorVacation = false;
                    return View(model);
                }

                for (DateTime date = model.RequestVacation.StartDate;
                    date <= model.RequestVacation.EndDate; date = date.AddDays(1))
                {
                    if (Array.IndexOf(DayOfWeekCheckbox,(int)date.DayOfWeek) != -1)
                    {
                        model.Id = 0;
                        model.VacationDate = date;
                        model.RequestVacation.RequestDate = DateTime.Now;
                        _context.VacationPlans.Add(model);
                        _context.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult GetVacationTypes()
        {
            return Json(_context.VacationTypes.OrderBy(x=>x.Id).ToList());
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
