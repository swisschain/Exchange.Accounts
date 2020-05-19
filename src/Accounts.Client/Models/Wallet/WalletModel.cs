using System;

namespace Swisschain.Exchange.Accounts.Client.Models.Wallet
{
    public class WalletModel
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public long AccountId { get; set; }

        public string Name { get; set; }

        public WalletType Type { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public WalletModel()
        {
        }

        public WalletModel(Contract.Wallet wallet)
        {
            Id = wallet.Id;
            BrokerId = wallet.BrokerId;
            AccountId = wallet.AccountId;
            Name = wallet.Name;
            Type = (WalletType)wallet.Type;
            IsEnabled = wallet.IsEnabled;
            Created = wallet.Created.ToDateTime();
            Modified = wallet.Modified.ToDateTime();
        }
    }
}
