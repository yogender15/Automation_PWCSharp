using HMRC.CTP.Specs.Extensions;
using HMRC.CTP.Specs.PageObjects.Job.Banding;
using HMRC.CTP.Specs.PageObjects.Job.Details;
using HMRC.CTP.Specs.PageObjects.Job.Research;
using HMRC.CTP.Specs.Scenarios.Request;
using HMRC.CTP.Specs.Scenarios.Request.Builders;
using HMRC.CTP.Specs.Utilities;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using PowerPlaywright.Framework.Controls.Pcf;
using PowerPlaywright.Framework.Extensions;
using PowerPlaywright.Framework.Pages;
using Reqnroll;

namespace HMRC.CTP.Specs.StepDefinitions
{
    [Binding]
    public class ValuationSteps
    {
        private readonly ServiceClient serviceClient;
        private readonly ScenarioContext ctx;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginSteps"/> class.
        /// </summary>
        /// <param name="browser">The playwright browser object.</param>
        /// <param name="page">The playwright page object.</param>
        /// <param name="config">The environment config object for the users.</param>
        public ValuationSteps(ScenarioContext scenarioContext, ServiceClient serviceClientFactory)
        {
            this.serviceClient = serviceClientFactory;
            this.ctx = scenarioContext;
        }

        [Given("I undertake the valuation exercise")]
        public async Task GivenICompleteDesktopResearchAndUndertakeTheBandingExercise()
        {
            var pp = ctx.Get<IEntityRecordPage>(Constants.PowerPlayWright);
            var scenario = ctx.Get<RequestScenario>(Constants.CurrentScenario);
            var createdRequest = scenario.GetContext().Get<Entity>(RequestScenarioBuilder.CurrentRequest);

            var voa_codedreasonId = createdRequest.GetAttributeValue<EntityReference>("voa_codedreasonid").Id;
            var codedReason = serviceClient.Retrieve("voa_codedreason", voa_codedreasonId, new ColumnSet(true));

            var detailsPage = new JobDetailsFormForm(pp);
            await detailsPage.MoveToNextStageAsync();

            //Research
            var desktopResearchPage = new DesktopResearchForm(pp, codedReason);
            if (codedReason.Id == CodedReasons.NewPropertyIndividual)
            {
                await desktopResearchPage.ClickCreatePADsAsync();
                await desktopResearchPage.FillPropertyAttributesAsync();
                await desktopResearchPage.AddSourceCodeAsync();
                await desktopResearchPage.ValidatePadCodeAsync();
            }
            if (codedReason.Id == CodedReasons.DataEnhancement)
            {
                await desktopResearchPage.ClickCreatePADsAsyncToClone();
                //await desktopResearchPage.FillPropertyAttributesAsync();
                await desktopResearchPage.AddSourceCodeAsync();
                await desktopResearchPage.ValidatePadCodeAsync();
            }

            if (codedReason.TryGetAttributeValue<bool>("voa_requiresdesktopresearch", out bool requiresDesktopResearch) && requiresDesktopResearch)
            {
                await desktopResearchPage.CompleteDesktopResearchAsync();
            }

            //Banding
            var b = new BandingForm(pp, codedReason);
            if (codedReason.TryGetAttributeValue<bool>("voa_requiresbanding", out bool requiresBanding) && requiresBanding)
            {
                await b.UndertakeBandingAsync();
            }

            //Maintain Assessment
            var m = new MaintainAssessmentForm(pp, codedReason);
            if (codedReason.TryGetAttributeValue<bool>("voa_ismaintainassessmentstagerequired", out bool requiresMaintainAssessment) && requiresMaintainAssessment)
            {
                await m.MaintainAssessment(true);
            }
        }
    }
}