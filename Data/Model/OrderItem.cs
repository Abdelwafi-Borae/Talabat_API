using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }
        public double price { get; set; }

        // Foreign key properties
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
    }
}
