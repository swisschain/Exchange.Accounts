using System;
using System.Threading.Tasks;
using Swisschain.Exchange.Accounts.Client;
using Swisschain.Exchange.Accounts.Client.Models.Accounts;

namespace TestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Accounts tests started.");
            
            var client = new AccountsClient(new AccountsClientSettings { ServiceAddress = "http://localhost:5001" });

            var newAccount = new AccountAddModel();
            newAccount.BrokerId = "83829aa1-5888-45e4-997c-b133e58b7ab8";
            newAccount.Name = "Exchange.Accounts.Tests";
            newAccount.IsEnabled = false;

            var cashOperationsFees = await client.Accounts.AddAsync(newAccount);

            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }
}
