using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Accounts.WebApi.Models.Wallet
{
    public class WalletRequestManyModel : PaginationRequest<long>
    {
        public string Name { get; set; }

        public bool? IsEnabled { get; set; }
    }
}
