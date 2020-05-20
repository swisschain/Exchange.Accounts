using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swisschain.Exchange.Accounts.Client;
using Swisschain.Exchange.Accounts.Client.Models.Account;
using Swisschain.Exchange.Accounts.Client.Models.Wallet;

namespace Accounts.Tests.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Accounts tests started.");

            var brokerId = "83829aa1-5888-45e4-997c-b133e58b7ab8";

            var client = new AccountsClient(new AccountsClientSettings { ServiceAddress = "http://localhost:5001" });

            var accounts = await GetAllAccounts(client, brokerId);

            var account = await GetAccount(client, brokerId);

            //await AddAccount(client, brokerId);

            //await GetAllWallets(client, brokerId);

            //await GetWallet(client, brokerId);

            Console.WriteLine("Finished!");
            Console.ReadLine();
        }

        private static async Task<IReadOnlyList<AccountModel>> GetAllAccounts(IAccountsClient client, string brokerId)
        {
            var accounts = await client.Account.GetAllAsync(new long[] { 4 }, brokerId);

            return accounts;
        }

        private static async Task<AccountModel> GetAccount(IAccountsClient client, string brokerId)
        {
            var account = await client.Account.GetAsync(4, brokerId);

            return account;
        }

        private static async Task<AccountModel> AddAccount(IAccountsClient client, string brokerId)
        {
            var newAccount = new AccountAddModel();
            newAccount.BrokerId = brokerId;
            newAccount.Name = "Exchange.Accounts.Tests";
            newAccount.IsEnabled = true;

            var account = await client.Account.AddAsync(newAccount);

            return account;
        }

        private static async Task<IReadOnlyList<WalletModel>> GetAllWallets(IAccountsClient client, string brokerId)
        {
            var wallets = await client.Wallet.GetAllAsync(new long[] { 1, 2, 3, 4 }, brokerId);

            return wallets;
        }

        private static async Task<WalletModel> GetWallet(IAccountsClient client, string brokerId)
        {
            var wallet = await client.Wallet.GetAsync(3, brokerId);

            return wallet;
        }
    }
}
