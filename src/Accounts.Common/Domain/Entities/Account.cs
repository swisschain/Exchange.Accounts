using System;

namespace Accounts.Common.Domain.Entities
{
    /// <summary>
    /// Represents account
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Broker identifier
        /// </summary>
        public string BrokerId { get; set; }

        /// <summary>
        /// Is account active
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Creation timestamp
        /// </summary>
        public DateTimeOffset Created { get; set; }
    }
}
