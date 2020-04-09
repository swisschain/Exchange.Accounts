using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Accounts.Domain.Entities;

namespace Accounts.Domain.Services
{
    public interface IAccountService
    {
        Task<IReadOnlyList<Account>> GetAllAsync(string brokerId);

        Task<IReadOnlyList<Account>> GetAllAsync(string brokerId, string accountId, string name, bool? isDisabled,
            ListSortDirection sortOrder = ListSortDirection.Ascending, string cursor = null, int limit = 50);

        Task<Account> GetByIdAsync(string brokerId, string accountId);

        Task<Account> AddAsync(Account account);

        Task<Account> UpdateAsync(Account account);

        Task DeleteAsync(string accountId);
    }
}
