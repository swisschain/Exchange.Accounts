using System.Collections.Generic;
using System.Threading.Tasks;
using Swisschain.Exchange.Accounts.Client.Models.Wallet;

namespace Swisschain.Exchange.Accounts.Client.Api
{
    public interface IWalletApi
    {
        Task<IReadOnlyList<WalletModel>> GetAllAsync(IEnumerable<long> ids, string brokerId);

        Task<WalletModel> GetAsync(long id, string brokerId);
    }
}
