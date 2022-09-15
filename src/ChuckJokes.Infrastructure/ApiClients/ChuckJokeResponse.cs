using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChuckJokes.Infrastructure.ApiClients
{
    public class ChuckJokeResponse
    {
        [JsonProperty("categories")]
        public List<object> Categories { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}