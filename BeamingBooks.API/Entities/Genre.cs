using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeamingBooks.API.Entities
{
    public partial class Genre
    {
        public Genre()
        {
            Book = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
