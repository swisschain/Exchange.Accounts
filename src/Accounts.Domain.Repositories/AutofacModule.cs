using Accounts.Common.Configuration;
using Accounts.Domain.Persistence.Context;
using Accounts.Domain.Persistence.Repositories;
using Accounts.Domain.Repositories;
using Autofac;

namespace Accounts.Domain.Persistence
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
            builder.RegisterType<ConnectionFactory>()
                .AsSelf()
                .WithParameter(TypedParameter.From(_config.ExchangeAccounts.Db.ConnectionString))
                .SingleInstance();

            builder.RegisterType<AccountRepository>()
                .As<IAccountRepository>()
                .SingleInstance();
        }
    }
}
