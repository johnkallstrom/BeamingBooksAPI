using System.ComponentModel.DataAnnotations;

namespace BeamingBooks.API.Models
{
    public class AuthenticateRequest
    {
        [Required(ErrorMessage = "Please enter email.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
    }
}
