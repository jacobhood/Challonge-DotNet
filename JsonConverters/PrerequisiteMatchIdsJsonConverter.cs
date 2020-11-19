using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Challonge.JsonConverters
{
    internal class PrerequisiteMatchIdsJsonConverter : JsonConverter<IEnumerable<long>>
    {
        public override IEnumerable<long> ReadJson(JsonReader reader, Type objectType, [AllowNull] IEnumerable<long> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string[] split = reader.Value?.ToString().Split(",");

            if (split == null || split.Any(s => !long.TryParse(s.Trim(), out _)))
            {
                return new List<long>();
            }

            return split.Select(s => long.Parse(s.Trim()));
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] IEnumerable<long> value, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(string.Join(",", value.Select(l => l.ToString())));
            }
        }
    }
}
