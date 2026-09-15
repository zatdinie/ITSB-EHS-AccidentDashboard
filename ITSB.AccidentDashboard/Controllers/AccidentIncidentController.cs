using System.Linq;
using System.Web.Mvc;
using ITSB.AccidentDashboard.Core.Entities;
using ITSB.AccidentDashboard.Core.Repositories;
using ITSB.AccidentDashboard.Web.Models;

namespace ITSB.AccidentDashboard.Web.Controllers
{
    public class AccidentIncidentController : Controller
    {
        private readonly AccidentIncidentRepository _repo = new AccidentIncidentRepository();
        private readonly LookupRepository _lookupRepo = new LookupRepository();

        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public ActionResult Create()
        {
            var vm = new AccidentIncidentViewModel { DateOfOccurrence = System.DateTime.Today };
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccidentIncidentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(vm);
                return View(vm);
            }

            var incident = MapToEntity(vm);
            _repo.Add(incident);
            TempData["Success"] = "Accident incident recorded successfully.";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var incident = _repo.GetById(id);
            if (incident == null) return HttpNotFound();

            var vm = MapToViewModel(incident);
            PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccidentIncidentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(vm);
                return View(vm);
            }

            _repo.Update(MapToEntity(vm));
            TempData["Success"] = "Accident incident updated successfully.";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var incident = _repo.GetById(id);
            if (incident == null) return HttpNotFound();
            return View(incident);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            TempData["Success"] = "Accident incident deleted.";
            return RedirectToAction("Index");
        }

        private void PopulateDropdowns(AccidentIncidentViewModel vm)
        {
            vm.PlantOptions = _lookupRepo.GetPlants()
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.PlantCode + " - " + p.Name });
            vm.AccidentCauseOptions = _lookupRepo.GetAccidentCauses()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Description });
            vm.BodyPartOptions = _lookupRepo.GetBodyParts()
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name });
            vm.InjuryTypeOptions = _lookupRepo.GetInjuryTypes()
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
        }

        private AccidentIncident MapToEntity(AccidentIncidentViewModel vm)
        {
            return new AccidentIncident
            {
                Id = vm.Id,
                WorkerName = vm.WorkerName,
                IcPassportNo = vm.IcPassportNo,
                EmployeeId = vm.EmployeeId,
                PlantId = vm.PlantId,
                Gender = vm.Gender,
                Age = vm.Age,
                Nationality = vm.Nationality,
                EmploymentStatus = vm.EmploymentStatus,
                DateOfOccurrence = vm.DateOfOccurrence,
                TimeOfOccurrence = vm.TimeOfOccurrence,
                HowAccidentHappened = vm.HowAccidentHappened,
                AccidentCauseId = vm.AccidentCauseId,
                BodyPartId = vm.BodyPartId,
                InjuryTypeId = vm.InjuryTypeId,
                IsFirstAidCase = vm.IsFirstAidCase,
                IsLostWorkDayCase = vm.IsLostWorkDayCase,
                IsRecordableCase = vm.IsRecordableCase,
                LostWorkDays = vm.LostWorkDays
            };
        }

        private AccidentIncidentViewModel MapToViewModel(AccidentIncident i)
        {
            return new AccidentIncidentViewModel
            {
                Id = i.Id,
                WorkerName = i.WorkerName,
                IcPassportNo = i.IcPassportNo,
                EmployeeId = i.EmployeeId,
                PlantId = i.PlantId,
                Gender = i.Gender,
                Age = i.Age,
                Nationality = i.Nationality,
                EmploymentStatus = i.EmploymentStatus,
                DateOfOccurrence = i.DateOfOccurrence,
                TimeOfOccurrence = i.TimeOfOccurrence,
                HowAccidentHappened = i.HowAccidentHappened,
                AccidentCauseId = i.AccidentCauseId,
                BodyPartId = i.BodyPartId,
                InjuryTypeId = i.InjuryTypeId,
                IsFirstAidCase = i.IsFirstAidCase,
                IsLostWorkDayCase = i.IsLostWorkDayCase,
                IsRecordableCase = i.IsRecordableCase,
                LostWorkDays = i.LostWorkDays
            };
        }
    }
}