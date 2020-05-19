using Accounts.Tests.RefitClient.Api;
using JetBrains.Annotations;

namespace Accounts.Tests.RefitClient
{
    [PublicAPI]
    public interface IAccountsClient
    {
        IAccountApi Account { get; }
    }
}
