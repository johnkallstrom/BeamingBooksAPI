using AutoMapper;
using BeamingBooks.API.Entities;
using BeamingBooks.API.Models;
using BeamingBooks.API.ResourceParameters;
using BeamingBooks.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BeamingBooks.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public BooksController(
            IAuthorService authorService,
            IMapper mapper,
            IBookService bookService)
        {
            _authorService = authorService;
            _mapper = mapper;
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetBooks(
            [FromQuery] BookResourceParameters bookResourceParameters)
        {
            var books = _bookService.GetBooks(bookResourceParameters);
            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }

        [HttpGet("{bookId}", Name = "GetBook")]
        public ActionResult<BookDto> GetBook(int bookId)
        {
            var book = _bookService.GetBook(bookId);
            if (book == null) return NotFound();

            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpPost("{authorId}")]
        public ActionResult<BookDto> CreateBook(int authorId, CreateBookDto book)
        {
            if (!_authorService.AuthorExists(authorId)) return NotFound();

            var bookEntity = _mapper.Map<Book>(book);
            _bookService.AddBook(authorId, bookEntity);

            var bookDto = _mapper.Map<BookDto>(bookEntity);
            return CreatedAtRoute("GetBook", new { bookId = bookDto.Id }, bookDto);
        }

        [HttpPut("{bookId}")]
        public IActionResult UpdateBook(int bookId, UpdateBookDto book)
        {
            var bookEntity = _bookService.GetBook(bookId);

            if (bookEntity == null) return NotFound();

            _mapper.Map(book, bookEntity);
            _bookService.UpdateBook(bookEntity);

            return NoContent();
        }

        [HttpDelete("{bookId}")]
        public IActionResult DeleteBook(int bookId)
        {
            var bookEntity = _bookService.GetBook(bookId);

            if (bookEntity == null) return NotFound();

            _bookService.DeleteBook(bookEntity);

            return NoContent();
        }
    }
}
