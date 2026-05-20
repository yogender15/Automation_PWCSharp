namespace HMRC.CTP.Specs.Extensions
{
    using global::HMRC.CTP.EntityModel;
    using Microsoft.Xrm.Sdk;
    using System.Linq;
    using System.Threading.Tasks;

    namespace HMRC.CTP.Specs.Extensions
    {
        public static class OrganizationContextExtensions
        {
            public static async Task<T> WaitForRecordAsync<T>(
                this OrgContext service,
                Func<IQueryable<T>, IQueryable<T>> predicate,
                TimeSpan timeout,
                TimeSpan pollInterval)
                where T : Entity
            {
                var start = DateTime.UtcNow;

                while (DateTime.UtcNow - start < timeout)
                {
                    var result = predicate(service.CreateQuery<T>())
                                 .FirstOrDefault();

                    if (result != null)
                        return result;

                    await Task.Delay(pollInterval);
                }

                throw new Exception($"Could not find the entity after {timeout}");
            }
        }
    }
}