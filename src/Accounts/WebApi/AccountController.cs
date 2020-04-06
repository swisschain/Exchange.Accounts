using System.Threading.Tasks;
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
        public async Task<IActionResult> CreateAsync(Account account)
        {
            await _accountService.CreateAsync(account);

            return Ok(account);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Account[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var accounts = await _accountService.GetAllAsync();

            return Ok(accounts);
        }
    }
}
