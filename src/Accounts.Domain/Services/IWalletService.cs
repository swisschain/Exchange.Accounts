using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Accounts.Domain.Entities;

namespace Accounts.Domain.Services
{
    public interface IWalletService
    {
        Task<IReadOnlyList<Wallet>> GetAllAsync(string brokerId);

        Task<IReadOnlyList<Wallet>> GetAllAsync(string brokerId, string name, bool? isEnabled,
            ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50);

        Task<Wallet> GetByIdAsync(long id, string brokerId);

        Task<Wallet> AddAsync(Wallet wallet);

        Task<Wallet> UpdateAsync(Wallet wallet);

        Task DeleteAsync(long id, string brokerId);
    }
}
