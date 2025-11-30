using Microsoft.Playwright;
 
namespace TheConnectedShop.Drivers
{
    public static class PlaywrightDriver
    {
        public static async Task<(IPlaywright playwright, IBrowser browser, IPage page)> InitBrowser()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
 
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
 
            return (playwright, browser, page);
        }
    }
}