using System.Threading.Tasks;
using ChuckJokes.Core.Entities.Base;

namespace ChuckJokes.Core.Interfaces.Persistence
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task UpsertItemAsync(T item);

    }
}
