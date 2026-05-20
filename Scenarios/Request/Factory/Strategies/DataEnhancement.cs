namespace HMRC.CTP.Specs.Scenarios.Request.Factory.Strategies
{
    using HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers;

    internal class DataEnhancement : IRequestBuilder
    {
        public RequestCreationEntity Build()
        {
            var faked = RequestFakerCommon.Create(CodedReasons.DataEnhancement).Generate();
            //New Entry Checks
            faked["voa_remarks"] = "Remarks - Data Enhancement";

            faked["voa_targetdate"] = DateTime.Now.AddDays(7);
            return new RequestCreationEntity { RequestDetail = faked, UseExistingHereditament = true };
        }
    }
}