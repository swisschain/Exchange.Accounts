namespace Swisschain.Exchange.Accounts.Client.Models.Accounts
{
    public class AccountAddModel
    {
        public string BrokerId { get; set; }

        public string Name { get; set; }

        public bool IsDisabled { get; set; }

        public AccountTypeModel Type { get; set; }
    }
}
