using System.ComponentModel.DataAnnotations;

namespace BeamingBooks.API.Models
{
    public class AuthenticateRequest
    {
        [Required(ErrorMessage = "Please enter a username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; }
    }
}
