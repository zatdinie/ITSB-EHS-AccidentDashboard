using System.Web.Mvc;
using ITSB.AccidentDashboard.Core.Entities;
using ITSB.AccidentDashboard.Core.Repositories;

namespace ITSB.AccidentDashboard.Web.Controllers
{
    public class AccidentCauseController : Controller
    {
        private readonly AccidentCauseRepository _repo = new AccidentCauseRepository();

        public ActionResult Index() => View(_repo.GetAll());
        public ActionResult Create() => View(new AccidentCause());

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(AccidentCause entity)
        {
            if (!ModelState.IsValid) return View(entity);
            _repo.Add(entity);
            TempData["Success"] = "Accident cause created.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return HttpNotFound();
            return View(entity);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(AccidentCause entity)
        {
            if (!ModelState.IsValid) return View(entity);
            _repo.Update(entity);
            TempData["Success"] = "Accident cause updated.";
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
            TempData["Success"] = "Accident cause deleted.";
            return RedirectToAction("Index");
        }
    }
}