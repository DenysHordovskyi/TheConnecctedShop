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
        await _homePage.Open();
        }
   
    }
}