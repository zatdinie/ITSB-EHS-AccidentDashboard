using System;
using System.Web.Mvc;
using ITSB.AccidentDashboard.Core.Repositories;

namespace ITSB.AccidentDashboard.Web.Controllers
{
    public class StatusByPlantController : Controller
    {
        private readonly StatusByPlantRepository _repo = new StatusByPlantRepository();

        public ActionResult Index(int? year)
        {
            int y = year ?? DateTime.Now.Year;
            ViewBag.Year = y;
            return View(_repo.GetStatus(y));
        }
    }
}