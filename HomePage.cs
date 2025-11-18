using Microsoft.Playwright;
using NUnit.Framework;

namespace TheConnectedShop
{
    public class HomePage
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;
        private const string ExpectedUrl = "https://theconnectedshop.com/";
        private const string ExpectedTitle = "The Connected Shop - Smart Locks, Smart Sensors, Smart Home & Office";

        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            var context = await _browser.NewContextAsync();
            _page = await context.NewPageAsync();
        }
       [Test]
        public async Task OpenHomePageTest()
        {
            await _page.GotoAsync(ExpectedUrl);
           string currentUrl = _page.Url;
            Assert.That(currentUrl,Is.EqualTo(ExpectedUrl), "Url page is not incorrect");

            string currentTitle = await _page.TitleAsync();
            Assert.That(currentTitle,Is.Not.Empty, "page is not title");
            Assert.That(currentTitle, Is.EqualTo(ExpectedTitle), $"Заголовок сторінки очікувався '{ExpectedTitle}', але отримано '{currentTitle}'");
        }
    }
}