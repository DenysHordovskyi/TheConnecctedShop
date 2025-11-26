using Microsoft.Playwright;
using NUnit.Framework;
using System.Buffers;
using static Microsoft.Playwright.Assertions;


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
            var logoLink = _page.Locator(".header__heading-link");
            var logoImage = _page.Locator(".header__heading-logo");
            await Assertions.Expect(logoLink).ToBeVisibleAsync();
            await Assertions.Expect(logoImage).ToBeVisibleAsync();

            string hrefLogo = await logoLink.GetAttributeAsync("href");
            Assert.That(hrefLogo, Is.EqualTo("/"), "не збігаєтся");

            string logoAtributeWidth = await logoImage.GetAttributeAsync("width");
            Assert.That(logoAtributeWidth, Is.EqualTo("180"));
            string logoAtributeHeight = await logoImage.GetAttributeAsync("height");
            Assert.That(logoAtributeHeight, Is.EqualTo("90.0"));
        }
        [Test]
        public async Task SearchFieldEnableAndEditableTest()
        {
            var searchField = _page.Locator("#Search-In-Inline");
            await Assertions.Expect(searchField).ToBeVisibleAsync();

            bool isEnabled = await searchField.IsEnabledAsync();
            bool isEditable = await searchField.IsEditableAsync();
            Assert.That(isEnabled, Is.True, "Search field should be enabled");
            Assert.That(isEditable, Is.True, "Search field should be editable");

            string placeholder = await searchField.GetAttributeAsync("placeholder");
            Assert.That(placeholder, Is.Not.Null.And.Not.Empty, "Search field should have placeholder");
            Assert.That(placeholder, Is.EqualTo("Search"), "не збігаєтся");
            Console.WriteLine($"Placeholder text: {placeholder}");

            string testSearchText = "smart lock";            
            await searchField.FillAsync(testSearchText);

            string actualText = await searchField.InputValueAsync(); //Читає що написано
            Assert.That(actualText, Is.EqualTo(testSearchText), $"Expected '{testSearchText}' but got '{actualText}'");
        }

       [Test]
        public async Task SupportPhoneNumberTest()
        {
            await Expect(_page.GetByRole(AriaRole.Link, new() { Name = "(305) 330-3424" }).First).ToBeVisibleAsync();           
        }

        [Test]
        public async Task CartTest()
        {
            var shoppingCart = _page.Locator(".header__cart-inner");
            await Expect(shoppingCart).ToBeVisibleAsync();
        }

        [Test]
        public async Task SearchItemTest()
        {
            var searchField = _page.Locator("#Search-In-Inline");
            await Expect(searchField).ToBeVisibleAsync();
            string searchValue = "smart door";

            await searchField.PressSequentiallyAsync(searchValue);
            await Task.Delay(3000);
            
            var searchItem = _page.Locator(".predictive-search__item__info").First;
            var resultText = await searchItem.InnerTextAsync();    //Зчитує текст 
          
            
            Assert.That(resultText.ToLower(), Does.Contain(searchValue)); //ToLower не чутливий до рієстру                                                                          
        }
        [Test]
        public async Task SearchUnknownItemTest()
        {
            var searchField = _page.Locator("#Search-In-Inline");
            string searchUnknownValue = "qqqqq";
            string textForWrongRuesalt = "Search for “qqqqq”";

            await searchField.PressSequentiallyAsync(searchUnknownValue);
            await Task.Delay(3000);

            var searchItem = _page.Locator(".predictive-search__header").First;
            var resultText = await searchItem.InnerTextAsync();

            Assert.That(resultText, Does.Contain(textForWrongRuesalt));

            await searchItem.ClickAsync();
            string descriptionWrongResult = "No results found for “qqqqq”. Check the spelling or use a different word or phrase.";
            var descriptionLoc = _page.Locator(".alert").First;
            var descriptionResult = await descriptionLoc.InnerTextAsync(); 

            Assert.That(descriptionWrongResult, Does.Contain(descriptionResult));




        }
    }
}