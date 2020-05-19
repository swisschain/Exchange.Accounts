using System;
using System.Collections.Generic;

namespace Accounts.Domain.Entities
{
    public class Account
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public string Name { get; set; }

        public ICollection<Wallet> Wallets { get; set; } = new HashSet<Wallet>();

        public bool IsEnabled { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
