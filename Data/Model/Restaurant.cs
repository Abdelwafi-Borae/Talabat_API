using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public TimeOnly OpeningHours { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        // Navigation Property 
        public IEnumerable<MenuItem>? MenuItems { get; set; }
    }
}

