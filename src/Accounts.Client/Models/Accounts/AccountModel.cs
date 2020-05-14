using System;
using Swisschain.Exchange.Accounts.Contract;

namespace Swisschain.Exchange.Accounts.Client.Models.Accounts
{
    public class AccountModel
    {
        public string Id { get; set; }

        public string BrokerId { get; set; }

        public string Name { get; set; }

        public bool IsDisabled { get; set; }

        public AccountTypeModel Type { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public AccountModel()
        {
        }

        public AccountModel(Account account)
        {
            Id = account.Id;
            BrokerId = account.BrokerId;
            Name = account.Name;
            IsDisabled = account.IsDisabled;
            Type = (AccountTypeModel)account.Type;
            Created = account.Created.ToDateTime();
            Modified = account.Modified.ToDateTime();
        }
    }
}
