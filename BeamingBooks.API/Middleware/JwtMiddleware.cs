using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeamingBooks.API.Helpers;
using BeamingBooks.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BeamingBooks.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext httpContext, IAccountService accountService)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var validatedToken = ValidateCurrentToken(token);

                if (validatedToken != null)
                {
                    AttachAccountToContext(httpContext, validatedToken, accountService);
                }
            }

            await _next(httpContext);
        }

        private SecurityToken ValidateCurrentToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return validatedToken;
            } catch
            {
                return null;
            }
        }

        private void AttachAccountToContext(HttpContext httpContext, SecurityToken token, IAccountService accountService)
        {
            var jwtToken = (JwtSecurityToken)token;

            var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            httpContext.Items["Account"] = accountService.GetAccount(accountId);
        }
    }
}
