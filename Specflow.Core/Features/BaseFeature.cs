using TechTalk.SpecFlow;
using System;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using Specflow.Core.Configuration;

namespace Specflow.Core.Features
{
    public abstract class BaseFeature
    {
        protected BaseFeature(ContextDriver contextDriver)
        {
            ContextDriver = contextDriver;
        }

        protected ContextDriver ContextDriver { get; private set; }

        [BeforeScenario("WebUI")]
        public void BeforeScenario()
        {
            ContextDriver?.CreateDriver();
        }

        [AfterScenario("WebUI")]
        public void AfterScenario()
        {
            ContextDriver?.DisposeDriver();
        }


        /// <summary>
        /// Test if the values passed are equal.
        /// </summary>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="assertFailedMessage">An error message to display if the value are not equal.</param>
        protected void AssertIsEqual(string expectedValue, string actualValue, string assertFailedMessage)
        {
            if (expectedValue != actualValue)
            {
                throw new AssertionException(
                    $"AssertIsEqual Failed: '{assertFailedMessage}' didn't match expectations. Expected [{expectedValue}], Actual [{actualValue}]");
            }
        }

        protected void AssertFailed(string assertFailMessage)
        {
            throw new AssertionException($"AssertFailed: {assertFailMessage}");
        }

        protected void AssertContains(string expectedValue, string actualValue, string assertFailedMessage)
        {
            if (!actualValue.Contains(expectedValue))
            {
                throw new AssertionException($"AssertContains Failed: '{assertFailedMessage}' didn't match expectations. Expected [{expectedValue}], Actual [{actualValue}]");
            }
        }

        protected void AssertRegEx(string regExPattern, string actualValue, string assertFailedMessage)
        {
            Match match;

            try
            {
                match = Regex.Match(regExPattern, actualValue);
            }
            catch
            {
                throw new AssertionException("AssertRegEx Failed: Unexpected exception");
            }

            if (match.Length <= 0)
            {
                throw new AssertionException($"{assertFailedMessage} '[Actual value {actualValue}' does not match regular expression {regExPattern}.]");
            }
        }
        protected void GotoPage(string page)
        {
            if (!String.IsNullOrEmpty(page) && !String.IsNullOrWhiteSpace(page))
            {
                ContextDriver.CurrentDriver.Navigate().GoToUrl(page);
            }
            else
            {
                throw new AssertionException("page argument is null");
            }
        }

        public string GetUrlForPage(string page)
        {
            return SiteConfigurationFactory.GetUrl(page);
        }

        public IWebElement GetHtmlElementByCssSelector(string cssSelector)
        {
            try
            {
                return ContextDriver.CurrentDriver.FindElementByCssSelector(cssSelector);
            }
            catch
            {
                throw new Exception("Element not found.");
            }
        }

        public bool DoesElementExist(string cssSelector)
        {
            try
            {
                return ContextDriver.CurrentDriver.FindElementByCssSelector(cssSelector) != null ? true : false;
            }
            catch
            {
                return false;
            }
        }

        private string RemoveCarriageReturns(string input)
        {
            return input.Replace("\r\n", "");
        }

        public string GetTextFromElement(IWebElement element)
        {
            string text = null;

            if (element != null)
            {
                text = RemoveCarriageReturns(element.Text);
            }

            return text;
        }

        public string GetColourFromElement(IWebElement element, string colourPropertyName)
        {
            string colour = null;

            if (element != null)
            {
                string rgbColour = element.GetCssValue(colourPropertyName);
                colour = ConvertRgbaToHex(rgbColour);
            }

            return colour;
        }

        public string ConvertRgbaToHex(string colourProperty)
        {
            if (!Regex.IsMatch(colourProperty, @"rgba\((\d{1,3},\s*){3}(0(\.\d+)?|1)\)"))
                throw new FormatException("rgba string was in a wrong format");

            MatchCollection matches = Regex.Matches(colourProperty, @"\d+");
            StringBuilder hexaString = new StringBuilder("#");

            for (int i = 0; i < matches.Count - 1; i++)
            {
                int value = Int32.Parse(matches[i].Value);

                hexaString.Append(value.ToString("X"));
            }

            return hexaString.ToString().ToLower();
        }

        public void TypeEnterKey(IWebElement element)
        {
            element.SendKeys("\r\n");
        }

        public bool DateFormatIsCorrect(string value)
        {
            return Regex.IsMatch(value, @"(([0-1])|([0-2][0-9])|([3][0-1]))\s(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[a-z]*\s\d{4}$");
        }
    }
}