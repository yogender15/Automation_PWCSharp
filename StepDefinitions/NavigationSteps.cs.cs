namespace HMRC.CTP.Specs.StepDefinitions
{
    using Microsoft.Playwright;
    using PowerPlaywright.Framework.Pages;

    [Binding]
    public class NavigationSteps
    {
        private readonly ScenarioContext context;

        public NavigationSteps(ScenarioContext context)
        {
            this.context = context;
        }

        [Given("I Open the comparator Tool")]
        public async Task GivenIOpenTheComparatorTool()
        {
            var pp = context.Get<IModelDrivenAppPage>(Constants.PowerPlayWright);

            await pp.SiteMap.OpenPageAsync<IModelDrivenAppPage>("Navigation", "Property", "Hereditaments");

            var ComparatorTab = pp.Page.GetByRole(AriaRole.Tab, new PageGetByRoleOptions { Name = "Comparator Tool" });
            await ComparatorTab.ClickAsync();
        }
    }
}