using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class Space
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
