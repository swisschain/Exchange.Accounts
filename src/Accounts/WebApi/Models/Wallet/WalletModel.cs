using System;

namespace Accounts.WebApi.Models.Wallet
{
    public class WalletModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public WalletType Type { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
