﻿using Microsoft.EntityFrameworkCore;

namespace Accounts.Domain.Persistence.Context
{
    public class ConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void EnsureMigration()
        {
            using (var context = CreateDataContext())
            {
                context.Database.Migrate();
            }
        }

        internal DataContext CreateDataContext()
        {
            return new DataContext(_connectionString);
        }
    }
}
