using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChuckJokes.Core.Entities;
using ChuckJokes.Infrastructure.ApiClients;

namespace ChuckJokes.Infrastructure.Services
{
    public class ChuckApiScrapingService : IChuckApiScrapingService
    {
        const int MaximumJokeLength = 200;
        private readonly IChuckApiClient _chuckApiClient;

        public ChuckApiScrapingService(IChuckApiClient chuckApiClient)
        {
            _chuckApiClient = chuckApiClient;
        }

        public async Task<IEnumerable<Joke>> ScrapeJokesFromApi(int amount, CancellationToken cancellationToken)
        {
            if (amount < 1)
            {
                throw new ArgumentException("Amount should be greater than 0");
            }

            var scrappedChuckJokes = await ScrapChuckJokes(amount, cancellationToken);
            var mappedJokes = MapJoke(scrappedChuckJokes);
            return mappedJokes;
        }

        private async Task<IEnumerable<ChuckJokeResponse>> ScrapChuckJokes(int amount, CancellationToken cancellationToken)
        {
            var scrapingTasks = Enumerable
                .Range(0, amount)
                .Select(_ => _chuckApiClient.GetRandomJoke(cancellationToken));
            var scrappedChuckJokes = await Task.WhenAll(scrapingTasks);
            
            return scrappedChuckJokes.Where(x => x.Value.Length <= MaximumJokeLength);
        }

        private static IEnumerable<Joke> MapJoke(IEnumerable<ChuckJokeResponse> results)
        {
            return results.Select(x => new Joke()
            {
                Id = x.Id,
                PartitionKey = "ChuckJokes",
                Value = x.Value
            });
        }
    }
}