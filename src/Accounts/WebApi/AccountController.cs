using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Accounts.Common.Domain.Entities;
using Accounts.Common.Domain.Services;
using Accounts.WebApi.Models.Account;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swisschain.Sdk.Server.Authorization;
using Swisschain.Sdk.Server.WebApi.Common;
using Swisschain.Sdk.Server.WebApi.Pagination;

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

        [HttpGet]
        [ProducesResponseType(typeof(Paginated<Account, string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionaryErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetManyAsync([FromQuery] AccountRequestMany request)
        {
            if (request.Limit > 1000)
            {
                ModelState.AddModelError($"{nameof(request.Limit)}", "Should not be more than 1000");

                return BadRequest(ModelState);
            }

            var sortOrder = request.Order == PaginationOrder.Asc
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending;

            var brokerId = User.GetTenantId();

            var accounts = await _accountService.GetAllAsync(brokerId, request.Id, request.Name, request.IsDisabled, sortOrder, request.Cursor, request.Limit);

            var result = _mapper.Map<List<Account>>(accounts);

            return Ok(result.Paginate(request, Url, x => x.Id));
        }

        [HttpGet("{accountId}")]
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(string accountId)
        {
            var account = await _accountService.GetByIdAsync(accountId);

            if (account == null)
                return NotFound();

            var model = _mapper.Map<AccountModel>(account);

            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync(AccountEditModel account)
        {
            var domain = _mapper.Map<Account>(account);

            domain.BrokerId = User.GetTenantId();

            var newDomain = await _accountService.AddAsync(domain);

            var newViewModel = _mapper.Map<AccountModel>(newDomain);

            return Ok(newViewModel);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] AccountEditModel model)
        {
            var domain = _mapper.Map<Account>(model);

            var updated = await _accountService.UpdateAsync(domain);

            if (updated == null)
                return NotFound();

            var newModel = _mapper.Map<Account>(updated);

            return Ok(newModel);
        }

        [HttpDelete("{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(string accountId)
        {
            await _accountService.DeleteAsync(accountId);

            return Ok();
        }
    }
}
