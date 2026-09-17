using System;
using System.Web.Mvc;
using ITSB.AccidentDashboard.Core.Repositories;

namespace ITSB.AccidentDashboard.Web.Controllers
{
    public class LWRController : Controller
    {
        private readonly LwrRepository _repo = new LwrRepository();

        public ActionResult Index(int? year)
        {
            int y = year ?? DateTime.Now.Year;
            ViewBag.Year = y;
            return View(_repo.GetRates(y));
        }
    }
}