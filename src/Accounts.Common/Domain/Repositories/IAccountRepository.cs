using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Common.Domain.Entities;

namespace Accounts.Common.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> InsertAsync(Account account);

        Task<IReadOnlyList<Account>> GetAllAsync(string brokerId);
    }
}
