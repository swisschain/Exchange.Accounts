using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Accounts.Domain.Entities;
using Accounts.Domain.Repositories;
using Accounts.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Accounts.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository accountRepository, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public Task<IReadOnlyList<Account>> GetAllAsync(string brokerId)
        {
            return _accountRepository.GetAllAsync(brokerId);
        }

        public Task<Account> GetByIdAsync(string accountId)
        {
            return _accountRepository.GetByIdAsync(accountId);
        }

        public Task<IReadOnlyList<Account>> GetAllAsync(string brokerId, string accountId, string name, bool? isDisabled,
            ListSortDirection sortOrder = ListSortDirection.Ascending, string cursor = null, int limit = 50)
        {
            return _accountRepository.GetAllAsync(brokerId, accountId, name, isDisabled, sortOrder, cursor, limit);
        }

        public Task<Account> AddAsync(Account account)
        {
            account.Id = Guid.NewGuid().ToString();

            return _accountRepository.InsertAsync(account);
        }

        public Task<Account> UpdateAsync(Account account)
        {
            return _accountRepository.UpdateAsync(account);
        }

        public Task DeleteAsync(string accountId)
        {
            return _accountRepository.DeleteAsync(accountId);
        }
    }
}
