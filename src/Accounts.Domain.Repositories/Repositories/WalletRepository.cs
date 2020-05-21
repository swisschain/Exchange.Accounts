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
    public class WalletRepository : IWalletRepository
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<WalletRepository> _logger;

        public WalletRepository(ConnectionFactory connectionFactory,
            IMapper mapper, ILogger<WalletRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Wallet>> GetAllAsync(string brokerId)
        {
            await using var context = _connectionFactory.CreateDataContext();

            var entities = await context.Wallets
                .Where(x => x.BrokerId == brokerId)
                .ToListAsync();

            return _mapper.Map<Wallet[]>(entities);
        }

        public async Task<IReadOnlyList<Wallet>> GetAllAsync(IEnumerable<long> ids, string brokerId)
        {
            await using var context = _connectionFactory.CreateDataContext();

            var query = context.Wallets
                .Where(x => x.BrokerId == brokerId)
                .Where(x => ids.Contains(x.Id));

            var data = await query.ToListAsync();

            return _mapper.Map<List<Wallet>>(data);
        }

        public async Task<IReadOnlyList<Wallet>> GetAllAsync(string brokerId, long accountId, string name, WalletType? type, bool? isEnabled,
            ListSortDirection sortOrder = ListSortDirection.Ascending, long cursor = 0, int limit = 50)
        {
            await using var context = _connectionFactory.CreateDataContext();

            IQueryable<WalletEntity> query = context.Wallets;

            query = query.Where(x => x.BrokerId.ToUpper() == brokerId.ToUpper());

            query = query.Where(x => x.AccountId == accountId);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => EF.Functions.ILike(x.Name, $"%{name}%"));

            if (isEnabled.HasValue)
                query = query.Where(x => x.IsEnabled == isEnabled.Value);

            if (type != null)
                query = query.Where(x => x.Type == type);

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

            return _mapper.Map<Wallet[]>(entities);
        }

        public async Task<Wallet> GetByIdAsync(long id, string brokerId)
        {
            await using var context = _connectionFactory.CreateDataContext();
            
            IQueryable<WalletEntity> query = context.Wallets;

            query = query.Where(x => x.BrokerId.ToUpper() == brokerId.ToUpper());

            query = query.Where(x => x.Id == id);

            var entity = await query.SingleOrDefaultAsync();

            return _mapper.Map<Wallet>(entity);
        }

        public async Task<Wallet> InsertAsync(Wallet wallet)
        {
            if (wallet.Type != WalletType.Api && wallet.Type != WalletType.Hft)
                throw new ArgumentException($"Wallet with type '{wallet.Type}' can't be added.");

            await using var context = _connectionFactory.CreateDataContext();

            var entity = _mapper.Map<WalletEntity>(wallet);

            entity.Created = DateTime.UtcNow;
            entity.Modified = entity.Created;

            await context.Wallets.AddAsync(entity);

            await context.SaveChangesAsync();

            _logger.LogInformation("Wallet has been created {@context}", entity);

            return _mapper.Map<Wallet>(entity);
        }

        public async Task<Wallet> UpdateAsync(Wallet wallet)
        {
            await using var context = _connectionFactory.CreateDataContext();

            var entity = await GetAsync(wallet.Id, wallet.BrokerId, context);

            if (entity.Type != WalletType.Api && entity.Type != WalletType.Hft)
                throw new ArgumentException($"Wallet with type '{wallet.Type}' can't be updated.");

            // save fields that has not be updated
            var created = entity.Created;
            var accountId = entity.AccountId;
            var type = entity.Type;

            _mapper.Map(wallet, entity);

            // restore fields that has not be updated
            entity.Created = created;
            entity.AccountId = accountId;
            entity.Type = type;

            entity.Modified = DateTime.UtcNow;

            await context.SaveChangesAsync();

            _logger.LogInformation("Wallet has been updated {@context}", entity);

            return _mapper.Map<Wallet>(entity);
        }

        public async Task DeleteAsync(long id, string brokerId)
        {
            await using var context = _connectionFactory.CreateDataContext();

            var existed = await GetAsync(id, brokerId, context);

            context.Remove(existed);

            await context.SaveChangesAsync();
        }

        private async Task<WalletEntity> GetAsync(long id, string brokerId, DataContext context)
        {
            IQueryable<WalletEntity> query = context.Wallets;

            var existed = await query
                .Where(x => x.Id == id)
                .Where(x => x.BrokerId == brokerId)
                .SingleOrDefaultAsync();

            return existed;
        }
    }
}
