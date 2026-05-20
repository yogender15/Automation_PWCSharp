using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.Scenarios.Request.Factory
{
    public enum RequestType
    {
        NewPropertyIndividual,
        MaterialIncrease,
        FullDemolition,
        Deletion,
        NewPropertyReEntry,
        ChangeOfBillingAuthorityReference,
        ChangeOfAddress,
        DataEnhancement,
        EffectiveDateChange
    }
}