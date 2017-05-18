using System.Linq;
using NUnit.Framework;
using Specflow.Core;
using Specflow.Core.Features;
using TechTalk.SpecFlow;

namespace Specflow.Tests.Features
{
    [Binding]
    public class CommonPageSteps : BaseFeature
    {
        public CommonPageSteps(ContextDriver contextDriver) : base(contextDriver)
        {
        }

        [Given(@"I am an anonymous user")]
        public void GivenIamAnonymous()
        {
            // nothing required
        }

        [Given(@"I visit the (.*) page")]
        public void GivenIEnterVisitThePage(string page)
        {
            GotoPage(GetUrlForPage(page));
        }

        [When(@"I visit the (.*) page")]
        public void WhenIVisitThePage(string page)
        {
            GotoPage(GetUrlForPage(page));
        }

        [Then(@"I should be presented the (.*) page")]
        public virtual void ThenIShouldBePresentedTheGivenPage(string page)
        {
            string actualUrl = GetUrlForPage(page);

            if (string.IsNullOrEmpty(actualUrl))
            {
                throw new AssertionException("The page specified was incorrect");
            }

            if (ContextDriver.CurrentDriver.Url != actualUrl)
            {
                throw new AssertionException($"Url not correct actual url was {actualUrl}");
            }
        }

        [Then(@"the Uri is (.*)")]
        public void ThenTheUriIs(string uri)
        {
            uri = uri.Split('/').Last();

            if (!ContextDriver.CurrentDriver.Url.Contains(uri))
            {
                AssertIsEqual(uri, ContextDriver.CurrentDriver.Url, "Expected url not found.");
            }
        }
    }
}
