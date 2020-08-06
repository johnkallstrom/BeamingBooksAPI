using BC = BCrypt.Net.BCrypt;
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
using BeamingBooks.API.Exceptions;
using AutoMapper;

namespace BeamingBooks.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly BeamingBooksContext _context;
        private readonly JwtSettings _jwtSettings;

        public AccountService(
            IMapper mapper,
            IOptions<JwtSettings> jwtSettings,
            BeamingBooksContext context)
        {
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _context = context;
        }

        public AuthenticateResponse AuthenticateAccount(string email, string password)
        {
            var account = _context.Account.FirstOrDefault(x => x.Email == email);

            if (account == null) throw new InvalidAccountException("The account does not exist.");
            if (BC.Verify(password, account.PasswordHash) == false) throw new InvalidAccountException("The email or password is incorrect.");
            
            var token = GenerateJwtToken(account);

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.Token = token;
            return response;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _context.Account.ToList();
        }

        public Account GetAccount(int id)
        {
            return _context.Account.FirstOrDefault(u => u.Id == id);
        }

        private string GenerateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}