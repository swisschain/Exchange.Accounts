using System;
using System.Net.Http;
using System.Threading.Tasks;
using Accounts.Tests.RefitClient.Api;
using Accounts.WebApi.Models.Account;
using Refit;
using Xunit;

namespace Accounts.Tests
{
    public class AccountsTest
    {
        [Fact(Skip = "manual")]
        public async Task Test()
        {
            var url = "http://localhost:5000";

            var authToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOlsic3dpc3NjaGFpbi5pbyIsInNpcml1cy5zd2lzc2NoYWluLmlvIiwiZXhjaGFuZ2Uuc3dpc3NjaGFpbi5pbyJdLCJ1c2VyLWlkIjoiZjllNzE4ZDUtM2M3Yy00YTUxLWFkNDMtYTliNzlmMDlkZDcxIiwidGVuYW50LWlkIjoiODM4MjlhYTEtNTg4OC00NWU0LTk5N2MtYjEzM2U1OGI3YWI4IiwibmJmIjoxNTg5NDg2MTg5LCJleHAiOjE1OTAwOTA5ODksImlhdCI6MTU4OTQ4NjE4OX0.C3LXgxXoB5cRVx0OCypIa3NaRjXFvyz8XHyDS2Yn6yw";
            var brokerId = "83829aa1-5888-45e4-997c-b133e58b7ab8";

            var client = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri(url)
            };
            client.DefaultRequestHeaders.Add("Authorization", authToken);
            client.DefaultRequestHeaders.Add("brokerId", brokerId);

            var builder = RequestBuilder.ForType<IAccountApi>();
            
            var accountsClient = RestService.For(client, builder);

            var getMany = new AccountRequestManyModel();

            var result = await accountsClient.GetManyAsync(getMany);
        }
    }
}
