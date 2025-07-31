using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class VehicleOwner
    {
        [Key]
        public int OwnerId { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName {  get; set; } = string.Empty;
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;
        [StringLength(100)]
        public string Email {  get; set; } = string.Empty;
        [StringLength(200)]
        public string Address {  get; set; } = string.Empty;
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
