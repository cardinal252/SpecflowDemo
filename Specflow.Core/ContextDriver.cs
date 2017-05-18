using System;
using OpenQA.Selenium.PhantomJS;
using Specflow.Core.Specflow.Core;
using TechTalk.SpecFlow;

namespace Specflow.Core
{
    /// <summary>
    /// Encapsulates the functionality of a headless web browser.
    /// </summary>
    public class ContextDriver
    {
        private PhantomJSDriver currentDriver;

        public PhantomJSDriver CurrentDriver
        {
            get { return currentDriver ?? (currentDriver = ScenarioContext.Current[Constants.CurrentDriverKey] as PhantomJSDriver); }
        }

        public void CreateDriver()
        {
            if (ScenarioContext.Current.ContainsKey(Constants.CurrentDriverKey))
            {
                return;
            }

            PhantomJSDriverService driverService = PhantomJSDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = false;

            PhantomJSDriver newDriver = new PhantomJSDriver(driverService);
            newDriver = new PhantomJSDriver(driverService);

            ScenarioContext.Current.Add(Constants.CurrentDriverKey, newDriver);
        }

        public void DisposeDriver()
        {
            if (currentDriver != null)
            {
                try
                {
                    currentDriver.Quit();
                    currentDriver.Dispose();
                }
                catch
                {
                }
            }

            if (ScenarioContext.Current.ContainsKey(Constants.CurrentDriverKey))
            {
                ScenarioContext.Current.Remove(Constants.CurrentDriverKey);
            }
        }
    }
}