using System.Threading.Tasks;
using Accounts.Common.Domain.Entities;
using Accounts.Common.Domain.Services;
using Accounts.WebApi.Models.Account;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swisschain.Sdk.Server.Authorization;

namespace Accounts.WebApi
{
    [Authorize]
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync(AccountEditModel account)
        {
            var domain = _mapper.Map<Account>(account);

            domain.BrokerId = User.GetTenantId();

            var newDomain = await _accountService.CreateAsync(domain);

            var newViewModel = _mapper.Map<AccountModel>(newDomain);

            return Ok(newViewModel);
        }

        [HttpGet]
        [ProducesResponseType(typeof(AccountModel[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var brokerId = User.GetTenantId();

            var domain = await _accountService.GetAllAsync(brokerId);

            var viewModel = _mapper.Map<AccountModel[]>(domain);

            return Ok(viewModel);
        }
    }
}
