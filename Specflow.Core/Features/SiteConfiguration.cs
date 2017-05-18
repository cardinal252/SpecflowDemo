using System.Collections.Generic;
using Newtonsoft.Json;

namespace Specflow.Core.Features
{
    public class SiteConfiguration
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("pages")]
        public IEnumerable<Page> Pages { get; set; }

        [JsonProperty("renderings")]
        public IEnumerable<RenderingDef> Renderings { get; set; }
    }
}
