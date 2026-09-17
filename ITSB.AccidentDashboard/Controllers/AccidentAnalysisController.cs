using System;
using System.Web.Mvc;
using ITSB.AccidentDashboard.Core.Repositories;

namespace ITSB.AccidentDashboard.Web.Controllers
{
    public class AccidentAnalysisController : Controller
    {
        private readonly AccidentAnalysisRepository _repo = new AccidentAnalysisRepository();

        public ActionResult Index(int? year)
        {
            int y = year ?? DateTime.Now.Year;
            ViewBag.Year = y;
            return View(_repo.GetAnalysis(y));
        }
    }
}