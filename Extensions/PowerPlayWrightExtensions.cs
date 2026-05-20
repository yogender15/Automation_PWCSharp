using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.Extensions
{
    using System;
    using System.Runtime.CompilerServices;
    using Microsoft.Playwright;
    using PowerPlaywright.Framework.Extensions;

    // Extension class
    public static class PageExtensions
    {
        /// <summary>
        /// Retrieves the current entity's logical name and record Id from the page.
        /// </summary>
        /// <param name="page">The current page.</param>
        /// <returns>A tuple of (EntityLogicalName, EntityId).</returns>
        public static async Task<(string EntityLogicalName, Guid EntityId)> GetCurrentEntityAsync(this IPage page)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));

            try
            {
                await page.WaitForAppIdleAsync();

                var entityTypeName = await page.EvaluateAsync<string>("() => Xrm?.Page?.data?.entity?.getEntityName()");
                var entityIdString = await page.EvaluateAsync<string>("() => Xrm?.Page?.data?.entity?.getId()");

                if (!string.IsNullOrWhiteSpace(entityTypeName) && !string.IsNullOrWhiteSpace(entityIdString))
                {
                    // Remove braces from id string if present
                    var id = Guid.Parse(entityIdString.Trim('{', '}'));
                    return (entityTypeName, id);
                }
            }
            catch
            {
                // Ignore JS errors and fallback to URL parsing
            }

            // Method 2: Parse from URL
            var url = page.Url;
            var uri = new Uri(url);
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

            // Try standard CRM query params: id=<guid>, etc=<typecode>
            var idParam = query["id"] ?? query["recordId"];
            var entityParam = query["etc"] ?? query["entityName"];

            if (!string.IsNullOrEmpty(idParam) && !string.IsNullOrEmpty(entityParam))
            {
                var entityId = Guid.Parse(idParam.Trim('{', '}'));
                return (entityParam, entityId);
            }

            throw new InvalidOperationException("Unable to determine current entity name and ID from the page.");
        }

        /// <summary>
        /// Fills the input text if value set;
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task FillIfNotNullAsync(this ILocator locator, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                await locator.FillAsync(value);
            }
        }
    }
}