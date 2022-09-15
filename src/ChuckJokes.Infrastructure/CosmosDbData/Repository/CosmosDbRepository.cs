using ChuckJokes.Core.Entities.Base;
using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;
using ChuckJokes.Core.Interfaces.Persistence;
using ChuckJokes.Infrastructure.CosmosDbData.Interfaces;

namespace ChuckJokes.Infrastructure.CosmosDbData.Repository
{
    public abstract class CosmosDbRepository<T> : IRepository<T>, IContainerContext<T> where T : BaseEntity
    {
        public abstract string ContainerName { get; }

        private readonly ICosmosDbContainerFactory _cosmosDbContainerFactory;
        
        private readonly Container _container;

        public CosmosDbRepository(ICosmosDbContainerFactory cosmosDbContainerFactory)
        {
            _cosmosDbContainerFactory = cosmosDbContainerFactory ?? throw new ArgumentNullException(nameof(ICosmosDbContainerFactory));
            _container = _cosmosDbContainerFactory.GetContainer(ContainerName)._container;
        }

        public async Task UpsertItemAsync(T item)
        {
            await _container.UpsertItemAsync<T>(item, new PartitionKey(item.PartitionKey));
        }
    }
}
