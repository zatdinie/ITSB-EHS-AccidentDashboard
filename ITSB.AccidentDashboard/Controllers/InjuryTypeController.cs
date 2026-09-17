using System.Web.Mvc;
using ITSB.AccidentDashboard.Core.Entities;
using ITSB.AccidentDashboard.Core.Repositories;

namespace ITSB.AccidentDashboard.Web.Controllers
{
    public class InjuryTypeController : Controller
    {
        private readonly InjuryTypeRepository _repo = new InjuryTypeRepository();

        public ActionResult Index() => View(_repo.GetAll());
        public ActionResult Create() => View(new InjuryType());

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(InjuryType entity)
        {
            if (!ModelState.IsValid) return View(entity);
            _repo.Add(entity);
            TempData["Success"] = "Injury type created.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return HttpNotFound();
            return View(entity);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(InjuryType entity)
        {
            if (!ModelState.IsValid) return View(entity);
            _repo.Update(entity);
            TempData["Success"] = "Injury type updated.";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return HttpNotFound();
            return View(entity);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "Injury type deleted.";
            return RedirectToAction("Index");
        }
    }
}