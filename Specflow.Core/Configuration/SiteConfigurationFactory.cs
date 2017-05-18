using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Sitecore.Data;
using Specflow.Core.Features;

namespace Specflow.Core.Configuration
{
    public class SiteConfigurationFactory
    {
        private static SiteConfiguration siteConfiguration;

        static SiteConfigurationFactory()
        {
            var serializer = JsonSerializer.Create();
            using (var textReader = File.OpenText($"{GetAssemblyDirectory()}\\siteconfiguration.json"))
            {
                JsonReader reader = new JsonTextReader(textReader);
                siteConfiguration = serializer.Deserialize<SiteConfiguration>(reader);

            }
        }

        public static string GetUrl(string name)
        {
            var page = siteConfiguration.Pages.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            return $"{siteConfiguration.BaseUrl}{page?.RelativePath ?? String.Empty}";
        }

        public static ID GetRenderingId(string name)
        {
            var renderingDef = siteConfiguration.Renderings.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (renderingDef == null)
            {
                return null;
            }

            Guid result;
            return Guid.TryParse(renderingDef.Id, out result) ? new ID(result) : null;
        }

        private static string GetAssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
