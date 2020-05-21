using System;
using System.Collections.Generic;
using System.Linq;
using Swisschain.Exchange.Accounts.Client.Models.Wallet;

namespace Swisschain.Exchange.Accounts.Client.Models.Account
{
    public class AccountModel
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public string Name { get; set; }

        public IReadOnlyList<WalletModel> Wallets = new List<WalletModel>();

        public bool IsEnabled { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public AccountModel()
        {
        }

        public AccountModel(Contract.Account account)
        {
            Id = account.Id;
            BrokerId = account.BrokerId;
            Name = account.Name;
            Wallets = account.Wallets.Select(x => new WalletModel(x)).ToList();
            IsEnabled = account.IsEnabled;
            Created = account.Created.ToDateTime();
            Modified = account.Modified.ToDateTime();
        }
    }
}
