using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeamingBooks.API.Entities
{
    public partial class Author
    {
        public Author()
        {
            Book = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
