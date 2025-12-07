using Microsoft.Playwright;
using TheConnectedShop.Pages;

namespace TheConnectedShop.Components

{

    public class HeaderComponent : BasePage

    {
        private ILocator LogoLink => Page.Locator(".header__heading-link");
        private ILocator LogoImage => Page.Locator(".header__heading-logo");
        private ILocator PhoneNumberLink => Page.GetByRole(AriaRole.Link, new() { Name = "(305) 330-3424" }).First;
        private ILocator ShoppingCart => Page.Locator(".header__cart-inner");
        private ILocator ContactButton => Page.Locator("header").GetByRole(AriaRole.Link, new() { Name = "Contact", Exact = true });
        private ILocator SearchField => Page.Locator("#Search-In-Inline");
        private ILocator NavigationMenu => Page.Locator(".header__inline-menu");
        private ILocator MainMenuItems => Page.Locator(".header__menu-item");
        private ILocator CartIcon => Page.Locator(".header__icon--cart");
        private ILocator CartCount => Page.Locator(".cart-count-bubble");
        private ILocator AccountIcon => Page.Locator(".header__icon--account"); 
        public HeaderComponent(IPage page) : base(page) { }
 
        public async Task<bool> IsLogoVisible()

        {
            return await LogoLink.IsVisibleAsync() && await LogoImage.IsVisibleAsync();
        }
 
        public async Task ClickLogo()

        {
            await LogoLink.ClickAsync();
            await WaitForPageLoad(); //після кліку очікуємо коли загрузится
        }
 
        public async Task<string> GetLogoHref()

        {
            return await LogoLink.GetAttributeAsync("href") ?? string.Empty; //перевіряє чи є ссилка
        }
 
        public async Task<(string width, string height)> GetLogoDimensions()

        {
            var width = await LogoImage.GetAttributeAsync("width") ?? string.Empty;
            var height = await LogoImage.GetAttributeAsync("height") ?? string.Empty;
            return (width, height);
        }
       

        public async Task<bool> IsPhoneNumberVisible()

        {
            return await PhoneNumberLink.IsVisibleAsync();
        }
 
        public async Task<string> GetPhoneNumberText()

        {
            return await PhoneNumberLink.TextContentAsync() ?? string.Empty;
        }
 
        public async Task ClickPhoneNumber()

        {
            await PhoneNumberLink.ClickAsync();
        }        

        public async Task<bool> IsCartVisible()

        {
            return await ShoppingCart.IsVisibleAsync();
        }
 
        public async Task ClickCart()

        {

            await ShoppingCart.ClickAsync();

            await WaitForPageLoad();

        }
 
        public async Task<bool> IsCartIconVisible()

        {
            return await CartIcon.IsVisibleAsync();
        }
 
        public async Task<int> GetCartItemCount()

        {
            if (await CartCount.IsVisibleAsync())

            {
                var countText = await CartCount.TextContentAsync();
                if (int.TryParse(countText, out int count))
                {
                    return count;
                }
            }
            return 0;
        }     

        public async Task ClickContactButton()

        {
            await ContactButton.ClickAsync();
            await WaitForPageLoad();
        }
 
        public async Task<bool> IsContactButtonVisible()

        {
            return await ContactButton.IsVisibleAsync();
        }
 
       

        public async Task<bool> IsSearchFieldVisible()

        {
            return await SearchField.IsVisibleAsync();
        }
 
        public async Task SetSearchText(string text)

        {
            await SearchField.FillAsync(text);
        }
 
        public async Task TypeSearchText(string text)

        {
            await SearchField.PressSequentiallyAsync(text);
        }
 
        public async Task<string> GetSearchText()

        {
            return await SearchField.InputValueAsync();
        }
 
        public async Task ClearSearchField()

        {
            await SearchField.ClearAsync();
        }
 
        public async Task SubmitSearch()

        {
            await SearchField.PressAsync("Enter");
            await WaitForPageLoad();
        }
 
        

        public async Task<bool> IsNavigationMenuVisible()

        {
            return await NavigationMenu.IsVisibleAsync();
        }
 
        public async Task<List<string>> GetMenuItemsText()

        {
            var items = new List<string>();
            var count = await MainMenuItems.CountAsync();
            for (int i = 0; i < count; i++)
            {
                var text = await MainMenuItems.Nth(i).TextContentAsync();
                if (!string.IsNullOrEmpty(text))
                {
                    items.Add(text.Trim());
                }
            }
            return items;
        }
 
        public async Task ClickMenuItemByText(string menuText)

        {

            var menuItem = Page.GetByRole(AriaRole.Link, new() { Name = menuText, Exact = true });

            if (await menuItem.IsVisibleAsync())

            {

                await menuItem.ClickAsync();

                await WaitForPageLoad();

            }

        }
 
        public async Task ClickMenuItemByIndex(int index)

        {

            if (await MainMenuItems.Nth(index).IsVisibleAsync())

            {

                await MainMenuItems.Nth(index).ClickAsync();

                await WaitForPageLoad();

            }

        }
 
      

        public async Task<bool> IsAccountIconVisible()

        {

            return await AccountIcon.IsVisibleAsync();

        }
 
        public async Task ClickAccountIcon()

        {

            if (await IsAccountIconVisible())

            {

                await AccountIcon.ClickAsync();

                await WaitForPageLoad();

            }

        }
 
       

        public async Task<bool> IsHeaderVisible()

        {

            return await LogoLink.IsVisibleAsync() && 

                   await NavigationMenu.IsVisibleAsync() &&

                   await SearchField.IsVisibleAsync();

        }
 
        

        public async Task OpenMobileMenu()

        {

            var mobileMenuButton = Page.Locator(".header__icon--menu");

            if (await mobileMenuButton.IsVisibleAsync())

            {

                await mobileMenuButton.ClickAsync();

            }

        }

    }

}
 