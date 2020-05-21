using MoreLinq;
using System.Threading.Tasks;
using Accounts.Domain.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Swisschain.Exchange.Accounts.Contract;

namespace Accounts.Grpc
{
    internal class AccountsService : AccountsGrpc.AccountsGrpcBase
    {
        private readonly IAccountService _accountsService;

        public AccountsService(IAccountService accountsService)
        {
            _accountsService = accountsService;
        }

        public override async Task<GetAccountsResponse> GetAll(GetAllAccountsByIdsRequest request, ServerCallContext context)
        {
            var domains = await _accountsService.GetAllAsync(request.Ids, request.BrokerId);

            var response = new GetAccountsResponse();

            foreach (var domain in domains)
            {
                var contract = Map(domain);

                response.Accounts.Add(contract);
            }

            return response;
        }

        public override async Task<GetAccountResponse> Get(GetAccountByIdRequest request, ServerCallContext context)
        {
            var domain = await _accountsService.GetByIdAsync(request.Id, request.BrokerId);

            var response = new GetAccountResponse();

            if (domain == null)
                return response;

            var contract = Map(domain);

            response.Account = contract;

            return response;
        }

        public override async Task<AddAccountResponse> Add(AddAccountRequest request, ServerCallContext context)
        {
            var domain = new Domain.Entities.Account();
            domain.BrokerId = request.BrokerId;
            domain.Name = request.Name;
            domain.IsEnabled = request.IsEnabled;

            domain = await _accountsService.AddAsync(domain);

            var response = new AddAccountResponse();

            var contract = Map(domain);

            response.Account = contract;

            return response;
        }

        internal static Account Map(Domain.Entities.Account domain)
        {
            var contract = new Account();

            contract.Id = domain.Id;
            contract.BrokerId = domain.BrokerId;
            contract.Name = domain.Name;
            domain.Wallets.ForEach(x => contract.Wallets.Add(WalletsService.Map(x)));
            contract.IsEnabled = domain.IsEnabled;
            contract.Created = domain.Created.ToTimestamp();
            contract.Modified = domain.Modified.ToTimestamp();

            return contract;
        }
    }
}
