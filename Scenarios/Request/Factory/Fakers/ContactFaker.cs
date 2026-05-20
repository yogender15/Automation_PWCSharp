using Bogus;
using HMRC.CTP.EntityModel;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.Scenarios.Request.Factory.Fakers
{
    public static class ContactFaker
    {
        public static Faker<Contact> Create()
        {
            return new Faker<Contact>()
                .CustomInstantiator(f =>
                {
                    var entity = new Contact
                    {
                        Id = Guid.NewGuid(),
                        FirstName = f.Name.FirstName(),
                        LastName = f.Name.LastName(),
                        EMailAddress1 = f.Internet.Email(),
                        Telephone1 = f.Phone.PhoneNumber(),
                        JobTitle = f.Name.JobTitle()
                    };

                    return entity;
                });
        }
    }
}