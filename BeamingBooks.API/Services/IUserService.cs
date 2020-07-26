using BeamingBooks.API.Entities;
using BeamingBooks.API.Models;
using System.Collections.Generic;

namespace BeamingBooks.API.Services
{
    public interface IUserService
    {
        AuthenticateResponse AuthenticateUser(AuthenticateRequest model);
        IEnumerable<User> GetUsers();
        User GetUser(int id);
        User GetUser(string username, string password);
    }
}
