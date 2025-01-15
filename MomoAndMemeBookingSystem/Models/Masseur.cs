using System.ComponentModel.DataAnnotations;

namespace MomoAndMemeBookingSystem.Models
{
    public class Masseur
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Bio { get; set; }
    }
}
