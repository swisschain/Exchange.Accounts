using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Worker.MessageConsumers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageConsumers(this IServiceCollection services)
        {
            // TODO: Just an example
            services.AddTransient<ExecuteSomethingConsumer>();

            return services;
        }
    }
}
