using TheConnectedShop.Components;
using TheConnectedShop.Drivers;
using TheConnectedShop.Pages;

namespace TheConnectedShop.Tests
{
    [TestFixture]
    public class SearchFieldTests : BaseTest
    {

        private HeaderComponent _headerComponent;
        private HomePage _homePage;
        


        [SetUp]
        public override async Task Setup()
        {
            await base.Setup();
            _homePage = new HomePage(Page);
            _headerComponent = new HeaderComponent(Page);             
            await _homePage.Open();
        }

        [Test]
        public async Task SearchField_ShouldHavePlaceholder_Search()
        {
            Assert.That(await _headerComponent.GetSearchPlaceholderText(), Is.EqualTo("Search"));
        }
    }
}