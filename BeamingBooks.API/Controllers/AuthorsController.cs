using AutoMapper;
using BeamingBooks.API.Entities;
using BeamingBooks.API.Models;
using BeamingBooks.API.ResourceParameters;
using BeamingBooks.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BeamingBooks.API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;

        public AuthorsController(
            IMapper mapper,
            IAuthorService authorService)
        {
            _mapper = mapper;
            _authorService = authorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors(
            [FromQuery] AuthorResourceParameters authorResourceParameters)
        {
            var authors = _authorService.GetAuthors(authorResourceParameters);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        public ActionResult<AuthorDto> GetAuthor(int authorId)
        {
            var author = _authorService.GetAuthor(authorId);

            if (author == null) return NotFound();

            return Ok(_mapper.Map<AuthorDto>(author));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorCreateDto author)
        {
            var authorEntity = _mapper.Map<Author>(author);
            _authorService.AddAuthor(authorEntity);

            var authorDto = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor", new { authorId = authorDto.Id }, authorDto);
        }

        [HttpPut("{authorId}")]
        public IActionResult UpdateAuthor(int authorId, AuthorUpdateDto author)
        {
            if (!_authorService.AuthorExists(authorId)) return NotFound();

            var authorEntity = _authorService.GetAuthor(authorId);
            _mapper.Map(author, authorEntity);
            _authorService.UpdateAuthor(authorEntity);

            return NoContent();
        }

        [HttpDelete("{authorId}")]
        public IActionResult DeleteAuthor(int authorId)
        {
            if (!_authorService.AuthorExists(authorId)) return NotFound();

            var authorEntity = _authorService.GetAuthor(authorId);
            _authorService.DeleteAuthor(authorEntity);

            return NoContent();
        }
    }
}
