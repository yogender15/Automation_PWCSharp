namespace HMRC.CTP.Specs.PageObjects.Job.Details
{
    using PowerPlaywright.Framework.Extensions;
    using PowerPlaywright.Framework.Pages;

    /// <summary>
    /// Manages the Maintain Assessment part of the list.
    /// </summary>
    public class MaintainAssessmentForm : EntityBaseForm
    {
        private readonly IEntityRecordPage entityRecordPage;

        public MaintainAssessmentForm(IEntityRecordPage entityPage, Microsoft.Xrm.Sdk.Entity codedReason) : base(entityPage)
        {
            entityRecordPage = entityPage;
        }

        /// <summary>
        /// Marks Assessment as complete.
        /// </summary>
        /// <param name="maintainComplete"></param>
        /// <returns></returns>
        internal async Task MaintainAssessment(bool maintainComplete)
        {
            if (maintainComplete)
            {
                await entityRecordPage.Page.EvaluateAsync(@"() => {
                const attr = Xrm.Page.getAttribute('voa_valuationcomplete');
                    if (attr) {
                        attr.setValue(true);   // Yes
                        attr.fireOnChange();
                    }
                }");
            }

            await entityRecordPage.ConfirmDialog.ConfirmAsync();

            await entityRecordPage.Page.WaitForAppIdleAsync();
        }
    }
}