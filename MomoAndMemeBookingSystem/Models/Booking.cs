using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MomoAndMemeBookingSystem.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int MassageTypeId { get; set; }
        [ForeignKey("MassageTypeId")]
        public MassageType? MassageType { get; set; }

        [Required]
        public int MasseurId { get; set; }
        [ForeignKey("MasseurId")]
        public Masseur? Masseur { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;
    }
}
