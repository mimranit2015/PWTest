using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightSnipeIT.Pages
{
    public class AssetDetailsPage
    {
        private readonly IPage _page;

        public AssetDetailsPage(IPage page) => _page = page;

        private ILocator AssetName => _page.Locator("h1.page-header");
        private ILocator HistoryTab => _page.Locator("a[href*='#history']");
        private ILocator HistoryEntries => _page.Locator("#history_tab tbody tr");

        public async Task<string> GetAssetNameAsync() =>
            await AssetName.InnerTextAsync();

        public async Task OpenHistoryTabAsync() =>
            await HistoryTab.ClickAsync();

        public async Task<int> GetHistoryCountAsync() =>
            await HistoryEntries.CountAsync();
    }
}
