using Microsoft.Playwright;
using NUnit.Framework;
using System.Buffers;
using System.Text.RegularExpressions;
using static Microsoft.Playwright.Assertions;


namespace TheConnectedShop
{
    public class HomePageTest
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;
        private const string ExpectedUrl = "https://theconnectedshop.com/";
        private const string ExpectedTitle = "The Connected Shop - Smart Locks, Smart Sensors, Smart Home & Office";

        [SetUp]
        public async Task SetupTest()
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
        /* [Test]
         public async Task SearchSuggestionsTest()
         {
             var searchField = _page.Locator("#Search-In-Inline");
             string searchValue = "smart door";

             await searchField.PressSequentiallyAsync(searchValue);
             await Task.Delay(3000);

             var suggestionLoc = _page.Locator(".predictive-search__heading").First;
             string suggestionBlock = "Suggestions";
             var blockName = await suggestionLoc.InnerTextAsync();

             Assert.That(suggestionBlock, Does.Contain(blockName));

             var firstSuggestionLoc = _page.Locator(".predictive-search__item-heading").First;
             string suggestionText = "smart door lock slim";
             var suggestionResult = await firstSuggestionLoc.InnerTextAsync();

             Assert.That(suggestionText, Does.Contain(suggestionResult));

             awa

             string searchResultPage = "https://theconnectedshop.com/search?q=smart+door+lock+slim&_pos=1&_psq=smart+door&_ss=e&_v=1.0";
             string currentUrl = _page.Url;

             Assert.That(currentUrl, Is.EqualTo(searchResultPage));
         }*/
        [Test]
        public async Task SearchSuggestionsTest()
        {
            var searchField = _page.Locator("#Search-In-Inline");
            string searchValue = "smart door";

            await searchField.PressSequentiallyAsync(searchValue);


            await _page.Locator(".predictive-search__heading").First.WaitForAsync(); //WaitForAsync очікування

            var suggestionLoc = _page.Locator(".predictive-search__heading").First;
            var blockName = await suggestionLoc.InnerTextAsync();
            string suggestionBlock = "Suggestions";
            Assert.That(blockName, Does.Contain(suggestionBlock));

            var firstSuggestionLoc = _page.Locator(".predictive-search__item-heading").First;
            var suggestionResult = await firstSuggestionLoc.InnerTextAsync();
            string suggestionText = "smart door lock slim";
            Assert.That(suggestionResult.ToLower(), Does.Contain(suggestionText));



            await firstSuggestionLoc.ClickAsync();


            await _page.WaitForURLAsync(url => url.Contains("/search"));

            string currentUrl = _page.Url;
            Assert.That(currentUrl, Does.Contain("smart+door"));

            var countLoc = _page.Locator("#ProductCount");
            var text = await countLoc.InnerTextAsync();

            int count = int.Parse(Regex.Match(text, @"\d+").Value);

            Assert.That(count, Is.GreaterThan(0));
        }
        // string message = await email.EvaluateAsync<string>("el => el.validationMessage");
        // Assert.That(message, Is.Not.Empty);

    private const string ContactButtonName = "Contact";
    private const string ContactUsText = "Contact Us"; 
    private const string NameLabel = "NAME";
    private const string EmailLabel = "EMAIL";
    private const string PhoneLabel = "PHONE NUMBER";
    private const string CommentLabel = "COMMENT";

        public HomePage(IPage page)
        {
            _page = page;
        }

        [Test]
        public async Task ContactTest()
        {

            var contactButtonLoc = _page.Locator("header").GetByRole(AriaRole.Link, new() { Name = "Contact", Exact = true });  //Exact = true Точне співпадіння
            await Expect(contactButtonLoc).ToBeVisibleAsync();
            await contactButtonLoc.ClickAsync();

            var contactUsHeading = _page.GetByText("Contact Us").First;
            var nameHeading = _page.GetByLabel("NAME");
            var emailHeading = _page.GetByLabel("EMAIL").First;
            var phoneNumberHeading = _page.GetByLabel("PHONE NUMBER");
            var commentHeading = _page.GetByLabel("COMMENT");

            await Expect(contactUsHeading).ToBeVisibleAsync();
            await Expect(nameHeading).ToBeVisibleAsync();
            await Expect(emailHeading).ToBeVisibleAsync();
            await Expect(phoneNumberHeading).ToBeVisibleAsync();
            await Expect(commentHeading).ToBeVisibleAsync();

            var sendButtonLoc = _page.GetByRole(AriaRole.Button, new() {Name = "Send"});
            await Expect(sendButtonLoc).ToBeVisibleAsync();
            await Expect(sendButtonLoc).ToBeEnabledAsync();   //Перевірка доступності. Можно нажати, реагует, не заблокований
            await sendButtonLoc.HoverAsync(); // імітує наведення курсору          


        }
     //  var isLogoVisible = await _homePage.Header.IsLogoVisible();
     //  Assert.That(isLogoVisible, Is.True, "Logo should be visible");
 }

}