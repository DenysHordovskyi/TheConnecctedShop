using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
 
namespace TheConnectedShop.Utils

{

    public static class GlobalMethods

    { 
        public static async Task ClickSafe(this ILocator locator)

        {

            await locator.WaitForAsync();
            //await locator.IsEnabledAsync();
            await locator.ClickAsync();

        }
        // public static async Task ClickSafe(
        //     this ILocator locator,
        //     string elementName = "Unknown element")
        // {
        //     Console.WriteLine($"Click: {elementName}");
 
        //     try
        //     {
        //         await locator.ScrollIntoViewIfNeededAsync();
        //         await locator.WaitForAsync(new()
        //         {
        //             State = WaitForSelectorState.Visible
        //         });
 
        //         await locator.ClickAsync();
        //         Console.WriteLine($"Click success: {elementName}");
        //     }
        //     catch (PlaywrightException ex)
        //     {
        //         Console.WriteLine(
        //             $"Click failed, trying force click: {elementName}\n{ex.Message}");
 
        //         try
        //         {
        //             await locator.ClickAsync(new() { Force = true });
        //             Console.WriteLine($"Force click success: {elementName}");
        //         }
        //         catch (Exception)
        //         {
        //             Console.WriteLine($"Click totally failed: {elementName}");
 
        //             throw new Exception(
        //                 $"Click failed on element: {elementName}",ex);
        //         }
        //     }
        // }
 
        public static async Task FillSafe(this ILocator locator, string value)

        {

            await locator.WaitForAsync();

            await locator.FillAsync(value);

        }
 
        public static async Task HoverSafe(this ILocator locator)

        {

            await locator.WaitForAsync();

            await locator.HoverAsync();

        }
 
    
 
        public static async Task<bool> IsVisibleSafe(this ILocator locator)

        {

            return await locator.IsVisibleAsync();

        }
 
        public static async Task<bool> IsEnabledSafe(this ILocator locator)

        {

            return await locator.IsEnabledAsync();

        }
 
        public static async Task<string> GetTextSafe(this ILocator locator)

        {

            return (await locator.TextContentAsync())?.Trim() ?? string.Empty;

        }
 
      
 
        public static async Task ExpectVisible(this ILocator locator)

        {

            await Assertions.Expect(locator).ToBeVisibleAsync();

        }
 
        public static async Task ExpectEnabled(this ILocator locator)

        {

            await Assertions.Expect(locator).ToBeEnabledAsync();

        }
 
        public static async Task ExpectCss(

            this ILocator locator,

            string cssProperty,

            string expectedValue)

        {

            await Assertions.Expect(locator)

                .ToHaveCSSAsync(cssProperty, expectedValue);

        }

    }

}

 