using BeamingBooks.API.Entities;
using BeamingBooks.API.ResourceParameters;
using System.Collections.Generic;

namespace BeamingBooks.API.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> GetBooks(BookResourceParameters bookResourceParameters);
        Book GetBook(int bookId);
        void AddBook(int authorId, Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
    }
}
