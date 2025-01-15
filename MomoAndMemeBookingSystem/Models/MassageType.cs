using System.ComponentModel.DataAnnotations;

namespace MomoAndMemeBookingSystem.Models
{
    public class MassageType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(1, 1000)]
        public decimal Price { get; set; }
    }
}
