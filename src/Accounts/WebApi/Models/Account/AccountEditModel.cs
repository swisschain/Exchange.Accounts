namespace Accounts.WebApi.Models.Account
{
    /// <summary>
    /// Represents account
    /// </summary>
    public class AccountEditModel
    {
        /// <summary>
        /// Account identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Is account active
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
