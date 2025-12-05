using Microsoft.Playwright;
 
namespace TheConnectedShop.Pages
{
    public abstract class BasePage
    {
        protected readonly IPage Page;
 
        protected BasePage(IPage page)
        {
            Page = page;
        }
 
        public ILocator Find(string selector) => Page.Locator(selector);

         public async Task WaitForPageLoad(){await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);} //чекає загррузку едементів DOM
    }
}