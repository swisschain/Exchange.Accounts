using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Accounts.WebApi.Models.Account
{
    public class AccountRequestManyModel : PaginationRequest<long>
    {
        public string Name { get; set; }

        public bool? IsDisabled { get; set; }
    }
}
