using System.Web.Mvc;
using ITSB.AccidentDashboard.Core.Entities;
using ITSB.AccidentDashboard.Core.Repositories;

namespace ITSB.AccidentDashboard.Web.Controllers
{
    public class BodyPartController : Controller
    {
        private readonly BodyPartRepository _repo = new BodyPartRepository();

        public ActionResult Index() => View(_repo.GetAll());
        public ActionResult Create() => View(new BodyPart());

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(BodyPart entity)
        {
            if (!ModelState.IsValid) return View(entity);
            _repo.Add(entity);
            TempData["Success"] = "Body part created.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return HttpNotFound();
            return View(entity);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(BodyPart entity)
        {
            if (!ModelState.IsValid) return View(entity);
            _repo.Update(entity);
            TempData["Success"] = "Body part updated.";
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
            TempData["Success"] = "Body part deleted.";
            return RedirectToAction("Index");
        }
    }
}