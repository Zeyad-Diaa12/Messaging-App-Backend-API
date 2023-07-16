using System.ComponentModel.DataAnnotations;

namespace SocialApi.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
