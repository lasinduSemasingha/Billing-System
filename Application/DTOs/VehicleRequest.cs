using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VehicleRequest
    {
        public int VehicleId { get; set; }
        public int OwnerId { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string? Make { get; set; } = string.Empty;
        public string? Model { get; set; } = string.Empty;
        public int? Year { get; set; }
        public string? VIN { get; set; } = string.Empty;
        public int? Mileage { get; set; }
    }
}
