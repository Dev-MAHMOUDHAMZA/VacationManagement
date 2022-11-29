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
        public IActionResult Edit(int? Id)
        {
            ViewBag.Employees = _context.Employees.OrderBy(x => x.Name).ToList();
            ViewBag.VactionTypes = _context.VacationTypes.OrderBy(x => x.VacationName).ToList();
            return View(_context.RequestVacations
                .Include(x=>x.Employee)
                .Include(x=>x.VacationType)
                .Include(x=>x.VacationPlanList).FirstOrDefault(x=>x.Id==Id));
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(RequestVacation model)
        {
            if (ModelState.IsValid)
            {
                if (model.Approved == true)
                    model.DateApproved = DateTime.Now;
                _context.RequestVacations.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employees = _context.Employees.OrderBy(x => x.Name).ToList();
            ViewBag.VactionTypes = _context.VacationTypes.OrderBy(x => x.VacationName).ToList();
            return View(model);
        }
        public IActionResult GetCountVacationEmployee(int Id)
        {
            return Json(_context.VacationPlans.Where(x=>x.RequestVacationId == Id).Count());
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

        public IActionResult ViewReportVactionPlan()
        {
            ViewBag.Employees = _context.Employees.ToList();
            return View();
        }

        public IActionResult ViewReportVactionPlan2()
        {
            ViewBag.Employees = _context.Employees.ToList();
            return View();
        }
        public IActionResult GetReportVactionPlan
                (int EmployeeId,DateTime FromDate,DateTime ToDate)
        {
            string Id = "";
            if (EmployeeId != 0 && EmployeeId.ToString() != "")
                Id = $"and Employees.Id={EmployeeId}";
            #region MyRegion
            //      var sqlQuery = _context.SqlDataTable($@"SELECT Distinct dbo.Employees.Id, dbo.Employees.Name,
            //            dbo.Employees.VacationBalance,
            //Sum(dbo.VacationTypes.NumberDays) As TotalVacation,
            //dbo.Employees.VacationBalance - Sum(dbo.VacationTypes.NumberDays) As Total
            //            FROM dbo.Employees INNER JOIN
            //            dbo.RequestVacations ON dbo.Employees.Id = dbo.RequestVacations.EmployeeId INNER JOIN
            //            dbo.VacationPlans ON dbo.RequestVacations.Id = dbo.VacationPlans.RequestVacationId INNER JOIN
            //            dbo.VacationTypes ON dbo.RequestVacations.VacationTypeId = dbo.VacationTypes.Id
            //   where VacationPlans.VacationDate between 
            //            '" + FromDate.ToString("yyyy-MM-dd") + "' and '" + ToDate.ToString("yyyy-MM-dd") + "' "+
            //            " and RequestVacations.Approved = 'True'" +
            //            $"{Id} Group By dbo.Employees.Id, dbo.Employees.Name, dbo.Employees.VacationBalance"); 
            #endregion

            #region MyRegion
            //      string sqlQuery =$@"SELECT Distinct dbo.Employees.Id, dbo.Employees.Name,
            //            dbo.Employees.VacationBalance,
            //Sum(dbo.VacationTypes.NumberDays) As TotalVacation,
            //dbo.Employees.VacationBalance - Sum(dbo.VacationTypes.NumberDays) As Total
            //            FROM dbo.Employees INNER JOIN
            //            dbo.RequestVacations ON dbo.Employees.Id = dbo.RequestVacations.EmployeeId INNER JOIN
            //            dbo.VacationPlans ON dbo.RequestVacations.Id = dbo.VacationPlans.RequestVacationId INNER JOIN
            //            dbo.VacationTypes ON dbo.RequestVacations.VacationTypeId = dbo.VacationTypes.Id
            //   where VacationPlans.VacationDate between 
            //            '" + FromDate.ToString("yyyy-MM-dd") + "' and '" + ToDate.ToString("yyyy-MM-dd") + "' " +
            //            " and RequestVacations.Approved = 'True'" +
            //            $"{Id} Group By dbo.Employees.Id, dbo.Employees.Name, dbo.Employees.VacationBalance"; 
            #endregion


            //var SpGetData = _context.SpGetReportVacationPlans.FromSqlRaw(sqlQuery).ToList();

            var SpGetData = _context.SpGetReportVacationPlans
                        .FromSqlRaw("SpGetReportVacationPlans {0},{1},{2}", EmployeeId, FromDate, ToDate).ToList();

            ViewBag.Employees = _context.Employees.ToList();
            return View("ViewReportVactionPlan2", SpGetData);
        }
    }
}
