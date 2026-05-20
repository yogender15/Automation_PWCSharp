namespace HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers
{
    using Bogus;
    using Microsoft.Xrm.Sdk;
    using System;

    public static class RequestFaker
    {
        // Change this to get a different but still repeatable dataset
        private const int Seed = 12345;

        private static readonly Guid CodedReasonId =
            Guid.Parse("c322ad67-412c-ec11-b6e6-002248432293");

        private static readonly Guid CouncilTaxRequestTypeId =
            Guid.Parse("63ea1cf3-cfd8-eb11-bacb-002248419a1d");

        private static readonly Guid DataSourceRoleId =
            Guid.Parse("10db3bf8-f5f7-ee11-a1fe-0022481b5aad");

        public static Faker<Entity> Create()
        {
            // Global deterministic seed
            Randomizer.Seed = new Random(Seed);

            return new Faker<Entity>()
                .CustomInstantiator(f =>
                {
                    var dateReceived = f.Date.Past(2, DateTime.UtcNow);
                    var effectiveDate = f.Date.Between(
                        DateTime.SpecifyKind(new DateTime(1993, 4, 1, 0, 0, 0, DateTimeKind.Utc), DateTimeKind.Utc),
                        dateReceived
                    ).Date;

                    var entity = new Entity("voa_requestlineitem", Guid.NewGuid());

                    entity["voa_codedreasonid"] = new EntityReference(
                        "voa_codedreason",
                        CodedReasonId
                    );

                    entity["voa_requesttypeid"] = new EntityReference(
                        "voa_requesttype",
                        CouncilTaxRequestTypeId
                    );

                    //entity["voa_proposedaddressid"] = new EntityReference( ProposedAddressFaker.Create();

                    entity["voa_origincode"] = new OptionSetValue(589160010);

                    entity["voa_datereceived"] = dateReceived;
                    entity["voa_effectivedate"] = effectiveDate;

                    entity["voa_datasourceroleid"] = new EntityReference(
                        "voa_datasource",
                        DataSourceRoleId
                    );

                    entity["voa_proposedbareferencenumber"] = new Random().NextInt64(111111111, 99999999999).ToString();

                    entity["voa_customer2id"] = new EntityReference("account", Guid.Parse("acd42469-c15d-ec11-8f8f-00224842cba8")); //Birmingham
                    entity["voa_partyrelationshiproleid"] = new EntityReference("voa_partyrelationshiprole", Guid.Parse("2db20153-5367-ed11-9561-002248428304"));
                    entity["voa_planningreferenceno"] = f.Random.Number(10000000, 99999099).ToString();
                    return entity;
                });
        }
    }
}