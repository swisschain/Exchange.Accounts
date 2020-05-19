using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swisschain.Exchange.Accounts.Client.Api;
using Swisschain.Exchange.Accounts.Client.Common;
using Swisschain.Exchange.Accounts.Client.Models.Wallet;
using Swisschain.Exchange.Accounts.Contract;

namespace Swisschain.Exchange.Accounts.Client.Grpc
{
    internal class WalletsApi : BaseGrpcClient, IWalletApi
    {
        private readonly WalletsGrpc.WalletsGrpcClient _client;

        public WalletsApi(string address) : base(address)
        {
            _client = new WalletsGrpc.WalletsGrpcClient(Channel);
        }

        public async Task<IReadOnlyList<WalletModel>> GetAllAsync(IEnumerable<long> ids, string brokerId)
        {
            var request = new GetAllWalletsByIdsRequest();
            request.Ids.AddRange(ids);
            request.BrokerId = brokerId;

            var response = await _client.GetAllAsync(request);

            var result = response.Wallets.Select(x => new WalletModel(x)).ToList();

            return result;
        }
    }
}
