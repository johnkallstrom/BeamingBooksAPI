using AutoMapper;

namespace BeamingBooks.API.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Entities.Author, Models.AuthorDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Book))
                .ForMember(dest => dest.Born, opt => opt.MapFrom(src => src.Birthday.ToShortDateString()));

            CreateMap<Models.AuthorCreateDto, Entities.Author>()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Books));

            CreateMap<Models.AuthorUpdateDto, Entities.Author>();
        }
    }
}
