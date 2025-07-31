using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TechnicianNote
    {
        [Key]
        public int NoteId { get; set; }
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public string NoteText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        [ForeignKey("CreatedByUser")]
        public int CreatedBy { get; set; }
        public Vehicle Vehicle { get; set; }
        public User CreatedByUser { get; set; }
    }
}
