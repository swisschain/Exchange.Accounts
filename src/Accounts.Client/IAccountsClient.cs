using Swisschain.Exchange.Accounts.Client.Api;

namespace Swisschain.Exchange.Accounts.Client
{
    public interface IAccountsClient
    {
        IAccountApi Account { get; }

        IWalletApi Wallet { get; }
    }
}
