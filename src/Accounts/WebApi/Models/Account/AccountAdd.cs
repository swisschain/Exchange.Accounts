﻿namespace Accounts.WebApi.Models.Account
{
    /// <summary>
    /// Represents account
    /// </summary>
    public class AccountAdd
    {
        /// <summary>
        /// Account name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Is account disabled
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}