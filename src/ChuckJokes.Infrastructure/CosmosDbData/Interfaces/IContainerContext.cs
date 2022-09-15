using ChuckJokes.Core.Entities.Base;

namespace ChuckJokes.Infrastructure.CosmosDbData.Interfaces
{
    /// <summary>
    ///  Defines the container level context
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContainerContext<T> where T : BaseEntity
    {
        string ContainerName { get; }
    }
}
