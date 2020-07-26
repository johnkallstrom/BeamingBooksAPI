using BeamingBooks.API.Data;
using BeamingBooks.API.Entities;
using BeamingBooks.API.Helpers;
using BeamingBooks.API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BeamingBooks.API.Services
{
    public class UserService : IUserService
    {
        private readonly BeamingBooksContext _context;
        private readonly JwtSettings _jwtSettings;

        public UserService(IOptions<JwtSettings> jwtSettings,
            BeamingBooksContext context)
        {
            _jwtSettings = jwtSettings.Value;
            _context = context;
        }

        public AuthenticateResponse AuthenticateUser(AuthenticateRequest model)
        {
            var user = GetUser(model.Username, model.Password);

            if (user == null) return null;
            
            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.User.ToList();
        }

        public User GetUser(int id)
        {
            return _context.User.FirstOrDefault(u => u.Id == id);
        }

        public User GetUser(string username, string password)
        {
            return _context.User.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        private string GenerateJwtToken(User user)
        {
            // Generate JWT token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}