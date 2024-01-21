using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Infrastructure.Configurations;

namespace Infrastructure.Extensions
{
    public static class AppConfigurationsExtensions
    {
        public static AppConfigurations RegisterConfigurations(this IServiceCollection servicesCollection, IConfiguration configuration)
        {
            servicesCollection.Configure<AppConfigurations>(options => configuration.Bind(options));
            servicesCollection.AddScoped(c => c.GetRequiredService<IOptionsSnapshot<AppConfigurations>>()?.Value); // Modified to use GetRequiredService

            var databaseConfiguration = new DatabaseConfiguration();
            configuration.GetSection(nameof(AppConfigurations.DatabBases)).Bind(databaseConfiguration);
            servicesCollection.AddSingleton(databaseConfiguration);

            return servicesCollection.BuildServiceProvider().GetRequiredService<AppConfigurations>(); // Modified to use GetRequiredService
        }
    }
}
