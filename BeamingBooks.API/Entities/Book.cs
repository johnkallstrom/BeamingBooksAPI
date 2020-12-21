using System;

namespace BeamingBooks.API.Entities
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Published { get; set; }
        public int Rating { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
