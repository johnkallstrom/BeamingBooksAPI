using AutoMapper;

namespace BeamingBooks.API.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Entities.User, Models.UserDto>();
        }
    }
}
