using System.Web.Mvc;
using ITSB.AccidentDashboard.Core.Entities;
using ITSB.AccidentDashboard.Core.Repositories;

namespace ITSB.AccidentDashboard.Web.Controllers
{
    public class PlantController : Controller
    {
        private readonly PlantRepository _repo = new PlantRepository();

        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public ActionResult Create()
        {
            return View(new Plant());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Plant plant)
        {
            if (!ModelState.IsValid) return View(plant);
            _repo.Add(plant);
            TempData["Success"] = "Plant created successfully.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var plant = _repo.GetById(id);
            if (plant == null) return HttpNotFound();
            return View(plant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Plant plant)
        {
            if (!ModelState.IsValid) return View(plant);
            _repo.Update(plant);
            TempData["Success"] = "Plant updated successfully.";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var plant = _repo.GetById(id);
            if (plant == null) return HttpNotFound();
            return View(plant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "Plant deleted.";
            return RedirectToAction("Index");
        }
    }
}