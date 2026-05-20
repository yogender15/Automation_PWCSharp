namespace HMRC.CTP.Specs.Extensions
{
    using PowerPlaywright.Framework.Pages;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace HMRC.CTP.Specs.Extensions
    {
        public static class EntityRecordPageExtensions
        {
            public static async Task VerifyFormAsync(
                this IEntityRecordPage page,
                string form,
                string persona)
            {
                var expectation = FormExpectationMap.Get(form, persona);

                var url = page.Page.Url;
                var urlMismatch = !url.Contains(expectation.LogicalName);

                if (urlMismatch)
                {
                    Assert.Fail(
                        new StringBuilder()
                            .AppendLine("Form verification failed")
                            .AppendLine($"Expected form logical name '{expectation.LogicalName}' not found in URL")
                            .AppendLine($"Form    : {form}")
                            .AppendLine($"Persona : {persona}")
                            .AppendLine($"URL     : {url}")
                            .ToString()
                    );
                }

                var fieldChecks = await Task.WhenAll(
                    expectation.Fields.Select(async f =>
                    {
                        try
                        {
                            return (Field: f, IsMissing: !await page.Form.GetField(f).IsVisibleAsync());
                        }
                        catch
                        {
                            return (Field: f, IsMissing: true);
                        }
                    })
                );

                var missingFields = fieldChecks
                    .Where(x => x.IsMissing)
                    .Select(x => x.Field)
                    .ToList();

                var commands = await page.Form.CommandBar.GetCommandsAsync();

                var missingCommands = expectation.Commands
                    .Where(c => !commands.Contains(c))
                    .ToList();

                Assert.IsTrue(
                    !missingFields.Any() && !missingCommands.Any(),
                    new StringBuilder()
                        .AppendLine("Form verification failed")
                        .AppendLine($"Form    : {form}")
                        .AppendLine($"Persona : {persona}")
                        .AppendLine()
                        .Append(missingFields.Any()
                            ? $"Missing fields:\n{string.Join("\n", missingFields.Select(f => $"  - {f}"))}\n\n"
                            : string.Empty)
                        .Append(missingCommands.Any()
                            ? $"Missing commands:\n{string.Join("\n", missingCommands.Select(c => $"  - {c}"))}\n"
                            : string.Empty)
                        .ToString()
                );
            }
        }
    }
}