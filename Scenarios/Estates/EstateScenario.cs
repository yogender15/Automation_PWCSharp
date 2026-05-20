using HMRC.CTP.Specs.Scenarios.Estates.EventHandlers;
using ScenarioBuilder.Core;
using ScenarioBuilder.Core.Attributes;
using ScenarioBuilder.Core.Interfaces;

namespace HMRC.CTP.Specs.Scenarios.Estates
{
    [Scenario]
    internal class EstateScenario : Scenario, IScenario
    {
        [ScenarioStep(typeof(CreateEstateEvent))]
        private CreateEstateEvent? CreateEstateEvent { get; init; }

        [ScenarioStep(typeof(CreateEstatePlotsEvent))]
        private CreateEstatePlotsEvent? CreateEstatePlotsEvent { get; init; }

        [ScenarioStep(typeof(CreateEstateHouseTypesEvent))]
        private CreateEstateHouseTypesEvent? CreateEstateHouseTypesEvent { get; init; }
    }
}