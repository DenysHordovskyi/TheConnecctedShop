using Microsoft.Playwright;
 
namespace TheConnectedShop.Pages

{

    public class ContactPage : BasePage

    {
        private ILocator ContactUsHeading => Page.GetByText("Contact Us").First;
        private ILocator NameField => Page.GetByLabel("NAME");
        private ILocator EmailField => Page.GetByLabel("EMAIL").First;
        private ILocator PhoneField => Page.GetByLabel("PHONE NUMBER");
        private ILocator CommentField => Page.GetByLabel("COMMENT");
        private ILocator SendButton => Page.Locator("button[type='submit'].button:has(span:text('Send'))").First;        
        private ILocator ContactLink => Page.Locator("a.header__menu-item[href='/pages/contact-us']");
        public ContactPage(IPage page) : base(page) { }
 
        // Validation methods
        public async Task ClickContactButton()
        {
            await ContactLink.ClickAsync();
        }        

        public async Task<bool> IsContactUsHeadingVisible()

        {
            return await ContactUsHeading.IsVisibleAsync();
        }
 
        public async Task<bool> AreAllFormFieldsVisible()

        {
            var nameVisible = await NameField.IsVisibleAsync();
            var emailVisible = await EmailField.IsVisibleAsync();
            var phoneVisible = await PhoneField.IsVisibleAsync();
            var commentVisible = await CommentField.IsVisibleAsync();
            return nameVisible && emailVisible && phoneVisible && commentVisible;
        }
 
        public async Task<bool> IsSendButtonVisible()

        {
            return await SendButton.IsVisibleAsync();
        }
 
        public async Task<bool> IsSendButtonEnabled() //????????? ???????????. ????? ??????, ??????, ?? ????????????

        {
            return await SendButton.IsEnabledAsync();
        }
 
        public async Task HoverSendButton() // ?????? ????????? ???????

        {
            await SendButton.HoverAsync();
        }      
        public async Task ExpectedSendButtonBackgroundColor(string expectedRgb)
        {
            await Assertions.Expect(SendButton).ToHaveCSSAsync("background-color", expectedRgb);
        }

        public async Task FillContactForm(string name, string email, string phone, string comment)

        {
            await NameField.FillAsync(name);
            await EmailField.FillAsync(email);
            await PhoneField.FillAsync(phone);
            await CommentField.FillAsync(comment);
        }
 
        public async Task ClickSendButton()

        {
            await SendButton.ClickAsync();
        }
 
        public async Task<string> GetContactUsHeadingText()

        {
            return await ContactUsHeading.TextContentAsync() ?? string.Empty;
        }
    }
}
 