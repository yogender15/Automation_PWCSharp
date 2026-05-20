namespace HMRC.CTP.Specs.StepDefinitions.Transformations
{
    using HMRC.CTP.Specs.Scenarios.Request.Factory;
    using Reqnroll;

    [Binding]
    public static class StepTransforms
    {
        [StepArgumentTransformation]
        public static RequestType TransformRequestType(string requestType)
        {
            return requestType.Trim().ToLower() switch
            {
                "new property individual" => RequestType.NewPropertyIndividual,
                "material increase" => RequestType.MaterialIncrease,
                "full demolition" => RequestType.FullDemolition,
                "deletion" => RequestType.Deletion,
                "new property re-entry" => RequestType.NewPropertyReEntry,
                "change of ba reference" => RequestType.ChangeOfBillingAuthorityReference,
                "change of address" => RequestType.ChangeOfAddress,
                "data enhancement" => RequestType.DataEnhancement,
                "effective date change" => RequestType.EffectiveDateChange,

                _ => throw new ArgumentException($"Unknown request type: {requestType}")
            };
        }

        [StepArgumentTransformation]
        public static int TransformRequestStatus(string statusCode)
        {
            return statusCode.Trim().ToLower() switch
            {
                "resolved" => 2,
                "active" => 1,
                _ => throw new ArgumentException($"Unknown request status: {statusCode}")
            };
        }
    }
}