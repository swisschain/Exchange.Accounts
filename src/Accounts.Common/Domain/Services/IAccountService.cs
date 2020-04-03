using System.Collections.Generic;
using Accounts.Common.Domain.Entities;

namespace Accounts.Common.Domain.Services
{
    public interface IAccountService
    {
        void Create(Account account);

        IReadOnlyList<Account> GetAll();
    }
}
