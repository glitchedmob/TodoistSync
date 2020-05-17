using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class Project
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("access")]
        public bool Access { get; set; }
    }
}
