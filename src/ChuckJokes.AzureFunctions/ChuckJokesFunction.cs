using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ChuckJokes.Core.Interfaces.Persistence;
using ChuckJokes.Infrastructure.Services;

namespace ChuckJokes.AzureFunctions
{
    public class ChuckJokesFunction
    {
        private readonly ILogger<ChuckJokesFunction> _logger;
        private readonly IChuckApiScrapingService _chuckApiScrapingService;
        private readonly IJokeRepository _jokeRepository;
        private const int JokesBatchSize = 10;


        public ChuckJokesFunction(ILogger<ChuckJokesFunction> logger, IChuckApiScrapingService chuckApiScrapingService, IJokeRepository jokeRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _chuckApiScrapingService = chuckApiScrapingService;
            _jokeRepository = jokeRepository;
        }

        [FunctionName("ChuckJokesFunction")]
        public async Task Run([TimerTrigger("*/5 * * * * *", RunOnStartup = true)] TimerInfo myTimer, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"ChuckJokesFunction Timer trigger function executed at: {DateTime.Now}");

            var jokes = await _chuckApiScrapingService.ScrapeJokesFromApi(JokesBatchSize, cancellationToken);

            foreach (var joke in jokes)
            {
                _logger.LogInformation($"Inserting joke id: {joke.Id}");
                await _jokeRepository.UpsertItemAsync(joke);
            }
        }
    }
}
