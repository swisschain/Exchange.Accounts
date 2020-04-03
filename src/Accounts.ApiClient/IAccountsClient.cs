using Swisschain.Exchange.Accounts.ApiContract;

namespace Swisschain.Exchange.Accounts.ApiClient
{
    public interface IAccountsClient
    {
        Monitoring.MonitoringClient Monitoring { get; }
    }
}
