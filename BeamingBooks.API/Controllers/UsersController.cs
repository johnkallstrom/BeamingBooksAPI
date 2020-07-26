using AutoMapper;
using BeamingBooks.API.Helpers;
using BeamingBooks.API.Models;
using BeamingBooks.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BeamingBooks.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private IMapper _mapper;
        private IUserService _userService;

        public UsersController(
            IMapper mapper,
            IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.AuthenticateUser(model);

            if (response == null)
                return BadRequest(new { Message = "Username or password is incorrect." });

            return Ok(response);
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var users = _userService.GetUsers();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }
    }
}
