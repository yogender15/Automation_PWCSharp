using Bogus;
using HMRC.CTP.EntityModel;
using HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers;
using HMRC.CTP.Specs.Scenarios.Request.Factory.Strategies;
using Microsoft.Xrm.Sdk;

namespace HMRC.CTP.Specs.Scenarios.Request.Factory
{
    public class RequestCreationEntity
    {
        public voa_requestlineitem? RequestDetail { get; set; }
        public voa_proposedaddress? ProposedAddress { get; set; }
        public voa_codedreason? CodedReason { get; set; }
        public Contact? CouncilTaxPayer { get; set; }
        public Entity? SubjectHereditament { get; internal set; }
        public bool UseExistingHereditament { get; internal set; }
    }

    public static class RequestFactory
    {
        private static readonly Dictionary<RequestType, IRequestBuilder> Builders =
            new()
            {
                { RequestType.NewPropertyIndividual, new NewPropertyRequest()},
                { RequestType.FullDemolition, new FullDemolitionRequest() },
                { RequestType.Deletion, new DeletionRequest() },
                { RequestType.NewPropertyReEntry, new NewPropertyReEntryRequest() },
                { RequestType.ChangeOfBillingAuthorityReference, new ChangeOfBAReferenceRequest() },
                { RequestType.ChangeOfAddress, new ChangeOfAddressRequest() },
                { RequestType.DataEnhancement, new DataEnhancement()},
                { RequestType.EffectiveDateChange, new EffectiveDateChange()}
            };

        public static RequestCreationEntity GetEntity(RequestType type)
        {
            if (!Builders.TryGetValue(type, out var builder))
                throw new ArgumentException($"No builder registered for {type}");

            return builder.Build();
        }
    }
}