using System.ComponentModel.DataAnnotations;

namespace BeamingBooks.API.Models.Account
{
    public class RegisterAccountDto
    {
        [Required(ErrorMessage = "Please enter first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter email.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [MinLength(6, ErrorMessage = "The password must be atleast 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm password.")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
