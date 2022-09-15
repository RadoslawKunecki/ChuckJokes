using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ChuckJokes.Infrastructure.ApiClients
{
    public class ChuckApiClient : IChuckApiClient
    {
        private readonly HttpClient _httpClient;
        private const string RandomJokeUrl = "/jokes/random";

        public ChuckApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChuckJokeResponse> GetRandomJoke(CancellationToken cancellationToken) =>
            await _httpClient.GetFromJsonAsync<ChuckJokeResponse>(
                RandomJokeUrl);
    }
}