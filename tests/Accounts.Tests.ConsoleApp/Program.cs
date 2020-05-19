using System;
using System.Threading.Tasks;
using Swisschain.Exchange.Accounts.Client;

namespace TestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Accounts tests started.");

            var brokerId = "83829aa1-5888-45e4-997c-b133e58b7ab8";

            var client = new AccountsClient(new AccountsClientSettings { ServiceAddress = "http://localhost:5001" });

            //var newAccount = new AccountAddModel();
            //newAccount.BrokerId = brokerId;
            //newAccount.Name = "Exchange.Accounts.Tests";
            //newAccount.IsEnabled = true;

            //var accountModel = await client.Account.AddAsync(newAccount);

            var wallets = await client.Wallet.GetAllAsync(new long[] {1, 2, 3, 4}, brokerId);

            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }
}
