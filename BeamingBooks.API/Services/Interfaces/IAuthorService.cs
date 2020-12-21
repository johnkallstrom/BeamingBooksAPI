using BeamingBooks.API.Entities;
using BeamingBooks.API.ResourceParameters;
using System.Collections.Generic;

namespace BeamingBooks.API.Services
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAuthors();
        IEnumerable<Author> GetAuthors(AuthorResourceParameters authorResourceParameters);
        Author GetAuthor(int authorId);
        void AddAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
        bool AuthorExists(int authorId);
    }
}
