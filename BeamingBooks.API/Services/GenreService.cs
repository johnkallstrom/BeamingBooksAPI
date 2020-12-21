using BeamingBooks.API.Data;
using BeamingBooks.API.Entities;
using BeamingBooks.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeamingBooks.API.Services
{
    public class GenreService : IGenreService
    {
        private readonly BeamingBooksContext _context;

        public GenreService(BeamingBooksContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public IEnumerable<Genre> GetGenres(
            GenreResourceParameters genreResourceParameters)
        {
            if (genreResourceParameters == null) throw new ArgumentNullException(nameof(genreResourceParameters));

            if (string.IsNullOrWhiteSpace(genreResourceParameters.SearchQuery) 
                && string.IsNullOrWhiteSpace(genreResourceParameters.Name))
            {
                return GetGenres();
            }

            var collection = _context.Genres as IQueryable<Genre>;

            if (!string.IsNullOrWhiteSpace(genreResourceParameters.SearchQuery))
            {
                var searchQuery = genreResourceParameters.SearchQuery.Trim();
                collection = collection.Where(g => g.Name.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(genreResourceParameters.Name))
            {
                var name = genreResourceParameters.Name.Trim();
                collection = collection.Where(g => g.Name == name);
            }

            return collection.ToList();
        }

        public Genre GetGenre(int genreId)
        {
            return _context.Genres.FirstOrDefault(g => g.Id == genreId);
        }

        public void AddGenre(Genre genre)
        {
            if (genre == null) throw new ArgumentNullException(nameof(genre));

            _context.Add(genre);
            _context.SaveChanges();
        }

        public void UpdateGenre(Genre genre)
        {
            if (genre == null) throw new ArgumentNullException(nameof(genre));

            _context.Update(genre);
            _context.SaveChanges();
        }
    }
}
