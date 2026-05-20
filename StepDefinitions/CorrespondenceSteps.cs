using HMRC.CTP.EntityModel;
using HMRC.CTP.Specs.Extensions.HMRC.CTP.Specs.Extensions;
using HMRC.CTP.Specs.Scenarios.Request;
using HMRC.CTP.Specs.Scenarios.Request.Builders;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Reqnroll;
using Reqnroll.Assist;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.StepDefinitions
{
    [Binding]
    public class CorrespondenceSteps
    {
        private ScenarioContext scenarioContext;
        private OrgContext orgContext;

        public CorrespondenceSteps(ScenarioContext context, OrgContext ctx)
        {
            this.scenarioContext = context;
            this.orgContext = ctx;
        }

        [Then("the correspondence is sent with the template {string} in the status of {string}")]
        public async Task ThenTheCorrespondenceIsSentWithTheTemplateInTheStatusOf(string templateName, string statusReason)
        {
            var scenario = scenarioContext.Get<RequestScenario>(Constants.CurrentScenario);
            var createdRequest = scenario.GetContext().Get<Entity>(RequestScenarioBuilder.CurrentRequest);

            var outboundCorrespondence =
                await orgContext.WaitForRecordAsync<voa_OBCSendCorrespondence>(
                    query => query.Where(x =>
                        x.voa_requestid.Id == createdRequest.Id),
                    timeout: TimeSpan.FromMinutes(1),
                    pollInterval: TimeSpan.FromSeconds(5));

            Assert.IsTrue(outboundCorrespondence.voa_TemplateGroupName == templateName && outboundCorrespondence.statuscodeName == statusReason);

        }
    }
}
