﻿using System.Threading.Tasks;
using Swisschain.Exchange.Accounts.Client.Api;
using Swisschain.Exchange.Accounts.Client.Common;
using Swisschain.Exchange.Accounts.Client.Models.Account;
using Swisschain.Exchange.Accounts.Contract;

namespace Swisschain.Exchange.Accounts.Client.Grpc
{
    internal class AccountApi : BaseGrpcClient, IAccountApi
    {
        private readonly AccountsGrpc.AccountsGrpcClient _client;

        public AccountApi(string address) : base(address)
        {
            _client = new AccountsGrpc.AccountsGrpcClient(Channel);
        }

        public async Task<AccountModel> AddAsync(AccountAddModel accountAddModel)
        {
            var request = new AddAccountRequest();
            request.BrokerId = accountAddModel.BrokerId;
            request.Name = accountAddModel.Name;
            request.IsEnabled = accountAddModel.IsEnabled;

            var response = await _client.AddAsync(request);

            if (response.Account == null)
                return null;

            return new AccountModel(response.Account);
        }
    }
}