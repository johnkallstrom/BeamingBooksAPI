using BeamingBooks.API.Entities;
using BeamingBooks.API.ResourceParameters;
using System.Collections.Generic;

namespace BeamingBooks.API.Services
{
    public interface IGenreService
    {
        IEnumerable<Genre> GetGenres();
        IEnumerable<Genre> GetGenres(GenreResourceParameters genreResourceParameters);
        Genre GetGenre(int genreId);
        void AddGenre(Genre genre);
        void UpdateGenre(Genre genre);
    }
}
