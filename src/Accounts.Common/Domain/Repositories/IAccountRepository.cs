using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Common.Domain.Entities;

namespace Accounts.Common.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task CreateAsync(Account account);

        Task<IReadOnlyList<Account>> GetAllAsync();
    }
}
