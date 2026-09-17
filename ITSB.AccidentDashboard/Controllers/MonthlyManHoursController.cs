using System.Linq;
using System.Web.Mvc;
using ITSB.AccidentDashboard.Core.Entities;
using ITSB.AccidentDashboard.Core.Repositories;

namespace ITSB.AccidentDashboard.Web.Controllers
{
    public class MonthlyManHoursController : Controller
    {
        private readonly MonthlyManHoursRepository _repo = new MonthlyManHoursRepository();
        private readonly LookupRepository _lookupRepo = new LookupRepository();

        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public ActionResult Create()
        {
            ViewBag.Plants = new SelectList(_lookupRepo.GetPlants(), "Id", "PlantCode");
            return View(new MonthlyManHours { Year = System.DateTime.Now.Year });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MonthlyManHours entry)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Plants = new SelectList(_lookupRepo.GetPlants(), "Id", "PlantCode", entry.PlantId);
                return View(entry);
            }
            _repo.Add(entry);
            TempData["Success"] = "Man-hours entry saved.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var entry = _repo.GetById(id);
            if (entry == null) return HttpNotFound();
            ViewBag.Plants = new SelectList(_lookupRepo.GetPlants(), "Id", "PlantCode", entry.PlantId);
            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MonthlyManHours entry)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Plants = new SelectList(_lookupRepo.GetPlants(), "Id", "PlantCode", entry.PlantId);
                return View(entry);
            }
            _repo.Update(entry);
            TempData["Success"] = "Man-hours entry updated.";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var entry = _repo.GetById(id);
            if (entry == null) return HttpNotFound();
            return View(entry);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "Entry deleted.";
            return RedirectToAction("Index");
        }
    }
}