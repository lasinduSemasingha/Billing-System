namespace Application.DTOs
{
    public class JobCardPartDto
    {
        public int PartId { get; set; }
        public int Quantity { get; set; }
    }

    public class JobCardServiceUpdateDto
    {
        public int ServiceId { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? Remarks { get; set; }
    }

    public class JobCardDto
    {
        public int VehicleID { get; set; }
        public int? MechanicID { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public int? Mileage { get; set; }
        public decimal? LabourCost { get; set; }
        public decimal? LabourHours { get; set; }
        public string? AdvisoryReport { get; set; }
        public string? Notes { get; set; }

        public List<int> ServiceIds { get; set; } = new();
        public List<JobCardPartDto> Parts { get; set; } = new();
    }

    public class JobCardUpdateDto
    {
        public DateTime? DateOut { get; set; }
        public decimal? LabourCost { get; set; }
        public decimal? LabourHours { get; set; }
        public List<JobCardPartDto>? Parts { get; set; }
        public List<JobCardServiceUpdateDto>? Services { get; set; }
        public string? Notes { get; set; }
        public string? AdvisoryReport { get; set; }
    }
}
