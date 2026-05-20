using HMRC.CTP.EntityModel;
using HMRC.CTP.Specs.Extensions;
using HMRC.CTP.Specs.PageObjects.Job.Banding;
using HMRC.CTP.Specs.PageObjects.Job.Details;
using HMRC.CTP.Specs.PageObjects.Job.Research;
using HMRC.CTP.Specs.Scenarios.Request;
using HMRC.CTP.Specs.Scenarios.Request.Builders;
using HMRC.CTP.Specs.Utilities;
using Microsoft.Playwright;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using PowerPlaywright.Framework.Controls.Pcf;
using PowerPlaywright.Framework.Extensions;
using PowerPlaywright.Framework.Pages;

namespace HMRC.CTP.Specs.StepDefinitions
{
    [Binding]
    public class JobSteps
    {
        private readonly ServiceClient serviceClient;
        private readonly ScenarioContext ctx;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginSteps"/> class.
        /// </summary>
        /// <param name="browser">The playwright browser object.</param>
        /// <param name="page">The playwright page object.</param>
        /// <param name="config">The environment config object for the users.</param>
        public JobSteps(ScenarioContext scenarioContext, ServiceClient serviceClientFactory)
        {
            this.serviceClient = serviceClientFactory;
            this.ctx = scenarioContext;
        }

        /// <summary>
        /// Picks the current job.
        /// </summary>
        /// <returns></returns>
        [Given(@"I open the job from the request form")]
        public async Task IOpenTheJobFromTheRequest()
        {
            var pp = ctx.Get<IEntityRecordPage>(Constants.PowerPlayWright);

            await pp.Form.OpenTabAsync("Related Jobs");
            var subGrid = pp.Form.GetDataSet("RelatedJobs").GetControl<IPowerAppsOneGrid>();

            await Task.Delay(5000); //Delay to allow subgrid to load, can be removed when

            pp = await subGrid.OpenRecordAsync(0);

            if (await pp.ConfirmDialog.IsVisibleAsync())
            {
                await pp.ConfirmDialog.CancelAsync();
            }

            await pp.Page.WaitForAppIdleAsync();

            ctx.Set(pp, Constants.PowerPlayWright);
        }

        /// <summary>
        /// Picks the current job.
        /// </summary>
        /// <returns></returns>
        [Given(@"I pick the job")]
        public async Task IPickTheJob()
        {
            var pp = ctx.Get<IEntityRecordPage>(Constants.PowerPlayWright);
            var user = this.ctx.Get<Config.User>(Constants.CurrentUser);
            var scenario = ctx.Get<RequestScenario>(Constants.CurrentScenario);
            var createdRequest = scenario.GetContext().Get<Entity>(RequestScenarioBuilder.CurrentRequest);

            var job = EntitytHelpers.GetJobForRequest(serviceClient, createdRequest.Id);

            job = await this.WaitForAutoAllocation(new EntityReference(job.LogicalName, job.Id));

            job.OwnerId = new EntityReference("systemuser", user.SystemUserId);
            await serviceClient.UpdateAsync(job);

            await pp.Page.ReloadAsync();
            await pp.Page.WaitForAppIdleAsync();
        }

        [Then("the job is assigned to national team of the billing authority")]
        public async Task ThenTheJobIsAssignedToTheNationalTeam()
        {
            var scenario = ctx.Get<RequestScenario>(Constants.CurrentScenario);
            var createdRequest = scenario.GetContext().Get<Entity>(RequestScenarioBuilder.CurrentRequest);

            var incident = EntitytHelpers.GetJobForRequest(serviceClient, createdRequest.Id);

            incident = await this.WaitForAutoAllocation(new EntityReference(incident.LogicalName, incident.Id));

            Assert.Contains("NATIONAL TEAM", incident.OwnerIdName);
        }

        [Then("the job is not assigned to the creator of the request")]
        public async Task ThenTheJobIsNotAssignedToTheCreatorOfTheRequest()
        {
            var scenario = ctx.Get<RequestScenario>(Constants.CurrentScenario);
            var createdRequest = scenario.GetContext().Get<Entity>(RequestScenarioBuilder.CurrentRequest);

            var incident = EntitytHelpers.GetJobForRequest(serviceClient, createdRequest.Id);

            await this.WaitForAutoAllocation(new EntityReference(incident.LogicalName, incident.Id));
        }

        [Then("The primary job is assigned to me")]
        public void ThenTheJobIsAssignedToMe()
        {
            var scenario = ctx.Get<RequestScenario>(Constants.CurrentScenario);
            var createdRequest = scenario.GetContext().Get<Entity>(RequestScenarioBuilder.CurrentRequest);
            var user = this.ctx.Get<Config.User>(Constants.CurrentUser);

            var incident = EntitytHelpers.GetJobForRequest(serviceClient, createdRequest.Id);

            Assert.AreEqual(user.SystemUserId, incident.OwnerId.Id);
        }

        [Then("The following errors should be presented from the pre-release validation")]
        public async Task ThenTheJobIsAssignedToMeAsync(Table errors)
        {
            var pp = ctx.Get<IEntityRecordPage>(Constants.PowerPlayWright);

            if (await pp.AlertDialog.IsVisibleAsync())
            {
                var actualError = await pp.AlertDialog.GetTextAsync();
                foreach (var row in errors.Rows)
                {
                    var expectedError = row["ErrorMessage"];
                    Assert.Contains(expectedError, actualError, $"Expected error message to contain: {expectedError}, but got: {actualError}");
                }
                await pp.AlertDialog.ConfirmAsync();
            }
            else
            {
                Assert.Fail("Expected an error dialog to be displayed, but it was not.");
            }
        }

        /// <summary>
        /// Waits for change of owner.
        /// </summary>
        /// <param name="jobRef"></param>
        /// <returns></returns>
        private async Task<Incident> WaitForAutoAllocation(EntityReference jobRef)
        {
            var predicates = new Dictionary<string, Func<object, bool>>
            {
                [Incident.Fields.OwnerId] = value => value is EntityReference i && i.Id != serviceClient.GetMyUserId()
            };

            var incident = await serviceClient.WaitForFieldsAsync(
               jobRef.LogicalName,
               jobRef.Id,
               predicates,
               timeout: TimeSpan.FromSeconds(120),
               pollInterval: TimeSpan.FromSeconds(5)
           );

            return (Incident)incident;
        }
    }
}