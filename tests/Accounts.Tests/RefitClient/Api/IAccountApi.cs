using System.Threading.Tasks;
using Accounts.WebApi.Models.Account;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Refit;
using Swisschain.Sdk.Server.WebApi.Pagination;

namespace Accounts.Tests.RefitClient.Api
{
    [PublicAPI]
    public interface IAccountApi
    {
        [Get("/api/account")]
        Task<Paginated<AccountModel, long>> GetManyAsync([FromQuery] AccountRequestManyModel request);

        [Get("/api/account/{id}")]
        Task<AccountModel> GetByIdAsync(long id);

        [Post("/api/account")]
        Task<AccountModel> AddAsync(AccountAddModel account);

        [Put("/api/account")]
        Task<AccountModel> UpdateAsync(AccountEditModel account);
    }
}
