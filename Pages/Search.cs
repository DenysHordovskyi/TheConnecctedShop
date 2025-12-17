using Microsoft.Playwright;

namespace TheConnectedShop.Pages
{

    public class Search : BasePage

    {
        public Search(IPage page) : base(page) { }
        private ILocator SearchField => Page.GetByTestId("Search-In-Inline");


        public async Task ClickSearchFild()
        {
            await SearchField.ClickAsync();
        }

        public async Task<bool> IsSearchFieldVisible()
        {
            return await SearchField.IsVisibleAsync();
        }

        public async Task<bool> IsSearchFieldEnabled()
        {
            return await SearchField.IsEnabledAsync();
        }

        public async Task<bool> IsSearchFieldEditable()
        {
            return await SearchField.IsEditableAsync();
        }

       

    }
}