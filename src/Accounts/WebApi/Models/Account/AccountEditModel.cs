namespace Accounts.WebApi.Models.Account
{
    public class AccountEditModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsEnabled { get; set; }
    }
}
