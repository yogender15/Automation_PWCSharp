using HMRC.CTP.EntityModel;
using Microsoft.Playwright;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using PowerPlaywright.Api;
using PowerPlaywright.Framework;
using PowerPlaywright.Framework.Extensions;
using PowerPlaywright.Framework.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.Utilities
{
    public static class EntitytHelpers
    {
        public static Incident GetJobForRequest(ServiceClient client, Guid requestId)
        {
            using var context = new OrganizationServiceContext(client);

            var incident = context.CreateQuery<Incident>()
                .FirstOrDefault(i => i.voa_requestlineitemid.Id == requestId);

            return incident ?? throw new InvalidOperationException($"No job found for request with ID {requestId}");
        }

        /// <summary>
        /// Gets All The BPF stages for an entity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task ExitBPFStage(string stage, IEntityRecordPage pp, bool refreshOnChange = false)
        {
            ILocator locator = pp.Page.GetByLabel(stage).First;
            await locator.ClickAsync();

            await pp.Page.WaitForAppIdleAsync();

            ILocator nextStage = pp.Page.GetByLabel("Next Stage").Last;
            await nextStage.ClickAsync();

            if (refreshOnChange)
            {
                await pp.Page.ReloadAsync();
            }

            await Task.Delay(10000);
        }

        public static async Task<bool> MoveToNextStageAsync(IPage page)
        {
            var activeStage = page.Locator("li[data-selected-stage='true']");
            var activeButton = activeStage.Locator("button");

            string? currentStageId = await activeStage.GetAttributeAsync("id")
                ?? throw new InvalidOperationException("Could not find the current stage id attribute.");

            Console.WriteLine($"Current stage: {currentStageId}");

            await activeButton.ClickAsync();

            var flyout = page.Locator("[data-id^='MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutContainer']");

            await flyout.WaitForAsync();

            var nextbutton = flyout.Locator("button:has-text('Next Stage')");

            await nextbutton.ClickAsync();

            await page.WaitForAppIdleAsync();

            Console.WriteLine("Moved to next stage successfully.");
            return true;
        }
    }
}