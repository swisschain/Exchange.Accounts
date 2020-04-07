using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Common.Domain.Entities;
using Accounts.Common.Domain.Repositories;
using Accounts.Common.Domain.Services;
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

        public Task<Account> CreateAsync(Account account)
        {
            account.Id = Guid.NewGuid().ToString();

            return _accountRepository.InsertAsync(account);
        }

        public Task<IReadOnlyList<Account>> GetAllAsync(string brokerId)
        {
            return _accountRepository.GetAllAsync(brokerId);
        }
    }
}
