namespace Accounts.Common.Configuration
{
    public class AppConfig
    {
        public AccountsServiceConfig ExchangeAccounts { get; set; }

        public JwtConfig Jwt { get; set; }
    }
}
