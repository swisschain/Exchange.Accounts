using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swisschain.Exchange.Accounts.Client.Api;
using Swisschain.Exchange.Accounts.Client.Common;
using Swisschain.Exchange.Accounts.Client.Models.Account;
using Swisschain.Exchange.Accounts.Contract;

namespace Swisschain.Exchange.Accounts.Client.Grpc
{
    internal class AccountApi : BaseGrpcClient, IAccountApi
    {
        private readonly AccountsGrpc.AccountsGrpcClient _client;

        public AccountApi(string address) : base(address)
        {
            _client = new AccountsGrpc.AccountsGrpcClient(Channel);
        }

        public async Task<IReadOnlyList<AccountModel>> GetAllAsync(IEnumerable<long> ids, string brokerId)
        {
            var request = new GetAllAccountsByIdsRequest();
            request.Ids.AddRange(ids);
            request.BrokerId = brokerId;

            var response = await _client.GetAllAsync(request);

            var result = response.Accounts.Select(x => new AccountModel(x)).ToList();

            return result;
        }

        public async Task<AccountModel> GetAsync(long id, string brokerId)
        {
            var request = new GetAccountByIdRequest();
            request.Id = id;
            request.BrokerId = brokerId;

            var response = await _client.GetAsync(request);

            if (response.Account == null)
                return null;

            var result = new AccountModel(response.Account);

            return result;
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

            var result = new AccountModel(response.Account);

            return result;
        }
    }
}
