using System;
using Accounts.Domain.Entities.Enums;

namespace Accounts.Domain.Persistence.Entities
{
    public class WalletEntity
    {
        public long Id { get; set; }

        public long AccountId { get; set; }

        public string Name { get; set; }

        public WalletType Type { get; set; }

        public bool IsEnabled { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
