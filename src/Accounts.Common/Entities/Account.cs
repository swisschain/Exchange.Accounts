using System;
using Accounts.Domain.Entities.Enums;

namespace Accounts.Domain.Entities
{
    /// <summary>
    /// Represents account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Broker identifier.
        /// </summary>
        public string BrokerId { get; set; }

        /// <summary>
        /// Account name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Is account disabled.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Account type.
        /// </summary>
        public AccountType Type { get; set; }

        /// <summary>
        /// Creation timestamp.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The last update date and time.
        /// </summary>
        public DateTime Modified { get; set; }
    }
}
