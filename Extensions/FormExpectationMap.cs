using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HMRC.CTP.Specs.Extensions
{
    public sealed class FormExpectationJson
    {
        public string Form { get; init; } = default!;
        public string Persona { get; init; } = default!;
        public string LogicalName { get; init; } = default!;
        public IReadOnlyCollection<string> Fields { get; init; } = [];
        public IReadOnlyCollection<string> Commands { get; init; } = [];
    }

    public record FormExpectation(
         string LogicalName,
         IReadOnlyCollection<string> Fields,
         IReadOnlyCollection<string> Commands
    );

    public static class FormExpectationMap
    {
        private static readonly Dictionary<(string Form, string Persona), FormExpectation> Map
            = Load();

        private static Dictionary<(string, string), FormExpectation> Load()
        {
            var json = File.ReadAllText("personaPermissions.json");

            var items = JsonSerializer.Deserialize<List<FormExpectationJson>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            ) ?? throw new InvalidOperationException("Failed to deserialize form expectations");

            return items.ToDictionary(
                x => (x.Form, x.Persona),
                x => new FormExpectation(
                    LogicalName: x.LogicalName,
                    Fields: x.Fields,
                    Commands: x.Commands
                )
            );
        }

        public static FormExpectation Get(string form, string persona)
        {
            if (!Map.TryGetValue((form, persona), out var expectation))
            {
                throw new ArgumentException(
                    $"No form mapping defined for Form='{form}', Persona='{persona}'"
                );
            }

            return expectation;
        }
    }
}