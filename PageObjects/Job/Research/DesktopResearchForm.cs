namespace HMRC.CTP.Specs.PageObjects.Job.Research
{
    using Microsoft.Playwright;
    using PowerPlaywright.Framework.Controls.Pcf;
    using PowerPlaywright.Framework.Controls.Pcf.Classes;
    using PowerPlaywright.Framework.Extensions;
    using PowerPlaywright.Framework.Pages;
    using System;
    using System.Security.Policy;
    using System.Threading.Tasks;

    public class DesktopResearchForm : EntityBaseForm
    {
        private readonly IEntityRecordPage entityRecordPage;

        public DesktopResearchForm(IEntityRecordPage entityPage, Microsoft.Xrm.Sdk.Entity codedReason) : base(entityPage)
        {
            entityRecordPage = entityPage;
        }

        #region Locators

        private ILocator CreateButton =>
            entityRecordPage.Page
                .Locator("div.gridContainer.gridItem.gridItemFirstPad")
                .Locator("button[title='Create']");

        private ILocator AddNewSourceCodeButton =>
            entityRecordPage.Page.GetByLabel("Add New Source Code");

        private ILocator ValidatePadCodeToggle =>
            entityRecordPage.Page.GetByLabel("Validate PAD Code: No");

        #endregion Locators

        #region Navigation

        public async Task OpenPvtTabAsync()
            => await OpenTabAsync("PVT");

        public async Task OpenCheckFactsTabAsync()
            => await OpenTabAsync("Check Facts");

        #endregion Navigation

        #region Actions

        public async Task ClickCreatePADsAsync()
        {
            await OpenPvtTabAsync();
            await CreateButton.ClickAsync();
            //await entityRecordPage.ConfirmDialog.CancelAsync();
        }

        public async Task FillPropertyAttributesAsync()
        {
            var random = new Random();

            await SetLookupAsync("voa_group",
                "35 - Individually designed large dwellings built after 1945");

            await SetLookupAsync("voa_type",
                "HS - Semi-detached house");

            await SetLookupAsync("voa_agecode",
                DateTime.Now.Year.ToString());

            await SetWholeNumberAsync("voa_rooms", random.Next(8, 12));
            await SetWholeNumberAsync("voa_bedrooms", random.Next(3, 6));
            await SetWholeNumberAsync("voa_bathrooms", random.Next(2, 3));
            await SetWholeNumberAsync("voa_floors", random.Next(2, 3));

            await SetLookupAsync("voa_parkingcode",
                "G1 - Garaging (within unit of assessment) for 1 car");

            await SetLookupAsync("voa_conservatorytype",
                "N - No Conservatory Exists");

            await SetWholeNumberAsync("voa_plotsize", random.Next(5, 10));
            await SetWholeNumberAsync("voa_areaofdwelling", random.Next(120, 200));
        }

        public async Task AddSourceCodeAsync()
        {
            await AddNewSourceCodeButton.ClickAsync();
            await entityRecordPage.ConfirmDialog.ConfirmAsync();

            await SetLookupAsync("voa_sourcecodeid",
                "A1 - Agents – data obtained from Rightmove");

            await entityRecordPage.Form.CommandBar.ClickCommandAsync("Save & Close");
        }

        public async Task ValidatePadCodeAsync()
        {
            await ValidatePadCodeToggle.ClickAsync();
        }

        public async Task CompleteDesktopResearchAsync()
        {
            var coPilotAssistant = entityRecordPage.Page.GetByRole(AriaRole.Button, new() { Name = "Clear all suggestions" });
            if (await coPilotAssistant.IsVisibleAsync())
            {
                await coPilotAssistant.ClickAsync();
            }

            await OpenCheckFactsTabAsync();

            var drc = entityRecordPage.Form.GetField<IOptionSetControl>("voa_isdesktopresearchcomplete");
            await drc.Control.SetValueAsync("Yes");

            var decisionNotes = entityRecordPage.Form.GetField<IMultiLineText>("voa_formaldecisionnotes");
            if (await decisionNotes.IsVisibleAsync())
            {
                await decisionNotes.Control.SetValueAsync("Automated test - entered formal decision notes.");
            }

            //Handles DR if we are at the last stage or just confirming.
            if (await entityRecordPage.ConfirmDialog.IsVisibleAsync())
            {
                await entityRecordPage.ConfirmDialog.ConfirmAsync();
                return; //End of Journey
            }
            else
            {
                await entityRecordPage.Form.CommandBar.ClickCommandAsync("Save");
            }

            await this.MoveToNextStageAsync();

            await Task.Delay(10000); //Wait for Banding Record Creation

            await entityRecordPage.Page.ReloadAsync();
            await entityRecordPage.Page.WaitForAppIdleAsync();
        }

        internal async Task EnterQuestionnaireAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["Question"];
                var answer = row["Answer"];

                await entityRecordPage.Page.GetByLabel(question)
                    .SelectOptionAsync(new[] { answer });
            }

            var decisionNotes = entityRecordPage.Form.GetField<IMultiLineText>("voa_formaldecisionnotes");

            if (await decisionNotes.IsVisibleAsync())
            {
                await decisionNotes.Control.SetValueAsync("Automated test - entered questionnaire answers.");
            }
        }

        public async Task ClickCreatePADsAsyncToClone()
        {
            await OpenPvtTabAsync();
       //     await CreateButton.ClickAsync();
            await ClickCloneToCommitted();
            //await entityRecordPage.ConfirmDialog.CancelAsync();
        }

        public async Task ClickCloneToCommitted()
        {
            await entityRecordPage.Page.GetByRole(AriaRole.Button, new() { Name = "History" }).ClickAsync();
            await entityRecordPage.Page.GetByRole(AriaRole.Radio).First.ClickAsync();
            
            await entityRecordPage.Page.Locator("button[title='Use the selected line as a template to enter PADs for a new Effective Date.']").ClickAsync();
            await entityRecordPage.Page.Locator("button[title='Choose this path if you want the resulting cloned PAD line to become Committed']").ClickAsync();

            await entityRecordPage.Page.GetByRole(AriaRole.Combobox).ClickAsync();
            var dayOfMonth = DateTime.Now.Day.ToString();
            await entityRecordPage.Page.GetByRole(AriaRole.Gridcell).Locator("[aria-current='date']").ClickAsync();
           //await entityRecordPage.Page.GetByRole(AriaRole.Button, new() { Name = dayOfMonth, Exact = true}).ClickAsync();
            await entityRecordPage.Page.GetByRole(AriaRole.Button, new() { Name = "Continue" }).ClickAsync();
        }

        #endregion Actions
    }
}