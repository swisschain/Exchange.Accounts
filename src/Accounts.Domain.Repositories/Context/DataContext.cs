using System;
using Accounts.Domain.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accounts.Domain.Persistence.Context
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

        internal DbSet<WalletEntity> Wallets { get; set; }

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

            AccountEntity(modelBuilder);

            WalletEntity(modelBuilder);
        }

        private static void AccountEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountEntity>()
                .ToTable("accounts")
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<AccountEntity>()
                .Property(x => x.BrokerId)
                .HasMaxLength(36)
                .IsRequired();

            modelBuilder.Entity<AccountEntity>()
                .Property(x => x.Name)
                .HasMaxLength(36)
                .IsRequired();

            modelBuilder.Entity<AccountEntity>()
                .HasMany(x => x.Wallets)
                .WithOne()
                .HasForeignKey(x => x.AccountId)
                .IsRequired();

            modelBuilder.Entity<AccountEntity>()
                .Property(x => x.Created)
                .IsRequired();

            modelBuilder.Entity<AccountEntity>()
                .Property(x => x.Modified)
                .IsRequired();

            modelBuilder.Entity<AccountEntity>()
                .HasIndex(x => x.BrokerId);

            modelBuilder.Entity<AccountEntity>()
                .HasIndex(x => new { x.BrokerId, x.Name }).IsUnique();
        }

        private static void WalletEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WalletEntity>()
                .ToTable("wallets")
                .HasKey(c => new { c.Id });

            modelBuilder.Entity<WalletEntity>()
                .Property(x => x.BrokerId)
                .HasMaxLength(36)
                .IsRequired();

            // AccountId set in AccountEntity

            modelBuilder.Entity<WalletEntity>()
                .Property(x => x.Name)
                .HasMaxLength(36)
                .IsRequired();

            modelBuilder.Entity<WalletEntity>()
                .Property(x => x.Type)
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();

            modelBuilder.Entity<WalletEntity>()
                .Property(x => x.Created)
                .IsRequired();

            modelBuilder.Entity<WalletEntity>()
                .Property(x => x.Modified)
                .IsRequired();


            modelBuilder.Entity<AccountEntity>()
                .HasIndex(x => new { x.BrokerId, x.Name }).IsUnique();
        }
    }
}
