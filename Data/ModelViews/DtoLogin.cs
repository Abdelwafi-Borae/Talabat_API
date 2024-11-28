using System.ComponentModel.DataAnnotations;

namespace Data.ModelViews
{
    public class DtoLogin
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
