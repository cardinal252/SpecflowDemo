using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Specflow.Core;
using Specflow.Core.Features;
using TechTalk.SpecFlow;

namespace Specflow.Tests.Features
{
    [Binding]
    public class Warmer : BaseFeature
    {
        public Warmer(ContextDriver contextDriver) : base(contextDriver)
        {
        }

        [When("I click the button")]
        public void ClickTheButton()
        {
            GetHtmlElementByCssSelector("input.clickable").Click();
        }

        [Then("I get a warming message")]
        public void TellMeIAmGreat()
        {
            AssertIsNotEmpty(GetHtmlElementByCssSelector("span.awesome").Text, "Awesome was empty");
        }
    }
}
