using System;
using System.Collections.Generic;
using System.Linq;
using Accounts.Common.Domain.Entities;
using Accounts.Common.Domain.Services;

namespace Accounts.Common.Services
{
    public class AccountService : IAccountService
    {
        private readonly object _sync = new object();
        private readonly Dictionary<string, Account> _inMemoryRepository;

        public AccountService()
        {
            _inMemoryRepository = new Dictionary<string, Account>();
        }

        public void Create(Account account)
        {
            if (string.IsNullOrWhiteSpace(account.Id))
                account.Id = Guid.NewGuid().ToString();

            account.Created = DateTimeOffset.UtcNow;

            lock (_sync)
            {
                _inMemoryRepository[account.Id] = account;
            }
        }

        public IReadOnlyList<Account> GetAll()
        {
            lock (_sync)
            {
                return _inMemoryRepository.Values.ToList();
            }
        }
    }
}
