using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeamingBooks.API.Models
{
    public class CreateAuthorDto
    {
        [Required(ErrorMessage = "Please enter a name.")]
        [MaxLength(50, ErrorMessage = "The name shouldn't have more than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a birthday date.")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Please enter a country.")]
        [MaxLength(50, ErrorMessage = "The country shouldn't have more than 50 characters.")]
        public string Country { get; set; }

        public ICollection<CreateBookDto> Books { get; set; } = new List<CreateBookDto>();
    }
}
