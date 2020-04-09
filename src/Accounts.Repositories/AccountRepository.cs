using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Domain.Entities;
using Accounts.Domain.Repositories;
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
                    .Where(x => x.BrokerId.ToUpper() == brokerId.ToUpper())
                    .ToListAsync();

                return _mapper.Map<List<Account>>(entities);
            }
        }
        
        public async Task<IReadOnlyList<Account>> GetAllAsync(string brokerId, string accountId, string name, bool? isDisabled,
            ListSortDirection sortOrder = ListSortDirection.Ascending, string cursor = null, int limit = 50)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<AccountEntity> query = context.Accounts;

                query = query.Where(x => x.BrokerId.ToUpper() == brokerId.ToUpper());

                if (!string.IsNullOrEmpty(accountId))
                    query = query.Where(x => x.Id.Contains(accountId, StringComparison.InvariantCultureIgnoreCase));

                if (!string.IsNullOrEmpty(name))
                    query = query.Where(x => x.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));

                if (isDisabled.HasValue)
                    query = query.Where(x => x.IsDisabled == isDisabled.Value);

                if (sortOrder == ListSortDirection.Ascending)
                {
                    if (cursor != null)
                        query = query.Where(x => String.Compare(x.Id, cursor, StringComparison.CurrentCultureIgnoreCase) >= 0);

                    query = query.OrderBy(x => x.Id);
                }
                else
                {
                    if (cursor != null)
                        query = query.Where(x => String.Compare(x.Id, cursor, StringComparison.CurrentCultureIgnoreCase) < 0);

                    query = query.OrderByDescending(x => x.Id);
                }

                query = query.Take(limit);

                var entities = await query.ToListAsync();

                return _mapper.Map<Account[]>(entities);
            }
        }

        public async Task<Account> GetByIdAsync(string brokerId, string accountId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<AccountEntity> query = context.Accounts;

                query = query.Where(x => x.BrokerId.ToUpper() == brokerId.ToUpper());

                query = query.Where(x => x.Id.ToUpper() == accountId.ToUpper());

                var entity = await query.SingleOrDefaultAsync();

                return _mapper.Map<Account>(entity);
            }
        }

        public async Task<Account> InsertAsync(Account account)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var entity = _mapper.Map<AccountEntity>(account);

                account.Created = DateTimeOffset.UtcNow;

                context.Accounts.Add(entity);

                await context.SaveChangesAsync();

                var result = _mapper.Map<Account>(entity);

                return result;
            }
        }

        public async Task<Account> UpdateAsync(Account account)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var entity = await context.Accounts
                    .FindAsync(account.Id);

                if (account.BrokerId != entity.BrokerId)
                    throw new InvalidOperationException($"BrokerIds are different: '{account.BrokerId}' != '{entity.BrokerId}'");

                _mapper.Map(account, entity);

                entity.Modified = DateTimeOffset.UtcNow;

                await context.SaveChangesAsync();

                var result = _mapper.Map<Account>(entity);

                return result;
            }
        }

        public async Task DeleteAsync(string accountId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var entity = new AccountEntity { Id = accountId };

                context.Entry(entity).State = EntityState.Deleted;

                await context.SaveChangesAsync();
            }
        }
    }
}
