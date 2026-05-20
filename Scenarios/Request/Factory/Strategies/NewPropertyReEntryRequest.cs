namespace HMRC.CTP.Specs.Scenarios.Request.Factory.Strategies
{
    using HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers;
    using Microsoft.Xrm.Sdk;

    internal class NewPropertyReEntryRequest : IRequestBuilder
    {
        public RequestCreationEntity Build()
        {
            var faked = RequestFakerCommon.Create(CodedReasons.NewPropertyReEntry).Generate();
            var address = ProposedAddressFaker.Create((DateTime)faked["voa_effectivedate"]);

            faked["voa_propertydeletedbymistakecode"] = new OptionSetValue(184360001);

            faked["voa_proposedaddressid"] = address.ToEntityReference();
            return new RequestCreationEntity { ProposedAddress = address, RequestDetail = faked };
        }
    }
}