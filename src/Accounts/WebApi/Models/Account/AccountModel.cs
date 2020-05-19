using System;

namespace Accounts.WebApi.Models.Account
{
    public class AccountModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
