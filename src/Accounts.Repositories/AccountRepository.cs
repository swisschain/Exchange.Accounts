using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Common.Domain.Entities;
using Accounts.Common.Domain.Repositories;
using Accounts.Repositories.Context;
using Accounts.Repositories.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IMapper _mapper;

        public AccountRepository(ConnectionFactory connectionFactory, IMapper mapper)
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<Account>> GetAllAsync(string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var entities = await context.Accounts
                    .Where(x => x.BrokerId == brokerId)
                    .ToListAsync();

                return _mapper.Map<List<Account>>(entities);
            }
        }

        public async Task<Account> InsertAsync(Account account)
        {
            account.Created = DateTimeOffset.UtcNow;

            using (var context = _connectionFactory.CreateDataContext())
            {
                var entity = _mapper.Map<AccountEntity>(account);

                context.Accounts.Add(entity);

                await context.SaveChangesAsync();

                var result = _mapper.Map<Account>(entity);

                return result;
            }
        }
    }
}
