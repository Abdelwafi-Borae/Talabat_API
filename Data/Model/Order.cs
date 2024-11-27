using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        [Required]
        public string OrderStatus { get; set; } = string.Empty;   // Pending, Confirmed, Delivered

        // Navigation Property
        [ForeignKey(nameof(OrderItem))]
        public int OrderItemId { get; set; }

        public OrderItem? OrderItem { get; set; }

        // Foreign key properties
        public string UserId { get; set; } = string.Empty;
        public int RestaurantId { get; set; }
    }
}
