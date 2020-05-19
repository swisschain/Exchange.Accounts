using Accounts.Tests.RefitClient.Api;
using Lykke.HttpClientGenerator;

namespace Accounts.Tests.RefitClient
{
    public class AccountsClient : IAccountsClient
    {
        public IAccountApi Account { get; }

        public AccountsClient(IHttpClientGenerator httpClientGenerator)
        {
            Account = httpClientGenerator.Generate<IAccountApi>();
        }
    }
}
