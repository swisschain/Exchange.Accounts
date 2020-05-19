using System;

namespace Accounts.WebApi.Models.Wallet
{
    public class Wallet
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public WalletType Type { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
