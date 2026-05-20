using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]

namespace HMRC.CTP.Specs.Hooks
{
    using HMRC.CTP.EntityModel;
    using HMRC.CTP.Specs.Config;
    using Microsoft.Playwright;
    using Microsoft.PowerPlatform.Dataverse.Client;
    using Microsoft.Xrm.Sdk;
    using PowerPlaywright.Api;
    using PowerPlaywright.Framework.Pages;
    using Reqnroll;
    using Reqnroll.BoDi;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    /// <summary>
    /// Test Hooks that fire.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="TestHooks"/> class.
    /// </remarks>
    /// <param name="container">The Object container injected.</param>
    [Binding]
    public sealed class TestHooks(IObjectContainer container)
    {
        private const string noGuiTag = "noGUI";

        /// <summary>
        /// Initiates a browser before a scenario.
        /// </summary>
        /// <returns>An Async Task.</returns>
        [BeforeScenario]
        public async Task BeforeScenario(ScenarioContext ctx)
        {
            var binPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ?? throw new ArgumentException("Could not get executable Path to read the configuration file");

            var filePath = Path.Combine(binPath, "ReqnrollSettings.yaml");

            var yaml = await File.ReadAllTextAsync(filePath);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var config = deserializer.Deserialize<EnvironmentConfig>(yaml);

            container.RegisterInstanceAs(config);

            var serviceClientFactory = new ServiceClient($"AuthType=ClientSecret;ClientId={config.ClientId};ClientSecret={config.ClientSecret};Url={config.EnvironmentUrl};");


            var orgContext = new OrgContext(serviceClientFactory);

            container.RegisterInstanceAs(orgContext);
            container.RegisterInstanceAs(serviceClientFactory);

            if (!ctx.ScenarioInfo.Tags.Contains(noGuiTag))
            {
                var powerPlaywright = await PowerPlaywright.CreateAsync();

                var playWright = await Playwright.CreateAsync();
                var browser = await playWright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
                var context = await browser.NewContextAsync();

                container.RegisterInstanceAs(context);
                container.RegisterInstanceAs(powerPlaywright);
            }
        }

        /// <summary>
        /// Runs after a Scenario.
        /// </summary>
        /// <param name="ctx">Scenario Context.</param>
        /// <returns>An async Task.</returns>
        [AfterScenario]
        public async Task AfterWebTestScenario(ScenarioContext ctx)
        {
            if (!ctx.ScenarioInfo.Tags.Contains(noGuiTag) && ctx.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
            {
                var _testContext = container.Resolve<TestContext>();

                if (string.IsNullOrEmpty(_testContext.TestResultsDirectory))
                {
                    return;
                }

                var page = (IModelDrivenAppPage)ctx[Constants.PowerPlayWright];

                var safeTitle = string.Join("_",
                    ctx.ScenarioInfo.Title.Split(Path.GetInvalidFileNameChars()));

                var fileName = $"{safeTitle}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

                var filePath = Path.Combine(_testContext.TestResultsDirectory, fileName);

                await page.Page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = filePath,
                    FullPage = true
                });

                _testContext.AddResultFile(filePath);
            }
            var context = container.Resolve<IBrowserContext>();
            await context.DisposeAsync();
        }
    }
}