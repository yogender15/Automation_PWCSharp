using HMRC.CTP.Specs.Config;
using HMRC.CTP.Specs.Scenarios.Estates;
using HMRC.CTP.Specs.Scenarios.Estates.Builders;
using HMRC.CTP.Specs.Scenarios.Request;
using HMRC.CTP.Specs.Scenarios.Request.Builders;
using HMRC.CTP.Specs.Scenarios.Request.Factory;
using HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers;
using Microsoft.PowerPlatform.Dataverse.Client;
using Reqnroll;
using System;

namespace HMRC.CTP.Specs.StepDefinitions
{
    [Binding]
    public class EstateSteps
    {
        private readonly ServiceClient serviceClient;
        private readonly ScenarioContext ctx;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginSteps"/> class.
        /// </summary>
        /// <param name="browser">The playwright browser object.</param>
        /// <param name="page">The playwright page object.</param>
        /// <param name="config">The environment config object for the users.</param>
        public EstateSteps(ScenarioContext scenarioContext, ServiceClient serviceClientFactory)
        {
            this.serviceClient = serviceClientFactory;
            this.ctx = scenarioContext;
        }

        [Given("I create an estate file")]
        public async Task GivenICreateAnEstateFile()
        {
            var scenario = new RequestScenario();
            var user = ctx.Get<User>(Constants.CurrentUser);

            var res = await scenario.ExecuteAsync<EstateScenarioBuilder>
                            (x => x.WithCredentials(user, serviceClient)
                            .WithOnlyEstateFileDetail());

            ctx.Set(res, Constants.CurrentScenario);
        }

        [Given("I create a complete estate file")]
        public async Task GivenICreateACompleteEstateFile()
        {
            var scenario = new RequestScenario();
            var user = ctx.Get<User>(Constants.CurrentUser);

            var res = await scenario.ExecuteAsync<EstateScenarioBuilder>
                            (x => x.WithCredentials(user, serviceClient));

            ctx.Set(res, Constants.CurrentScenario);
        }

        [When("I create {string} plots")]
        public void WhenICreatePlots(string p0)
        {
            throw new PendingStepException();
        }

        [Then("I am presented with an error")]
        public void ThenIAmPresentedWithAnError()
        {
            throw new PendingStepException();
        }

        [Then("The plots are created successfully")]
        public void ThenThePlotsAreCreatedSuccessfully()
        {
            throw new PendingStepException();
        }

        [Given("I rename a house type on the estate file")]
        public void GivenIRenameAHouseTypeOnTheEstateFile()
        {
            throw new PendingStepException();
        }

        [Then("The house type is renamed successfully and stored in the audit history")]
        public void ThenTheHouseTypeIsRenamedSuccessfullyAndStoredInTheAuditHistory()
        {
            throw new PendingStepException();
        }

        [Given("I amend a house type on the estate file")]
        public void GivenIAmendAHouseTypeOnTheEstateFile()
        {
            throw new PendingStepException();
        }

        [Then("Consequentials jobs are created successfully and stored in the audit history")]
        public void ThenConsequentialsJobsAreCreatedSuccessfullyAndStoredInTheAuditHistory()
        {
            throw new PendingStepException();
        }
    }
}