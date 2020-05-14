using Swisschain.Exchange.Accounts.Client.Api;
using Swisschain.Exchange.Accounts.Client.Common;
using Swisschain.Exchange.Accounts.Client.Grpc;

namespace Swisschain.Exchange.Accounts.Client
{
    public class AccountsClient : BaseGrpcClient, IAccountsClient
    {
        public AccountsClient(AccountsClientSettings settings) : base(settings.ServiceAddress)
        {
            Accounts = new AccountsApi(settings.ServiceAddress);
        }

        public IAccountsApi Accounts { get; }
    }
}
