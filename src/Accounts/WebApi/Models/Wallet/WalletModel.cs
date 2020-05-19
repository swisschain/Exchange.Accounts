using System;

namespace Accounts.WebApi.Models.Wallet
{
    public class WalletModel
    {
        public long Id { get; set; }

        public long AccountId { get; set; }

        public string Name { get; set; }

        public WalletType Type { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
