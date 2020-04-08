using Accounts.Common.Configuration;
using Accounts.Domain.Repositories;
using Accounts.Repositories.Context;
using Autofac;

namespace Accounts.Repositories
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
