using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Accounts.Domain.Entities;

namespace Accounts.Domain.Repositories
{
    public interface IWalletRepository
    {
        Task<IReadOnlyList<Wallet>> GetAllAsync(string brokerId);

        Task<IReadOnlyList<Wallet>> GetAllAsync(string brokerId, string name, bool? isEnabled,
            ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50);

        Task<Wallet> GetByIdAsync(long id, string brokerId);

        Task<Wallet> InsertAsync(Wallet wallet);

        Task<Wallet> UpdateAsync(Wallet wallet);

        Task DeleteAsync(long id, string brokerId);
    }
}
