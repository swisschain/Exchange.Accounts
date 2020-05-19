using System;
using Accounts.Domain.Entities.Enums;

namespace Accounts.Domain.Persistence.Entities
{
    public class WalletEntity
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public long AccountId { get; set; }

        public string Name { get; set; }

        public WalletType Type { get; set; }

        public bool IsEnabled { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }

        public WalletEntity()
        {
        }

        public WalletEntity(string brokerId, long accountId, string name, WalletType type, bool isEnabled)
        {
            BrokerId = brokerId;
            AccountId = accountId;
            Name = name;
            Type = type;
            IsEnabled = isEnabled;
            Created = DateTimeOffset.UtcNow;
            Modified = Created;
        }

        public override string ToString()
        {
            return $"Id={Id}, BrokerId={BrokerId}, AccountId={AccountId}, Name={Name}, " +
                   $"Type={Type}, IsEnabled={IsEnabled}, Created={Created}, Modified={Modified}";
        }
    }
}
