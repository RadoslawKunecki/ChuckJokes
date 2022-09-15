using System.Threading;
using System.Threading.Tasks;

namespace ChuckJokes.Infrastructure.ApiClients
{
    public interface IChuckApiClient
    {
        public Task<ChuckJokeResponse> GetRandomJoke(CancellationToken cancellationToken);
    }
}