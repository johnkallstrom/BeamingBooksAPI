using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeamingBooks.API.Entities
{
    public partial class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public DateTime Published { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }

        public int AuthorId { get; set; }
        public int GenreId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
        
        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }
    }
}
