using Challonge.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Challonge.JsonConverters
{
    internal class ScoresJsonConverter : JsonConverter<IEnumerable<Score>>
    {
        public override IEnumerable<Score> ReadJson(JsonReader reader, Type objectType, [AllowNull] IEnumerable<Score> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            IEnumerable<string[]> scoreSplits = reader.Value?.ToString().Split(",")
                .Select(s => s.Trim().Split("-"));

            if (scoreSplits == null || scoreSplits.Any(a => a.Any(s => !int.TryParse(s.Trim(), out _))))
            {
                return new List<Score>();
            }
            return scoreSplits.Select(a => new Score(int.Parse(a[0].Trim()), int.Parse(a[1].Trim())));
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] IEnumerable<Score> value, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(string.Join(",", value.Select(s => $"{s.PlayerOneScore}-{s.PlayerTwoScore}")));
            }
        }
    }
}
