using Accounts.Common.Domain.Entities;
using Accounts.Common.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.WebApi
{
    [Authorize]
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
        public IActionResult CreateAsync(Account account)
        {
            _accountService.Create(account);

            return Ok(account);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Account[]), StatusCodes.Status200OK)]
        public IActionResult GetAllAsync()
        {
            var accounts = _accountService.GetAll();

            return Ok(accounts);
        }
    }
}
