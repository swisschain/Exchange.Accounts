﻿using System;
using Swisschain.Exchange.Accounts.Contract;

namespace Swisschain.Exchange.Accounts.Client.Models.Accounts
{
    public class AccountModel
    {
        public long Id { get; set; }

        public string BrokerId { get; set; }

        public string Name { get; set; }

        public bool IsEnabled { get; set; }

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
            IsEnabled = account.IsEnabled;
            Created = account.Created.ToDateTime();
            Modified = account.Modified.ToDateTime();
        }
    }
}
