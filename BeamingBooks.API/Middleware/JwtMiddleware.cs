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

        public async Task Invoke(HttpContext httpContext, IUserService userService)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var validatedToken = ValidateCurrentToken(token);

                if (validatedToken != null)
                {
                    AttachUserToContext(httpContext, validatedToken, userService);
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

        private void AttachUserToContext(HttpContext httpContext, SecurityToken token, IUserService userService)
        {
            var jwtToken = (JwtSecurityToken)token;

            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            httpContext.Items["User"] = userService.GetUser(userId);
        }
    }
}
