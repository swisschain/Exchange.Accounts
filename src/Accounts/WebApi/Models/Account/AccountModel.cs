using System;
using System.Collections.Generic;
using Accounts.WebApi.Models.Wallet;

namespace Accounts.WebApi.Models.Account
{
    public class AccountModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public IReadOnlyCollection<WalletModel> Wallets = new List<WalletModel>();

        public bool IsEnabled { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
