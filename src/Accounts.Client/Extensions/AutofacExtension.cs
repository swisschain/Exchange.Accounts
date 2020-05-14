using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace Swisschain.Exchange.Accounts.Client.Extensions
{
    public static class AutofacExtension
    {
        public static void RegisterAccountsClient(
            [NotNull] this ContainerBuilder builder,
            [NotNull] AccountsClientSettings settings)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            builder.RegisterInstance(new AccountsClient(settings))
                .As<IAccountsClient>()
                .SingleInstance();
        }
    }
}
