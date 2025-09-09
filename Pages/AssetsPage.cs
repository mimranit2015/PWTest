using Microsoft.Playwright;
using System.Threading.Tasks;
using PlaywrightSnipeIT.Helpers;

namespace PlaywrightSnipeIT.Pages
{
    public class AssetsPage
    {
        private readonly IPage _page;

        public AssetsPage(IPage page) => _page = page;

        private ILocator CreateAssetButton => _page.Locator("a[href*='/hardware/create']");
        private ILocator SearchBox => _page.Locator("input[type='search']");

        public async Task NavigateAsync() => await _page.GotoAsync(UrlHelper.AssetsUrl);

        //public async Task GoToCreateAssetAsync() => await CreateAssetButton.ClickAsync();

        public async Task GoToCreateAssetAsync()
        {
            //await _page.GetByText("Create New").ClickAsync();
            //await _page.GetByRole(AriaRole.Navigation).GetByText("Asset", new() { Exact = true }).ClickAsync();

            //await _page.ClickAsync("ul.dropdown-menu >> text=Asset");

        }


        public async Task SearchAssetAsync(string assetTag)
        {
            await SearchBox.FillAsync(assetTag);
            await _page.Keyboard.PressAsync("Enter");
        }

        public ILocator AssetInList(string assetTag) =>
            _page.Locator($"table tbody tr td a:has-text('{assetTag}')");
    }
}
