﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                    .Where(x => x.BrokerId.ToUpper() == brokerId.ToUpper())
                    .ToListAsync();

                return _mapper.Map<List<Account>>(entities);
            }
        }
        
        public async Task<IReadOnlyList<Account>> GetAllAsync(string brokerId, string accountId, string name, bool isDisabled = false,
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

                query = query.Where(x => x.IsDisabled == isDisabled);

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

        public async Task<Account> GetByIdAsync(string accountId)
        {
            using (var context = _connectionFactory.CreateDataContext())
            {
                var entity = await context.Accounts
                    .FindAsync(accountId);

                return _mapper.Map<Account>(entity);
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

        public async Task<Account> UpdateAsync(Account account)
        {
            account.Modified = DateTime.UtcNow;

            using (var context = _connectionFactory.CreateDataContext())
            {
                var entity = await context.Accounts
                    .FindAsync(account.Id);

                _mapper.Map(account, entity);

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
