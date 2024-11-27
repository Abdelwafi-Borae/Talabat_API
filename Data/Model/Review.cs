using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public DateTime ReviewDate { get; set; }

        // Foregin key properties
        public string UserId { get; set; } = string.Empty;
        public int RestaurantId { get; set; }
    }
}
