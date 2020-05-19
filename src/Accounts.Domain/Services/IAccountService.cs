using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Accounts.Domain.Entities;

namespace Accounts.Domain.Services
{
    public interface IAccountService
    {
        Task<IReadOnlyList<Account>> GetAllAsync(string brokerId);

        Task<IReadOnlyList<Account>> GetAllAsync(IEnumerable<long> ids, string brokerId);

        Task<IReadOnlyList<Account>> GetAllAsync(string brokerId, string name, bool? isEnabled,
            ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50);

        Task<Account> GetByIdAsync(long id, string brokerId);

        Task<Account> AddAsync(Account account);

        Task<Account> UpdateAsync(Account account);

        Task DeleteAsync(long id, string brokerId);
    }
}
