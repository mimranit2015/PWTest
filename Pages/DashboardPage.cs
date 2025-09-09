using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightSnipeIT.Pages
{
    public class DashboardPage
    {
        private readonly IPage _page;

        public DashboardPage(IPage page) => _page = page;

        private ILocator AssetsMenu => _page.Locator("a[href*='/hardware']");

       // public async Task NavigateToAssetsAsync() => await AssetsMenu.ClickAsync();

        public async Task NavigateToAssetsAsync()
        {
            await _page.GetByText("Create New").ClickAsync();
            await _page.GetByRole(AriaRole.Navigation).GetByText("Asset", new() { Exact = true }).ClickAsync();

        }
    }
}
