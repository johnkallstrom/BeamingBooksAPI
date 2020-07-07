﻿namespace BeamingBooks.API.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Published { get; set; }
        public int Rating { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
    }
}
