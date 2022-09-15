﻿using Microsoft.Azure.Cosmos;

namespace ChuckJokes.Infrastructure.CosmosDbData.Interfaces
{
    public interface ICosmosDbContainer
    {
        /// <summary>
        ///     Instance of Azure Cosmos DB Container class
        /// </summary>
        Container _container { get; }
    }
}
