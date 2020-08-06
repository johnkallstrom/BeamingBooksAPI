using System;
using System.ComponentModel.DataAnnotations;

namespace BeamingBooks.API.Models
{
    public class UpdateBookDto
    {
        [Required(ErrorMessage = "Please enter a title.")]
        [MaxLength(50, ErrorMessage = "The title shouldn't have more than 50 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a published date.")]
        public DateTime Published { get; set; }

        [Required(ErrorMessage = "Please enter a rating.")]
        [Range(1, 10, ErrorMessage = "Please enter a rating between 1 and 10.")]
        public int Rating { get; set; }
    }
}
