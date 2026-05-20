using Bogus;
using HMRC.CTP.EntityModel;
using Microsoft.Xrm.Sdk;

namespace HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers
{
    public class ProposedAddressFaker
    {
        protected ProposedAddressFaker()
        {
        }

        public static voa_proposedaddress Create(DateTime effectiveFrom)
        {
            var address = AddressGenerator.GenerateAddress();
            var faker = new Faker("en_GB");

            var entity = new voa_proposedaddress
            {
                Id = Guid.NewGuid(),
                voa_name = "Proposed Address Description created by automation",
                statuscode = voa_proposedaddress_statuscode.Draft
            };

            entity["voa_addressstring"] = $"{address.Street}, {address.Locality}, {address.Town}, {address.Postcode}";

            entity.voa_effectivefromdate = effectiveFrom;
            entity.voa_addresslabelpostcode = address.Postcode;
            entity.voa_addresslabeltown = address.Town;
            entity.voa_addresslabellocality = address.Locality;
            entity.voa_addresslabelstreet = address.Street;
            entity.voa_addresslabelbuildingnumber = new Random().Next(1, 600).ToString();
            entity.voa_addresslabelsourceid = new EntityReference("voa_ssulabelsource", Guid.Parse("286ab967-bcb2-ed11-83ff-0022484283ba"));
            entity.voa_voa_addresslabel_buildingname = faker.Random.Bool() ? faker.Company.CompanyName() : "STREET RECORD";
            entity.voa_addresslabeluprn = faker.Random.Long(10_000_000, 999_999_999_999).ToString();

            return entity;
        }
    }
}