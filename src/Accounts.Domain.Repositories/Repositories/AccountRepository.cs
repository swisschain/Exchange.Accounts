using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Domain.Entities;
using Accounts.Domain.Entities.Enums;
using Accounts.Domain.Persistence.Context;
using Accounts.Domain.Persistence.Entities;
using Accounts.Domain.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Accounts.Domain.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(ConnectionFactory connectionFactory,
            IMapper mapper, ILogger<AccountRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
            _logger = logger;
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
        
        public async Task<IReadOnlyList<Account>> GetAllAsync(string brokerId, string name, bool? isEnabled,
            ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<AccountEntity> query = context.Accounts;

                query = query.Where(x => x.BrokerId.ToUpper() == brokerId.ToUpper());

                if (!string.IsNullOrEmpty(name))
                    query = query.Where(x => EF.Functions.ILike(x.Name, $"%{name}%"));

                if (isEnabled.HasValue)
                    query = query.Where(x => x.IsEnabled == isEnabled.Value);

                if (sortOrder == ListSortDirection.Ascending)
                {
                    if (cursor > 0)
                        query = query.Where(x => x.Id >= cursor);

                    query = query.OrderBy(x => x.Id);
                }
                else
                {
                    if (cursor > 0)
                        query = query.Where(x => x.Id < cursor);

                    query = query.OrderByDescending(x => x.Id);
                }

                query = query.Take(limit);

                var entities = await query.ToListAsync();

                return _mapper.Map<Account[]>(entities);
            }
        }

        public async Task<Account> GetByIdAsync(long id, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                IQueryable<AccountEntity> query = context.Accounts;

                query = query.Where(x => x.BrokerId.ToUpper() == brokerId.ToUpper());

                query = query.Where(x => x.Id == id);

                var entity = await query.SingleOrDefaultAsync();

                return _mapper.Map<Account>(entity);
            }
        }

        public async Task<Account> InsertAsync(Account account)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var entity = _mapper.Map<AccountEntity>(account);

                entity.Created = DateTime.UtcNow;
                entity.Modified = entity.Created;

                await context.Accounts.AddAsync(entity);

                await context.SaveChangesAsync();

                var fundingWallet = new WalletEntity(entity.Id, "Funding", WalletType.Funding, true);

                await context.Wallets.AddAsync(fundingWallet);

                var tradingWallet = new WalletEntity(entity.Id, "Trading", WalletType.Trading, true);

                await context.Wallets.AddAsync(tradingWallet);

                await context.SaveChangesAsync();

                _logger.LogInformation("Account has been created {@context}", entity);

                return _mapper.Map<Account>(entity);
            }
        }

        public async Task<Account> UpdateAsync(Account account)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var entity = await GetAsync(account.Id, account.BrokerId, context);

                // save fields that has not be updated
                var created = entity.Created;

                _mapper.Map(account, entity);

                // restore fields that has not be updated
                entity.Created = created;

                entity.Modified = DateTime.UtcNow;

                await context.SaveChangesAsync();

                _logger.LogInformation("Account has been updated {@context}", entity);

                return _mapper.Map<Account>(entity);
            }
        }

        public async Task DeleteAsync(long id, string brokerId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var existed = await GetAsync(id, brokerId, context);

                context.Remove(existed);

                await context.SaveChangesAsync();
            }
        }

        private async Task<AccountEntity> GetAsync(long id, string brokerId, DataContext context)
        {
            IQueryable<AccountEntity> query = context.Accounts;

            var existed = await query
                .Where(x => x.Id == id)
                .Where(x => x.BrokerId == brokerId)
                .SingleOrDefaultAsync();

            return existed;
        }
    }
}
