using HMRC.CTP.Specs.Utilities;
using PowerPlaywright.Framework.Controls.Pcf.Classes;
using PowerPlaywright.Framework.Extensions;
using PowerPlaywright.Framework.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.PageObjects.Job
{
    public abstract class EntityBaseForm
    {
        private readonly IEntityRecordPage entityRecordPage;

        protected EntityBaseForm(IEntityRecordPage page)
        {
            this.entityRecordPage = page;
        }

        public async Task MoveToNextStageAsync()
        {
            await EntitytHelpers.MoveToNextStageAsync(entityRecordPage.Page);
            await entityRecordPage.Page.WaitForAppIdleAsync();
        }

        public async Task SetLookupAsync(string fieldName, string value)
        {
            var field = entityRecordPage.Form.GetField<ILookup>(fieldName);
            await field.Control.SetValueAsync(value);
        }

        public async Task SetWholeNumberAsync(string fieldName, int value)
        {
            var field = entityRecordPage.Form.GetField<IWholeNumber>(fieldName);
            await field.Control.SetValueAsync(value);
        }

        public async Task OpenTabAsync(string name)
        {
            await entityRecordPage.Form.OpenTabAsync(name);
        }
    }
}