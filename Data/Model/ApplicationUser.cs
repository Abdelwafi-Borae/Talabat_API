using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
