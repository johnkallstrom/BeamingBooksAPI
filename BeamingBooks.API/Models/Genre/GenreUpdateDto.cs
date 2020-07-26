using System.ComponentModel.DataAnnotations;

namespace BeamingBooks.API.Models
{
    public class GenreUpdateDto
    {
        [Required(ErrorMessage = "Please enter a name.")]
        [MaxLength(50, ErrorMessage = "The name shouldn't have more than 50 characters.")]
        public string Name { get; set; }
    }
}
