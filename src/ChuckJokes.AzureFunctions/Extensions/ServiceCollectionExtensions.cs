using System;
using ChuckJokes.Core.Interfaces.Persistence;
using ChuckJokes.Infrastructure.ApiClients;
using ChuckJokes.Infrastructure.AppSettings;
using ChuckJokes.Infrastructure.CosmosDbData.Repository;
using ChuckJokes.Infrastructure.Extensions;
using ChuckJokes.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ChuckJokes.AzureFunctions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureLogging(this IServiceCollection serviceCollection)
        {
            // if default ILogger is desired instead of Serilog
            //services.AddLogging();

            // configure serilog
            Serilog.Core.Logger logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("C:\\Logs\\log-ChuckApiFunction.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            serviceCollection.AddLogging(lb => lb.AddSerilog(logger));
            return serviceCollection;
        }
        
        public static IServiceCollection ConfigureCosmosDb(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // Bind database-related bindings
            CosmosDbSettings cosmosDbConfig = configuration.GetSection("ConnectionStrings:CosmosDB").Get<CosmosDbSettings>();
            // register CosmosDB client and data repositories
            serviceCollection.AddCosmosDb(cosmosDbConfig.EndpointUrl,
                cosmosDbConfig.PrimaryKey,
                cosmosDbConfig.DatabaseName,
                cosmosDbConfig.Containers);
            return serviceCollection;
        }
        
        public static IServiceCollection AddChuckApiHttpClient(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddHttpClient<ChuckApiClient>( client =>
            {
                client.BaseAddress = new Uri(configuration.GetConnectionStringOrSetting("ChuckApiUrl") ?? string.Empty);
            });
            return serviceCollection;
        }
        
        public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IJokeRepository, JokeRepository>();
            serviceCollection.AddScoped<IChuckApiClient, ChuckApiClient>();
            serviceCollection.AddScoped<IChuckApiScrapingService, ChuckApiScrapingService>();
            return serviceCollection;
        }
    }
}