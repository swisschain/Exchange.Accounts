using System.Threading.Tasks;
using Accounts.Domain.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Swisschain.Exchange.Accounts.Contract;

namespace Accounts.Grpc
{
    internal class WalletsService : WalletsGrpc.WalletsGrpcBase
    {
        private readonly IWalletService _walletsService;

        public WalletsService(IWalletService walletsService)
        {
            _walletsService = walletsService;
        }

        public override async Task<GetWalletsResponse> GetAll(GetAllWalletsByIdsRequest request, ServerCallContext context)
        {
            var domains = await _walletsService.GetAllAsync(request.Ids, request.BrokerId);

            var response = new GetWalletsResponse();

            foreach (var domain in domains)
            {
                var contract = new Wallet();

                contract.Id = domain.Id;
                contract.BrokerId = domain.BrokerId;
                contract.AccountId = domain.AccountId;
                contract.Name = domain.Name;
                contract.Type = (WalletType)domain.Type;
                contract.IsEnabled = domain.IsEnabled;
                contract.Created = domain.Created.ToTimestamp();
                contract.Modified = domain.Modified.ToTimestamp();

                response.Wallets.Add(contract);
            }

            return response;
        }
    }
}
