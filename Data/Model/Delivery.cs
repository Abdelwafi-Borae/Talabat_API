using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Delivery
    {
        [Key]
        public int Id {  get; set; }

        [Required]
        public DateTime DeliveryTime { get; set; }

        public string DeliveryStatus { get; set; } = string.Empty;

        // Forgin key properties
        public int OrderId { get; set; }
        public string DeliveryPersonId { get; set; } = string.Empty;
    }

}
