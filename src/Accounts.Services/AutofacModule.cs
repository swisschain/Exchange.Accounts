using Accounts.Common.Configuration;
using Accounts.Domain.Services;
using Autofac;

namespace Accounts.Services
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
