namespace HMRC.CTP.Specs
{
    /// <summary>
    /// Constants for the Specification Tests.
    /// </summary>
    public static class Constants
    {
        public const string CurrentScenario = "CurrentScenario";
        public const string PowerPlayWright = "PowerPlayWright";
        public const string CurrentUser = "CurrentUser";
    }

    /// <summary>
    /// Stores all Job Type Coded reasons.
    /// </summary>
    public static class CodedReasons
    {
        public static readonly Guid ChangeOfBAReference = new("911f39b4-317f-ee11-8179-6045bd0c1c1b");
        public static readonly Guid MaterialIncrease = new("f435f15b-3337-ec11-8c64-000d3a0af4cd");
        public static readonly Guid NewPropertyReEntry = new("4b70bade-3e48-ef11-a316-000d3a0ce7f2");
        public static readonly Guid DataEnhancement = new("30787a01-4259-ee11-be6f-000d3a86c49a");
        public static readonly Guid BorderlineCtToNdr = new("3ad18dfd-5f5e-ec11-8f8f-000d3ad65ca9");
        public static readonly Guid DeprecatedChangeOfUse = new("a520715c-605e-ec11-8f8f-000d3ad65ca9");
        public static readonly Guid Cr04ChangeToDomesticUseInclusionInCtList = new("f80966c9-7f5e-ec11-8f8f-000d3ad65ca9");
        public static readonly Guid PartialDemolition = new("ac4ad7e9-1300-ec11-94ef-000d3ad67dc2");
        public static readonly Guid SchemeReviews = new("8ea5907e-e6bc-ed11-83fe-00224801d8f0");
        public static readonly Guid Consequentials = new("57247b91-e6bc-ed11-83fe-00224801d8f0");
        public static readonly Guid MineralsReview = new("80accd9d-e6bc-ed11-83fe-00224801d8f0");
        public static readonly Guid Cr03New = new("c30e7460-075b-ef11-bfe3-0022481a3da4");
        public static readonly Guid Cr12MajorAddressChange = new("f0c10a74-075b-ef11-bfe3-0022481a3da4");
        public static readonly Guid ReconstitutionDelete = new("f7602dfb-67e7-ee11-904c-0022481b5aad");
        public static readonly Guid ReconstitutionNew = new("0d9930c8-68e7-ee11-904c-0022481b5aad");
        public static readonly Guid TradeAccountSubmissions = new("947b5a2a-7980-ec11-8d21-0022483f6a81");
        public static readonly Guid DeprecatedMaterialChangeOfCircumstance = new("e2a9358b-bea6-ec11-9840-00224840ac31");
        public static readonly Guid CompiledList = new("5276eea0-bea6-ec11-9840-00224840ac31");
        public static readonly Guid PlantAndMachinery = new("a253a8af-bea6-ec11-9840-00224840ac31");
        public static readonly Guid UpdateList = new("268dc1bb-bea6-ec11-9840-00224840ac31");
        public static readonly Guid CourtCase = new("1386e1ca-bea6-ec11-9840-00224840ac31");
        public static readonly Guid EffectiveDate = new("bbced9d0-bea6-ec11-9840-00224840ac31");
        public static readonly Guid NewEntry = new("844fbbdd-bea6-ec11-9840-00224840ac31");
        public static readonly Guid WrongDescription = new("e3c7e3f3-bea6-ec11-9840-00224840ac31");
        public static readonly Guid PendingUpdate = new("a9ad8910-c0a6-ec11-9840-00224840ac31");
        public static readonly Guid New = new("f1c53549-d0d8-eb11-bacb-002248419a1d");
        public static readonly Guid NewFormerlyDomestic = new("42545655-d0d8-eb11-bacb-002248419a1d");
        public static readonly Guid NewFormerlyExempt = new("f6aef25c-d0d8-eb11-bacb-002248419a1d");
        public static readonly Guid NewOther = new("d1560e69-d0d8-eb11-bacb-002248419a1d");
        public static readonly Guid DeletedDemolished = new("16653375-d0d8-eb11-bacb-002248419a1d");
        public static readonly Guid DeletedCeasedToBeRateable = new("3f46f881-d0d8-eb11-bacb-002248419a1d");
        public static readonly Guid DeletedExempt = new("1d77078a-d0d8-eb11-bacb-002248419a1d");
        public static readonly Guid DeletedOther = new("98610990-d0d8-eb11-bacb-002248419a1d");
        public static readonly Guid ImprovementsAlterations = new("e90d069c-d0d8-eb11-bacb-002248419a1d");
        public static readonly Guid Reconstitution = new("fded07a2-d0d8-eb11-bacb-002248419a1d");
        public static readonly Guid OtherReason = new("79c409a8-d0d8-eb11-bacb-002248419a1d");
        public static readonly Guid AddressRefNumberChangesOnly = new("359401b4-d0d8-eb11-bacb-002248419a1d");
        public static readonly Guid FullDemolition = new("0440cd9f-3876-ed11-81ac-002248428304");
        public static readonly Guid Corrections = new("14d10b46-ed76-ed11-81ac-002248428304");
        public static readonly Guid FullExemption = new("b7f17c24-0d77-ed11-81ac-002248428304");
        public static readonly Guid Extension = new("5cea0559-d177-ed11-81ac-002248428304");
        public static readonly Guid BorderlinePartPropertyMovingCtNdr = new("3b717bd7-d177-ed11-81ac-002248428304");
        public static readonly Guid ChangeInTone = new("b6c8e599-c82a-ed11-9db2-002248428304");
        public static readonly Guid ChangeInValuationApproach = new("6d6b32b2-c82a-ed11-9db2-002248428304");
        public static readonly Guid Appeal = new("8caa7145-ce91-ed11-aad1-002248428304");
        public static readonly Guid EffectiveDateChange = new("f22adfe6-6a93-ed11-aad1-002248428304");
        public static readonly Guid ConsequentialBandChange = new("715126b6-9018-ed11-b83f-002248428304");
        public static readonly Guid ConsequentialBandReview = new("11265cca-9018-ed11-b83f-002248428304");
        public static readonly Guid CustomerSubmissionCosting = new("990a4efa-244e-ed11-bba3-002248428304");
        public static readonly Guid Other = new("10f0888a-cb34-ed11-9db2-0022484283ba");
        public static readonly Guid Proposal = new("b676852a-0690-ed11-aad1-0022484283ba");
        public static readonly Guid UndergoingSignificantWorks = new("72e0a4ea-b54f-ed11-bba3-0022484283ba");
        public static readonly Guid DeprecatedPropertyUndergoingWorks = new("b322ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid BeyondEconomicRepair = new("b522ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid Deletion = new("bd22ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid Cr02ChangeFromDomesticUse = new("c122ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid NewPropertyIndividual = new("c322ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid BorderlineNdrToCt = new("c522ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid Cr05ReconstitutedProperty = new("c722ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid CompositePropertyChange = new("cd22ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid MaterialReduction = new("cf22ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid Cr09ReferenceOrEffectiveDateChangeOnly = new("d122ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid Cr10ImprovementsOrAlterations = new("d522ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid ChangeOfAddress = new("d722ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid Cr14MinorAddressChange = new("d922ad67-412c-ec11-b6e6-002248432293");
        public static readonly Guid ProposalOrAppealAlteration = new("df1dac73-472a-ef11-8409-002248c5d712");
    }

    public static class DataSourceRoles
    {
        public static readonly Guid BillingAuthority = new("41A9B8F4-F5F7-EE11-A1FE-6045BD0E5C5F");
        public static readonly Guid ListingOfficer = new("10DB3BF8-F5F7-EE11-A1FE-0022481B5AAD");
    }

    public static class IntegrationDataSource
    {
        public static readonly Guid BARs = new("E4389720-D6E8-EC11-BB3C-6045BD0EA21F");
        public static readonly Guid LAPortal = new("edd0f130-24cc-ed11-a7c6-6045bd0d6729");
    }
}