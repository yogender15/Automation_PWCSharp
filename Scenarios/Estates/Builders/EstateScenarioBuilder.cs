using HMRC.CTP.Specs.Config;
using HMRC.CTP.Specs.Scenarios.Estates.EventHandlers;
using HMRC.CTP.Specs.Scenarios.Request;
using HMRC.CTP.Specs.Scenarios.Request.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using ScenarioBuilder.Core;
using ScenarioBuilder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.Scenarios.Estates.Builders
{
    public class EstateScenarioBuilder : IScenarioOptionsBuilder
    {
        private ScenarioExecutionOptions ScenarioOptions { get; }
        private readonly Lazy<IServiceProvider> _services;
        private IServiceProvider Services => _services.Value;

        public ScenarioExecutionOptions Options => ScenarioOptions;

        public EstateScenarioBuilder()
        {
            ScenarioOptions = new ScenarioExecutionOptions();

            _services = new Lazy<IServiceProvider>(() =>
            {
                var services = new ServiceCollection();

                // Core services
                services.AddScoped<ScenarioContext>();

                return services.BuildServiceProvider();
            });
        }

        public User? CurrentUser { get; private set; }

        public ServiceClient? ApplicationUserServiceClient { get; private set; }

        public EstateScenarioBuilder WithCredentials(User u, ServiceClient serviceClient)
        {
            this.CurrentUser = u;
            this.ApplicationUserServiceClient = serviceClient;
            return this;
        }

        internal EstateScenarioBuilder WithOnlyEstateFileDetail()
        {
            ScenarioOptions.StopBefore<CreateEstatePlotsEvent>();
            return this;
        }

        IServiceProvider IScenarioOptionsBuilder.Services => Services;
    }
}