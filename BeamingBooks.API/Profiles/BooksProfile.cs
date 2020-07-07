using AutoMapper;

namespace BeamingBooks.API.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Entities.Book, Models.BookDto>()
                .ForMember(dest => dest.Published, opt => opt.MapFrom(src => src.Published.Year))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<Models.BookCreateDto, Entities.Book>();
            CreateMap<Models.BookUpdateDto, Entities.Book>();
        }
    }
}
