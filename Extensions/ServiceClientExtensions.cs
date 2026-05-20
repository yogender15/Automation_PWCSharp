using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using Microsoft.PowerPlatform.Dataverse.Client;

    public static class ServiceClientExtensions
    {
        /// <summary>
        /// Waits for multiple fields on an entity to satisfy their respective predicates.
        /// </summary>
        /// <param name="service">Dataverse ServiceClient instance</param>
        /// <param name="entityName">Logical name of the entity (e.g., "incident")</param>
        /// <param name="recordId">Record GUID</param>
        /// <param name="fieldPredicates">Dictionary of field name → predicate</param>
        /// <param name="timeout">Maximum wait time (default 30 seconds)</param>
        /// <param name="pollInterval">Polling interval between checks (default 1 second)</param>
        /// <returns>The latest Entity once all predicates are satisfied</returns>
        /// <exception cref="TimeoutException">Thrown if all predicates are not satisfied within the timeout</exception>
        public static async Task<Entity> WaitForFieldsAsync(
            this ServiceClient service,
            string entityName,
            Guid recordId,
            Dictionary<string, Func<object, bool>> fieldPredicates,
            TimeSpan? timeout = null,
            TimeSpan? pollInterval = null)
        {
            ArgumentNullException.ThrowIfNull(service);
            if (fieldPredicates == null || fieldPredicates.Count == 0)
                throw new ArgumentException("At least one field predicate must be provided.", nameof(fieldPredicates));

            timeout ??= TimeSpan.FromSeconds(30);
            pollInterval ??= TimeSpan.FromSeconds(1);

            var deadline = DateTime.UtcNow + timeout.Value;

            // Create a ColumnSet with all the fields to monitor
            var columnSet = new ColumnSet([.. fieldPredicates.Keys]);

            while (DateTime.UtcNow < deadline)
            {
                var entity = await service.RetrieveAsync(entityName, recordId, columnSet);

                bool allMatch = true;

                foreach (var kvp in fieldPredicates)
                {
                    var fieldName = kvp.Key;
                    var predicate = kvp.Value;

                    entity.Attributes.TryGetValue(fieldName, out var currentValue);
                    var valueToCheck = currentValue is OptionSetValue os ? os.Value : currentValue;

                    if (!predicate(valueToCheck))
                    {
                        allMatch = false;
                        break;
                    }
                }

                if (allMatch)
                    return entity;

                await Task.Delay(pollInterval.Value);
            }

            throw new TimeoutException(
            $"Entity '{entityName}' ({recordId}) did not satisfy all field predicates within {timeout.Value.TotalSeconds} seconds."
            );
        }

        /// <summary>
        /// Creates a random SSU on the fly.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static async Task<Entity> GetRandomActiveSsu(this ServiceClient service)
        {
            var automationRequest = new OrganizationRequest("voa_autotestcreatessu");
            var resp = await service.ExecuteAsync(automationRequest);

            var ssu = (Guid)resp["voa_outputssuid"];

            return new Entity("voa_ssu", ssu);
        }
    }
}