namespace HMRC.CTP.Specs.Scenarios.Request.Factory.Strategies
{
    using HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers;
    using Microsoft.Xrm.Sdk;

    internal class FullDemolitionRequest : IRequestBuilder
    {
        public RequestCreationEntity Build()
        {
            var faked = RequestFakerCommon.Create(CodedReasons.FullDemolition).Generate();

            faked["voa_iscaravanboatannex"] = new OptionSetValue(589160001);

            var contact = ContactFaker.Create().Generate();

            return new RequestCreationEntity { RequestDetail = faked, CouncilTaxPayer = contact, UseExistingHereditament = true };
        }
    }
}