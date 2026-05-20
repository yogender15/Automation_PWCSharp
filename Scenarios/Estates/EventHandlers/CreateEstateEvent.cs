using ScenarioBuilder.Core;
using ScenarioBuilder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.Scenarios.Estates.EventHandlers
{
    internal class CreateEstateEvent : IScenarioEvent
    {
        public Task ExecuteAsync(ScenarioBuilderContext context, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}