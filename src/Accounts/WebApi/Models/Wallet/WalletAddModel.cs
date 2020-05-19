namespace Accounts.WebApi.Models.Wallet
{
    public class WalletAddModel
    {
        public string Name { get; set; }

        public long AccountId { get; set; }

        public bool IsEnabled { get; set; }

        public WalletType Type { get; set; }
    }
}
