using System.ComponentModel.DataAnnotations;

namespace ITSB.AccidentDashboard.Core.Entities
{
    public class Plant
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Plant code is required.")]
        [StringLength(10)]
        [Display(Name = "Plant Code")]
        public string PlantCode { get; set; }

        [Required(ErrorMessage = "Plant name is required")]
        [StringLength(100)]
        [Display(Name = "Plant Name")]
        public string Name { get; set; }

        public int PlantGroupId { get; set; }
        public virtual PlantGroup PlantGroup { get; set; }
    }
}