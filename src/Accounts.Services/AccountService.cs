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

        public Task CreateAsync(Account account)
        {
            return _accountRepository.CreateAsync(account);
        }

        public Task<IReadOnlyList<Account>> GetAllAsync()
        {
            return _accountRepository.GetAllAsync();
        }
    }
}
