using System.ComponentModel.DataAnnotations;

namespace ITSB.AccidentDashboard.Core.Entities
{
    public class AccidentCause
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(200)]
        public string Description { get; set; }
    }
}