using System.ComponentModel.DataAnnotations;

namespace ITSB.AccidentDashboard.Core.Entities
{
    public class MonthlyManHours
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Plant is required")]
        public int PlantId { get; set; }
        public virtual Plant Plant { get; set; }

        [Required]
        [Range(2000, 2100)]
        public int Year { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Direct Labor Headcount")]
        public int DirectLaborHeadcount { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Direct Labor Hours")]
        public int DirectLaborHours { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Indirect Labor Headcount")]
        public int IndirectLaborHeadcount { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Indirect Labor Hours")]
        public int IndirectLaborHours { get; set; }
    }
}