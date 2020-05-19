using System;
using System.Collections.Generic;

namespace Accounts.Domain.Persistence.Entities
{
    public class AccountEntity
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public string Name { get; set; }

        public ICollection<WalletEntity> Wallets { get; set; } = new HashSet<WalletEntity>();

        public bool IsEnabled { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }

        public override string ToString()
        {
            return $"Id={Id}, BrokerId={BrokerId}, Name={Name}, Wallets={Wallets.Count}, IsEnabled={IsEnabled}, Created={Created}, Modified={Modified}";
        }
    }
}
