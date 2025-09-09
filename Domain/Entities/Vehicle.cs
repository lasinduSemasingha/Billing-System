using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        [ForeignKey("VehicleOwner")]
        public int OwnerId { get; set; }
        [StringLength(50)]
        public string LicensePlate { get; set; } = string.Empty;
        [StringLength(50)]
        public string? Make { get; set; } = string.Empty;
        [StringLength(50)]
        public string? Model { get; set; } = string.Empty;
        public int? Year { get; set; }
        [StringLength(17)]
        public string? VIN { get; set; } = string.Empty;
        public int? Mileage { get; set; }
        public VehicleOwner VehicleOwner { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
        public ICollection<TechnicianNote> TechnicianNotes { get; set; }
        public ICollection<JobCard> JobCards { get; set; }
    }
}
