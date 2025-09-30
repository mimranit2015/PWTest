using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Playwright;
using System.Threading.Tasks;
using PlaywrightSnipeIT.Helpers;
using PlaywrightSnipeIT.Pages;

namespace PlaywrightSnipeIT.Tests
{
    [TestClass]
    public class AssetTests
    {
        private IBrowser _browser;
        private IPage _page;

        [TestInitialize]
        public async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var context = await _browser.NewContextAsync();
            _page = await context.NewPageAsync();
        }

        [TestCleanup]
        public async Task Teardown() => await _browser.CloseAsync();

        [TestMethod]
        public async Task CreateAndValidateAsset()
        {
            //test
            //test1
            string assetTag = RandomDataHelper.RandomAssetTag();
            string assetName = "Macbook Pro 13\"";

            var loginPage = new LoginPage(_page);
            await loginPage.NavigateAsync();
            await loginPage.LoginAsync("admin", "password"); // demo creds

            var dashboard = new DashboardPage(_page);
            await dashboard.NavigateToAssetsAsync();

            var assetsPage = new AssetsPage(_page);
            await assetsPage.GoToCreateAssetAsync();


     
            var serial = "123456";

            var IMEI = "123456789012345";

            var phonenumber = "1234";

            //Fill asset form

            // Click the dropdown to open it
            await _page.Locator("#select2-company_select-container").ClickAsync();

            // Wait for the search input to appear
            var searchInput = _page.Locator("input.select2-search__field");
            await searchInput.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5000 });

            // 3Optional: type empty string or first letter to trigger options
            await searchInput.FillAsync("");

            // Press ArrowDown and Enter to select the first option
            await searchInput.PressAsync("ArrowDown");
            await searchInput.PressAsync("Enter");

            // Get the selected company name from the main container
            var selectedCompany = await _page.Locator("#select2-company_select-container").InnerTextAsync();

        
 
            var assetTaggenerated = await _page.GetAttributeAsync("#asset_tag", "value");


            await _page.GetByText("Select a Model").ClickAsync();
            await _page.GetByRole(AriaRole.Searchbox).FillAsync("Mac");
            await _page.Locator("#select2-model_select_id-results").GetByText("Laptops - Macbook Pro 13\" (#").ClickAsync();

            await _page.GetByLabel("Select Status").GetByText("Select Status").ClickAsync();
            await _page.GetByRole(AriaRole.Option, new() { Name = "Ready to Deploy" }).ClickAsync();
  
          

            // Click the dropdown to open the user list
            var userDropdown = _page.GetByRole(AriaRole.Combobox, new() { Name = "Select a User" }).Locator("b");
            await userDropdown.ClickAsync();

            // Focus the dropdown (or search input if exists)
            var searchInput1 = _page.Locator("body .select2-dropdown.select2-dropdown--below input.select2-search__field");
            if (await searchInput1.CountAsync() > 0)
            {
                await searchInput1.FocusAsync();
            }

            // Press ArrowDown a random number of times (default to 1 if options are not counted)
            var random = new Random();
            int steps = random.Next(1, 5); // press ArrowDown 1–4 times

            for (int i = 0; i < steps; i++)
            {
                if (await searchInput1.CountAsync() > 0)
                    await searchInput1.PressAsync("ArrowDown");
                else
                    await _page.Keyboard.PressAsync("ArrowDown");
            }

            // Press Enter to select the highlighted user
            if (await searchInput1.CountAsync() > 0)
                await searchInput1.PressAsync("Enter");
            else
                await _page.Keyboard.PressAsync("Enter");

            // Read the selected user
            var selectedUser = await _page.Locator("#select2-assigned_user_select-container").InnerTextAsync();
            Console.WriteLine($"Selected user: {selectedUser}");


            await _page.GetByRole(AriaRole.Textbox, new() { Name = "IMEI" }).ClickAsync();
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "IMEI" }).FillAsync(IMEI);
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Phone Number" }).ClickAsync();
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Phone Number" }).FillAsync(phonenumber);
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Test Encrypted" }).ClickAsync();
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Test Encrypted" }).FillAsync("test");
            await _page.GetByRole(AriaRole.Checkbox, new() { Name = "One" }).CheckAsync();
            await _page.GetByRole(AriaRole.Radio, new() { Name = "One" }).CheckAsync();


    
            await _page.Locator("button[name=\"submit\"]").ClickAsync();


            await _page.GetByText("Success:").ClickAsync();
            await _page.GetByText("× Success: Asset with tag").ClickAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Click here to view" }).ClickAsync();

            string assetTagsuccess = await _page.InnerTextAsync("div.row:has(strong:has-text(\"Asset Tag\")) span.js-copy-assettag");

            Assert.AreEqual(assetTaggenerated, assetTagsuccess, "Tag not matching");


            await _page.GetByText("Create New", new() { Exact = true }).ClickAsync();
            await _page.GetByRole(AriaRole.Navigation).GetByText("Asset", new() { Exact = true }).ClickAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Assets" }).ClickAsync();
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Lookup by Asset Tag" }).ClickAsync();
            await _page.GetByRole(AriaRole.Textbox, new() { Name = "Lookup by Asset Tag" }).FillAsync(assetTagsuccess);
            await _page.GetByRole(AriaRole.Button, new() { Name = "Search", Exact = true }).ClickAsync();
            await _page.Locator("#details").GetByText("Model", new() { Exact = true }).ClickAsync();

            // Get the link locator
            var linkLocator = _page.GetByRole(AriaRole.Link, new() { Name = "Macbook Pro 13\"", Exact = true });

            // Hover over the link
            await linkLocator.HoverAsync();

            // Get the text of the link
            var assetnamecreated = await linkLocator.InnerTextAsync();

            Assert.AreEqual(assetName.ToString(),assetnamecreated.ToString(),"Asset name does not match");

            await _page.GetByRole(AriaRole.Link, new() { Name = "History" }).ClickAsync();
      
        }
    }
}
