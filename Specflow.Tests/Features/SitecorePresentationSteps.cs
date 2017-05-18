using System;
using System.Linq;
using Sitecore.Data;
using Specflow.Core;
using Specflow.Core.Configuration;
using Specflow.Core.Features;
using TechTalk.SpecFlow;

namespace Specflow.Tests.Features
{
    [Binding]
    public class SitecorePresentationSteps : BaseFeature
    {
        public SitecorePresentationSteps(ContextDriver contextDriver) : base(contextDriver)
        {
        }

        [Then("I see the (.*) rendering")]
        public void ThenICanSeeARendering(string renderingName)
        {
            ID id = SiteConfigurationFactory.GetRenderingId(renderingName);
            if (id == null as ID)
            {
                throw new Exception("Id not found.");
            }

            var renderingsOnScreen = ContextDriver.CurrentDriver.FindElementsByClassName("renderingInfo");
            bool result = renderingsOnScreen.Any(x =>
            {
                string attribute = x.GetAttribute("data-renderingid");
                return String.Equals(attribute, id.ToString(), StringComparison.OrdinalIgnoreCase);
            });
            if (!result)
            {
                throw new Exception("Rendering not found.");
            }
        }
    }
}
