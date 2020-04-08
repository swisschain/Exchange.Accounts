using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Accounts.WebApi.Models.Account
{
    public class AccountRequestMany : PaginationRequest<string>
    {
        /// <summary>
        /// Account identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Account name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Is account disabled
        /// </summary>
        public bool? IsDisabled { get; set; }
    }
}
