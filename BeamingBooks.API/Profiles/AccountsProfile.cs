using AutoMapper;

namespace BeamingBooks.API.Profiles
{
    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {
            CreateMap<Entities.Account, Models.AccountDto>();
            CreateMap<Entities.Account, Models.AuthenticateResponse>();
        }
    }
}
