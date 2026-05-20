namespace HMRC.CTP.Specs.Scenarios.Request.Factory.Strategies
{
    using HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers;
    using Microsoft.Xrm.Sdk;

    internal class ChangeOfBAReferenceRequest : IRequestBuilder
    {
        public RequestCreationEntity Build()
        {
            var faked = RequestFakerCommon.Create(CodedReasons.ChangeOfBAReference).Generate();
            var address = ProposedAddressFaker.Create((DateTime)faked["voa_effectivedate"]);

            faked["voa_proposedaddressid"] = address.ToEntityReference();

            //New Entry Checks
            faked["voa_reasonforvalidation"] = "Automation Test - Change of BA Reference";
            faked["voa_isexistingaddressformatcompliantcode"] = new OptionSetValue(184360000); // Yes
            faked["voa_ishereditamentaddresssameasreportedcode"] = new OptionSetValue(184360000); // Yes

            faked["voa_swapbareferencenumber"] = false;

            return new RequestCreationEntity { ProposedAddress = address, RequestDetail = faked, UseExistingHereditament = true };
        }
    }
}