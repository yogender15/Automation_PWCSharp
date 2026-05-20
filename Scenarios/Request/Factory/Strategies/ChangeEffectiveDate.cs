namespace HMRC.CTP.Specs.Scenarios.Request.Factory.Strategies
{
    using HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers;
    using Microsoft.Xrm.Sdk;

    internal class EffectiveDateChange : IRequestBuilder
    {
        public RequestCreationEntity Build()
        {
            var faked = RequestFakerCommon.Create(CodedReasons.EffectiveDateChange).Generate();
            var address = ProposedAddressFaker.Create((DateTime)faked["voa_effectivedate"]);

            faked["voa_proposedaddressid"] = address.ToEntityReference();

            //New Entry Checks
            faked["voa_reasonforvalidation"] = "EffectiveDateChange";
         
            return new RequestCreationEntity { ProposedAddress = address, RequestDetail = faked, UseExistingHereditament = true };
        }
    }
}