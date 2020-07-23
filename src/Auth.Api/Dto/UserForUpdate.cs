using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Dto
{
    public class UserForUpdate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}