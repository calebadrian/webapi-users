using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
    public class RegisterUserModel
    {
        [MaxLength(20)]
        public string Username { get; set; }
        [MaxLength(255), EmailAddress]
        public string Email { get; set; }
        public string profilePic { get; set; }
        [MinLength(4)]
        public string Password { get; set; }
    }
}