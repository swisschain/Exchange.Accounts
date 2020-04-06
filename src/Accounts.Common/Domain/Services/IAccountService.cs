﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Common.Domain.Entities;

namespace Accounts.Common.Domain.Services
{
    public interface IAccountService
    {
        Task CreateAsync(Account account);

        Task<IReadOnlyList<Account>> GetAllAsync();
    }
}
