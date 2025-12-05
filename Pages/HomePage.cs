using Microsoft.Playwright;
using TheConnectedShop.Pages;
 
namespace TheConnectedShop.Pages
{
    public class HomePage : BasePage
    {
        private const string ExpectedUrl = "https://theconnectedshop.com/";
        private const string ExpectedTitle = "The Connected Shop - Smart Locks, Smart Sensors, Smart Home & Office";
 
        public HomePage(IPage page) : base(page) {}
 
        public async Task Open()
        {
            await Page.GotoAsync(ExpectedUrl);
        }
 
        public async Task<string> GetTitle() => await Page.TitleAsync();
        public string GetUrl() => Page.Url;
    }
}