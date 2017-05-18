using Newtonsoft.Json;

namespace Specflow.Core.Features
{
    public class Page
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("relativePath")]
        public string RelativePath { get; set; }
    }
}
