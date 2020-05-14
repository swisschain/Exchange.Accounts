using System.Threading.Tasks;
using Swisschain.Exchange.Accounts.Client.Models.Accounts;

namespace Swisschain.Exchange.Accounts.Client.Api
{
    public interface IAccountsApi
    {
        Task<AccountModel> AddAsync(AccountAddModel accountAddModel);
    }
}
