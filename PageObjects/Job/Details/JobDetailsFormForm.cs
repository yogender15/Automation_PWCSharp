using PowerPlaywright.Framework.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.PageObjects.Job.Details
{
    public class JobDetailsFormForm : EntityBaseForm
    {
        private IEntityRecordPage entityRecordPage;

        public JobDetailsFormForm(IEntityRecordPage entityPage) : base(entityPage)
        {
            entityRecordPage = entityPage;
        }
    }
}