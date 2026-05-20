using HMRC.CTP.Specs.Extensions;
using Microsoft.Playwright;
using PowerPlaywright.Framework.Pages;

namespace HMRC.CTP.Specs.PageObjects.Tools.Comparator
{
    public class AddressSearchTool
    {
        private readonly IAppPage page;

        public AddressSearchTool(IAppPage page)
        {
            this.page = page;
        }

        private ILocator SearchBy => this.page.Page.Locator("#dropdown48");

        private ILocator BuildingNameOrNumber => this.page.Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Building Name/Number" });

        private ILocator Street => this.page.Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Street" }).First;

        private ILocator TownOrCity => this.page.Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Town/City" });

        private ILocator PostCode => this.page.Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Postcode" });

        private ILocator SearchButton => this.page.Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Search" });

        private ILocator SelectSourceHereditament => this.page.Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Select Source Hereditament" });

        private ILocator LoadToComparator => this.page.Page.GetByLabel("Load to Comparator");

        private ILocator ValidateComparators => this.page.Page.GetByLabel("Validate Comparators");

        private ILocator Grid => this.page.Page.GetByRole(AriaRole.Grid);

        public async Task SearchAsync(string searchType, string buildingNameorNumber, string street, string townOrCity, string postCode)
        {
            await Street.FillIfNotNullAsync(street);
            await PostCode.FillIfNotNullAsync(postCode);
            await TownOrCity.FillIfNotNullAsync(townOrCity);
            await BuildingNameOrNumber.FillIfNotNullAsync(buildingNameorNumber);

            await SearchButton.ClickAsync();
        }

        public async Task SelectSubjectHereditamentAsync()
        {
            await SelectRows(1);
            await SelectSourceHereditament.ClickAsync();
        }

        public async Task SelectRows(int rowCount)
        {
            var rows = Grid.Locator("[data-automationid='DetailsRow']")
                           .Locator("[data-automationid='DetailsRowCheck']");

            for (int i = 0; i < rowCount; i++)
            {
                await rows.Nth(i).ClickAsync();
            }
        }

        public async Task SelectComparatorsAsync()
        {
            await SearchButton.ClickAsync();
            await SelectRows(2);
            await LoadToComparator.ClickAsync();
        }

        public async Task CompareAsync()
        {
            await ValidateComparators.ClickAsync();
        }
    }
}