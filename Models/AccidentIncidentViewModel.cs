using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ITSB.AccidentDashboard.Web.Models
{
    public class AccidentIncidentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Worker name is required")]
        [Display(Name = "Worker Name")]
        public string WorkerName { get; set; }

        [Display(Name = "IC/Passport No")]
        public string IcPassportNo { get; set; }

        [Required]
        [Display(Name = "Employee ID")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "Plant is required")]
        [Display(Name = "Plant")]
        public int PlantId { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Range(15, 100)]
        public int Age { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Employment Status")]
        public string EmploymentStatus { get; set; }

        [Required]
        [Display(Name = "Date of Occurrence")]
        [DataType(DataType.Date)]
        public DateTime DateOfOccurrence { get; set; }

        [Display(Name = "Time of Occurrence")]
        public TimeSpan TimeOfOccurrence { get; set; }

        [Required]
        [Display(Name = "How the Accident Happened")]
        [DataType(DataType.MultilineText)]
        public string HowAccidentHappened { get; set; }

        [Display(Name = "Accident Cause")]
        public int? AccidentCauseId { get; set; }

        [Display(Name = "Body Part")]
        public int? BodyPartId { get; set; }

        [Display(Name = "Injury Type")]
        public int? InjuryTypeId { get; set; }

        [Display(Name = "First Aid Case")]
        public bool IsFirstAidCase { get; set; }

        [Display(Name = "Lost Work Day Case")]
        public bool IsLostWorkDayCase { get; set; }

        [Display(Name = "Recordable Case")]
        public bool IsRecordableCase { get; set; }

        [Display(Name = "Lost Work Days")]
        public int LostWorkDays { get; set; }

        public IEnumerable<SelectListItem> PlantOptions { get; set; }
        public IEnumerable<SelectListItem> AccidentCauseOptions { get; set; }
        public IEnumerable<SelectListItem> BodyPartOptions { get; set; }
        public IEnumerable<SelectListItem> InjuryTypeOptions { get; set; }
    }
}