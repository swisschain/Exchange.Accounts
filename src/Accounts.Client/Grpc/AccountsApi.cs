using System.Threading.Tasks;
using Swisschain.Exchange.Accounts.Client.Api;
using Swisschain.Exchange.Accounts.Client.Common;
using Swisschain.Exchange.Accounts.Client.Models.Accounts;
using Swisschain.Exchange.Accounts.Contract;

namespace Swisschain.Exchange.Accounts.Client.Grpc
{
    internal class AccountsApi : BaseGrpcClient, IAccountsApi
    {
        private readonly AccountsGrpc.AccountsGrpcClient _client;

        public AccountsApi(string address) : base(address)
        {
            _client = new AccountsGrpc.AccountsGrpcClient(Channel);
        }

        public async Task<AccountModel> AddAsync(AccountAddModel accountAddModel)
        {
            var request = new AddAccountRequest();
            request.BrokerId = accountAddModel.BrokerId;
            request.Name = accountAddModel.Name;
            request.IsEnabled = accountAddModel.IsEnabled;

            var response = await _client.AddAsync(request);

            if (response.Account == null)
                return null;

            return new AccountModel(response.Account);
        }
    }
}
