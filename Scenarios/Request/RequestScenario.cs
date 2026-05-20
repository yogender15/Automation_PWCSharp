using HMRC.CTP.Specs.Scenarios.Request.EventHandlers;
using ScenarioBuilder.Core;
using ScenarioBuilder.Core.Attributes;
using ScenarioBuilder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.Scenarios.Request
{
    [Scenario]
    public class RequestScenario : Scenario, IScenario
    {
        [ScenarioStep(typeof(CreateRequestEvent))]
        private CreateRequestEvent? CreateRequest { get; init; }
    }
}