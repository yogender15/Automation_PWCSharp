namespace HMRC.CTP.Specs.StepDefinitions.Tools.Comparator
{
    using HMRC.CTP.Specs.PageObjects.Tools.Comparator;
    using Microsoft.Playwright;
    using PowerPlaywright.Framework.Pages;

    [Binding]
    public class AddressSearchToolSteps
    {
        private readonly ScenarioContext ctx;

        public AddressSearchToolSteps(ScenarioContext ctx)
        {
            this.ctx = ctx;
        }

        private IModelDrivenAppPage ModelDrivenPage => ctx.Get<IModelDrivenAppPage>(Constants.PowerPlayWright);

        private AddressSearchTool AddressSearchPage => new AddressSearchTool(ModelDrivenPage);

        private class AddressSearchModel
        {
            public string SearchType { get; set; } = string.Empty;
            public string BuildingNameOrNumber { get; set; } = string.Empty;
            public string Street { get; set; } = string.Empty;
            public string TownOrCity { get; set; } = string.Empty;
            public string PostCode { get; set; } = string.Empty;
        }

        [Given(@"I search for an address with the following details")]
        [When(@"I search for an address with the following details")]
        public async Task WhenISearchForAnAddressWithTheFollowingDetails(Table table)
        {
            var model = table.CreateInstance<AddressSearchModel>();

            await AddressSearchPage.SearchAsync(
                model.SearchType,
                model.BuildingNameOrNumber,
                model.Street,
                model.TownOrCity,
                model.PostCode);
        }

        [Given(@"I select a subject hereditament")]
        public async Task ISelectASubjectHereditament()
        {
            await AddressSearchPage.SelectSubjectHereditamentAsync();
        }

        [Given(@"I select a comparables")]
        public async Task ISelectComparables()
        {
            await AddressSearchPage.SelectComparatorsAsync();
        }

        [When(@"I compare the comparable properties")]
        public async Task ICompareComparables()
        {
            await AddressSearchPage.CompareAsync();
        }

        [Then(@"the address search results should be displayed")]
        public async Task ThenTheAddressSearchResultsShouldBeDisplayed()
        {
            var grid = ModelDrivenPage.Page.GetByRole(AriaRole.Grid);
            await Assertions.Expect(grid).ToBeVisibleAsync();
        }

        [Then("I am shown the acceptance Screen after the calculation is complete")]
        public async Task ThenIAmShownTheAcceptanceScreen()
        {
            var loadingScreen = ModelDrivenPage.Page.GetByText("Loading");

            await loadingScreen.WaitForAsync(new()
            {
                State = WaitForSelectorState.Hidden
            });

            var tabCount = this.ModelDrivenPage.Page.Context.Pages.Count();
            Assert.AreEqual(2, tabCount);
        }
    }
}