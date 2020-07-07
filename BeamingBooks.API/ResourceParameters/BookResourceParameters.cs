namespace BeamingBooks.API.ResourceParameters
{
    public class BookResourceParameters
    {
        public string SearchQuery { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int? Published { get; set; }
        public int? Rating { get; set; }
    }
}
