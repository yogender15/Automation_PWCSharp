namespace HMRC.CTP.Specs.StepDefinitions.Login
{
    using HMRC.CTP.Specs.Extensions.HMRC.CTP.Specs.Extensions;
    using PowerPlaywright.Framework.Pages;
    using Reqnroll;
    using System.Threading.Tasks;

    /// <summary>
    /// Login Steps for the scenarios.
    /// </summary>
    [Binding]
    public class GenericFormSteps
    {
        private readonly ScenarioContext ctx;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginSteps"/> class.
        /// </summary>
        /// <param name="browser">The playwright browser object.</param>
        /// <param name="page">The playwright page object.</param>
        /// <param name="config">The environment config object for the users.</param>
        public GenericFormSteps(ScenarioContext scenarioContext)
        {
            this.ctx = scenarioContext;
        }

        /// <summary>
        /// Opens the request to verifty creation.
        /// </summary>
        /// <returns></returns>
        [Then(@"the '([^']*)' form displays the correct fields and commands for a '([^']*)'")]
        public async Task Then_The_Request_Is_CreatedSuccessfully(string form, string persona)
        {
            var pp = ctx.Get<IEntityRecordPage>(Constants.PowerPlayWright);
            await pp.VerifyFormAsync(form, persona);
        }
    }
}