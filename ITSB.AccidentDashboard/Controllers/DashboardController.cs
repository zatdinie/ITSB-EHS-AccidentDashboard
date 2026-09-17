using System;
using System.Linq;
using System.Web.Mvc;
using ITSB.AccidentDashboard.Core.Repositories;
using ITSB.AccidentDashboard.Web.Models;

namespace ITSB.AccidentDashboard.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardRepository _repo = new DashboardRepository();

        public ActionResult Index(int? year)
        {
            int y = year ?? DateTime.Now.Year;
            var data = _repo.GetData(y);

            var vm = new DashboardViewModel
            {
                Year = y,
                TotalAccidentCases = data.TotalAccidentCases,
                TotalFirstAidCases = data.TotalFirstAidCases,
                TotalLostWorkDay = data.TotalLostWorkDay,
                TotalRecordableCases = data.TotalRecordableCases,
                CasesByPlantGroup = data.CasesByPlantGroup.Select(g => new GroupCaseCount { DisplayName = g.DisplayName, Count = g.Count }).ToList(),
                CasesByPlant = data.CasesByPlant.Select(p => new PlantCaseCount { PlantCode = p.PlantCode, Count = p.Count }).ToList(),
                CasesByBodyPart = data.CasesByBodyPart.Select(b => new BodyPartCaseCount { Name = b.Name, Count = b.Count }).ToList(),
                CasesByMonth = data.CasesByMonth,
                RecordableCasesByMonth = data.RecordableCasesByMonth,
                HoursWorkedByMonth = data.HoursWorkedByMonth
            };
            return View(vm);
        }
    }
}