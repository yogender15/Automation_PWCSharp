namespace HMRC.CTP.Specs.Scenarios.Request.Factory.Strategies
{
    using HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers;
    using Microsoft.Xrm.Sdk;

    internal class DeletionRequest : IRequestBuilder
    {
        public RequestCreationEntity Build()
        {
            var faked = RequestFakerCommon.Create(CodedReasons.Deletion).Generate();

            var contact = ContactFaker.Create().Generate();

            return new RequestCreationEntity { RequestDetail = faked, CouncilTaxPayer = contact, UseExistingHereditament = true };
        }
    }
}