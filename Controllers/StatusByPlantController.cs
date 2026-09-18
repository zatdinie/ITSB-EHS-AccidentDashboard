using System;
using System.Web.Mvc;
using ITSB.AccidentDashboard.Core.Repositories;
using ITSB.AccidentDashboard.Web.Models;

namespace ITSB.AccidentDashboard.Web.Controllers
{
    public class StatusByPlantController : Controller
    {
        private readonly StatusByPlantRepository _repo = new StatusByPlantRepository();
        private readonly AccidentAnalysisRepository _analysisRepo = new AccidentAnalysisRepository();

        public ActionResult Index(int? year)
        {
            int y = year ?? DateTime.Now.Year;
            ViewBag.Year = y;

            var vm = new StatusByPlantViewModel
            {
                Year = y,
                PlantStatus = _repo.GetStatus(y),
                ByBodyPart = _analysisRepo.GetAnalysis(y).ByBodyPart,
                MonthlyBreakdown = _repo.GetMonthlyBreakdown(y)
            };
            return View(vm);
        }
    }
}