using AutoMapper;

namespace BeamingBooks.API.Profiles
{
    public class GenresProfile : Profile
    {
        public GenresProfile()
        {
            CreateMap<Entities.Genre, Models.GenreDto>();
            CreateMap<Models.GenreCreateDto, Entities.Genre>();
            CreateMap<Models.GenreUpdateDto, Entities.Genre>();
        }
    }
}
