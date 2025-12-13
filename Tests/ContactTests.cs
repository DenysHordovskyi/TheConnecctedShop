using Microsoft.Playwright;
using TheConnectedShop.Drivers;
using TheConnectedShop.Pages;

namespace TheConnectedShop.Tests
{
    [TestFixture]
    public class ContactTest : BaseTest
    {

        //  private IPlaywright _playwright;
        //  private IBrowser _browser;
        //  private IPage _page;
        private HomePage _homePage;
        private ContactPage _contactPage;

        [SetUp]
        public async Task Setup()
        {
            await base.Setup();
            _homePage = new HomePage(Page);
            _contactPage = new ContactPage(Page);
            await _homePage.Open();
        }
        [Test]
        public async Task ContactPage_ContactUsHeading_ShouldBeVisible()
        {
            var isContactUsHeadingVisible = await _contactPage.IsContactUsHeadingVisible();
            Assert.That(isContactUsHeadingVisible, Is.True, "Contact Us heading should be visible");
        }
        [Test]
        public async Task ContactPage_AllFormFields_ShouldBeVisible()
        {
            var areFormFieldsVisible = await _contactPage.AreAllFormFieldsVisible();
            Assert.That(areFormFieldsVisible, Is.True, "All form fields should be visible");
        }
        [Test]
        public async Task ContactPage_Heading_ShouldHaveText_ContactUs()
        {
            var headingText = await _contactPage.GetContactUsHeadingText();
            Assert.That(headingText, Is.EqualTo("Contact Us"), "Contact Us heading is incorrect");

        }
    }
}