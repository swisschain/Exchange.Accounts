using System.Threading.Tasks;
using Swisschain.Exchange.Accounts.Client.Models.Account;

namespace Swisschain.Exchange.Accounts.Client.Api
{
    public interface IAccountApi
    {
        Task<AccountModel> AddAsync(AccountAddModel accountAddModel);
    }
}
