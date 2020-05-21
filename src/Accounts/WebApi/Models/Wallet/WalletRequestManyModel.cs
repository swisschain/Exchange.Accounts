using System.ComponentModel.DataAnnotations;
using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Accounts.WebApi.Models.Wallet
{
    public class WalletRequestManyModel : PaginationRequest<long>
    {
        [Required]
        public long AccountId { get; set; }

        public string Name { get; set; }

        public bool? IsEnabled { get; set; }

        public WalletType? Type { get; set; }
    }
}
