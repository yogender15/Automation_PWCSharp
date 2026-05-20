namespace HMRC.CTP.Specs.Scenarios.Request.Factory.Strategies
{
    using HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers;

    internal class NewPropertyRequest : IRequestBuilder
    {
        public RequestCreationEntity Build()
        {
            var faked = RequestFakerCommon.Create(CodedReasons.NewPropertyIndividual).Generate();
            var address = ProposedAddressFaker.Create((DateTime)faked["voa_effectivedate"]);

            faked["voa_proposedaddressid"] = address.ToEntityReference();
            return new RequestCreationEntity { ProposedAddress = address, RequestDetail = faked };
        }
    }
}