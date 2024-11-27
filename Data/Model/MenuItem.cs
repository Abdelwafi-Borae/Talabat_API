using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty ;

        [Required]
        public double Price { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;

        // Navigation Property

        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }

        public Restaurant? Restaurant { get; set; }
    }
}
