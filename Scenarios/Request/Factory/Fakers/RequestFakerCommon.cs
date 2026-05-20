using Microsoft.Xrm.Sdk;

namespace HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers
{
    using Bogus;
    using HMRC.CTP.EntityModel;
    using Microsoft.Xrm.Sdk;
    using System;

    public static class RequestFakerCommon
    {
        // Change this to get a different but still repeatable dataset
        private const int Seed = 12345;

        private static readonly Guid CouncilTaxRequestTypeId =
            Guid.Parse("63ea1cf3-cfd8-eb11-bacb-002248419a1d");

        private static readonly Guid DataSourceRoleId = DataSourceRoles.BillingAuthority;

        public static Faker<voa_requestlineitem> Create(Guid RequestType)
        {
            // Global deterministic seed
            Randomizer.Seed = new Random(Seed);

            return new Faker<voa_requestlineitem>()
                .CustomInstantiator(f =>
                {
                    var dateReceived = f.Date.Past(2, DateTime.UtcNow);
                    var effectiveDate = f.Date.Between(
                        DateTime.SpecifyKind(new DateTime(1993, 4, 1, 0, 0, 0, DateTimeKind.Utc), DateTimeKind.Utc),
                        dateReceived
                    ).Date;

                    var entity = new voa_requestlineitem();

                    entity.Id = Guid.NewGuid();
                    entity.voa_codedreasonid = new EntityReference("voa_codedreason", RequestType);
                    entity.voa_requesttypeid = new EntityReference("voa_requesttype", CouncilTaxRequestTypeId);
                    entity.voa_DateReceived = dateReceived;
                    entity.voa_EffectiveDate = effectiveDate;
                    entity.voa_DataSourceRoleid = new EntityReference("voa_datasource", DataSourceRoleId);
                    entity.voa_ProposedBAReferenceNumber = new Random().NextInt64(111111111, 99999999999).ToString();

                    //entity["voa_customer2id"] = new EntityReference("account", Guid.Parse("c2132d17-2175-ec11-8941-002248070f74")); //Birmingham =acd42469-c15d-ec11-8f8f-00224842cba8
                    entity.voa_Customer2id = new EntityReference("account", Guid.Parse("c2132d17-2175-ec11-8941-002248070f74")); //Birmingham =acd42469-c15d-ec11-8f8f-00224842cba8

                    entity.voa_origincode = incident_caseorigincode.Integration;
                    entity.voa_integrationdatasourceid = new EntityReference("voa_datasource", IntegrationDataSource.LAPortal);

                    entity.voa_partyrelationshiproleid = new EntityReference("voa_partyrelationshiprole", Guid.Parse("2db20153-5367-ed11-9561-002248428304"));
                    entity.voa_PlanningReferenceNo = f.Random.Number(10000000, 99999099).ToString();
                    entity.voa_BAReportNumber = f.Random.Number(10000000, 99999099).ToString();

                    return entity;
                });
        }
    }
}