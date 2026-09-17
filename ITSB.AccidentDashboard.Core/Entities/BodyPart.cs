using System.ComponentModel.DataAnnotations;

namespace ITSB.AccidentDashboard.Core.Entities
{
    public class BodyPart
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}