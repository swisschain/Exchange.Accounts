using System;
using Autofac;
using JetBrains.Annotations;
using Lykke.HttpClientGenerator;
using Lykke.HttpClientGenerator.Infrastructure;

namespace Accounts.Tests.RefitClient
{
    [PublicAPI]
    public static class AutofacExtension
    {
        public static void RegisterCryptoIndexClient(
            [NotNull] this ContainerBuilder builder,
            [NotNull] string url,
            [CanBeNull] Func<HttpClientGeneratorBuilder, HttpClientGeneratorBuilder> builderConfigure)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Url is empty.", nameof(url));

            var clientBuilder = HttpClientGenerator.BuildForUrl(url)
                .WithAdditionalCallsWrapper(new ExceptionHandlerCallsWrapper());

            clientBuilder = builderConfigure?.Invoke(clientBuilder) ?? clientBuilder.WithoutRetries();

            builder.RegisterInstance(new AccountsClient(clientBuilder.Create()))
                .As<IAccountsClient>()
                .SingleInstance();
        }
    }
}
