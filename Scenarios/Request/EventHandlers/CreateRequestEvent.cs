using HMRC.CTP.Specs.Scenarios.Request.Builders;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using ScenarioBuilder.Core;
using ScenarioBuilder.Core.Interfaces;
using HMRC.CTP.Specs.Extensions;

namespace HMRC.CTP.Specs.Scenarios.Request.EventHandlers
{
    public class CreateRequestEvent : IScenarioEvent
    {
        public CreateRequestEvent()
        {
        }

        public async Task ExecuteAsync(ScenarioBuilderContext context, CancellationToken ct = default)
        {
            var serviceClient = context.Get<ServiceClient>(RequestScenarioBuilder.ApplicationUser);
            var requestEntity = context.Get<Entity>(RequestScenarioBuilder.CurrentRequest);

            //Proposed Address
            var proposedAddress = context.GetOrDefault<Entity>(RequestScenarioBuilder.ProposedAddressEntity);
            if (proposedAddress != null)
            {
                await serviceClient.CreateAndReturnAsync(proposedAddress, ct);
                requestEntity["voa_proposedaddressid"] = proposedAddress.ToEntityReference();
            }

            //CT Payer
            var ctPayer = context.GetOrDefault<Entity>(RequestScenarioBuilder.CouncilTaxPayerEntity);
            if (ctPayer != null)
            {
                await serviceClient.CreateAndReturnAsync(ctPayer, ct);
                requestEntity["voa_ctpayer"] = ctPayer.ToEntityReference();
                requestEntity["voa_customer2ididtype"] = 2; //Contact
                requestEntity["voa_partyrelationshiproleid"] = new EntityReference("voa_partyrelationshiprole", Guid.Parse("FAA50B88-388A-EF11-8A69-6045BD0D285D")); //CT payer
            }

            //Existing SSU
            var useExisting = context.Get<bool>(RequestScenarioBuilder.UseRandomActiveHereditament);

            if (useExisting)
            {
                var randomHereditament = await serviceClient.GetRandomActiveSsu();

                requestEntity["voa_statutoryspatialunitid"] = randomHereditament.ToEntityReference();
                requestEntity["voa_effectivedate"] = DateTime.Now;
            }

            await serviceClient.CreateAndReturnAsync(requestEntity, ct);
        }
    }
}