﻿using System;

namespace Accounts.WebApi.Models.Account
{
    /// <summary>
    /// Represents account
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// Account identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Account name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Is account disabled
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Creation timestamp
        /// </summary>
        public DateTimeOffset Created { get; set; }
    }
}
