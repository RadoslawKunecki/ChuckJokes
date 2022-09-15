using ChuckJokes.Core.Entities;
using ChuckJokes.Core.Interfaces.Persistence;
using ChuckJokes.Infrastructure.CosmosDbData.Interfaces;

namespace ChuckJokes.Infrastructure.CosmosDbData.Repository
{
    public class JokeRepository : CosmosDbRepository<Joke>, IJokeRepository
    {
        public override string ContainerName { get; } = "Joke";

        public JokeRepository(ICosmosDbContainerFactory factory) : base(factory)
        { }
    }
}
