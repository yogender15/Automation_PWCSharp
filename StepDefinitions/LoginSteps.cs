namespace HMRC.CTP.Specs.StepDefinitions.Login
{
    using HMRC.CTP.Specs.Config;
    using Microsoft.Crm.Sdk.Messages;
    using Microsoft.Playwright;
    using Microsoft.PowerPlatform.Dataverse.Client;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using PowerPlaywright.Api;
    using Reqnroll;
    using Reqnroll.Formatters.PayloadProcessing.Cucumber;
    using System;
    using System.Linq;
    using System.Security.Policy;
    using System.Threading.Tasks;

    /// <summary>
    /// Login Steps for the scenarios.
    /// </summary>
    [Binding]
    public class LoginSteps
    {
        private readonly IPowerPlaywright powerPlayWright;
        private readonly EnvironmentConfig environmentConfig;
        private readonly IBrowserContext ctx;
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenariocontext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginSteps"/> class.
        /// </summary>
        /// <param name="browser">The playwright browser object.</param>
        /// <param name="page">The playwright page object.</param>
        /// <param name="config">The environment config object for the users.</param>
        public LoginSteps(IPowerPlaywright powerPlaywright, EnvironmentConfig config, IBrowserContext ctx, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.powerPlayWright = powerPlaywright;
            environmentConfig = config;
            this.ctx = ctx;
            this.featureContext = featureContext;
            this.scenariocontext = scenarioContext;
        }

        /// <summary>
        /// Steps - To Login into required app.
        /// </summary>
        /// <param name="appName"> App Name.</param>
        /// <param name="userPersona"> User role.</param>
        /// <returns>A Task.</returns>
        [Given(@"I am logged in to the '([^']*)' app as a '([^']*)'")]
        public async Task GivenIAmLoggedInAsA(string appName, string userPersona)
        {
            // Ensure Users is not null before using First
            if (environmentConfig.Users == null)
            {
                throw new ArgumentException("EnvironmentConfig.Users is null.");
            }

            var userConfig = environmentConfig.Users.FirstOrDefault(u => u.User?.Alias == userPersona);
            var user = userConfig?.User;

            if (user == null)
            {
                throw new ArgumentException($"User with alias {userPersona} could not be found");
            }

            string connectionString = $@"AuthType=OAuth;Url={this.environmentConfig.EnvironmentUrl};
            Username={user.Name};Password={user.Password};AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;
            RedirectUri=http://localhost;";

            using (ServiceClient serviceClient = new ServiceClient(connectionString))
            {
                if (!serviceClient.IsReady)
                {
                    Console.WriteLine($"Connection failed: {serviceClient.LastError}");
                    return;
                }

                Console.WriteLine("Connected to Dataverse");

                var whoAmIResponse = (WhoAmIResponse)await serviceClient.ExecuteAsync(new WhoAmIRequest());

                user.SystemUserId = whoAmIResponse.UserId;
                scenariocontext[Constants.CurrentUser] = user;
            }

            var homePage = await this.powerPlayWright.LaunchAppAsync(this.ctx, new Uri(this.environmentConfig.EnvironmentUrl), appName, user.Name, user.Password);
            scenariocontext.Set(homePage, Constants.PowerPlayWright);

            //if (!featureContext.TryGetValue("hasHandledAnnoyingPopup", out bool hasHandled) || !hasHandled)
            //{
            //    var signInButton = homePage.Page.GetByRole(
            //        AriaRole.Button,
            //        new PageGetByRoleOptions { Name = "Sign in" });

            //    try
            //    {
            //        await signInButton.WaitForAsync(new LocatorWaitForOptions
            //        {
            //            State = WaitForSelectorState.Visible,
            //            Timeout = 60_000
            //        });

            //        await signInButton.ClickAsync();
            //        featureContext["hasHandledAnnoyingPopup"] = true;
            //    }
            //    catch (Exception)
            //    {

            //    }
            //}
        }
    }
}