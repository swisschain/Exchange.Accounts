using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Accounts.Domain.Entities;
using Accounts.Domain.Repositories;

namespace Accounts.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<IReadOnlyList<Account>> GetAllAsync(string brokerId)
        {
            return _accountRepository.GetAllAsync(brokerId);
        }

        public Task<Account> GetByIdAsync(long id, string brokerId)
        {
            return _accountRepository.GetByIdAsync(id, brokerId);
        }

        public Task<IReadOnlyList<Account>> GetAllAsync(string brokerId, string name, bool? isEnabled,
            ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50)
        {
            return _accountRepository.GetAllAsync(brokerId, name, isEnabled, sortOrder, cursor, limit);
        }

        public Task<Account> AddAsync(Account account)
        {
            return _accountRepository.InsertAsync(account);
        }

        public Task<Account> UpdateAsync(Account account)
        {
            return _accountRepository.UpdateAsync(account);
        }

        public Task DeleteAsync(long id, string brokerId)
        {
            return _accountRepository.DeleteAsync(id, brokerId);
        }
    }
}
