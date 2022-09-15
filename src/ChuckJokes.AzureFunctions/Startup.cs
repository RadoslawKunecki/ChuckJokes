using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using ChuckJokes.AzureFunctions.Extensions;

[assembly: FunctionsStartup(typeof(ChuckJokes.AzureFunctions.Startup))]

namespace ChuckJokes.AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureServices(builder.Services);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configurations
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            
            services.AddSingleton<IConfiguration>(configuration);

            services.ConfigureLogging();
            services.ConfigureCosmosDb(configuration);
            services.ConfigureServices();
        }
    }
}
