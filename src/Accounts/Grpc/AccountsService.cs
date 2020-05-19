using System.Threading.Tasks;
using Accounts.Domain.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Swisschain.Exchange.Accounts.Contract;
using Account = Accounts.Domain.Entities.Account;

namespace Accounts.Grpc
{
    internal class AccountsService : AccountsGrpc.AccountsGrpcBase
    {
        private readonly IAccountService _accountsService;

        public AccountsService(IAccountService accountsService)
        {
            _accountsService = accountsService;
        }

        public override async Task<AddAccountResponse> Add(AddAccountRequest request, ServerCallContext context)
        {
            var domain = new Account();
            domain.BrokerId = request.BrokerId;
            domain.Name = request.Name;
            domain.IsEnabled = request.IsEnabled;

            domain = await _accountsService.AddAsync(domain);

            var response = new AddAccountResponse();
            var model = new Swisschain.Exchange.Accounts.Contract.Account();
            model.Id = domain.Id;
            model.BrokerId = domain.BrokerId;
            model.Name = domain.Name;
            model.IsEnabled = domain.IsEnabled;
            model.Created = Timestamp.FromDateTime(domain.Created);
            model.Modified = Timestamp.FromDateTime(domain.Modified);

            response.Account = model;

            return response;
        }
    }
}
