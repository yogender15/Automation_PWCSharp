using Bogus.DataSets;
using HMRC.CTP.Specs.Utilities;
using PowerPlaywright.Framework.Controls.Pcf;
using PowerPlaywright.Framework.Controls.Pcf.Classes;
using PowerPlaywright.Framework.Extensions;
using PowerPlaywright.Framework.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.PageObjects.Job.Banding
{
    public class BandingForm : EntityBaseForm
    {
        private readonly IEntityRecordPage entityRecordPage;

        public BandingForm(IEntityRecordPage page, Microsoft.Xrm.Sdk.Entity codedReason) : base(page)
        {
            this.entityRecordPage = page;
        }

        internal async Task UndertakeBandingAsync()
        {
            var thoughts = this.entityRecordPage.Page.GetByLabel("Rich Text Editor Control voa_counciltaxbanding voa_caseworkerconclusionsremarksthoughtprocess");
            await thoughts.FillAsync(new Lorem().Lines(2));

            var band = this.entityRecordPage.Form.GetField<ILookup>("voa_counciltaxbandid");
            await band.Control.SetValueAsync("C");

            this.entityRecordPage.Form.GetField<IOptionSetControl>("voa_isbandingcomplete").Control.SetValueAsync("Yes").Wait();

            await Task.Delay(5000);
            await this.entityRecordPage.ConfirmDialog.ConfirmAsync();
            await Task.Delay(10000);

            await this.entityRecordPage.Page.ReloadAsync();

            await EntitytHelpers.MoveToNextStageAsync(this.entityRecordPage.Page);
            await Task.Delay(5000);
        }
    }
}