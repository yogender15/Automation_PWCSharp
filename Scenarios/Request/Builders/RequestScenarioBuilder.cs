using HMRC.CTP.Specs.Config;

namespace HMRC.CTP.Specs.Scenarios.Request.Builders
{
    using HMRC.CTP.EntityModel;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.PowerPlatform.Dataverse.Client;
    using Microsoft.Xrm.Sdk;
    using ScenarioBuilder.Core;
    using ScenarioBuilder.Core.Interfaces;
    using ScenarioBuilder.Domain.Extensions;

    /// <summary>
    /// Builds a request and its nested children.
    /// </summary>
    public sealed class RequestScenarioBuilder : IScenarioOptionsBuilder
    {
        private ScenarioExecutionOptions ScenarioOptions { get; }
        private readonly Lazy<IServiceProvider> _services;
        private IServiceProvider Services => _services.Value;

        public const string User = nameof(CurrentUser);
        public const string ApplicationUser = nameof(ApplicationUserServiceClient);
        public const string CurrentRequest = nameof(RequestEntity);
        public const string ProposedAddressEntity = nameof(ProposedAddress);
        public const string CouncilTaxPayerEntity = nameof(CouncilTaxPayer);
        public const string SubjectHereditamentEntity = nameof(SubjectHereditament);
        public const string UseRandomActiveHereditament = nameof(UseExistingHereditament);

        IServiceProvider IScenarioOptionsBuilder.Services => Services;

        public ScenarioExecutionOptions Options => ScenarioOptions;

        public RequestScenarioBuilder()
        {
            ScenarioOptions = new ScenarioExecutionOptions();

            _services = new Lazy<IServiceProvider>(() =>
            {
                var services = new ServiceCollection();

                // Core services
                services.AddScoped<ScenarioContext>();

                services.AddAllScenarioEvents(typeof(RequestScenario).Assembly);
                // Events

                return services.BuildServiceProvider();
            });
        }

        public User? CurrentUser { get; private set; }

        public ServiceClient? ApplicationUserServiceClient { get; private set; }

        public voa_requestlineitem? RequestEntity { get; private set; }

        public voa_proposedaddress? ProposedAddress { get; private set; }

        public Contact? CouncilTaxPayer { get; private set; }

        public Entity? SubjectHereditament { get; private set; }

        public bool UseExistingHereditament { get; private set; }

        /// <summary>
        /// Supplies user and credentials for dataverse creation.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="serviceClient"></param>
        /// <returns></returns>
        internal RequestScenarioBuilder WithCredentials(User u, ServiceClient serviceClient)
        {
            this.CurrentUser = u;
            this.ApplicationUserServiceClient = serviceClient;
            return this;
        }

        /// <summary>
        /// Sets the request entity for the scenario builder.
        /// </summary>
        /// <param name="request">The entity to associate with the request.</param>
        /// <returns>The current instance for method chaining.</returns>
        internal RequestScenarioBuilder WithEntity(voa_requestlineitem? request)
        {
            RequestEntity = request;
            return this;
        }

        /// <summary>
        /// Sets the request attribute. Useful when you want to  invalidate a record or override a single field.
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal RequestScenarioBuilder WithEntityValue(string attribute, object value)
        {
            if (RequestEntity is null)
            {
                throw new InvalidOperationException("Request Entity must be initialized before setting attributes.");
            }

            RequestEntity[attribute] = value;
            return this;
        }

        /// <summary>
        /// Sets the proposed Address for the Request if desired.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        internal RequestScenarioBuilder WithProposedAddress(voa_proposedaddress? address)
        {
            ProposedAddress = address;
            return this;
        }

        /// <summary>
        /// Sets the Council Tax Payer for the request.
        /// </summary>
        /// <param name="ctPayer"></param>
        /// <returns></returns>
        internal RequestScenarioBuilder WithCouncilTaxPayer(Contact? ctPayer)
        {
            CouncilTaxPayer = ctPayer;
            return this;
        }

        /// <summary>
        /// Sets the Subject Hereditament for a request.
        /// </summary>
        /// <param name="sourceHereditament"></param>
        /// <returns></returns>
        internal RequestScenarioBuilder WithHereditament(Entity? sourceHereditament)
        {
            SubjectHereditament = sourceHereditament;
            return this;
        }

        /// <summary>
        /// Denotes whether to use a random active hereditament.
        /// </summary>
        /// <param name="useExisting"></param>
        /// <returns></returns>
        internal RequestScenarioBuilder WithExistingActiveHereditament(bool useExisting)
        {
            UseExistingHereditament = useExisting;
            return this;
        }
    }
}