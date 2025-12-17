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

        [Test]
        public async Task SearchField_ShouldBeEnabled()
        {
            var isSearchFieldEnabled = await _headerComponent.IsSearchFieldEnabled();
            Assert.That(isSearchFieldEnabled, Is.True, "Search field should be enabled");
        }

        [Test]
        public async Task SearchField_DropDownSearchItem_ShouldBeVisible()
        {
            const string setText = "Smart door";

            await _headerComponent.TypeSearchText(setText);

            await _headerComponent.ExpectFirstSearchResultVisible();
        }

        [Test]
        public async Task SearchField_DropDownSearchItem_ShoudContainSearchText()
        {
            const string setText = "smart door";
            await _headerComponent.TypeSearchText(setText);   
            await _headerComponent.ExpectFirstSearchResultContains(setText);

            var searchResult = await _headerComponent.GetFirstSearchResultText();
            Assert.That(searchResult.ToLower(), Does.Contain(setText));
        }
    }
}