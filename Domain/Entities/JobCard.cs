using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class JobCard
    {
        [Key]
        public int JobCardID { get; set; }
        public int? VehicleID { get; set; }
        public int? MechanicID { get; set; }

        public DateTime? DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public int? Mileage { get; set; }
        public string? AdvisoryReport { get; set; }
        public decimal? LabourHours { get; set; }
        public decimal? LabourCost { get; set; }
        public decimal? PartsCost { get; set; }
        public decimal? TotalCost { get; set; }
        public string? Notes { get; set; }
        public bool? IsCompleted { get; set; }

        // Navigation Properties
        public Vehicle Vehicle { get; set; }
        public User Mechanic { get; set; }
        public ICollection<Domain.Entities.JobCardService> JobCardServices { get; set; } = new List<JobCardService>();
        public ICollection<Domain.Entities.JobCardPart> JobCardParts { get; set; } = new List<JobCardPart>();
    }
}
