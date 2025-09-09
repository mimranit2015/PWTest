using Microsoft.Playwright;

namespace PWTest
{
    [TestClass]
    public class PlaywrightTests
    {
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IBrowserContext? _context;
        private IPage? _page;

        [TestInitialize]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false // must be false to see browser
            });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        [TestMethod]
        public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingToTheIntroPage()
        {
            var page = _page!; // ensure not null

            await page.GotoAsync("https://playwright.dev");

            // Expect a title "to contain" a substring.
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            var title = await page.TitleAsync();
            Assert.IsTrue(title.Contains("Playwright"), "Title does not contain 'Playwright'");

            // create a locator
            var getStarted = page.Locator("text=Get Started");

            // Expect an attribute "to be strictly equal" to the value.
            var href = await getStarted.GetAttributeAsync("href");
            Assert.AreEqual("/docs/intro", href);

            // Click the get started link
            await getStarted.ClickAsync();

            // Expects the URL to contain intro
            Assert.IsTrue(page.Url.Contains("intro"));
        }

        [TestCleanup]
        public async Task TearDown()
        {
            await _browser?.CloseAsync();
            _playwright?.Dispose();
        }
    }
}

