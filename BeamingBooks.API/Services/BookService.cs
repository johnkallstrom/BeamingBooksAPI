using BeamingBooks.API.Data;
using BeamingBooks.API.Entities;
using BeamingBooks.API.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeamingBooks.API.Services
{
    public class BookService : IBookService
    {
        private readonly BeamingBooksContext _context;

        public BookService(BeamingBooksContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToList();
        }

        public IEnumerable<Book> GetBooks(
            BookResourceParameters bookResourceParameters)
        {
            if (bookResourceParameters == null) throw new ArgumentNullException(nameof(bookResourceParameters));

            if (string.IsNullOrWhiteSpace(bookResourceParameters.SearchQuery) 
                && string.IsNullOrWhiteSpace(bookResourceParameters.Title) 
                && string.IsNullOrWhiteSpace(bookResourceParameters.Author) 
                && string.IsNullOrWhiteSpace(bookResourceParameters.Genre))
            {
                if (!bookResourceParameters.Rating.HasValue 
                    && !bookResourceParameters.Published.HasValue)
                {
                    return GetBooks();
                }
            }

            var collection = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre) as IQueryable<Book>;

            if (!string.IsNullOrWhiteSpace(bookResourceParameters.SearchQuery))
            {
                var searchQuery = bookResourceParameters.SearchQuery.Trim();
                collection = collection.Where(b => b.Title.Contains(searchQuery) 
                || b.Author.Name.Contains(searchQuery) 
                || b.Genre.Name.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(bookResourceParameters.Title))
            {
                var title = bookResourceParameters.Title.Trim();
                collection = collection.Where(b => b.Title == title);
            }

            if (!string.IsNullOrWhiteSpace(bookResourceParameters.Author))
            {
                var author = bookResourceParameters.Author.Trim();
                collection = collection.Where(b => b.Author.Name == author);
            }

            if (!string.IsNullOrWhiteSpace(bookResourceParameters.Genre))
            {
                var genre = bookResourceParameters.Genre.Trim();
                collection = collection.Where(b => b.Genre.Name == genre);
            }

            if (bookResourceParameters.Rating.HasValue)
            {
                collection = collection.Where(b => b.Rating == bookResourceParameters.Rating);
            }

            if (bookResourceParameters.Published.HasValue)
            {
                collection = collection.Where(b => b.Published.Year == bookResourceParameters.Published);
            }

            return collection.ToList();
        }

        public Book GetBook(int bookId)
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefault(b => b.Id == bookId);
        }

        public void AddBook(int authorId, Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            book.AuthorId = authorId;

            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void DeleteBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}