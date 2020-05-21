using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Accounts.Domain.Entities;
using Accounts.Domain.Entities.Enums;
using Accounts.Domain.Repositories;

namespace Accounts.Domain.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public Task<IReadOnlyList<Wallet>> GetAllAsync(string brokerId)
        {
            return _walletRepository.GetAllAsync(brokerId);
        }

        public Task<IReadOnlyList<Wallet>> GetAllAsync(IEnumerable<long> ids, string brokerId)
        {
            return _walletRepository.GetAllAsync(ids, brokerId);
        }

        public Task<IReadOnlyList<Wallet>> GetAllAsync(string brokerId, long accountId, string name, WalletType type, bool? isEnabled,
            ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50)
        {
            return _walletRepository.GetAllAsync(brokerId, accountId, name, type, isEnabled, sortOrder, cursor, limit);
        }

        public Task<Wallet> GetByIdAsync(long id, string brokerId)
        {
            return _walletRepository.GetByIdAsync(id, brokerId);
        }

        public Task<Wallet> AddAsync(Wallet wallet)
        {
            return _walletRepository.InsertAsync(wallet);
        }

        public Task<Wallet> UpdateAsync(Wallet wallet)
        {
            return _walletRepository.UpdateAsync(wallet);
        }

        public Task DeleteAsync(long id, string brokerId)
        {
            return _walletRepository.DeleteAsync(id, brokerId);
        }
    }
}
