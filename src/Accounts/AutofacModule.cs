using Accounts.Common.Configuration;
using Accounts.Common.Domain.Services;
using Accounts.Common.Services;
using Autofac;

namespace Accounts
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
        }
    }
}
