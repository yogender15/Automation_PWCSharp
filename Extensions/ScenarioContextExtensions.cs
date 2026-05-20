using HMRC.CTP.Specs.Config;
using HMRC.CTP.Specs.Scenarios.Request;
using HMRC.CTP.Specs.Scenarios.Request.Builders;
using Microsoft.Xrm.Sdk;

namespace HMRC.CTP.Specs.Extensions
{
    public static class ScenarioContextExtensions
    {
        public static Entity GetRequest(this ScenarioContext ctx)
        {
            var scenario = ctx.Get<RequestScenario>(Constants.CurrentScenario);
            var createdRequest = scenario.GetContext().Get<Entity>(RequestScenarioBuilder.CurrentRequest);
            return createdRequest;
        }

        public static User GetCurrentUser(this ScenarioContext ctx)
        {
            return ctx.Get<User>(Constants.CurrentUser);
        }

        public static T? GetOrDefault<T>(this ScenarioContext context, string key)
        where T : class
        {
            if (context.TryGetValue(key, out T value))
            {
                return value;
            }

            return null;
        }
    }
}