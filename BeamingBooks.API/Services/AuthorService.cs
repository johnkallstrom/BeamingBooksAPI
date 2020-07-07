﻿using BeamingBooks.API.Data;
using BeamingBooks.API.Entities;
using BeamingBooks.API.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeamingBooks.API.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly BeamingBooksContext _context;

        public AuthorService(BeamingBooksContext context)
        {
            _context = context;
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Author
                .Include(a => a.Book)
                .ToList();
        }

        public Author GetAuthor(int authorId)
        {
            return _context.Author
                .Include(a => a.Book)
                .FirstOrDefault(a => a.Id == authorId);
        }

        public IEnumerable<Author> GetAuthors(
            AuthorResourceParameters authorResourceParameters)
        {
            if (authorResourceParameters == null) 
                throw new ArgumentNullException(nameof(authorResourceParameters));

            if (string.IsNullOrWhiteSpace(authorResourceParameters.SearchQuery) 
                && string.IsNullOrWhiteSpace(authorResourceParameters.Name) 
                && string.IsNullOrWhiteSpace(authorResourceParameters.Country))
            {
                return GetAuthors();
            }

            var collection = _context.Author.Include(a => a.Book) as IQueryable<Author>;

            if (!string.IsNullOrWhiteSpace(authorResourceParameters.SearchQuery))
            {
                var searchQuery = authorResourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(authorResourceParameters.Name))
            {
                var name = authorResourceParameters.Name.Trim();
                collection = collection.Where(a => a.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(authorResourceParameters.Country))
            {
                var country = authorResourceParameters.Country.Trim();
                collection = collection.Where(a => a.Country == country);
            }

            return collection.ToList();
        }

        public void AddAuthor(Author author)
        {
            if (author == null) throw new ArgumentNullException(nameof(author));

            _context.Author.Add(author);
            _context.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            if (author == null) throw new ArgumentNullException(nameof(author));

            _context.Author.Update(author);
            _context.SaveChanges();
        }

        public void DeleteAuthor(Author author)
        {
            if (author == null) throw new ArgumentNullException(nameof(author));

            if (author.Book.Count >= 1)
            {
                _context.RemoveRange(author.Book);
            }

            _context.Author.Remove(author);
            _context.SaveChanges();
        }

        public bool AuthorExists(int authorId)
        {
            return _context.Author.Any(a => a.Id == authorId);
        }
    }
}
