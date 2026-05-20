namespace HMRC.CTP.Specs.StepDefinitions
{
    using System.Threading.Tasks;
    using HMRC.CTP.Specs.Extensions;
    using HMRC.CTP.Specs.Scenarios.Request;
    using HMRC.CTP.Specs.Scenarios.Request.Builders;
    using HMRC.CTP.Specs.Scenarios.Request.Factory;
    using Microsoft.Playwright;
    using Microsoft.PowerPlatform.Dataverse.Client;
    using Microsoft.Xrm.Sdk;
    using PowerPlaywright.Framework.Controls.Pcf;
    using PowerPlaywright.Framework.Extensions;
    using PowerPlaywright.Framework.Pages;
    using Reqnroll;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginSteps"/> class.
    /// </summary>
    /// <param name="browser">The playwright browser object.</param>
    /// <param name="page">The playwright page object.</param>
    /// <param name="config">The environment config object for the users.</param>
    [Binding]
    public class RequestSteps(ScenarioContext scenarioContext, ServiceClient serviceClientFactory)
    {
        /// <summary>
        /// Steps - To Login into required app.
        /// </summary>
        /// <param name="appName"> App Name.</param>
        /// <param name="userRole"> User role.</param>
        /// <returns>A Task.</returns>
        [Given(@"I manually create a valid '([^']*)' request")]
        public async Task I_Create_A_Valid_Request_Of_Type(RequestType requestType)
        {
            var user = scenarioContext.Get<Config.User>(Constants.CurrentUser);
            var scenario = new RequestScenario();

            var request = RequestFactory.GetEntity(requestType);

            var res = await scenario.ExecuteAsync<RequestScenarioBuilder>
                            (x => x.WithCredentials(user, serviceClientFactory)
                                   .WithEntity(request.RequestDetail)
                                   .WithProposedAddress(request.ProposedAddress)
                                   .WithCouncilTaxPayer(request.CouncilTaxPayer)
                                   .WithHereditament(request.SubjectHereditament)
                                   .WithExistingActiveHereditament(request.UseExistingHereditament));

            scenarioContext.Set(res, Constants.CurrentScenario);
        }

        /// <summary>
        /// Opens the request to verifty creation.
        /// </summary>
        /// <returns></returns>
        [Then(@"the request is created successfully")]
        public void Then_The_Request_Is_CreatedSuccessfully()
        {
            var scenario = scenarioContext.Get<RequestScenario>(Constants.CurrentScenario);
            var createdRequest = scenario.GetContext().Get<Entity>(RequestScenarioBuilder.CurrentRequest);

            var pp = scenarioContext.Get<IEntityRecordPage>(Constants.PowerPlayWright);
            Assert.Contains(createdRequest.LogicalName, pp.Page.Url, "The created request record is not displayed.");
        }

        /// <summary>
        /// Opens the request to verifty creation.
        /// </summary>
        /// <returns></returns>
        [Then(@"the request with a primary job is created successfully")]
        public async Task Then_The_Request_Is_CreatedSuccessfully_With_A_Job()
        {
            var pp = scenarioContext.Get<IEntityRecordPage>(Constants.PowerPlayWright);
            await pp.Form.OpenTabAsync("Related Jobs");

            var subGrid = pp.Form.GetDataSet("RelatedJobs").GetControl<IPowerAppsOneGrid>();

            var count = await subGrid.GetTotalRowCountAsync();

            Assert.AreEqual(1, count);
        }

        /// <summary>
        /// Opens the request and validates it.
        /// </summary>
        /// <returns></returns>
        [When(@"the request is opened")]
        public async Task When_The_Request_Is_Opened()
        {
            var scenario = scenarioContext.Get<RequestScenario>(Constants.CurrentScenario);
            var createdRequest = scenario.GetContext().Get<Entity>(RequestScenarioBuilder.CurrentRequest);

            var pp = scenarioContext.Get<IModelDrivenAppPage>(Constants.PowerPlayWright);
            var entityPage = await pp.ClientApi.NavigateToRecordAsync(createdRequest.LogicalName, createdRequest.Id);
            scenarioContext.Set(entityPage, Constants.PowerPlayWright);
        }

        /// <summary>
        /// Saves and Closes the popup Dialog
        /// </summary>
        /// <returns></returns>
        [StepDefinition(@"I Save and Close the request validation Dialog")]
        public async Task When_Save_And_Close()
        {
            var pp = scenarioContext.Get<IEntityRecordPage>(Constants.PowerPlayWright);
            await pp.Form.CommandBar.ClickCommandAsync("Save & Close");
        }

        /// <summary>
        /// Opens the request and validates it.
        /// </summary>
        /// <returns></returns>
        [StepDefinition(@"the request is validated")]
        public async Task The_Request_Is_Validated()
        {
            var scenario = scenarioContext.Get<RequestScenario>(Constants.CurrentScenario);
            var createdRequest = scenario.GetContext().Get<Entity>(RequestScenarioBuilder.CurrentRequest);

            var pp = scenarioContext.Get<IModelDrivenAppPage>(Constants.PowerPlayWright);

            var entityPage = await pp.ClientApi.NavigateToRecordAsync(createdRequest.LogicalName, createdRequest.Id);

            await entityPage.Form.CommandBar.ClickCommandAsync("Save");

            await entityPage.Form.CommandBar.ClickCommandAsync("Request Action", "Validate Request");

            var errors = await entityPage.Form.GetFormNotificationsAsync();

            if (errors.Any())
            {
                var hasError = errors.Any(n => n.Level == PowerPlaywright.Framework.Model.FormNotificationLevel.Error);
                Assert.IsFalse(hasError, "Validation failed with error notifications.");
            }

            if (await entityPage.ConfirmDialog.IsVisibleAsync())
            {
                await entityPage.ConfirmDialog.ConfirmAsync();
            }

            await entityPage.Page.WaitForAppIdleAsync();

            scenarioContext.Set(entityPage, Constants.PowerPlayWright);
        }

        [Then(@"the request is in status of '([^']*)'")]
        public async Task Then_The_Request_Is_In_Status_Of(int statusCodeExpected)
        {
            var scenario = scenarioContext.Get<RequestScenario>(Constants.CurrentScenario);
            var createdRequest = scenario.GetContext().Get<Entity>(RequestScenarioBuilder.CurrentRequest);

            var predicates = new Dictionary<string, Func<object, bool>>
            {
                ["statuscode"] = statusCode => statusCode is int i && i == statusCodeExpected
            };

            await serviceClientFactory.WaitForFieldsAsync(
               createdRequest.LogicalName,
               createdRequest.Id,
               predicates,
               timeout: TimeSpan.FromSeconds(180),
               pollInterval: TimeSpan.FromSeconds(3)
           );
        }
        
        [When(@"I release and publish the request")]
        public async Task When_I_Release_And_Publish_The_Request()
        {
            var scenario = scenarioContext.Get<RequestScenario>(Constants.CurrentScenario);
            var createdRequest = scenario.GetContext().Get<Entity>(RequestScenarioBuilder.CurrentRequest);
            var pp = scenarioContext.Get<IEntityRecordPage>(Constants.PowerPlayWright);

            await pp.ClientApi.NavigateToRecordAsync(createdRequest.LogicalName, createdRequest.Id);

            await pp.Form.CommandBar.ClickCommandAsync("Request Action", "Release and Publish");
            await Task.Delay(5000);
       if (await pp.ConfirmDialog.IsVisibleAsync())
            {
                await pp.ConfirmDialog.ConfirmAsync();
                await Task.Delay(10000);
            }
        }

        [Given(@"I initiate a new BA Reference form for autoprocessing")]
        public async Task IOpenTheBAReferenceFromTheRequestForAuroProcessing()
        {
            await createBAReferenceForm("Yes");
            //var pp = scenarioContext.Get<IEntityRecordPage>(Constants.PowerPlayWright);

            //await pp.Form.OpenTabAsync("BA Reference");

            //var subGrid = pp.Form.GetDataSet("BA Reference").GetControl<IPowerAppsOneGrid>();

            //await Task.Delay(5000); //Delay to allow subgrid to load, can be removed when
            //await pp.Page.GetByLabel("New Proposed BA Reference Amendment").ClickAsync();
            //await Task.Delay(3000);
            //await pp.Form.CommandBar.ClickCommandAsync("Save & Close");

            //var drc = pp.Form.GetField<IOptionSetControl>("voa_isvalidateforautoprocess");

            //await drc.Control.SetValueAsync("Yes");
            //await Task.Delay(1000);
            //await pp.Form.CommandBar.ClickCommandAsync("Save");
            //await Task.Delay(1000);
            //await pp.Page.WaitForAppIdleAsync();

            //scenarioContext.Set(pp, Constants.PowerPlayWright);
        }

        [Given(@"I initiate a new BA Reference form from the request form")]
        public async Task IOpenTheBAReferenceFromTheRequest()
        {
            await createBAReferenceForm("No");
            //var pp = scenarioContext.Get<IEntityRecordPage>(Constants.PowerPlayWright);

            //await pp.Form.OpenTabAsync("BA Reference");

            //var subGrid = pp.Form.GetDataSet("BA Reference").GetControl<IPowerAppsOneGrid>();

            //await Task.Delay(5000); //Delay to allow subgrid to load, can be removed when
            //await pp.Page.GetByLabel("New Proposed BA Reference Amendment").ClickAsync();
            //await Task.Delay(3000);
            //await pp.Form.CommandBar.ClickCommandAsync("Save & Close");

            //var drc = pp.Form.GetField<IOptionSetControl>("voa_isvalidateforautoprocess");

            //await drc.Control.SetValueAsync("Yes");
            //await Task.Delay(1000);
            //await pp.Form.CommandBar.ClickCommandAsync("Save");
            //await Task.Delay(1000);
            //await pp.Page.WaitForAppIdleAsync();

            //scenarioContext.Set(pp, Constants.PowerPlayWright);
        }

        public async Task createBAReferenceForm(String autoProcess)
        {
            var pp = scenarioContext.Get<IEntityRecordPage>(Constants.PowerPlayWright);

            await pp.Form.OpenTabAsync("BA Reference");

            var subGrid = pp.Form.GetDataSet("BA Reference").GetControl<IPowerAppsOneGrid>();

            await Task.Delay(5000); //Delay to allow subgrid to load, can be removed when
            await pp.Page.GetByLabel("New Proposed BA Reference Amendment").ClickAsync();
            await Task.Delay(3000);
            await pp.Form.CommandBar.ClickCommandAsync("Save & Close");

            if (autoProcess.Equals("Yes")) { 

                 var drc = pp.Form.GetField<IOptionSetControl>("voa_isvalidateforautoprocess");
                 await drc.Control.SetValueAsync(autoProcess);
                 await Task.Delay(1000);
            }

            await pp.Form.CommandBar.ClickCommandAsync("Save");
            await Task.Delay(1000);
            await pp.Page.WaitForAppIdleAsync();

            scenarioContext.Set(pp, Constants.PowerPlayWright);
        }
    }
}