using AutoMapper;
using BeamingBooks.API.Entities;
using BeamingBooks.API.Models;
using BeamingBooks.API.ResourceParameters;
using BeamingBooks.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BeamingBooks.API.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenresController(
            IGenreService genreService,
            IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GenreDto>> GetGenres(
            [FromQuery] GenreResourceParameters genreResourceParameters)
        {
            var genres = _genreService.GetGenres(genreResourceParameters);
            return Ok(_mapper.Map<IEnumerable<GenreDto>>(genres));
        }

        [HttpGet("{genreId}", Name = "GetGenre")]
        public ActionResult<GenreDto> GetGenre(int genreId)
        {
            var genre = _genreService.GetGenre(genreId);

            if (genre == null) return NotFound();

            return Ok(_mapper.Map<GenreDto>(genre));
        }

        [HttpPost]
        public ActionResult<GenreDto> CreateGenre(GenreCreateDto genre)
        {
            var genreEntity = _mapper.Map<Genre>(genre);
            _genreService.AddGenre(genreEntity);

            var genreDto = _mapper.Map<GenreDto>(genreEntity);
            return CreatedAtRoute("GetGenre", new { genreId = genreDto.Id }, genreDto);
        }

        [HttpPut("{genreId}")]
        public IActionResult UpdateGenre(int genreId, GenreUpdateDto genre)
        {
            var genreEntity = _genreService.GetGenre(genreId);

            if (genreEntity == null) return NotFound();

            _mapper.Map(genre, genreEntity);
            _genreService.UpdateGenre(genreEntity);

            return NoContent();
        }
    }
}
