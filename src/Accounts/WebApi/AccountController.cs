﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Accounts.Domain.Entities;
using Accounts.Domain.Services;
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
        [ProducesResponseType(typeof(Paginated<AccountModel, long>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionaryErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetManyAsync([FromQuery] AccountRequestManyModel request)
        {
            var sortOrder = request.Order == PaginationOrder.Asc
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending;

            var brokerId = User.GetTenantId();

            var accounts = await _accountService.GetAllAsync(brokerId, request.Name, request.IsEnabled, sortOrder, request.Cursor, request.Limit);

            var result = _mapper.Map<AccountModel[]>(accounts);

            return Ok(result.Paginate(request, Url, x => x.Id));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var brokerId = User.GetTenantId();

            var account = await _accountService.GetByIdAsync(id, brokerId);

            if (account == null)
                return NotFound();

            var model = _mapper.Map<AccountModel>(account);

            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync(AccountAddModel account)
        {
            var domain = _mapper.Map<Account>(account);

            domain.BrokerId = User.GetTenantId();

            var newDomain = await _accountService.AddAsync(domain);

            var newViewModel = _mapper.Map<AccountModel>(newDomain);

            return Ok(newViewModel);
        }

        [HttpPut]
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] AccountEditModel model)
        {
            var domain = _mapper.Map<Account>(model);

            domain.BrokerId = User.GetTenantId();

            var updated = await _accountService.UpdateAsync(domain);

            if (updated == null)
                return NotFound();

            var newModel = _mapper.Map<AccountModel>(updated);

            return Ok(newModel);
        }
    }
}
