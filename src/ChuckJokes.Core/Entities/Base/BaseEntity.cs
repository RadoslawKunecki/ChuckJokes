using Newtonsoft.Json;

namespace ChuckJokes.Core.Entities.Base
{
    public abstract class BaseEntity
    {
        [JsonProperty(PropertyName = "id")]
        public virtual string Id { get; set; }
        
        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }
    }
}
