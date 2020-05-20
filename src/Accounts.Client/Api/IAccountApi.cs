using System.Collections.Generic;
using System.Threading.Tasks;
using Swisschain.Exchange.Accounts.Client.Models.Account;

namespace Swisschain.Exchange.Accounts.Client.Api
{
    public interface IAccountApi
    {
        Task<IReadOnlyList<AccountModel>> GetAllAsync(IEnumerable<long> ids, string brokerId);

        Task<AccountModel> GetAsync(long id, string brokerId);

        Task<AccountModel> AddAsync(AccountAddModel accountAddModel);
    }
}
