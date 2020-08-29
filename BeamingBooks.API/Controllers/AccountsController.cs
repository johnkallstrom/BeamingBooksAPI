using AutoMapper;
using BeamingBooks.API.Exceptions;
using BeamingBooks.API.Helpers;
using BeamingBooks.API.Models;
using BeamingBooks.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BeamingBooks.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private IMapper _mapper;
        private IAccountService _accountService;

        public AccountsController(
            IMapper mapper,
            IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterAccount(RegisterAccountDto model)
        {
            try
            {
                _accountService.Register(model);
                return Ok(new { Message = "Registration successful." });
            }
            catch (EmailExistsException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult AuthenticateAccount(AuthenticateRequest model)
        {
            try
            {
                var response = _accountService.AuthenticateAccount(model.Email, model.Password);
                return Ok(response);
            } catch (InvalidAccountException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult<IEnumerable<AccountDto>> GetAccounts()
        {
            var accounts = _accountService.GetAccounts();
            return Ok(_mapper.Map<IEnumerable<AccountDto>>(accounts));
        }

        [CustomAuthorize]
        [HttpGet("{accountId}")]
        public ActionResult<AccountDto> GetAccount(int accountId)
        {
            var account = _accountService.GetAccount(accountId);

            if (account == null) return NotFound();

            return Ok(_mapper.Map<AccountDto>(account));
        }
    }
}
