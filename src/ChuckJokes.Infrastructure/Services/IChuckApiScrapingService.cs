using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChuckJokes.Core.Entities;

namespace ChuckJokes.Infrastructure.Services
{
    public interface IChuckApiScrapingService
    {
        Task<IEnumerable<Joke>> ScrapeJokesFromApi(int amount, CancellationToken cancellationToken);
    }
}