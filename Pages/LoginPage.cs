using Microsoft.Playwright;
using System.Threading.Tasks;
using PlaywrightSnipeIT.Helpers;

namespace PlaywrightSnipeIT.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page) => _page = page;

        private ILocator EmailInput => _page.Locator("#username");
        private ILocator PasswordInput => _page.Locator("#password");
        private ILocator LoginButton => _page.Locator("button[type='submit']");

        public async Task NavigateAsync() => await _page.GotoAsync(UrlHelper.LoginUrl);

        public async Task LoginAsync(string username, string password)
        {
            await EmailInput.FillAsync(username);
            await PasswordInput.FillAsync(password);
            await LoginButton.ClickAsync();
            //await _page.WaitForURLAsync("**/dashboard");
        }
    }
}
