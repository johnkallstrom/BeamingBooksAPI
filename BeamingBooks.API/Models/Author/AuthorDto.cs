using System.Collections.Generic;

namespace BeamingBooks.API.Models
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Born { get; set; }
        public string Country { get; set; }

        public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
    }
}
