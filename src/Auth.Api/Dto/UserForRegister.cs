using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Dto
{
    public class UserForRegister
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(8, MinimumLength=4, ErrorMessage="You must specify a password between 4 and 8 characters.")]
        public string Password { get; set; }

        public string Claims { get; set; } = "user";
    }
}