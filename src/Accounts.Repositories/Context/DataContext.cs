using System;
using Accounts.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounts.Repositories.Context
{
    public class DataContext : DbContext
    {
        private const string Schema = "accounts";

        private string _connectionString;

        public DataContext()
        {
        }

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        internal DbSet<AccountEntity> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connectionString == null)
            {
                Console.Write("Enter connection string: ");
                _connectionString = Console.ReadLine();
            }

            optionsBuilder.UseNpgsql(_connectionString,
                o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schema));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            SetupAssetPairs(modelBuilder);
        }

        private static void SetupAssetPairs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountEntity>();
        }
    }
}
