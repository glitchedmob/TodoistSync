using Newtonsoft.Json;

namespace TodoistSync.Models.Clickup
{
    public class Tag
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tag_fg")]
        public string TagFg { get; set; }

        [JsonProperty("tag_bg")]
        public string TagBg { get; set; }

        [JsonProperty("creator")]
        public User Creator { get; set; }
    }
}
