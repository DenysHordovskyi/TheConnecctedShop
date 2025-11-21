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
            await _page.GotoAsync(ExpectedUrl);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        [Test]
        public async Task OpenHomePageTest()
        {

            string currentUrl = _page.Url;
            Assert.That(currentUrl, Is.EqualTo(ExpectedUrl), "Url page is not incorrect");

            string currentTitle = await _page.TitleAsync();
            Assert.That(currentTitle, Is.Not.Empty, "page is not title");
            Assert.That(currentTitle, Is.EqualTo(ExpectedTitle), $"Заголовок сторінки очікувався '{ExpectedTitle}', але отримано '{currentTitle}'");
        }
        [Test]
        public async Task LogoTest()
        {
            var logoLink = _page.Locator("a.header__heading-link");
            var logoImage = _page.Locator("header__heading-logo");
            string hrefLogo = await logoLink.GetAttributeAsync("href");
            Assert.That(hrefLogo, Is.EqualTo("/"), "не збігаєтся");

            string LogoAtributeWidth = await logoLink.GetAttributeAsync("width");
            Assert.That(hrefLogo, Is.EqualTo("180"));
            string LogoAtributeHeight = await logoLink.GetAttributeAsync("height");
            Assert.That(hrefLogo, Is.EqualTo("90.0"));
        }
        [Test]
        public async Task SearchFieldTest()
        {
            var searchField = _page.GetByTestId("Search");
            await Assertions.Expect(searchField).ToBeVisibleAsync();
        }
        [Test]
        public async Task SupportPhoneNumberTest()
        {
            var supportPhoneNumber = _page.GetByText("(305) 330-3424");
            Assert.That(supportPhoneNumber, Is.EqualTo("(305) 330-3424"));
        }
        [Test]
        public async Task CartTest()
        {
            var shoppingCart = _page.GetByTestId("cart-icon-bubble");
            await Assertions.Expect(shoppingCart).ToBeAttachedAsync();
        }
         
    }
}