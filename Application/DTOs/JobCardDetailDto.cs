using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class JobCardDetailDto
    {
        public int JobCardID { get; set; }
        public int? VehicleID { get; set; }
        public string? LisencePlate { get; set; }
        public int? MechanicID { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public bool? IsCompleted { get; set; }
        public int? Mileage { get; set; }
        public string? Notes { get; set; }

        public List<JobCardPartsDto> Parts { get; set; } = new();
        public List<JobCardServiceDto> Services { get; set; } = new();
    }
}
