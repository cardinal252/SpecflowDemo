using Newtonsoft.Json;

namespace Specflow.Core.Features
{
    public class RenderingDef
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
