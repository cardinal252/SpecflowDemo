using Specflow.Core;
using Specflow.Core.Features;
using TechTalk.SpecFlow;

namespace Specflow.Tests.Features
{
    [Binding]
    public class HomePageSteps : BaseFeature
    {
        public HomePageSteps(ContextDriver contextDriver) : base(contextDriver)
        {
        }

        [Then("I can see the title (.*)")]
        public void ThenICanSeeATitle(string title)
        {
            var element = GetHtmlElementByCssSelector("h1.contentTitle");
            AssertIsEqual(title, element.Text, "The expected title was not found");
        }
    }
}
