using AutoMapper;

namespace BeamingBooks.API.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Entities.Author, Models.AuthorDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books))
                .ForMember(dest => dest.Born, opt => opt.MapFrom(src => src.Birthday.ToShortDateString()));

            CreateMap<Models.CreateAuthorDto, Entities.Author>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

            CreateMap<Models.UpdateAuthorDto, Entities.Author>();
        }
    }
}
