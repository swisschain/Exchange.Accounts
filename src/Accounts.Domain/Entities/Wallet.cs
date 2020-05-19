using System;
using Accounts.Domain.Entities.Enums;

namespace Accounts.Domain.Entities
{
    public class Wallet
    {
        public long Id { get; set; }

        public long AccountId { get; set; }

        public string Name { get; set; }

        public WalletType Type { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public override string ToString()
        {
            return $"Id={Id}, AccountId={AccountId}, Name={Name}, Type={Type}, IsEnabled={IsEnabled}, Created={Created}, Modified={Modified}";
        }
    }
}
