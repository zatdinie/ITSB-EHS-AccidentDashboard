using System.ComponentModel.DataAnnotations;

namespace ITSB.AccidentDashboard.Core.Entities
{
    public class PlantGroup
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Label { get; set; }

        [StringLength(100)]
        public string DisplayName { get; set; }
    }
}