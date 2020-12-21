using BeamingBooks.API.Entities;
using BeamingBooks.API.Models;
using System.Collections.Generic;

namespace BeamingBooks.API.Services
{
    public interface IAccountService
    {
        AuthenticateResponse AuthenticateAccount(string email, string password);
        IEnumerable<Account> GetAccounts();
        Account GetAccount(int id);
        void Register(RegisterAccountDto model);
    }
}
