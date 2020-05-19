using Accounts.Common.Configuration;
using Autofac;

namespace Accounts.Domain.Services
{
    public class AutofacModule : Module
    {
        private readonly AppConfig _config;

        public AutofacModule(AppConfig config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>()
                .As<IAccountService>()
                .SingleInstance();

            builder.RegisterType<WalletService>()
                .As<IWalletService>()
                .SingleInstance();
        }
    }
}
