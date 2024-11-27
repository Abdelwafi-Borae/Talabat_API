using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string PaymentStatus { get; set; } = string.Empty;

        // Foregin key properties
        public int OrderId { get; set; }
    }

    
}
