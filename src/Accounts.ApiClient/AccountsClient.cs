using Swisschain.Exchange.Accounts.ApiClient.Common;
using Swisschain.Exchange.Accounts.ApiContract;

namespace Swisschain.Exchange.Accounts.ApiClient
{
    public class AccountsClient : BaseGrpcClient, IAccountsClient
    {
        public AccountsClient(string serverGrpcUrl) : base(serverGrpcUrl)
        {
            Monitoring = new Monitoring.MonitoringClient(Channel);
        }

        public Monitoring.MonitoringClient Monitoring { get; }
    }
}
