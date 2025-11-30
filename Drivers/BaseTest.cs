using Microsoft.Playwright;
using NUnit.Framework;
using TheConnectedShop.Drivers;
 
namespace TheConnectedShop.Drivers
{
    public abstract class BaseTest
    {
        protected IPlaywright Playwright;
        protected IBrowser Browser;
        protected IPage Page;
 
        [SetUp]
        public async Task Setup()
        {
            (Playwright, Browser, Page) = await PlaywrightDriver.InitBrowser();
        }
 
        [TearDown]
        public async Task TearDown()
        {
            await Browser.CloseAsync();
            Playwright.Dispose();
        }
    }
}