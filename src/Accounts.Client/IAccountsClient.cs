using Swisschain.Exchange.Accounts.Client.Api;

namespace Swisschain.Exchange.Accounts.Client
{
    public interface IAccountsClient
    {
        IAccountsApi Accounts { get; }
    }
}
