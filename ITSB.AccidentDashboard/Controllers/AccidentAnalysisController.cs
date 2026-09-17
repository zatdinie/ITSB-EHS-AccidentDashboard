using System;
using System.Web.Mvc;
using ITSB.AccidentDashboard.Core.Repositories;
using ITSB.AccidentDashboard.Web.Models;

namespace ITSB.AccidentDashboard.Web.Controllers
{
    public class AccidentAnalysisController : Controller
    {
        private readonly AccidentAnalysisRepository _repo = new AccidentAnalysisRepository();

        public ActionResult Index(int? year)
        {
            int y = year ?? DateTime.Now.Year;
            ViewBag.Year = y;

            var vm = new AccidentAnalysisPageViewModel
            {
                Year = y,
                CauseByPlant = _repo.GetCauseByPlantMatrix(y),
                Occupational = _repo.GetAnalysis(y),
                MonthlyInjury = _repo.GetInjuryTypeByMonth(y)
            };
            return View(vm);
        }
    }
}